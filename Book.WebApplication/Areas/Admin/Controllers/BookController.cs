using Application.Contracts.Identity;
using Application.Features.Book.Command.Delete;
using Application.Features.Book.Command.Insert;
using Application.Features.Book.Command.Update;
using Application.Features.Book.Query.GetAll;
using Application.Features.Book.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Book.WebApplication.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Policy = ConstantPolicies.DynamicPermission)]
    [DisplayName("پنل ادمین / کتاب ها")] 
    public class BookController : Controller
    {

        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]

        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("صفحه اصلی")]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        [ActionName("AddBook")]
        [DisplayName("صفحه افزودن کتاب")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> AddBook()
        {
            return View();
        }

        [HttpGet]
        [ActionName("BookList")]
        [DisplayName("لیست کتاب ها ")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> BookList()
        {
            return View();
        }

        [HttpGet]
        [ActionName("GetAll")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("گرفتن همه")]
        public async Task<IActionResult> GetAll()
        {
            var query = new BookGetAllQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }
         
        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("حذف")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new BookDeleteCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);

        }

        [HttpPost]
        [ActionName("Create")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("افزودن")]
        public async Task<IActionResult>  Create([FromBody] BookInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Update")]
        [DisplayName("آپدیت")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Update (int Id , [FromBody] BookUpdateCommand command)
        {
           

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet]
        [ActionName("GetById")]
        [DisplayName("گرفتن بر اساس آیدی")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> GetById(int Id)
        {
            
                var query = new BookGetByIdQuery { Id = Id };
                var result = await _mediator.Send(query);

                return Ok(result);
            
            
            
        }
    }
}
