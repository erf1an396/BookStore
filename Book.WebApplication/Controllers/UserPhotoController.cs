using Application.Features.UserPhoto.Command.Delete;
using Application.Features.UserPhoto.Command.Insert;
using Application.Features.UserPhoto.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        [ActionName("Delete")]

        public async Task<IActionResult> Delete(int Id)
        {
            var command = new UserPhotoDeleteCommand { Id = Id };
            var result = await _mediator.Send(command);

            return Ok(result);


        }


        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromForm] UserPhotoInsertCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
