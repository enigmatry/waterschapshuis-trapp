using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity
{
    public interface ICurrentUserIdProvider
    {
        // we do method injection of IQueryable to avoid circular dependency between DbContext and CurrentUserIdProvider
        Guid? FindUserId(IQueryable<User> query);
        string Email { get; }
        bool IsAuthenticated { get; }
    }
}
