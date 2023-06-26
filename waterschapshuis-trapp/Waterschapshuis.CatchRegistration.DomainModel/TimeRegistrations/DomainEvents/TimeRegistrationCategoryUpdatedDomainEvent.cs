using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.DomainEvents
{
    public class TimeRegistrationCategoryUpdatedDomainEvent : AuditableDomainEvent
    {
        public TimeRegistrationCategoryUpdatedDomainEvent(Guid id, string name, bool active) : base("TimeRegistrationCategoryUpdated")
        {
            Id = id;
            Name = name;
            Active = active;
        }

        public Guid Id { get; }
        public string Name { get;  } 
        public bool Active { get; }

        public override object AuditPayload => new
        {
            Id,
            Name,
            Active
        };
    }
}
