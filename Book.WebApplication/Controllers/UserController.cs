using Application.Features.User.Command.Insert;
using Application.Features.User.Command.Update;
using Application.Features.User.Query.GetAll;
using Application.Features.User.Query.GetById;
using Infrastructure.Extentions;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book.WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }



        //[HttpPost]
        //[ActionName("Create")]
        //public async Task<IActionResult> Create([FromBody]  UserInsertCommand command)
        //{
        //    var result = await _mediator.Send(command); 
        //    return Ok(result);


        //}

        [HttpPost]
        [ActionName("UserUpdate")]

        public async Task<IActionResult> UpdateUser( [FromBody] UserUpdateCommand command)
        {
            command.Id = Guid.Parse( User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        //[HttpPost]
        //[ActionName("Admin/Update")]
        //public async Task<IActionResult> UpdateAdmin(int UserName , [FromBody] UserUpdateCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}



        //[HttpPost]
        //[ActionName("Delete")]

        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var command = new UserGetByIdQuery { Id = Id };
        //    var result = await _mediator.Send(command);

        //    return Ok(result);

        //}

        //[HttpGet]
        //[ActionName("Admin/GetById")]

        //public async Task<IActionResult> GetById (Guid Id)
        //{

        //    var query = new UserGetByIdQuery { Id = Id };
        //    var result = await _mediator.Send(query);
        //    return Ok(result);

        //}


        [HttpGet]
        [ActionName("UserGetById")]
        public async Task<IActionResult> GetById()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();


            var userIdClaim = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            var query = new UserGetByIdQuery { Id = userIdClaim  };
            var result = await _mediator.Send(query);
            return Ok(result);


            //var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);


            //var userId = Guid.Parse(userIdClaim);

            //var query = new UserGetByIdQuery { Id = userId };
            //var result = await _mediator.Send(query);
            //return Ok(result);
        }



        //[HttpGet]
        //[ActionName("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var query = new UserGetAllQuery();
        //    var result = _mediator.Send(query);
        //    return Ok(result);
        //}
    }
}
