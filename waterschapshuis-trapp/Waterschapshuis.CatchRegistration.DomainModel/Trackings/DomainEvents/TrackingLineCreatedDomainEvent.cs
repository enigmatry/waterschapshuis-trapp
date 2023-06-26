using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings.DomainEvents
{
    public class TrackingLineCreatedDomainEvent : AuditableDomainEvent
    {
        public TrackingLineCreatedDomainEvent(Guid id, DateTimeOffset date) : base("TrackingLineCreated")
        {
            Id = id;
            Date = date;
        }

        public Guid Id { get; }
        public DateTimeOffset Date { get; }

        public override object AuditPayload => new {Id, Date};
    }
}
