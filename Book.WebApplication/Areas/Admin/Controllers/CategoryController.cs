using Application.Features.Category.Command.Delete;
using Application.Features.Category.Command.Insert;
using Application.Features.Category.Command.Update;
using Application.Features.Category.Query.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.WebApplication.Areas.Admin.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query =  new CategoryGetAllQuery();

            var result = await _mediator.Send(query);
            return Ok(result);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new CategoryDeleteCommand { Id = id };

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CategoryInsertCommand command)
        
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }




        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
