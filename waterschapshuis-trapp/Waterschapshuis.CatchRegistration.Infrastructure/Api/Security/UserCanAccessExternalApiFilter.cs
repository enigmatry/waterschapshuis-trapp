using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security
{
    public class UserCanAccessExternalApiFilter : AuthorizeFilter
    {
        public UserCanAccessExternalApiFilter(string policy) : base(policy) { }
        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await base.OnAuthorizationAsync(context);

            var logger = context.HttpContext.Resolve<ILogger<UserCanAccessExternalApiFilter>>();

            if (context.Result is ForbidResult || context.Result is ChallengeResult)
            {
                logger.LogWarning($"Forbidden access. Uri: {context.HttpContext.Request.GetUri()}");
            }
        }
    }
}
