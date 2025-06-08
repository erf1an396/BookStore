using Application.Contracts.Identity;
using Application.Features.Author.Command.Delete;
using Application.Features.Author.Command.Insert;
using Application.Features.Author.Command.Update;
using Application.Features.Author.Query.GetAll;
using Application.Features.Author.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("نویسنده")]
    public class AuthorController : Controller
    {

        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        [ActionName("AuthorList")]
        public async Task<IActionResult> AuthorList()
        {
            return View();
        }

        [HttpGet]
        [ActionName("GetAll")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("گرفتن همه")]
        public async Task<IActionResult> GetAll()
        {
            var query = new AuthorGetAllQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        [DisplayName("حذف")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new AuthorDeleteCommand { Id = id };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [ActionName("Create")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("افزودن")]

        public async Task<IActionResult> Create([FromBody] AuthorInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Update")]
        [DisplayName("آپدیت")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Update(int Id, [FromBody] AuthorUpdateCommand command)
        {
            var resutl = await _mediator.Send(command);
            return Ok(resutl);

        }

        [HttpGet]
        [ActionName("GetById")]
        [DisplayName("گرفتن بر اساس آیدی")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new AuthorGetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
            
    }
}
