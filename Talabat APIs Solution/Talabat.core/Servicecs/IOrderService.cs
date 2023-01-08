using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Order_Aggregate;

namespace Talabat.core.Servicecs
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(string BuyerEmail,string basketId,Address shippingaddress,int deliveryMethodId);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail);
        Task<Order> GetOrderByIdForUserAsync(string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync(string BuyerEmail);


    }
}
