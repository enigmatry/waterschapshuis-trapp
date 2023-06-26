using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings.DomainEvents
{
    public class TrackingLineUpdatedDomainEvent : AuditableDomainEvent
    {
        public TrackingLineUpdatedDomainEvent(Guid id, DateTimeOffset date) : base("TrackingLineUpdated")
        {
            Id = id;
            Date = date;
        }

        public Guid Id { get; }
        public DateTimeOffset Date { get; }

        public override object AuditPayload => new {Id, Date};
    }
}
