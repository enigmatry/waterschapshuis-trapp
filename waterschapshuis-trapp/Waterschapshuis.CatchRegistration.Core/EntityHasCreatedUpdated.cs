using System;

namespace Waterschapshuis.CatchRegistration.Core
{
    public abstract class EntityHasCreatedUpdated<TId> : EntityHasCreated<TId>, IEntityHasCreatedUpdated
    {
        public DateTimeOffset UpdatedOn { get; private set; }
        public Guid UpdatedById { get; private set; }

        public void SetUpdated(DateTimeOffset updatedOn, Guid updatedBy)
        {
            UpdatedOn = updatedOn;
            UpdatedById = updatedBy;
        }
    }
}
