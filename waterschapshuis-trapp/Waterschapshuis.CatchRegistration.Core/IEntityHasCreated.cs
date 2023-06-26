using System;

namespace Waterschapshuis.CatchRegistration.Core
{
    public interface IEntityHasCreated : IEntity
    {
        DateTimeOffset CreatedOn { get; }
        Guid CreatedById { get; }

        void SetCreated(DateTimeOffset createdOn, Guid createdById);
    }
}
