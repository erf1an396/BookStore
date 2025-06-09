using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Contracts.Identity
{
    public interface IIdentityRoleManager
    {
        Task<ApiResult> CreateAsync(ApplicationRole applicationRole);

        Task<ApiResult> DeleteAsync(Guid id);   

        Task<ApplicationRole> FindById(Guid id);

        Task<ApplicationRole> FindByNameAsync(string name);

        Task<List<ApplicationRole>> GetAll();

        Task<List<Claim>> GetClaimsAsync(ApplicationRole applicationRole);

        Task<ApiResult> UpdateRoleWithClaimsAsync(Guid id , string name , List<string> ClaimValue);

        Task<bool> RoleExistAsync(string name);
    }

}
