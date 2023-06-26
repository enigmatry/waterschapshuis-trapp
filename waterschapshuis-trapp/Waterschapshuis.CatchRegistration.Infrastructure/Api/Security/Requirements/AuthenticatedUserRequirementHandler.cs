using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements
{
    public abstract class AuthenticatedUserRequirementHandler<TRequirement>
        : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement
    {
        protected readonly ICurrentUserProvider _currentUserProvider;
        protected readonly ILogger<AuthenticatedUserRequirementHandler<TRequirement>> _logger;

        protected AuthenticatedUserRequirementHandler(
            ICurrentUserProvider currentUserProvider,
            ILogger<AuthenticatedUserRequirementHandler<TRequirement>> logger)
        {
            _currentUserProvider = currentUserProvider;
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            if (!_currentUserProvider.IsAuthenticated)
            {
                _logger.LogWarning("User not authenticated.");
                context.Fail();
                return Task.CompletedTask;
            }

            if (_currentUserProvider.UserAuthorizationStatus == UserAuthorizationStatus.NotFound)
            {
                _logger.LogWarning(
                    "User with {Email} is authenticated but not found in the database.", _currentUserProvider.Email);
                context.Fail();
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
