using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Talabat.core.Entities.Identity;
using Talabat.Repository.Identity;

namespace Talabat_APIs.Extensions
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {

            services.AddIdentity<AppUser,IdentityRole>(ops => { }).AddEntityFrameworkStores<AppIdentityContext>();
            services.AddAuthentication();
            return services;
        }
    }
}
