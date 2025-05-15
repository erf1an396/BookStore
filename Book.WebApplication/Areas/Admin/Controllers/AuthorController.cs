using Application.Features.Author.Command.Delete;
using Application.Features.Author.Command.Insert;
using Application.Features.Author.Command.Update;
using Application.Features.Author.Query.GetAll;
using Application.Features.Author.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> GetAll()
        {
            var query = new AuthorGetAllQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new AuthorDeleteCommand { Id = id };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [ActionName("Create")]

        public async Task<IActionResult> Create([FromBody] AuthorInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Update")]

        public async Task<IActionResult> Update(int Id, [FromBody] AuthorUpdateCommand command)
        {
            var resutl = await _mediator.Send(command);
            return Ok(resutl);

        }

        [HttpGet]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new AuthorGetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
            
    }
}
