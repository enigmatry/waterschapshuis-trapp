using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements
{
    public static class UserCanAccessMobile
    {
        public class Requirement : IAuthorizationRequirement
        {
        }

        public class RequirementHandler : AuthenticatedUserRequirementHandler<Requirement>
        {
            public RequirementHandler(
                ICurrentUserProvider currentUserProvider,
                ILogger<RequirementHandler> logger)
                : base(currentUserProvider, logger) { }

            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirement requirement)
            {
                base.HandleRequirementAsync(context, requirement);

                if (context.HasFailed)
                {
                    return Task.CompletedTask;
                }

                if (_currentUserProvider.UserHasAnyPermission(new List<PermissionId> { PermissionId.Mobile }))
                {
                    context.Succeed(requirement);
                    _logger.LogDebug("Requirement succeeded");
                }
                else
                {
                    var currentUser = new { _currentUserProvider.UserId, _currentUserProvider.Email };
                    _logger.LogWarning("{@CurrentUser} does not have permission to access mobile.", currentUser);
                }

                return Task.CompletedTask;
            }
        }
    }
}
