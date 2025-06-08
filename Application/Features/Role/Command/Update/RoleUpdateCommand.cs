using Application.Contracts.Identity;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role.Command.Update
{
    public class RoleUpdateCommand : IRequest<ApiResult>
    {
        public Guid Id   { get; set; }

        public string Name { get; set; }

        public List<string> ClaimValue { get; set; }
    }

    public class RoleUpdateCommandHandler : IRequestHandler<RoleUpdateCommand , ApiResult>
    {
        private readonly IIdentityRoleManager _identityRoleManager;

        public RoleUpdateCommandHandler(IIdentityRoleManager identityRoleManager)
        {
            _identityRoleManager = identityRoleManager;
        }

        public async Task<ApiResult> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _identityRoleManager.UpdateRoleWithClaimsAsync(request.Id , request.Name , request.ClaimValue);
        }
    }
}
