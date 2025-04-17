using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Identity
{
    public interface IIdentityUserManager
    {
        Task<bool> UserHasClaimValueAsync(Guid userId, string claimValue);
        Task<ApplicationUser> FindByIdAsync(Guid userId);
        Task<ApiResult> UpdateAsync(ApplicationUser user);
        Task<ApiResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task<List<ApplicationUser>> GetUsersAsync();
        Task<ApiResult> CreateAsync(ApplicationUser user, string password);
        Task<List<string>> GetRolesAsync(ApplicationUser user);
        Task<ApiResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<ApiResult> RemoveFromRoleAsync(ApplicationUser user, string role);
        Task<ApiResult> ChangePasswordAsync(ApplicationUser user, string password);
        Task<ApiResult<Tuple<string, List<string>>>> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
    }
}
