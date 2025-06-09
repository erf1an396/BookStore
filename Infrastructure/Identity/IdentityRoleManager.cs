using Application.Contracts.Identity;
using Application.Models;
using Domain.Entities;
using Infrastructure.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityRoleManager : IIdentityRoleManager
    {

        private readonly RoleManager<ApplicationRole> _roleManager;

        public IdentityRoleManager(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
            
        }

        public async Task<ApiResult> CreateAsync(ApplicationRole applcationRole)
        {
            return (await _roleManager.CreateAsync(applcationRole)).ToApiResult();
        }

        public async Task<ApiResult> UpdateRoleWithClaimsAsync(Guid id , string name , List<string> ClaimValue)
        {
            ApiResult result = new();

            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id.ToString());
            if (applicationRole == null)
            {
                result.Fail(ApiResultStaticMessage.NotFound);
                return result;
            }

            applicationRole.Name = name;
            var updateApiResult = (await _roleManager.UpdateAsync(applicationRole)).ToApiResult();

            if (!updateApiResult.IsSuccess)
                return updateApiResult;

            IList<System.Security.Claims.Claim> claims = await _roleManager.GetClaimsAsync(applicationRole);
            foreach (var item in claims.Where(x => !ClaimValue.Contains(x.Value)))
                {
                await _roleManager.RemoveClaimAsync(applicationRole , item);
            }

            foreach ( var item in ClaimValue.Where(x => !claims.Select(z =>z.Value).Contains(x)))
            {
                await _roleManager.AddClaimAsync(applicationRole, new System.Security.Claims.Claim(ConstantPolicies.DynamicPermission , item));
            }

            result.Success(ApiResultStaticMessage.UpdateSuccessfully);
            return result;

            
        }

        public async Task<ApiResult> DeleteAsync(Guid id)
        {
            ApplicationRole applicatioinRole = await _roleManager.FindByIdAsync(id.ToString());
            if (applicatioinRole == null)
            {
                ApiResult result = new();
                result.Fail(ApiResultStaticMessage.NotFound);   
                return result;
            }
            return (await _roleManager.DeleteAsync(applicatioinRole)).ToApiResult();
        }

        public async Task<List<ApplicationRole>> GetAll()
        {
            return await _roleManager.Roles.ToListAsync();
           
        }

        public async Task<ApplicationRole> FindById(Guid id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }


        public async Task<ApplicationRole> FindByNameAsync(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }
        public async Task<List<Claim>> GetClaimsAsync(ApplicationRole applicationRole)
        {
            return (await _roleManager.GetClaimsAsync(applicationRole)).ToList();
        }

        public async Task<bool> RoleExistAsync(string name)
        {
           var role = await _roleManager.FindByNameAsync(name);
            return role != null;
        }



    }
}
