using Application.Contracts.Identity;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role.Query.Get
{
    public  class RoleGetQuery : IRequest<ApiResult<List<RoleDto>>>

    {

    }
    public class RoleGetQueryHandler : IRequestHandler<RoleGetQuery , ApiResult<List<RoleDto>>>
    {
        private readonly IIdentityRoleManager _identityRoleManager;
        private readonly IMapper _mapper;

        public RoleGetQueryHandler(IIdentityRoleManager identityRoleManager, IMapper mapper)
        {
            _identityRoleManager = identityRoleManager;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<RoleDto>>> Handle(RoleGetQuery request, CancellationToken cancellationToken)
        {
            ApiResult<List<RoleDto>> resutl = new();

            var data = await _identityRoleManager.GetAll();

            resutl.Value = _mapper.Map<List<RoleDto>>(data);
            resutl.Success();
            return resutl;
        }
    }
    
}
