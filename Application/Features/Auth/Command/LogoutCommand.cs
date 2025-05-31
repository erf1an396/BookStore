using Application.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Command
{
    public class LogoutCommand : IRequest<ApiResult<string>>
    {
    }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResult<string>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<ApiResult<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            ApiResult<string> result = new();

            await _signInManager.SignOutAsync();
            return result;
        }
    }
}
