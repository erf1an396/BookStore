using Application.Contracts.Identity;
using Application.Models;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dbinitializer
{
    public interface IDbInitializerService
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }
    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        private readonly string name = "admin";
        private readonly string password = "Admin@123";
        private readonly string email = "admin@my.store";
        private readonly string roleName = "مدیر سیستم";
        private readonly string firstName = "مدیر";
        private readonly string lastName = "سیستم";

        public DbInitializerService(
            IServiceScopeFactory scopeFactory, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _scopeFactory = scopeFactory;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            using (IServiceScope serviceScope = _scopeFactory.CreateScope())
            {
                using (BookStoreContext context = serviceScope.ServiceProvider.GetService<BookStoreContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (IServiceScope serviceScope = _scopeFactory.CreateScope())
            {
                ILogger<DbInitializerService> logger = serviceScope.ServiceProvider.GetService<ILogger<DbInitializerService>>();
                using (BookStoreContext dbContext = serviceScope.ServiceProvider.GetService<BookStoreContext>())
                {
                    SeedDatabaseWithAdminUserAsync().Wait();
                    SeedRolesAsync().Wait();
                    AddClaimForAdmin(serviceScope, dbContext).Wait();
                    //AddDefaultConfig(dbContext).Wait();
                }
            }
        }

        private async Task<IdentityResult> SeedDatabaseWithAdminUserAsync()
        {
            ApplicationUser adminUser = await _userManager.FindByNameAsync(name);
            if (adminUser != null)
            {
                ApplicationRole adminRoleSeed = await _roleManager.FindByNameAsync(roleName);
                if (adminRoleSeed != null)
                {
                    //await AddClaimForAdmin(adminUser, adminRoleSeed);
                }
                return IdentityResult.Success;
            }

            //Create the `Admin` Role if it does not exist
            ApplicationRole adminRole = await _roleManager.FindByNameAsync(roleName);
            if (adminRole == null)
            {
                adminRole = new ApplicationRole()
                {
                    Name = roleName,
                };
                IdentityResult adminRoleResult = await _roleManager.CreateAsync(adminRole);
                if (adminRoleResult == IdentityResult.Failed())
                {
                    return IdentityResult.Failed();
                }
            }
            else
            {
            }

            adminUser = new ApplicationUser
            {
                UserName = name,
                Email = email,
                EmailConfirmed = true,
                LockoutEnabled = false,
                FirstName = firstName,
                LastName = lastName,
                IsAdmin = true,
                IsActive = true,
            };

            IdentityResult adminUserResult = await _userManager.CreateAsync(adminUser, password);
            if (adminUserResult == IdentityResult.Failed())
            {
                return IdentityResult.Failed();
            }

            IdentityResult setLockoutResult = _userManager.SetLockoutEnabledAsync(adminUser, enabled: false).Result;
            if (setLockoutResult == IdentityResult.Failed())
            {
                return IdentityResult.Failed();
            }

            IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(adminUser, adminRole.Name);
            if (addToRoleResult == IdentityResult.Failed())
            {
                return IdentityResult.Failed();
            }
            return IdentityResult.Success;
        }

        private async Task SeedRolesAsync()
        {
            ApplicationRole roleAdmin = await _roleManager.FindByNameAsync(roleName);
            if (roleAdmin == null)
            {
                roleAdmin = new()
                {
                    Name = roleName
                };
                await _roleManager.CreateAsync(roleAdmin);
            }
        }
        private async Task AddClaimForAdmin(IServiceScope serviceScope, BookStoreContext letterContext)
        {
            IMvcActionsDiscovery mvcActionsDiscoveryService = serviceScope.ServiceProvider.GetService<IMvcActionsDiscovery>();

            ApplicationRole roleAdmin = await _roleManager.FindByNameAsync(roleName);
            IList<Claim> claimlist = new List<Claim>();
            List<ApplicationRoleClaim> roleClaim = new();

            foreach (ControllerVM controller in mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission))
            {
                foreach (ActionVM action in controller.MvcActions)
                {
                    claimlist.Add(new Claim(ConstantPolicies.DynamicPermission, action.ActionId));
                    roleClaim.Add(new ApplicationRoleClaim()
                    {
                        RoleId = roleAdmin.Id,
                        ClaimType = ConstantPolicies.DynamicPermission,
                        ClaimValue = action.ActionId,
                    });
                }
            }
            var claims = letterContext.RoleClaims.Where(x => x.RoleId == roleAdmin.Id).Select(x => x.ClaimValue).ToList();

            foreach (var item in claims)
            {
                roleClaim.RemoveAll(x => x.ClaimValue == item);
            }

            await letterContext.RoleClaims.AddRangeAsync(roleClaim);
            await letterContext.SaveChangesAsync();
        }

        //private async Task AddDefaultConfig(StoreContext dbContext)
        //{
        //    if (!await dbContext.Configs.AnyAsync(x => x.Type == Domain.Enums.ConfigTypeEnum.StoreName))
        //    {
        //        await dbContext.Configs.AddAsync(new Config()
        //        {
        //            Type = Domain.Enums.ConfigTypeEnum.StoreName,
        //            Value = "فروشگاه هوش پرداز"
        //        });
        //        await dbContext.SaveChangesAsync();
        //    }
        //}

    }
}
