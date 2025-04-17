using Application.Contracts.Identity;
using Application.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Identity
{
    public class MvcActionsDiscovery : IMvcActionsDiscovery
    {
        private readonly ConcurrentDictionary<string, Lazy<ICollection<ControllerVM>>> _allSecuredActionsWithPolicy = new();

        public ICollection<ControllerVM> MvcControllers { get; private set; }

        public MvcActionsDiscovery(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            if (actionDescriptorCollectionProvider == null)
            {
                throw new ArgumentNullException(nameof(actionDescriptorCollectionProvider));
            }


            MvcControllers = new List<ControllerVM>();
            string lastContorllerName = string.Empty;
            ControllerVM currentController = null;


            IReadOnlyList<Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor> actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor actionDescriptor in actionDescriptors)
            {
                if (!(actionDescriptor is ControllerActionDescriptor descriptor))
                {
                    continue;
                }

                TypeInfo controllerTypeInfo = descriptor.ControllerTypeInfo;
                MethodInfo actionMethodInfo = descriptor.MethodInfo;

                if (lastContorllerName != descriptor.ControllerName)
                {
                    currentController = new ControllerVM
                    {
                        AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                        ControllerAttributes = GetAttributes(controllerTypeInfo),
                        ControllerDisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                        ControllerName = descriptor.ControllerName,
                    };
                    MvcControllers.Add(currentController);

                    lastContorllerName = descriptor.ControllerName;
                }

                currentController?.MvcActions.Add(new ActionVM
                {
                    ControllerId = currentController.ControllerId,
                    ActionName = descriptor.ActionName,
                    ActionDisplayName = actionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    ActionAttributes = GetAttributes(actionMethodInfo),
                    IsSecuredAction = IsSecuredAction(controllerTypeInfo, actionMethodInfo)
                });
            }
        }

        public ICollection<ControllerVM> GetAllSecuredControllerActionsWithPolicy(string policyName)
        {
            Lazy<ICollection<ControllerVM>> getter = _allSecuredActionsWithPolicy.GetOrAdd(policyName, y => new Lazy<ICollection<ControllerVM>>(
                () =>
                {
                    List<ControllerVM> controllers = new(MvcControllers);
                    foreach (ControllerVM controller in controllers)
                    {
                        controller.MvcActions = controller.MvcActions.Where(
                            model => model.IsSecuredAction &&
                            (
                            model.ActionAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName ||
                            controller.ControllerAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName
                            )).ToList();
                    }
                    return controllers.Where(model => model.MvcActions.Any()).ToList();
                }));
            return getter.Value;
        }

        private static List<Attribute> GetAttributes(MemberInfo actionMethodInfo)
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
        private static bool IsSecuredAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
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


    }
}
