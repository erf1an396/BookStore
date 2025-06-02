using Application.Contracts;
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

namespace Application.Features.User.Command.Update
{
    public class UserUpdateCommand : IRequest<ApiResult>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        

        public string UserName { get; set; }

        public string Email {  get; set; }

        public string BirthDay_Date { get; set; }

        public GenderEnum Gender { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

    }

    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand , ApiResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserUpdateCommandHandler(UserManager<ApplicationUser> userManager)
        {
             _userManager = userManager;
        }

        public async Task<ApiResult> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            ApiResult result = new();

            

            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.BirthDay_Date = request.BirthDay_Date;
            user.Gender = request.Gender;
            user.Email = request.Email;



            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    result.Fail(error.Description);
                return result;
            }


            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetResult = await _userManager.ResetPasswordAsync(user, token, request.Password);
                if (!resetResult.Succeeded)
                {
                    foreach (var error in resetResult.Errors)
                        result.Fail(error.Description);
                    return result;
                }
            }

            result.Success(ApiResultStaticMessage.UpdateSuccessfully);
            return result;

        }
    }
}
