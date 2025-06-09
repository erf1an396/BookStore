using Application.Contracts.Identity;
using Application.Features.Category.Command.Delete;
using Application.Features.Category.Command.Insert;
using Application.Features.Category.Command.Update;
using Application.Features.Category.Query.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book.WebApplication.Areas.Admin.Controllers
{

    //[ApiController]
    //[Route("api/[controller]")]
    [Area("Admin")]
    [DisplayName("پنل ادمین / دسته بندی ها")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        //[Url("admin/category")]
        public async Task<IActionResult> Index()
        {
            return View();

        }

        [HttpGet]
        [ActionName("GetAll")]
        [DisplayName("گرفتن همه")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetAll()
        {
            var query =  new CategoryGetAllQuery();

            var result = await _mediator.Send(query);
            return Ok(result);

        }

        [HttpPost]
        [ActionName("Delete")]
        [DisplayName("حذف")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new CategoryDeleteCommand { Id = id };

            var result = await _mediator.Send(command);
            return Ok(result);
        }



        [HttpPost]
        [ActionName("Create")]
        [DisplayName("افزودن")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Create([FromBody] CategoryInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }





        [HttpPost]
        [ActionName("Update")]
        [DisplayName("ویرایش")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Update(int Id,[FromBody] CategoryUpdateCommand command)
        {
            var commandd = new CategoryUpdateCommand { Id = Id  , Title = command.Title };
            var result = await _mediator.Send(commandd);
            return Ok(result);
        }


    }
}
