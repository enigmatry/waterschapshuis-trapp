using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.FieldTest.DomainEvents
{
    public class FieldTestCreatedDomainEvent : AuditableDomainEvent
    {
        public FieldTestCreatedDomainEvent(Guid id, string name, string startPeriod, string endPeriod) : base(
            "FieldTestCreated")
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
