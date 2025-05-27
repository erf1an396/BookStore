
using Application.Features.AuthorPhoto.Command.Delete;
using Application.Features.AuthorPhoto.Command.Insert;
using Application.Features.AuthorPhoto.Query.GetAll;
using Application.Features.AuthorPhoto.Query.GetById;
using Application.Features.BookPhoto.Command.Insert;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.WebApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AuthorPhotoController : Controller
    {
        private readonly IMediator _mediator;

        public AuthorPhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] int AuthorId)
        {
            ViewBag.AuthorId = AuthorId;

            return View();
        }


        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int AuthorId)
        {
            ViewBag.AuthorId = AuthorId;

            var query = new AuthorPhotoGetAllQuery() { AuthorId = AuthorId };

            var result = await  _mediator.Send(query);

            return Ok(result);


        }


        [HttpGet]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new AuthorPhotoGetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);

        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new AuthorPhotoDeleteCommand { Id = id };

            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromForm] AuthorPhotoInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }




    }
}
