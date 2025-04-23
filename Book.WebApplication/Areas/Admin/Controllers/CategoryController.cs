using Application.Features.Category.Command.Delete;
using Application.Features.Category.Command.Insert;
using Application.Features.Category.Command.Update;
using Application.Features.Category.Query.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Book.WebApplication.Areas.Admin.Controllers
{

    //[ApiController]
    //[Route("api/[controller]")]
    [Area("Admin")]
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
        public async Task<IActionResult> GetAll()
        {
            var query =  new CategoryGetAllQuery();

            var result = await _mediator.Send(query);
            return Ok(result);

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new CategoryDeleteCommand { Id = id };

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] CategoryInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }




        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update(int Id,[FromBody] CategoryUpdateCommand command)
        {
            var commandd = new CategoryUpdateCommand { Id = Id  , Title = command.Title };
            var result = await _mediator.Send(commandd);
            return Ok(result);
        }


    }
}
