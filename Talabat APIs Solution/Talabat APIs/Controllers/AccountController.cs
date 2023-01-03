using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;
using Talabat.core.Servicecs;
using Talabat.Service;
using Talabat_APIs.Dtos;
using Talabat_APIs.Errors;

namespace Talabat_APIs.Controllers
{
    public class AccountController : BaseAPIController
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public ITokenService ITokenService { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> SignInManager,ITokenService ITokenService)
        {
            UserManager = userManager;
            this.SignInManager = SignInManager;
            this.ITokenService = ITokenService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto RegisterDto)
        {
            var user = new AppUser()
            {
                Email = RegisterDto.Email,
                PhoneNumber = RegisterDto.PhoneNumber,
                DisplayName = RegisterDto.DisplayName,
                UserName = RegisterDto.Email.Split("@")[0]
            };
            var result = await this.UserManager.CreateAsync(user, RegisterDto.Password);
            if (result.Succeeded)
            {
                return Ok(new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await this.ITokenService.CreateToken(user, UserManager)
                });
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto LoginDto)
        {
            var user = await UserManager.FindByEmailAsync(LoginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var result = await this.SignInManager.CheckPasswordSignInAsync(user, LoginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await this.ITokenService.CreateToken(user, UserManager)
            }) ;
        }
    }
}
