using Application.Models;
using Domain.Entities;
using MediatR;
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

namespace Application.Features.Auth.Command
{
    public class RegisterCommand : IRequest<ApiResult<string>>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

    }


    public class RegisterCommandHandler : IRequestHandler<RegisterCommand , ApiResult<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;


        public RegisterCommandHandler(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
              _userManager = userManager;
            _configuration = configuration;

        }

        public async Task<ApiResult<string>> Handle(RegisterCommand request , CancellationToken cancellationToken)
        {
            ApiResult<string> result = new();

            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
            {
                result.Fail("یوزر وجود دارد");
                return result;
            }

            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FirstName = "",
                LastName = ""

            };
            var finalResult = await  _userManager.CreateAsync(user, request.Password);
            if (!finalResult.Succeeded)
            {
                result.Fail(finalResult.Errors.ToString());
                return result;
            }

            await _userManager.AddToRoleAsync(user, "user");

            var token = GenerateJwtToken(user);
            result.Value = token;
            result.Success("OK");
            return result;  


        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["jwtConfig:SignInKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Test", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["jwtConfig:SignInKey"],
                Audience = _configuration["jwtConfig:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
