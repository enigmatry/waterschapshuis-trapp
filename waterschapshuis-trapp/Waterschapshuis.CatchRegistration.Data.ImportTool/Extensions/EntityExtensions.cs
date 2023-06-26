using System;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class EntityExtensions
    {
        public static EntityHasCreatedUpdated<TId> PopulateCreatedUpdated<TId>(
            this EntityHasCreatedUpdated<TId> entity,
            Guid userId,
            DateTimeOffset date)
        {
            entity.SetCreated(date, userId);
            entity.SetUpdated(date, userId);

            return entity;
        }

        public static EntityHasCreatedUpdatedRecorded<TId> PopulateCreatedUpdatedRecorded<TId>(
            this EntityHasCreatedUpdatedRecorded<TId> entity,
            Guid userId,
            DateTimeOffset date)
        {
            entity.SetCreated(date, userId);
            entity.SetUpdated(date, userId);
            entity.SetRecorded(date);

            return entity;
        }
    }
}
