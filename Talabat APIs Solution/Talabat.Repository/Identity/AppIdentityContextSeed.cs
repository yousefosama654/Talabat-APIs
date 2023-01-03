using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;
using Talabat.Repository.Data;

namespace Talabat.Repository.Identity
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUser(ILoggerFactory Ilogger, UserManager<AppUser> userManager)
        {
            try
            {
                if (userManager.Users.Any() == false)
                {
                    var user = new AppUser()
                    {
                        DisplayName = "yousef osama",
                        Email = "yousefosama654@gmail.com",
                        UserName = "yousefosama654"
                    };
                   await userManager.CreateAsync(user, "Yousef2020*");
                }
            }
            catch (Exception ex)
            {
                var logger = Ilogger.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
