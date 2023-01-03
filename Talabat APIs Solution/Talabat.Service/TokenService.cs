using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;
using Talabat.core.Servicecs;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<string> CreateToken(AppUser AppUser, UserManager<AppUser> UserManager)
        {
            // private claims
            var authclaims = new List<Claim>() {
            new Claim(ClaimTypes.Email,AppUser.Email),
            new Claim(ClaimTypes.Name,AppUser.DisplayName),
            new Claim(ClaimTypes.Name,AppUser.DisplayName),
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
            //public claims
            issuer: Configuration["JWT:issuer"],
            audience: Configuration["JWT:Audience"],
            expires: DateTime.Now.AddDays(double.Parse(Configuration["JWT:DurationInDays"])),
            signingCredentials: credentials,
            claims:authclaims
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
