using System;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.ApplicationServices.Identity.Claims;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Identity
{
    [UsedImplicitly]
    public class CurrentUserIdProvider : ICurrentUserIdProvider
    {
        private readonly IClaimsPrincipalProvider _principalProvider;
        private Guid? _userId;

        public CurrentUserIdProvider(IClaimsPrincipalProvider principalProvider)
        {
            _principalProvider = principalProvider;
        }

        private ClaimsPrincipal? Principal => _principalProvider.Principal;
        public string Email => IsAuthenticated ? _principalProvider.Email : String.Empty;
        public bool IsAuthenticated => Principal?.Identity.IsAuthenticated ?? false;

        public Guid? FindUserId(IQueryable<User> query)
        {
            var email = Email;

            if (email.IsNotNullOrEmpty())
            {
                if (_userId != null)
                {
                    return _userId;
                }

                _userId = query.QueryByEmail(email).Select(u => (Guid?)u.Id).SingleOrDefault();
                return _userId;
            }
            return null;
        }
    }
}
