using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework.MediatR
{
    internal static class MediatorExtension
    {
        public static List<INotification> GatherExceptHistoryDomainEventsFromContext(this DbContext ctx) =>
            GetEntitiesWithDomainEvents(ctx)
                .SelectMany(x => x.Entity.DomainEvents)
                .QueryButCreateEntityDomainEvents().ToList();

        public static List<INotification> GatherHistoryDomainEventsFromContext(this DbContext ctx) =>
            GetEntitiesWithDomainEvents(ctx)
                .SelectMany(x => x.Entity.DomainEvents)
                .QueryCreateEntityDomainEvents().ToList();

        public static void ClearDomainEvents(this DbContext ctx) =>
            GetEntitiesWithDomainEvents(ctx)
                .ForEach(entity => entity.Entity.ClearDomainEvents());

        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IEnumerable<INotification> domainEvents, ILogger logger)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            // sequentially publish domain events to avoid problems with same DbContext used by different threads 
            // fixes problem "A second operation started on this context before a previous operation completed"
            // this happens when one event handler is doing DbContext saving while some other one is doing the reading
            foreach (INotification domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                logger.LogDebug("Time to publish domain event - {DomainEvent}: {Time}s",domainEvent.GetType(), ts.TotalSeconds);
                stopWatch.Restart();
            }
        }


        private static List<EntityEntry<Entity>> GetEntitiesWithDomainEvents(DbContext ctx) =>
            ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any()).ToList();

        private static IEnumerable<INotification> QueryCreateEntityDomainEvents(this IEnumerable<INotification> events) =>
            events.Where(x => x.GetType().IsSubclassOf(typeof(HistoryDomainEvent)));

        private static IEnumerable<INotification> QueryButCreateEntityDomainEvents(this IEnumerable<INotification> events) =>
            events.Where(x => !x.GetType().IsSubclassOf(typeof(HistoryDomainEvent)));
    }
}
