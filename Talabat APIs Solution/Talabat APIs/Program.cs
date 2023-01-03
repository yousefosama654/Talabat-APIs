using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat_APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // it holds the kestrel
            var host = CreateHostBuilder(args).Build();
            // creation of new scope  
            using var scope = host.Services.CreateScope();
            // holds the scoped services
            var services = scope.ServiceProvider;
            var loggerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                // ask the CLR to create a service of that type
                // Don't Forget to allow dependency injection for that type
                var context = services.GetRequiredService<StoreContext>();
                await StoreContextSeed.SeedAsync(loggerfactory, context);
                await context.Database.MigrateAsync();
                // ask the CLR to create a service of that type
                // Don't Forget to allow dependency injection for that type  
                var usermanager = services.GetRequiredService<UserManager<AppUser>>();
                var Identitycontext = services.GetRequiredService<AppIdentityContext>();
                await AppIdentityContextSeed.SeedUser(loggerfactory,usermanager);
                await Identitycontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex.Message);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
