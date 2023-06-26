using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements
{
    public static class ControllerSecuredWithUserHasPermissionAttribute
    {
        public class Requirement : IAuthorizationRequirement
        {
        }

        public class RequirementHandler : AuthorizationHandler<Requirement>
        {
            private readonly ILogger<RequirementHandler> _logger;

            public RequirementHandler(ILogger<RequirementHandler> logger)
            {
                _logger = logger;
            }

            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirement requirement)
            {
                IEnumerable<IAuthorizationRequirement> requirements = context.Requirements;
                if(!requirements.Any(r => r is UserHasPermission.Requirement))
                {
                    (string actionName, string controlerName) resource = GetActionAndControllerNames(context);

                    const string requirementName = nameof(UserHasPermission.Requirement);
                    const string attributeName = nameof(UserHasPermissionAttribute);
                    _logger.LogWarning("{Requirement} has not been found in the authorization context for {@Resource}. This usually means that user has not added {Attribute} on the controller.", requirementName, resource, attributeName);
                    context.Fail();
                }
                else
                {
                    context.Succeed(requirement);
                    _logger.LogDebug("Requirement succeeded");
                }
                return Task.CompletedTask;
            }

            private static (string actionName, string controlerName) GetActionAndControllerNames(AuthorizationHandlerContext context)
            {
                var result = (actionName: String.Empty, controlerName: String.Empty);
                if (context.Resource == null)
                {
                    return result;
                }

                if (context.Resource is AuthorizationFilterContext authContext)
                {
                    if (authContext.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
                    {
                        result.actionName = actionDescriptor.ActionName;
                        result.controlerName = actionDescriptor.ControllerName;
                    }
                }
                return result;
            }
        }
    }
}
