using Application.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Command.Insert
{
    public class UserInsertCommand : IRequest<ApiResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName  { get; set; }

        public string Email { get; set; }

        public string BirthDay_Date { get; set; }

        public GenderEnum Gender { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class UserInsertCommandHandler : IRequestHandler<UserInsertCommand, ApiResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserInsertCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResult> Handle(UserInsertCommand request, CancellationToken cancellationToken)
        {

            ApiResult result = new();

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDay_Date = request.BirthDay_Date,
                Gender = request.Gender,
            };
            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                    result.Fail(error.Description);
                return result;
            }

            result.Success(ApiResultStaticMessage.SavedSuccessfully);
            return result;

        }
    }
}
