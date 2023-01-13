using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;
using Talabat.core.Servicecs;
using Talabat.Service;
using Talabat_APIs.Dtos;
using Talabat_APIs.Errors;
using Talabat_APIs.Extensions;

namespace Talabat_APIs.Controllers
{
    public class AccountController : BaseAPIController
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public ITokenService ITokenService { get; }
        public IMapper Mapper { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> SignInManager, ITokenService ITokenService, IMapper mapper)
        {
            UserManager = userManager;
            this.SignInManager = SignInManager;
            this.ITokenService = ITokenService;
            Mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto RegisterDto)
        {
            if (CheckEmailExists(RegisterDto.Email).Result.Value)
                return BadRequest(new ApiResponse(400, "The Email Already Exists"));
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
            });
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurentUser()
        {
            // no nneds of validation as the endpoint is authorized
            var UserEmail = this.User.FindFirstValue(ClaimTypes.Email);
            var user = await this.UserManager.FindByEmailAsync(UserEmail);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await this.ITokenService.CreateToken(user, UserManager)
            });
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            //first get the cuurent user   
            var user = await this.UserManager.FindUsersByEmailWithAddressAsync(this.User);
            var mappedAddress = this.Mapper.Map<Address, AddressDto>(user.Address);
            return Ok(mappedAddress);
        }
        [Authorize]
        [HttpPut("UpdateCurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto newaddress)
        {
            //first get the cuurent user email
            var useremail = this.User.FindFirstValue(ClaimTypes.Email);
            //first get the cuurent user from his email
            var myuser = await this.UserManager.FindByEmailAsync(useremail);
            // update the address of the user 
            myuser.Address = this.Mapper.Map<AddressDto, Address>(newaddress);
            //user.Address.AppUser = user;
            //user.Address.AppUserId = user.Id;
            //update the user data in the database
            var result = await this.UserManager.UpdateAsync(myuser);
            // check the state of update
            if (result.Succeeded)
            {
                var mappedAddress = this.Mapper.Map<Address, AddressDto>(myuser.Address);
                return Ok(mappedAddress);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "An Error Occured During Updating The Current User Address"));
            }
        }
        [HttpGet]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return this.UserManager.FindByEmailAsync(email) != null;
        }

    }
}
