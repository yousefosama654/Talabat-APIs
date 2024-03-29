﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.core.Repositories;
using Talabat.core.Servicecs;
using Talabat.core.Specification;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        public OrderService(IBasketRepository BasketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            this.BasketRepo = BasketRepo;
            UnitOfWork = unitOfWork;
            PaymentService = paymentService;
        }


        public IBasketRepository BasketRepo { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IPaymentService PaymentService { get; }

        public async Task<Order> CreateOrder(string BuyerEmail, string basketId, Address shippingaddress, int deliveryMethodId)
        {
            var basket = await this.BasketRepo.GetBasketByIdAsync(basketId);
            var items = new List<OrderItem>();
            if (basket == null)
            {
                return null;
            }
            foreach (var item in basket.Items)
            {
                var product = new ProductItemOrdered(item.Id, item.Name, item.PictureUrl);
                var orderitem = new OrderItem(item.Price, item.Quantity, product);
                items.Add(orderitem);
            }
            var subtotal = items.Sum(o => o.Quantity * o.Price);
            var deliverymethod = await this.UnitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            var specs = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var existingOrder = await this.UnitOfWork.Repository<Order>().GetByIdWithSpecsAsync(specs);
            if (existingOrder != null)
            {
                this.UnitOfWork.Repository<Order>().Delete(existingOrder);
                await this.PaymentService.CreateOrUpdatePaymentIntent(basketId);
            }
            var order = new Order(BuyerEmail, shippingaddress, subtotal, deliverymethod, items, basket.PaymentIntentId);
            await this.UnitOfWork.Repository<Order>().CreateAsync(order);
            var result = await this.UnitOfWork.Complete();
            if (result == 0)
                return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return (IReadOnlyList<DeliveryMethod>)await this.UnitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdForUserAsync(string BuyerEmail, int OrderId)
        {
            var specs = new OrderWithItemsAndDeliveryMethodsSpecification(BuyerEmail, OrderId);
            return (Order)await this.UnitOfWork.Repository<Order>().GetByIdWithSpecsAsync(specs);

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var specs = new OrderWithItemsAndDeliveryMethodsSpecification(BuyerEmail);
            return (IReadOnlyList<Order>)await this.UnitOfWork.Repository<Order>().GetAllWithSpecsAsync(specs);
        }
    }
}
