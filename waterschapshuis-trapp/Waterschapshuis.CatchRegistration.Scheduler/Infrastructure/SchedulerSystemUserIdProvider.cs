using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Scheduler.Infrastructure
{
    public class SchedulerSystemUserIdProvider : ICurrentUserIdProvider
    {
        public Guid? FindUserId(IQueryable<User> query) => User.SystemUserId;
        public string Email => String.Empty;
        public bool IsAuthenticated => true;
    }
}
