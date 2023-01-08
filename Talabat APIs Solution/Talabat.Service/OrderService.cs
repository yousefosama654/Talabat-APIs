using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.core.Repositories;
using Talabat.core.Servicecs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        public OrderService(IBasketRepository BasketRepo,IUnitOfWork unitOfWork)
        {
            this.BasketRepo = BasketRepo;
            UnitOfWork = unitOfWork;
        }


        public IBasketRepository BasketRepo { get; }
        public IUnitOfWork UnitOfWork { get; }

        public async Task<Order> CreateOrder(string BuyerEmail, string basketId, Address shippingaddress, int deliveryMethodId)
        {
            var basket = await this.BasketRepo.GetBasketByIdAsync(basketId);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = new ProductItemOrdered(item.Id, item.Name, item.PictureUrl);
                var orderitem = new OrderItem(item.Price, item.Quantity, product);
                items.Add(orderitem);
            }
            var subtotal = items.Sum(o => o.Quantity * o.Price);
            var deliverymethod = await this.UnitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            var order = new Order(BuyerEmail, shippingaddress, subtotal, deliverymethod, items);
            await this.UnitOfWork.Repository<Order>().CreateAsync(order);
            var result= await this.UnitOfWork.Complete();
            if (result == 0)
                return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderByIdForUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
