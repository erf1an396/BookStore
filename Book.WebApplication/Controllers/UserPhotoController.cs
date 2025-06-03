
using Application.Features.UserPhoto.Command.Insert;
using Application.Features.UserPhoto.Query.GetById;
using Application.Features.UserPhoto.Query.GetByUserId;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book.WebApplication.Controllers
{
    public class UserPhotoController : Controller
    {

        private readonly IMediator _mediator;


        public UserPhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [ActionName("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            

            var query = new UserPhotoGetByIdQuery { Id = Id };
            var result = await _mediator.Send(query);

            return Ok(result);

        }

        [HttpPost]
        [ActionName("GetByUserId")]
        public async Task<IActionResult> GetByUserId(Guid UserId)
        {
            var query = new UserPhotoGetByUserIdQuery { UserId = UserId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        //[HttpPost]
        //[ActionName("Delete")]

        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var command = new UserPhotoDeleteCommand { Id = Id };
        //    var result = await _mediator.Send(command);

        //    return Ok(result);


        //}


        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromForm] UserPhotoInsertCommand command)
        {
            command.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
