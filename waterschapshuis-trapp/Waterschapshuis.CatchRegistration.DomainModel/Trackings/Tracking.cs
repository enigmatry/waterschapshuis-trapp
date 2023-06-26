using System;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings.DomainEvents;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings
{
    public class Tracking : EntityHasCreatedUpdatedRecorded<Guid>
    {
        private Tracking() { }
        
        public Point Location { get; private set; } = Point.Empty;
        public Guid TrappingTypeId { get; private set; } = Guid.Empty;
        public TrappingType TrappingType { get; private set; } = null!;
        public Guid SessionId { get; private set; } = Guid.Empty;

        public User CreatedBy { get; } = null!;
        public User UpdatedBy { get; } = null!;

        public bool IsTimewriting { get; set; }
        public bool IsTrackingMap { get; set; }
        public bool isTrackingPrivate { get; set; }

        public static Tracking Create(TrackingSync.Command.TrackingItem item)
        {
            var result = new Tracking
            {
                Id = Guid.Parse(item.Id),
                Location = GeometryUtil.Factory.CreatePoint(item.Longitude, item.Latitude),
                TrappingTypeId = item.TrappingTypeId,
                SessionId = item.SessionId,
                IsTimewriting = item.IsTimewriting,
                IsTrackingMap = item.IsTrackingMap,
                isTrackingPrivate = item.IsTrackingPrivate
            };
            result.SetRecorded(item.RecordedOn);

            result.AddDomainEvent(new TrackingCreatedDomainEvent(result));

            return result;
        }
    }
}
