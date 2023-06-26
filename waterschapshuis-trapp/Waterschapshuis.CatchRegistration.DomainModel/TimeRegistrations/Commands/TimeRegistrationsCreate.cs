using JetBrains.Annotations;
using MediatR;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    public static partial class TimeRegistrationsCreate
    {
        [PublicAPI]
        public class Command : IRequest<Unit>
        {
            private List<TrackingItem> _trackingItems = new List<TrackingItem>();

            public IReadOnlyList<TrackingItem> TrackingItems => _trackingItems.AsReadOnly();

            public void AddTrackingItem(Tracking tracking, Guid userId)
            {
                _trackingItems.Add(TrackingItem.Create(tracking, userId));
            }

            public static Command Create(List<Tracking> trackings, Guid userId)
            {
                var command = new Command();
                trackings.ForEach(tracking =>
                    command.AddTrackingItem(tracking, userId)
                );
                return command;
            }

            public static Command Create(List<Tracking> trackings)
            {
                var command = new Command();
                trackings.ForEach(tracking =>
                    command.AddTrackingItem(tracking, tracking.CreatedById)
                );
                return command;
            }
        }

        [PublicAPI]
        public class TrackingItem
        {
            public Guid SessionId { get; set; }
            public Guid TrackingId { get; set; }
            public Guid UserId { get; set; }
            public Guid TrappingTypeId { get; set; }
            public DateTimeOffset RecordedOn { get; set; }
            public Point Location { get; set; } = Point.Empty;

            public static TrackingItem Create(Tracking tracking, Guid userId) =>
                new TrackingItem
                {
                    SessionId = tracking.SessionId,
                    TrackingId = tracking.Id,
                    UserId = userId,
                    TrappingTypeId = tracking.TrappingTypeId,
                    RecordedOn = tracking.RecordedOn,
                    Location = tracking.Location
                };

            public static TrackingItem Create(Tracking tracking) =>
                Create(tracking, tracking.CreatedById);
        }
    }
}
