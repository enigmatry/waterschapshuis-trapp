using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation
{
    internal class TestCurrentUserIdProvider : ICurrentUserIdProvider
    {
        private Guid UserId { get; }
        public Guid? FindUserId(IQueryable<User> query) { return UserId; }
        public string Email { get; }
        public bool IsAuthenticated => true;

        public TestCurrentUserIdProvider(TestPrincipal principal)
        {
            UserId = principal.UserId;
            Email = principal.Email;
        }
    }
}
