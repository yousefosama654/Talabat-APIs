using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketByIdAsync(string Id);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket CustomerBasket);
        Task<bool> DeleteBasketsync(string Id);

    }
}
