using System;

namespace Waterschapshuis.CatchRegistration.Core
{
    public static class EntityHasCreatedUpdatedExtensions
    {
        public static T WithCreated<T>(this T entity, DateTimeOffset createdOn, Guid createdBy)
            where T : IEntityHasCreated
        {
            entity.SetCreated(createdOn, createdBy);
            return entity;
        }

        public static T WithUpdated<T>(this T entity, DateTimeOffset updatedOn, Guid updatedBy)
            where T : IEntityHasCreatedUpdated
        {
            entity.SetUpdated(updatedOn, updatedBy);
            return entity;
        }
    }
}
