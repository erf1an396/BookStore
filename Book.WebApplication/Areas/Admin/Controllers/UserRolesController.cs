using Application.Contracts.Identity;
using Application.Features.Role.Query.Get;
using Application.Features.User.Query.GetAll;
using Application.Features.UserRoles.Command;
using Application.Features.UserRoles.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Book.WebApplication.Areas.Admin.Controllers
{

    [Area("admin")]
    [DisplayName("پنل ادمین / نقش یوزر ها")]
    public class UserRolesController : Controller
    {
        private readonly IMediator _mediator;

        public UserRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpPost]
        [DisplayName("ویرایش")]
        [ActionName("Update")]
        
        public async Task<IActionResult> Update([FromBody] UserRolesUpdateCommand command)
        {
            var result  = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [DisplayName("گرفتن نقش های کاربر")]
     

        public async Task<IActionResult> GetAllUserRoles([FromQuery]Guid userId)
            {
            var query = new UserRolesGetAllQuery {UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
            
        }


        [HttpGet]

        [DisplayName("گرفتن همه نقش ها")]
      
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new RoleGetQuery());
            return Ok(result);
        }


        [HttpGet]
        [ActionName("GetAll")]
        [DisplayName("گرفتن همه یوزرها")]
       
        public async Task<IActionResult> GetAll()
        {
            var query = new UserGetAllQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}
