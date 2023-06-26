using System;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Identity
{
    [UsedImplicitly]
    public class SystemUserIdProvider : ISystemUserIdProvider
    {
        public Guid UserId
        {
            get
            {
                return User.SystemUserId; 
            }
        }
    }
}
