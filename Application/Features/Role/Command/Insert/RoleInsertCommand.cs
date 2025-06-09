using Application.Contracts.Identity;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role.Command.Insert
{
    public class RoleInsertCommand : IRequest<ApiResult>
    {
        public string Name  { get; set; }

        public List<string> ClaimValue { get; set; }
    }

    public class RoleInsertCommandHandler : IRequestHandler<RoleInsertCommand, ApiResult>
    {
        private readonly IIdentityRoleManager _identityRoleManager;

        public RoleInsertCommandHandler(IIdentityRoleManager identityRoleManager)
        {
            _identityRoleManager = identityRoleManager;
        }

        public Task<ApiResult> Handle(RoleInsertCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.ApplicationRole applicationRole = new Domain.Entities.ApplicationRole()
            {
                Name = request.Name,
                
                ApplicationRoleClaims =
                request.ClaimValue.Select(x => new Domain.Entities.ApplicationRoleClaim()
                {
                    ClaimValue = x,
                    ClaimType = ConstantPolicies.DynamicPermission,
                }).ToList()
            };

            return _identityRoleManager.CreateAsync(applicationRole);
            
        }
    }
}
