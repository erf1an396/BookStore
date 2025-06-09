using Application.Contracts.Identity;
using Application.Features.BookPhoto.Command.Delete;
using Application.Features.BookPhoto.Command.Insert;
using Application.Features.BookPhoto.Query.GetAll;
using Application.Features.BookPhoto.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("پنل ادمین / عکس های کتاب")]
    public class BookPhotoController : Controller
    {
        private readonly IMediator _mediator;

        public BookPhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int BookId)
        {
            ViewBag.BookId = BookId;
            return View();
        }

        [HttpGet]
        [ActionName("GetAll")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("گرفتن همه")]
        public async Task<IActionResult> GetAll([FromQuery] int BookId)
        {
            ViewBag.BookId = BookId;

            var query = new BookPhotoGetAllQuery()
            {BookId = BookId};

            var result  = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetById")]

        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [DisplayName("گرفتن بر اساس آیدی")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new BookPhotoGetByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpPost]
        [ActionName("Delete")]
        [DisplayName("حذف")]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Delete(int id)
        {
            var commmand = new BookPhotoDeleteCommand { Id = id };

            var result = await _mediator.Send(commmand);

            return Ok(result);
        }

        [HttpPost]
        [ActionName("Create")]
        [DisplayName("افزودن")]

        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> Create([FromForm] BookPhotoInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
