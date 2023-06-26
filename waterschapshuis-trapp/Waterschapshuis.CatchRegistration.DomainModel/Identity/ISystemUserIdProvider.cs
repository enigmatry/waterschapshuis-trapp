using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity
{
    public interface ISystemUserIdProvider
    {
        Guid UserId
        {
            get;
        }
    }
}
