using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase db;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            db = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketsync(string Id)
        {
            return await db.KeyDeleteAsync(Id);
        }
        public async Task<CustomerBasket> GetBasketByIdAsync(string Id)
        {
            // TO Get the basket of specific Id string
            var basket = await db.StringGetAsync(Id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }
        // it is used to update or create the customer basket
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket CustomerBasket)
        {
            var basket = await db.StringSetAsync(CustomerBasket.Id, JsonSerializer.Serialize(CustomerBasket),TimeSpan.FromDays(30));
            if (basket == false)
            {
                return null;
            }
            else
                return await GetBasketByIdAsync(CustomerBasket.Id);
        }
    }
}
