using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }

        public Order(string buyerEmail, Address shippingAddress, decimal subTotal, DeliveryMethod deliveryMethod, ICollection<OrderItem> items)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            SubTotal = subTotal;
            OrderDeliveryMethod = deliveryMethod;
            Items = items;
        }

        public string BuyerEmail { get; set; }
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal SubTotal { get; set; }
        public DeliveryMethod OrderDeliveryMethod { get; set; } //Navigational Property [ONE]
        public ICollection<OrderItem> Items { get; set; } //Navigational Property [Many]
        public string PaymentIntentId { get; set; }
        // To Express the Derived Attribute
        public decimal Total()
        {
            return this.SubTotal + this.OrderDeliveryMethod.Cost;
        }
    }
}
