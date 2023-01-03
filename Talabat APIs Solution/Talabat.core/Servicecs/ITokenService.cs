using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;

namespace Talabat.core.Servicecs
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser AppUser,UserManager<AppUser> UserManager);

    }
}
