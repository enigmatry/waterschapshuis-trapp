using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.DomainEvents
{
    public class TrapTypeUpdatedDomainEvent : AuditableDomainEvent
    {
        public TrapTypeUpdatedDomainEvent(Guid id, string name, Guid trappingTypeId, bool active, short order) : base("TrapTypeUpdated")
        {
            Id = id;
            Name = name;
            TrappingTypeId = trappingTypeId;
            Active = active;
            Order = order;
        }

        public Guid Id { get; }
        public string Name { get;  } 
        public Guid TrappingTypeId { get; }
        public bool Active { get; }
        public short Order { get; }

        public override object AuditPayload => new
        {
            Id,
            Name,
            TrappingTypeId,
            Active,
            Order
        };
    }
}
