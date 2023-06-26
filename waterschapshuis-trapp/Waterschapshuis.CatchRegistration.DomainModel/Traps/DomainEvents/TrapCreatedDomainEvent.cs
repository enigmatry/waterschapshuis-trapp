using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.DomainEvents
{
    public class TrapCreatedDomainEvent : AuditableDomainEvent
    {
        public TrapCreatedDomainEvent(Trap trap) : base("TrapTypeCreated")
        {
            Id = trap.Id;
            Remarks = trap.Remarks;
            NumberOfTraps = trap.NumberOfTraps;
            Status = trap.Status;
            TrapTypeId = trap.TrapTypeId;
        }

        public Guid Id { get; }
        public string? Remarks { get; }
        public int NumberOfTraps { get; }
        public TrapStatus Status { get; }
        public Guid TrapTypeId { get; }

        public override object AuditPayload =>
            new
            {
                Id,
                Remarks,
                NumberOfTraps,
                Status,
                TrapTypeId
            };
    }
}
