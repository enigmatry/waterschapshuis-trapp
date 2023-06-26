using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.DomainEvents
{
    public class TrapTypeCreatedDomainEvent : AuditableDomainEvent
    {
        public TrapTypeCreatedDomainEvent(Guid id, string name, Guid trappingTypeId, bool active, short order) :
            base("TrapTypeCreated")
        {
            Id = id;
            Name = name;
            TrappingTypeId = trappingTypeId;
            Active = active;
            Order = order;
        }

        public Guid Id { get; }
        public string Name { get; }
        public Guid TrappingTypeId { get; }
        public bool Active { get; }
        public short Order { get; }

        public override object AuditPayload =>
            new
            {
                Id,
                Name,
                TrappingTypeId,
                Active,
                Order
            };
    }
}
