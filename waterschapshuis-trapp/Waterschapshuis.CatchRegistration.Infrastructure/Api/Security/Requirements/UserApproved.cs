using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements
{
    public static class UserApproved
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

                UserAuthorizationStatus authorizationStatus = _currentUserProvider.UserAuthorizationStatus;
                var currentUser = new { _currentUserProvider.UserId, _currentUserProvider.Email };
                if (authorizationStatus == UserAuthorizationStatus.Success)
                {
                    context.Succeed(requirement);
                    _logger.LogDebug("Requirement succeeded");
                }
                else if (authorizationStatus == UserAuthorizationStatus.NotAuthorized)
                {
                    _logger.LogWarning("{@CurrentUser} has not been approved.", currentUser);
                }
                else
                {
                    _logger.LogWarning(
                        "{@CurrentUser} has authorization status: {AuthorizationStatus}.", currentUser, authorizationStatus);
                }

                return Task.CompletedTask;
            }
        }
    }
}
