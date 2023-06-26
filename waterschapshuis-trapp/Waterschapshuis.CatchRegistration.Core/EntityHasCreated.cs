using System;

namespace Waterschapshuis.CatchRegistration.Core
{
    public abstract class EntityHasCreated<TId> : Entity<TId>, IEntityHasCreated
    {
        public DateTimeOffset CreatedOn { get; private set; }
        public Guid CreatedById { get; private set; }
        
        public virtual void SetCreated(DateTimeOffset createdOn, Guid createdById)
        {
            CreatedOn = createdOn;
            CreatedById = createdById;
        }
    }
}
