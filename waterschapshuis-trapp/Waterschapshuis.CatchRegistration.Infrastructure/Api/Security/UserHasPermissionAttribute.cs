using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security
{
    public class UserHasPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public UserHasPermissionAttribute(string policy) : base(policy) { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var logger = context.HttpContext.Resolve<ILogger<UserHasPermissionAttribute>>();

            if (context.Result is ForbidResult || context.Result is ChallengeResult)
            {
                logger.LogWarning($"Forbidden access. Uri: {context.HttpContext.Request.GetUri()}");
            }
        }
    }
}
