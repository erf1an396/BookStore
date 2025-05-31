using Application.Features.Auth.Command;
using Book.WebApplication.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book.WebApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
            
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();  
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result  = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());

            return RedirectToAction("Index", "Home");
            
        }

        //public IActionResult SetToken([FromBody] TokenDto dto)
        //{
        //    Response.Cookies.Append("access_token", dto.Token, new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true,
        //        SameSite = SameSiteMode.Strict,
        //        Expires = DateTimeOffset.UtcNow.AddHours(1)
        //    });

        //    return Ok();
        //}

        //public class TokenDto
        //{
        //    public string Token { get; set; }
        //}

       

    }
}
