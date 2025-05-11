using Application.Contracts;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
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
        private readonly IConfiguration _configuration;
       


        public LoginCommandHandler(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            
            
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
            

            var token = GenerateJwtToken(user);
            result.Value = token;
            result.Success("OK");
            return result;

        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Test", user.Id.ToString()),
                    new Claim("FirstName", user.LastName.ToString()),
                    new Claim("lastName", user.FirstName.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                //Issuer = _configuration["jwtConfig:SignInKey"],
                //Audience = _configuration["jwtConfig:Audience"],

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }


    

}
