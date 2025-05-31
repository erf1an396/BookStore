using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
    }
}
