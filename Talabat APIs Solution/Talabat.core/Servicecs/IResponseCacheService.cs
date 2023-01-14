using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Servicecs
{
    public interface IResponseCacheService
    {
        Task CachResponseAsync(string cacheKey, object response,TimeSpan timeToLive);

        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
