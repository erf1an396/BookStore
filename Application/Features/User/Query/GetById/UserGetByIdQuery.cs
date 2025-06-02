using Application.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Query.GetById
{
    public class UserGetByIdQuery : IRequest<ApiResult<UserDto>>
    {
        public Guid Id { get; set; }
    }

    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, ApiResult<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserGetByIdQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResult<UserDto>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {

            ApiResult<UserDto> result = new();

            var user = await  _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }


            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                BirthDay_Date = user.BirthDay_Date,
                Gender = user.Gender,

            };

            result.Value = userDto;
            result.Success();

            return result; 

           
        }
    }
}
