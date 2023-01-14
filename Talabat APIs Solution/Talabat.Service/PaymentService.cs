using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.FinancialConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.core.Repositories;
using Talabat.core.Servicecs;
using Talabat.core.Specification;
using Product = Talabat.core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        public IConfiguration Configuration { get; }
        public IBasketRepository BasketRepository { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IGenericRepository<DeliveryMethod> GenericRepository { get; }

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            Configuration = configuration;
            BasketRepository = basketRepository;
            UnitOfWork = unitOfWork;
        }


        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await this.BasketRepository.GetBasketByIdAsync(basketId);
            if (basket == null)
            {
                return null;
            }
            StripeConfiguration.ApiKey = this.Configuration["Stripe:Secretkey"];
            var DeliveryCost = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await UnitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                DeliveryCost = deliveryMethod.Cost;
                basket.ShippingPrice = DeliveryCost;
            }
            foreach (var item in basket.Items)
            {
                var product = await this.UnitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != product.Price)
                {
                    item.Price = product.Price;
                }
            }
            PaymentIntent intent;
            var service = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(o => o.Quantity * o.Price * 100) + (long)DeliveryCost * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>
                    {
                    "card"
                    }
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(o => o.Quantity * o.Price * 100) + (long)DeliveryCost * 100,
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await this.BasketRepository.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdatePayementIntentSucceededOrFailed(string paymentIntentId, bool isSucceeded, string basketId)
        {
            var specs = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await this.UnitOfWork.Repository<Order>().GetByIdWithSpecsAsync(specs);
            if (isSucceeded)
            {
                order.status = OrderStatus.PaymentRecieved;
                await this.BasketRepository.DeleteBasketsync(basketId);
            }
            else
            {
                order.status = OrderStatus.PaymentFailed;
            }
            this.UnitOfWork.Repository<Order>().Update(order);
            await this.UnitOfWork.Complete();
            return order;
        }
    }
}
