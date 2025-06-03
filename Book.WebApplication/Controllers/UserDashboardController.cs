using Application.Features.UserPhoto.Query.GetByUserId;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book.WebApplication.Controllers
{
    [Authorize]

    public class UserDashboardController : Controller
    {
        private readonly ILogger<UserDashboardController> _logger;
        private readonly IMediator _mediator;

        public UserDashboardController(ILogger<UserDashboardController> logger , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async  Task<IActionResult> Index()
        {
            Guid userId =Guid.Parse(User.Identity.GetUserId());
            var photo = (await _mediator.Send(new UserPhotoGetByUserIdQuery { UserId = userId })).Value;

            ViewBag.Photo = photo;

            return View();
        }
    }
}
