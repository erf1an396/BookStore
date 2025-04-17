using Application.Features.MvcActionsDiscovery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Models;

namespace Infrastructure.Extentions
{
    public static class MvcActionExtension
    {
        public static List<Attribute> GetAttributes(this MemberInfo actionMethodInfo)
        {
            return actionMethodInfo.GetCustomAttributes(inherit: true)
                                   .Where(attribute =>
                                   {
                                       string attributeNamespace = attribute.GetType().Namespace;
                                       return attributeNamespace != typeof(CompilerGeneratedAttribute).Namespace &&
                                              attributeNamespace != typeof(DebuggerStepThroughAttribute).Namespace;
                                   })
                                    .Cast<Attribute>()
                                   .ToList();
        }

        public static bool IsSecuredAction(this MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            bool actionHasAllowAnonymousAttribute = actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(inherit: true) != null;
            if (actionHasAllowAnonymousAttribute)
            {
                return false;
            }

            bool controllerHasAuthorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
            if (controllerHasAuthorizeAttribute)
            {
                return true;
            }

            bool actionMethodHasAuthorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;


            if (actionMethodHasAuthorizeAttribute)
            {
                return true;
            }

            return false;
        }

        private static ApiControllerActionDto GetControllerAction(this ControllerActionDescriptor controllerActionDescriptor)
        {
            if (!(controllerActionDescriptor is ControllerActionDescriptor descriptor))
            {
                return null;
            }

            ApiControllerActionDto currentController = new();
            currentController.ControllerName = descriptor.ControllerName;
            currentController.ControllerDisplayName = descriptor.ControllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
            currentController.ActionName = descriptor.ActionName;
            currentController.ActionDisplayName = descriptor.MethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
            currentController.IsSecured = descriptor.ControllerTypeInfo.IsSecuredAction(descriptor.MethodInfo);
            //currentController.HttpMethod = descriptor.MethodInfo.htt;

            //currentController.ActionRoute = GetAttributes(actionMethodInfo).OfType<Microsoft.AspNetCore.Mvc.RouteAttribute>().FirstOrDefault()?.Template;


            List<AuthorizeAttribute> policy = descriptor.MethodInfo.GetAttributes().OfType<AuthorizeAttribute>().ToList();
            policy.AddRange(descriptor.ControllerTypeInfo.GetAttributes().OfType<AuthorizeAttribute>().ToList());

            currentController.Policy = string.Join("|", policy.Select(x => x.Policy).Where(x => !string.IsNullOrEmpty(x)));
            //mvcControllers.Add(currentController);

            return currentController;
        }
        public static Guid GetUserIdFromToken(this ClaimsPrincipal user)
        {
            try
            {
                return Guid.Parse(user.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public static void FillReqeustDetails<T>(this HttpRequest httpRequest, T model) where T : BaseRequestDto
        {
            var endpoint = httpRequest.HttpContext.GetEndpoint();
            var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            if (httpRequest.Headers.TryGetValue("Pagination-PageNumber", out var paginationPageNumber))
                if (int.TryParse(paginationPageNumber, out int pageNumber))
                    model.SetPageNumber(pageNumber);

            if (httpRequest.Headers.TryGetValue("Pagination-PageSize", out var paginationPageSize))
                if (int.TryParse(paginationPageSize, out int pageSize))
                    model.SetPageSize(pageSize);

            model.SetApiContorllerAction(actionDescriptor.GetControllerAction());

            model.SetUserId(httpRequest.HttpContext.User.GetUserIdFromToken());

            model.SetIpAddress(httpRequest.HttpContext.Connection.RemoteIpAddress.ToString());
        }
    }
}
