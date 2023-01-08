using System.Collections.Generic;
using System;
using Talabat.core.Entities.Order_Aggregate;

namespace Talabat_APIs.Dtos
{
    public class OrderToReturnDto
    {
        public string BuyerEmail { get; set; }
        public string status { get; set; } 
        public Address ShippingAddress { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } 
        public string PaymentIntentId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
