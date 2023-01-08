using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Order_Aggregate;

namespace Talabat.core.Specification
{
    public class OrderWithItemsAndDeliveryMethodsSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliveryMethodsSpecification(string buyerEmail) : base(o => o.BuyerEmail == buyerEmail)
        {
            this.Includes.Add(o => o.Items);
            this.Includes.Add(o => o.OrderDeliveryMethod);
        }
        public OrderWithItemsAndDeliveryMethodsSpecification(string buyerEmail, int orderId) : base(o => o.BuyerEmail == buyerEmail && o.Id == orderId)
        {
            this.Includes.Add(o => o.Items);
            this.Includes.Add(o => o.OrderDeliveryMethod);
        }
    }
}
