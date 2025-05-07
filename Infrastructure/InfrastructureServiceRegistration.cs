using Application.Contracts;
using Application.Contracts.Identity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Infrastructure.Dbinitializer;
using Application.Contracts.Service.OutSide;
using Infrastructure.SignalRHub;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Application.Contracts.WebHostEnvironment;
using Infrastructure.WebHostEnvironment;
namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<BookStoreContext>(options =>
            
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMvcActionsDiscovery, MvcActionsDiscovery>();
            services.AddScoped<IBookStoreContext , BookStoreContext>();

            services.AddScoped<IIdentityUserManager , IdentityUserManger>();
            services.AddScoped<IIdentityRoleManager, IdentityRoleManager>();

            services.AddScoped<IDbInitializerService, DbInitializerService>();


            services.AddScoped<ISignalRService, SignalRService>();

            services.AddScoped<IWebHostEnvironmentAccessor , WebHostEnvironmentAccessor>


            services.AddOurAddIdentity(configuration);
            services.AddOurAddAuthentication(configuration);
            services.AddOurAuthorization();



            return services;
        }

        private static IServiceCollection AddOurAddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection("JWT").GetSection("SecretKey").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

            

            return services;
        }
        private static IServiceCollection AddOurAddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                options =>
                {
                    //Configure Password
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;

                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;

                    options.SignIn.RequireConfirmedEmail = false;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                    options.Lockout.MaxFailedAccessAttempts = 3;


                    options.ClaimsIdentity.UserIdClaimType = "nameid";
                    options.ClaimsIdentity.UserNameClaimType = "unique_name";
                    options.ClaimsIdentity.RoleClaimType = "role";
                })
                .AddEntityFrameworkStores<BookStoreContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            return services;
        }

        private static IServiceCollection AddOurAuthorization(this IServiceCollection services)
        {

            services.AddScoped<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddAuthorization(options =>
            {

                options.AddPolicy(ConstantPolicies.DynamicPermission,
                    policy =>
                    {
                        policy.Requirements.Add(new DynamicPermissionRequirement());
                        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);

                    }
                );
            });
            return services;
        }
        public static void AddOurInfrastructure(this WebApplication app)
        {
            app.RunDataBaseInitializer();
        }
    }
}
