using System;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.Core
{
    public static class EntityExtensions
    {
        public static T WithNextSequentialId<T>(this T entity) where T: Entity<Guid>
        {
            return entity.WithId(SequentialGuidGenerator.Generate());
        }

        public static T WithId<T, TId>(this T entity, TId id) where T: Entity<TId>
        {
            entity.Id = id;
            return entity;
        }
    }
}
