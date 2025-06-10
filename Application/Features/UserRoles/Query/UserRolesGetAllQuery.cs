using Application.Contracts.Identity;
using Application.Features.Role;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserRoles.Query
{
    public class UserRolesGetAllQuery : IRequest<ApiResult<List<Guid>>>
    {
        public Guid UserId { get; set; }
    }
    public class UserRolesGetAllQueryHandler : IRequestHandler<UserRolesGetAllQuery, ApiResult<List<Guid>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IIdentityRoleManager _identityRole;

        public UserRolesGetAllQueryHandler(UserManager<ApplicationUser> userManager , IMapper mapper , IIdentityRoleManager IdentityRole)
        {
            _userManager = userManager;
            _mapper = mapper;
            _identityRole = IdentityRole;
        }

        public async Task<ApiResult<List<Guid>>> Handle(UserRolesGetAllQuery request, CancellationToken cancellationToken)
        {
            ApiResult<List<Guid>> apiResult = new();

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
               apiResult.Fail(ApiResultStaticMessage.NotFound);

            }
            var roles = await _userManager.GetRolesAsync(user);
            List<Guid> roleId = new();

            foreach(var rolename in roles)
            {
                var role = await _identityRole.FindByNameAsync(rolename);
                if (role != null)
                {
                    roleId.Add(role.Id);
                }

            }
            
            apiResult.Value = roleId;
            apiResult.Success();
            return apiResult;
        }

    }
}
