using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Talabat.core.Repositories;
using Talabat.core.Servicecs;
using Talabat.Repository;
using Talabat.Service;
using Talabat_APIs.Errors;
using Talabat_APIs.Helpers;

namespace Talabat_APIs.Extensions
{
    public static class AppServicesExtension
    {
        // the return type due to  case of chaining 
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    // to catch the errors the every not valid model state(paramater to the function)
                    //1- first select every state the has mpotre than zero errors and select the errors in it se=nd select the error message 
                    var errors = ActionContext.ModelState.Where(m => m.Value.Errors.Count() > 0).SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage).ToList();
                    return new BadRequestObjectResult(new ApiValidationErrorResponse(404, errors));
                };
            });
            return services;
        }
    }
}
