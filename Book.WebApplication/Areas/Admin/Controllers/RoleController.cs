using Application.Contracts.Identity;
using Application.Features.Role.Command.Delete;
using Application.Features.Role.Command.Insert;
using Application.Features.Role.Command.Update;
using Application.Features.Role.Query.Get;
using Application.Features.Role.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.WebApplication.ControllerBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book.WebApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [DisplayName("پنل ادمین / نقش ها")]
    public class RoleController : Controller
    {

        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        //[Authorize(Policy =ConstantPolicies.DynamicPermission)]
        [DisplayName("مشاهده")]
        public async Task<IActionResult> List()
        {
            return View();
        }



        [HttpGet]
        //[Authorize(Policy =ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Index(Guid id)
        {
            return View();
        }


        [HttpPost]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("افزودن")]
        [ActionName("Insert")]
        public async Task<IActionResult> Insert([FromBody]RoleInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("ویرایش")]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody]RoleUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("حذف")]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
           var command = new RoleDeleteCommand {Id = id};
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("مشاهده")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new RoleGetQuery());
            return Ok(result);
        }


        [HttpGet]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new RoleGetByIdQuery {Id = id});
            return Ok(result);
        }
    } 
}
