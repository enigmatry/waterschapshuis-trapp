using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework
{
    public static class ChangeTrackerExtensions
    {
        public static List<EntityEntry<Entity>> GetChangedEntitiesWithHCreatedUpdated(this ChangeTracker changeTracker) =>
            changeTracker
                .Entries<Entity>()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified) && x.Entity is IEntityHasCreatedUpdated)
                .ToList();
    }
}
