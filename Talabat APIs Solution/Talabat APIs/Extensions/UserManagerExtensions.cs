using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;

namespace Talabat_APIs.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUsersByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal userclaim)
        {
            var useremail = userclaim.FindFirstValue(ClaimTypes.Email);
            // to have exception if the eamil is repeated 
            var user = await userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == useremail);
            return user;
        }
    }
}
