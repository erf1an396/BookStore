using Application.Contracts.Identity;
using Application.Models;
using Application.Models.Options;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Extentions;


namespace Infrastructure.Identity
{
    public class IdentityUserManger : IIdentityUserManager
    {

        private readonly BookStoreContext _dbContext;
        private readonly JwtOption _option;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityRoleManager _identityRoleManager;

        public IdentityUserManger(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<JwtOption> options, BookStoreContext db, IIdentityRoleManager identityRoleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _option = options.Value;
            _dbContext = db;
            _identityRoleManager = identityRoleManager;
        }

        public async Task<ApiResult<Tuple<string, List<string>>>> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            ApiResult<Tuple<string, List<string>>> result = new();
            var signInResult = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure: lockoutOnFailure);
            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    List<string> claims = new();
                    IList<string> roles = await _userManager.GetRolesAsync(user);

                    foreach (var item in roles)
                    {
                        ApplicationRole applicationRole = await _identityRoleManager.FindByNameAsync(item);
                        if (applicationRole != null)
                        {
                            claims.AddRange((await _identityRoleManager.GetClaimsAsync(applicationRole)).Select(x => x.Value).ToList());
                        }
                    }

                    result.Value = Tuple.Create(GenerateJwtToken(user), claims);
                    result.Success();
                    return result;







                }
                else
                {
                    result.Fail("نام کاربری یا رمز عبور اشتباه است");
                    return result;
                }
            }
            else if (signInResult.RequiresTwoFactor)
            {
                result.Fail("کاربر فعال نمیباشد");


            }
            else if (signInResult.IsLockedOut)
            {
                result.Fail("کاربر مسددود شده است");
            }
            else
            {
                result.Fail("نام کاربری یا رمز عبور اشتباه است");
            }
            return result;

        }

        private string GenerateJwtToken(ApplicationUser applicationUser)
        {
            var prinicple = new ClaimsIdentity();
            prinicple.AddClaim(new Claim(ClaimTypes.NameIdentifier, applicationUser.Id.ToString()));//id
            string name = !string.IsNullOrEmpty(applicationUser.FirstName) ? applicationUser.FirstName + " " + applicationUser.LastName : applicationUser.UserName;
            prinicple.AddClaim(new Claim(ClaimTypes.Name, name));

            var TokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_option.SecretKey);
            var TokenDiscription = new SecurityTokenDescriptor
            {
                Subject = prinicple,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(TokenDiscription);

            return TokenHandler.WriteToken(token);
        }
        public async Task<bool> UserHasClaimValueAsync(Guid userId, string claimValue)
        {
            return await _dbContext.UserRoles.Include(x => x.ApplicationRole)
                .ThenInclude(x => x.ApplicationRoleClaims)
                .AnyAsync(x =>
                x.UserId == userId && x.ApplicationRole.ApplicationRoleClaims.Select(z => z.ClaimValue).Contains(claimValue));

        }
        public async Task<ApplicationUser> FindByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());

        }

        public async Task<ApiResult> UpdateAsync(ApplicationUser user)
        {
            ApiResult result = new();
            IdentityResult updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                result.Success(ApiResultStaticMessage.UpdateSuccessfully);
                return result;
            }
            if (updateResult.Errors.Where(x => !string.IsNullOrEmpty(x.Description)).Any())
            {
                foreach (var error in updateResult.Errors.Where(x => !string.IsNullOrEmpty(x.Description)))
                {
                    result.Fail(error.Description);
                }
            }
            else
                result.Fail(ApiResultStaticMessage.UnknownExeption);
            return result;



        }
        public async Task<ApiResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            ApiResult result = new();
            var changePassword = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (changePassword.Succeeded)
            {
                result.Success(ApiResultStaticMessage.UpdateSuccessfully);
                return result;
            }
            if (changePassword.Errors.Where(x => !string.IsNullOrEmpty(x.Description)).Any())
            {
                foreach (var error in changePassword.Errors.Where(x => !string.IsNullOrEmpty(x.Description)))
                {
                    result.Fail(error.Description);
                }
            }
            else
                result.Fail(ApiResultStaticMessage.UnknownExeption);
            return result;
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApiResult> CreateAsync(ApplicationUser user, string password)
        {
            return (await _userManager.CreateAsync(user, password)).ToApiResult();
        }
        public async Task<List<string>> GetRolesAsync(ApplicationUser user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }
        public async Task<ApiResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return (await _userManager.AddToRoleAsync(user, role)).ToApiResult();
        }
        public async Task<ApiResult> RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            return (await _userManager.RemoveFromRoleAsync(user, role)).ToApiResult();
        }
        public async Task<ApiResult> ChangePasswordAsync(ApplicationUser user, string password)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return (await _userManager.ResetPasswordAsync(user, token, password)).ToApiResult();
        }



    }
}