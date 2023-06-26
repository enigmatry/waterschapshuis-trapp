using System;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.DomainEvents
{
    public class CatchTypeUpdatedDomainEvent : AuditableDomainEvent
    {
        public CatchTypeUpdatedDomainEvent(Guid id,
            string name,
            bool isByCatch,
            AnimalType animalType,
            short order) : base("CatchTypeUpdated")
        {
            Id = id;
            Name = name;
            IsByCatch = isByCatch;
            AnimalType = animalType;
            Order = order;
        }

        
        public Guid Id { get; }
        public string Name { get; }
        public bool IsByCatch { get; }
        public AnimalType AnimalType { get; }
        public short Order { get; }

        public override object AuditPayload =>
            new
            {
                Id,
                Name,
                IsByCatch,
                AnimalType,
                Order
            };
    }
}
