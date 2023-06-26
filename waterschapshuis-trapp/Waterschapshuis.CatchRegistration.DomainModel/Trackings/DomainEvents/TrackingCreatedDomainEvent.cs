using System;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings.DomainEvents
{
    public class TrackingCreatedDomainEvent : AuditableDomainEvent
    {
        public TrackingCreatedDomainEvent(Tracking tracking) : base("TrackingCreated")
        {
            Tracking = tracking;
            DoNotAudit();// we turn off audit of tracking creation to avoid having big log files, but we allow option to be easily turn on
        }

        //do not put entity in the payload, only primitive types
        public override object AuditPayload =>
            new
            {
                Id,
                RecordedOn,
                TrappingTypeId,
                SessionId,
                IsTimewriting,
                IsTrackingMap
            };

        public Tracking Tracking { get; }
        
        public Guid Id => Tracking.Id;
        public DateTimeOffset RecordedOn=> Tracking.RecordedOn;
        public Guid TrappingTypeId => Tracking.TrappingTypeId;
        public Guid SessionId => Tracking.SessionId;
        public bool IsTimewriting => Tracking.IsTimewriting;
        public bool IsTrackingMap=> Tracking.IsTrackingMap;
        public bool IsTrackingPrivate => Tracking.isTrackingPrivate;
        
        public Point Location => Tracking.Location;
        public Guid UserId => Tracking.CreatedById;
    }
}
