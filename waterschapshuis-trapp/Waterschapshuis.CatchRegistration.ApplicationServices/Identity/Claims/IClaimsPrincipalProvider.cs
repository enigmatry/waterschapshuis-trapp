using System.Security.Claims;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Identity.Claims
{
    public interface IClaimsPrincipalProvider
    {
        public string Email { get; }
        public ClaimsPrincipal? Principal { get; }
        public ClaimsIdentity? Identity { get; }
    }
}
