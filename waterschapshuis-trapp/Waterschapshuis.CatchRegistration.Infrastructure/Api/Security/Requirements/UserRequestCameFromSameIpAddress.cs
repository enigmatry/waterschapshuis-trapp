using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Security.Requirements
{
    public static class UserRequestCameFromSameIpAddress
    {
        public class Requirement : IAuthorizationRequirement
        {
            public IHostEnvironment HostEnvironment { get; private set; }

            public Requirement(IHostEnvironment hostEnvironment)
            {
                HostEnvironment = hostEnvironment;
            }
        }

        public class RequirementHandler : AuthenticatedUserRequirementHandler<Requirement>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public RequirementHandler(
                ICurrentUserProvider currentUserProvider,
                ILogger<RequirementHandler> logger,
                IHttpContextAccessor httpContextAccessor)
                : base(currentUserProvider, logger)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            // https://stackoverflow.com/questions/53373832/check-ip-with-jwt-authorization-in-asp-net-core-web-api
            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirement requirement)
            {
                if (requirement.HostEnvironment.IsDevelopment())
                {
                    context.Succeed(requirement);
                    return;
                }

                await base.HandleRequirementAsync(context, requirement);

                if (context.HasFailed)
                {
                    return;
                }

                var ipAddresses = new RequestIpAddressess(
                    context.User.FindFirst(claim => claim.Type == "ipaddr"),
                    _httpContextAccessor.HttpContext.Connection.RemoteIpAddress
                );

                if (ipAddresses.DifferentIpAddresses)
                {
                    _logger.LogWarning($"Request was made from different IP address (Claim_Ip: {ipAddresses.ClaimIpAddress}, Remote_Ip: {ipAddresses.RemoteIpAddress}).");
                    context.Fail();
                    return;
                }

                context.Succeed(requirement);
            }
        }

        private class RequestIpAddressess
        {
            private readonly Claim? _ipAddressClaim;
            private readonly IPAddress? _remoteIpAddress;

            public RequestIpAddressess(Claim? ipAddressClaim, IPAddress? remoteIpAddress)
            {
                _ipAddressClaim = ipAddressClaim;
                _remoteIpAddress = remoteIpAddress;
            }

            public bool DifferentIpAddresses =>
                _ipAddressClaim != null &&
                _remoteIpAddress != null &&
                _ipAddressClaim.Value != _remoteIpAddress.ToString();

            public string ClaimIpAddress => _ipAddressClaim?.Value ?? String.Empty;
            public string RemoteIpAddress => _remoteIpAddress?.ToString() ?? String.Empty;
        }
    }
}
