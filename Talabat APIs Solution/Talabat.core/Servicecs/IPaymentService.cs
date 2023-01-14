using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.Order_Aggregate;

namespace Talabat.core.Servicecs
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdatePayementIntentSucceededOrFailed(string paymentIntentId, bool isSucceeded,string basketId);
    }
}
