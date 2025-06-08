using Application.Contracts.Identity;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role.Command.Delete
{
    public class RoleDeleteCommand : IRequest<ApiResult>
    {
        public Guid Id { get; set; }

    }

    public class RoleDeleteCommandHandler : IRequestHandler<RoleDeleteCommand, ApiResult>
    {
        private readonly IIdentityRoleManager _identityRoleManager;

        public RoleDeleteCommandHandler(IIdentityRoleManager identityRoleManager)
        {
            _identityRoleManager = identityRoleManager;
        }

        public async Task<ApiResult> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _identityRoleManager.DeleteAsync(request.Id);
        }
    }
}
