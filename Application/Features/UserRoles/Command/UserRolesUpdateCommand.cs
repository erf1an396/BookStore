using Application.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserRoles.Command
{
    public class UserRolesUpdateCommand : IRequest<ApiResult>
    {
        public Guid UserId { get; set; }
        public List<string> SelectedRoles { get; set; }
    }

    public class UserRolesUpdateCommandHandler : IRequestHandler<UserRolesUpdateCommand, ApiResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public UserRolesUpdateCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager; 
        }
        public async Task<ApiResult> Handle(UserRolesUpdateCommand request, CancellationToken cancellationToken)
        {
            ApiResult result = new();

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = request.SelectedRoles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(request.SelectedRoles).ToList();

            if(rolesToAdd.Any() )
            {
                var RTADone = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!RTADone.Succeeded)
                {
                    result.Fail(ApiResultStaticMessage.UnknownExeption);
                    return result;
                }

            }

            if (rolesToRemove.Any())
            {
                var RTRDone = await  _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!RTRDone.Succeeded)
                {
                    result.Fail(ApiResultStaticMessage.UnknownExeption);
                         return result;
                }
            }


             result.Success(ApiResultStaticMessage.UpdateSuccessfully);
             return result;
        }
    }
}
