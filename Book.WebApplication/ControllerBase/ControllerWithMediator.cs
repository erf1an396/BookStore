using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Store.WebApplication.ControllerBase
{
    public class ControllerWithMediator : Controller
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

        protected IActionResult BindStatus(ApiResult apiResult)
        {
            if (apiResult.IsForbiden)
                return Forbid();

            return Ok(apiResult);
        }
        protected IActionResult BindStatus<T>(ApiResult<T> apiResult)
        {
            if (apiResult.IsForbiden)
                return Forbid();

            return Ok(apiResult);
        }
    }

}
