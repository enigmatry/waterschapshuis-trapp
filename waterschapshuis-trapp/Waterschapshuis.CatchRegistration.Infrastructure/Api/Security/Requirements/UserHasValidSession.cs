using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.UserSessions;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Filters;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements
{
    public static class UserHasValidSession
    {
        public class Requirement : IAuthorizationRequirement { }

        public class RequirementHandler : AuthenticatedUserRequirementHandler<Requirement>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserSessionSettings _userSessionSettings;
            private readonly IMediator _mediator;

            public RequirementHandler(
                ICurrentUserProvider currentUserProvider,
                ILogger<RequirementHandler> logger,
                IHttpContextAccessor httpContextAccessor,
                UserSessionSettings userSessionSettings,
                IMediator mediator)
                : base(currentUserProvider, logger)
            {
                _httpContextAccessor = httpContextAccessor;
                _userSessionSettings = userSessionSettings;
                _mediator = mediator;
            }

            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirement requirement)
            {
                if (ShouldIgnoreSessionValidation(_userSessionSettings, context))
                {
                    context.Succeed(requirement);
                    return;
                }

                await base.HandleRequirementAsync(context, requirement);

                if (context.HasFailed)
                {
                    return;
                }

                var accessToken = AccessToken.CreateFromHeaders(_httpContextAccessor.HttpContext.Request.Headers);
                var query = ValidateAccessToken.Query.Create(accessToken, _userSessionSettings.SessionOrigin);

                ValidateAccessToken.Response validationResult = await _mediator.Send(query);

                if (!validationResult.IsValid)
                {
                    if (!_currentUserProvider.UserId.HasValue)
                    {
                        _logger.LogWarning("Missing user id");
                        context.Fail();
                        return;
                    }
                    
                    var command = CreateOrUpdateUserSession.Command.Create(accessToken, _currentUserProvider.UserId.Value, _userSessionSettings.SessionOrigin);
                    var result = await _mediator.Send(command);

                    if (!result.IsSuccess)
                    {
                        _logger.LogWarning("Unable to create session. Reason: {Reason}", result.FailReason);
                        _httpContextAccessor.HttpContext.Response.WithInvalidUserSessionHeader();
                        context.Fail();
                        return;
                    }
                }

                context.Succeed(requirement);
            }

            private static bool ShouldIgnoreSessionValidation(UserSessionSettings userSessionSettings, AuthorizationHandlerContext context)
            {
                if (!userSessionSettings.SessionsEnabled)
                {
                    return true;
                }

                if (context.Resource is AuthorizationFilterContext authContext)
                {
                    if (authContext.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
                    {
                        return actionDescriptor.MethodInfo.CustomAttributes
                            .Any(x => x.AttributeType == typeof(IgnoreUserSessionValidationAttribute));
                    }
                }

                return false;
            }
        }
    }
}
