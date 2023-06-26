using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.DomainEvents
{
    public class CatchUpdatedDomainEvent : AuditableDomainEvent
    {
        public CatchUpdatedDomainEvent(Catch @catch) : base("CatchUpdated")
        {
            Id = @catch.Id;
            CatchTypeId = @catch.CatchTypeId;
            Number = @catch.Number;
            Status = @catch.Status;
        }

        public Guid Id { get; }
        public Guid CatchTypeId { get; }
        public int Number { get; }
        public CatchStatus Status { get; }
        public Guid TrapId { get; }

        public override object AuditPayload =>
            new
            {
                Id,
                TrapId,
                CatchTypeId,
                Number,
                Status
            };
    }

}
