using Microsoft.AspNetCore.Http.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Servicecs;

namespace Talabat.Service
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase db;

        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            this.db = redis.GetDatabase();
        }

        public IConnectionMultiplexer Redis { get; }

        public async Task CachResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }
            var JsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serilizedResponse = JsonSerializer.Serialize(response, JsonOptions);
            await this.db.StringSetAsync(cacheKey, serilizedResponse, timeToLive);
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var response = await this.db.StringGetAsync(cacheKey);
            if (response.IsNullOrEmpty)
            {
                return null;
            }
            return response;
        }
    }
}
