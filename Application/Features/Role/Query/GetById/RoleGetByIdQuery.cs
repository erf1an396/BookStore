using Application.Contracts.Identity;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role.Query.GetById
{
    public class RoleGetByIdQuery : IRequest<ApiResult<RoleGetByIdVm>>
    {
        public Guid Id { get; set; }
    }


    public class RoleGetByIdQueryHandler : IRequestHandler<RoleGetByIdQuery , ApiResult<RoleGetByIdVm>>
    {
        private readonly IIdentityRoleManager _identityRoleManager;
        private readonly IMvcActionsDiscovery _mvcActionsDiscovery;

        public RoleGetByIdQueryHandler(IMvcActionsDiscovery mvcActionsDiscovery, IIdentityRoleManager identityRoleManager)
        {
            _identityRoleManager = identityRoleManager;
            _mvcActionsDiscovery = mvcActionsDiscovery;
        }

        public async Task<ApiResult<RoleGetByIdVm>> Handle(RoleGetByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResult<RoleGetByIdVm> result = new ApiResult<RoleGetByIdVm>();
            result.Value = new();

            Domain.Entities.ApplicationRole applicationRole = await _identityRoleManager.FindById(request.Id);
            if (applicationRole != null)
            {
                var claims = await _identityRoleManager.GetClaimsAsync(applicationRole);
                result.Value.Id = applicationRole.Id;
                result.Value.Name = applicationRole.Name;
                result.Value.RoleClaimVM =
                    claims
                    .Select(x => new RoleClaimVM()
                    {
                        ClaimValue = x.Value
                    })
                    .ToList();
            }

            ICollection<Models.ControllerVM> allSecuredControllerActions =
                _mvcActionsDiscovery.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);

            result.Value.AllControllerVms = allSecuredControllerActions.OrderBy(x => x.ControllerDisplayName).ToList();



            return result;
        }

    }


}
