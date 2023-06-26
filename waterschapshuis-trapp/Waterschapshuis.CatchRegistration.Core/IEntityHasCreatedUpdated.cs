using System;

namespace Waterschapshuis.CatchRegistration.Core
{
    public interface IEntityHasCreatedUpdated : IEntityHasCreated
    {
        DateTimeOffset UpdatedOn { get; }
        Guid UpdatedById { get; }

        void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy);
    }
}
