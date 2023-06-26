using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Microsoft.AspNetCore.Http;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements
{
    public static class UserHasPermission
    {
        public class Requirement : IAuthorizationRequirement
        {
            public IReadOnlyList<PermissionId> PermissionIds { get; }

            public Requirement(IReadOnlyList<PermissionId> permissionIds)
            {
                PermissionIds = permissionIds;
            }
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

                if (_currentUserProvider.UserHasAnyPermission(requirement.PermissionIds))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var user = new {_currentUserProvider.UserId, _currentUserProvider.Email};
                    _logger.LogWarning("{@User} does not have appropriate permissions. Needed permissions: {@PermissionIds}.", user, requirement.PermissionIds);
                }

                return Task.CompletedTask;
            }
        }
    }
}
