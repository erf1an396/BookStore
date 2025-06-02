using Application.Contracts;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Application.Features.Auth.Command
{

    public class LoginCommand : IRequest<ApiResult<string>>
    {
        public string Username { get; set; }

        

        public string Password { get; set; }

    }


    public class LoginCommandHandler : IRequestHandler<LoginCommand , ApiResult<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public LoginCommandHandler(UserManager<ApplicationUser> userManager , IConfiguration configuration , IHttpContextAccessor httpContextAccessor , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            
            
        }

        public async Task<ApiResult<string>> Handle(LoginCommand request , CancellationToken cancellationToken)
        {
            ApiResult<string> result = new();

            var user = await  _userManager.FindByNameAsync(request.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user , request.Password))
            {
                result.Fail("یوزر یا پسسورد نادرست است ");
                return result;
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!checkPassword)
            {
                result.Fail("نام کاربری یا رمز عبور اشتباه است");
                return result;
            }


            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if(signInResult.Succeeded)
            {
                var principal = await _signInManager.CreateUserPrincipalAsync(user);
                 
                if (principal.Identity is ClaimsIdentity identity)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim("FullName",user.FirstName + " " + user.LastName));
                    identity.AddClaim(new Claim("IsAdmin", user.IsAdmin.ToString()));
                }

                await _signInManager.SignOutAsync();

                await _httpContextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal, new AuthenticationProperties { IsPersistent = true });

                result.Success("OK");
                return result;
            }
            else if(signInResult.IsLockedOut)
            {
                result.Fail("کاربر مسدود شده است ");
            }
            else
            {
                result.Fail("نام کاربری یا رمز عبور اشتباه است");
            }

            return result;
            

            


            


            //_httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Expires = DateTime.UtcNow.AddHours(1)

            //});


            //result.Value = token;

            

        }

    }


    

}
