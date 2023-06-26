using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.FieldTest.DomainEvents
{
    public class FieldTestUpdatedDomainEvent : AuditableDomainEvent
    {
        public FieldTestUpdatedDomainEvent(Guid id, string name, string startPeriod, string endPeriod) : base(
            "FieldTestUpdated")
        {
            Id = id;
            Name = name;
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string StartPeriod { get; }
        public string EndPeriod { get; }

        public override object AuditPayload => new {Id, Name, StartPeriod, EndPeriod};
    }
}
