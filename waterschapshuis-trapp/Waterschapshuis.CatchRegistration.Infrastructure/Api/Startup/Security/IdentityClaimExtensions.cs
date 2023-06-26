using System;
using System.Linq;
using System.Security.Claims;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup.Security
{
    public static class IdentityClaimExtensions
    {
        public static string ReadStringValue(this ClaimsIdentity claimsIdentity, string claimType)
        {
            return claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == claimType)?.Value ?? String.Empty;
        }

        public static Guid ReadGuidValue(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var value = ReadStringValue(claimsIdentity, claimType);
            Guid.TryParse(value, out Guid parsedValue);
            return parsedValue;
        }

        public static AutoCreateUserAfterLogin.Command ToCommand(this ClaimsIdentity claimsIdentity)
        {
            var cmd = new AutoCreateUserAfterLogin.Command
            {
                Email = claimsIdentity.ReadStringValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn"),
                Name = claimsIdentity.ReadStringValue("name"),
                GivenName = claimsIdentity.ReadStringValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"),
                Surname = claimsIdentity.ReadStringValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")
            };
            return cmd;
        }
    }
}
