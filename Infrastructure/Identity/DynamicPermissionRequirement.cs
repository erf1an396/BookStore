using Application.Contracts.Identity;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }

    public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly IIdentityUserManager _identityUserMnager;

        public DynamicPermissionsAuthorizationHandler(IIdentityUserManager identityUserManager)
        {
            _identityUserMnager = identityUserManager;
        }

        protected override async Task HandleRequirementAsync
            (AuthorizationHandlerContext context,
            DynamicPermissionRequirement requirement)

        {
            AuthorizationFilterContext filterContext = context.Resource as AuthorizationFilterContext;
            HttpResponse response = filterContext?.HttpContext.Response;


            if (!context.User.Identity.IsAuthenticated || string.IsNullOrEmpty(context.User.Identity.Name))
            {
                response?.OnStarting(async () =>
                {
                    filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                    context.Fail();
                    await Task.CompletedTask;
                });
            }
            else if (context.Resource is HttpContext mvcContext)
            {
                ControllerActionDescriptor cad =  mvcContext.GetEndpoint().Metadata.OfType<ControllerActionDescriptor>().FirstOrDefault();


                cad.RouteValues.TryGetValue("area", out string areaName);
                string area = string.IsNullOrWhiteSpace(areaName) ? string.Empty : areaName;

                cad.RouteValues.TryGetValue("controller", out string controllerName);
                string controller = string.IsNullOrWhiteSpace(controllerName) ? string.Empty : controllerName;

                cad.RouteValues.TryGetValue("action" , out string actionName);
                string action = string.IsNullOrWhiteSpace(actionName) ? string.Empty : actionName;


                var userDataClaim = (context.User.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                Guid userId = Guid.Parse((string)Convert.ChangeType(userDataClaim.Value, typeof(string), CultureInfo.InvariantCulture));

                string currentClaimValue = $"{area}:{controller}:{action}";

                if ( await _identityUserMnager.UserHasClaimValueAsync(userId , currentClaimValue))
                {
                    context.Succeed(requirement);

                }
                await Task.CompletedTask;

            }


            response?.OnStarting(async () =>
            {
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                context.Fail();
                await Task.CompletedTask;
            });
        }
    }
}
