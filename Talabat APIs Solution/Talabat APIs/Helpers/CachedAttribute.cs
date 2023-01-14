using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Servicecs;

namespace Talabat_APIs.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        public int Timetolive { get; }

        public CachedAttribute(int timetolive)
        {
            this.Timetolive = timetolive;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // the RequestServices is the container of all services which can be injected
            var cacheservice = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            // how to get the request from the context
            var cachkey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheservice.GetCachedResponseAsync(cachkey);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                // a class implements IActionResult and inherit ActionResult 
                var contentresult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                // the result of the url is == contentresult
                context.Result = contentresult;
                return;
            }
            var ExcutedEndPointContext = await next.Invoke();
            if (ExcutedEndPointContext.Result is OkObjectResult okobjectresult)
            {
                await cacheservice.CachResponseAsync(cachkey, okobjectresult.Value, TimeSpan.FromSeconds(Timetolive));
            }
        }
        public string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keybuilder = new StringBuilder();
            keybuilder.Append(request.Path);
            foreach (var (key, value) in request.Query)
            {
                keybuilder.Append($"|{key}-{value}");
            }
            return keybuilder.ToString();
        }
    }
}
