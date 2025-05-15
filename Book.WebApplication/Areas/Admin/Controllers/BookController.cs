using Application.Features.Book.Command.Delete;
using Application.Features.Book.Command.Insert;
using Application.Features.Book.Command.Update;
using Application.Features.Book.Query.GetAll;
using Application.Features.Book.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book.WebApplication.Areas.Admin.Controllers
{

    [Area("Admin")]
    
    public class BookController : Controller
    {

        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        [ActionName("AddBook")]
        public async Task<IActionResult> AddBook()
        {
            return View();
        }

        [HttpGet]
        [ActionName("BookList")]
        public async Task<IActionResult> BookList()
        {
            return View();
        }

        [HttpGet]
        [ActionName("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var query = new BookGetAllQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }
         
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new BookDeleteCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);

        }

        [HttpPost]
        [ActionName("Create")]  
        public async Task<IActionResult>  Create([FromBody] BookInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update (int Id , [FromBody] BookUpdateCommand command)
        {
           

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            
                var query = new BookGetByIdQuery { Id = Id };
                var result = await _mediator.Send(query);

                return Ok(result);
            
            
            
        }
    }
}
