using Application.Features.Author.Query.GetAll;
using Application.Features.Author.Query.GetById;
using Application.Features.User.Command.Delete;
using Application.Features.User.Command.Insert;
using Application.Features.User.Command.Update;
using Application.Features.User.Query.GetAll;
using Application.Features.User.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.WebApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
            
        }

        [HttpGet]
        
        public async Task <ActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        [ActionName("UserList")]
        public async Task<IActionResult> UserList()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] UserInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);

        }


        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update (int UserName , [FromBody] UserUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string UserName)
        {
            var command = new UserDeleteCommand { UserName = UserName };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new UserGetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new UserGetAllQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
