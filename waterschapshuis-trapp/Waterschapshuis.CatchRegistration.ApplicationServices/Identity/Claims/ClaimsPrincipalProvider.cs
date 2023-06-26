using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Logging;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Identity.Claims
{
    public class ClaimsPrincipalProvider : IClaimsPrincipalProvider
    {
        private readonly ILogger<ClaimsPrincipalProvider> _logger;

        public ClaimsPrincipalProvider(Func<IPrincipal> principalProvider, ILogger<ClaimsPrincipalProvider> logger)
        {
            _logger = logger;

            Principal = principalProvider() as ClaimsPrincipal;
            Identity = Principal?.Identity as ClaimsIdentity;
            Email = GetEmail();
        }

        public ClaimsPrincipal? Principal { get; }
        public ClaimsIdentity? Identity { get; }
        public string Email { get; }

        private string GetEmail()
        {
            if (Identity == null)
            {
                return String.Empty;
            }

            var email =
                GetClaimValue(ClaimTypes.Upn) // included in v1.0 tokens, but isn't included in v2.0 by default
                ?? GetClaimValue(ClaimTypes.PreferredUsername) // included in v2.0 tokens
                ?? GetClaimValue(ClaimTypes.Email); // fallback to email if upn or preferred_username claims are not found

            return email ?? String.Empty;
        }

        private string? GetClaimValue(string claimType)
        {
            var claim = Identity?.Claims.FirstOrDefault(c => c.Type == claimType);
            if (claim == null)
            {
                _logger.LogWarning($"Could not find claim of type: {claimType} in the ClaimsIdentity.");
            }
            return claim?.Value;
        }

        private static class ClaimTypes
        {
            public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";
            public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
            public const string PreferredUsername = "preferred_username";
        }
    }
}
