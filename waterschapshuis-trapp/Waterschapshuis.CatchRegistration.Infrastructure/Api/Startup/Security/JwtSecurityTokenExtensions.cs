using System;
using System.IdentityModel.Tokens.Jwt;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup.Security
{
    public static class JwtSecurityTokenExtensions
    {
        public static string ReadValueFromPayload(this JwtSecurityToken jwtSecurityToken, string key)
        {
            var value = jwtSecurityToken.Payload[key];
            return value != null ? value.ToString()?? String.Empty : String.Empty;
        }
    }
}
