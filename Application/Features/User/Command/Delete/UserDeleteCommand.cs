using Application.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Command.Delete
{
    public class UserDeleteCommand : IRequest<ApiResult>
    {
        public string UserName { get; set; }
    }

    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand , ApiResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDeleteCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

         public async Task<ApiResult> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            ApiResult result = new();

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            var deleteUser = await _userManager.DeleteAsync(user);
            if (!deleteUser.Succeeded)
            {
                foreach (var error in deleteUser.Errors)
                    result.Fail(error.Description);
                return result;
            }
            result.Success(ApiResultStaticMessage.DeleteSuccessfully);
            return result;
        }
    }
}
