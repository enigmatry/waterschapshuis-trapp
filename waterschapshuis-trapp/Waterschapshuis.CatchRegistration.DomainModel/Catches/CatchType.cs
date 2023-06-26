using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches
{
    public class CatchType : Entity<Guid>, IEntityHasName<Guid>
    {
        public const int NameMaxLength = 50;
        public static readonly Guid HistoryMuskusratId = new Guid("C8783519-41C6-5654-1977-F6956ABA2EF4");
        public static readonly Guid HistoryBeverratId = new Guid("49B51935-918B-5A38-2493-A4141FEF8C52");

        public static readonly Guid BeverratMoerOudId = new Guid("85803328-15E7-92EF-528F-00E91B6D4815");
        public static readonly Guid ByCatchVlaamseGaaiId = new Guid("D1F172CE-15C8-88E8-92EC-3E8997F97374");
        public static readonly Guid ByCatchGroteModderkruiperId = new Guid("AA139422-996C-7970-3F81-CC52421F42DB");

        private CatchType() { }

        public string Name { get; private set; } = String.Empty;
        public bool IsByCatch { get; private set; }
        public AnimalType AnimalType { get; private set; }
        public short Order { get; private set; }

        public static CatchType Create(string name, bool isByCatch, AnimalType animalType, short order)
        {
            var result =new  CatchType
            {
                Id = GenerateId(),
                Name = name,
                IsByCatch = isByCatch,
                AnimalType = animalType,
                Order = order
            };
            RaiseCreatedEvent(result);
            return result;
        }

        public static CatchType Create(CatchTypeCreateOrUpdate.Command command)
        {
            var result = new CatchType
            {
                Id = GenerateId(),
                Name = command.Name,
                IsByCatch = command.IsByCatch,
                AnimalType = command.AnimalType,
                Order = command.Order
            };
            RaiseCreatedEvent(result);
            return result;
        }

        public CatchType Update(CatchTypeCreateOrUpdate.Command request)
        {
            Name = request.Name;
            IsByCatch = request.IsByCatch;
            AnimalType = request.AnimalType;
            Order = request.Order;
            RaiseUpdatedEvent();
            return this;
        }

        private static void RaiseCreatedEvent(CatchType result)
        {
            result.AddDomainEvent(new CatchTypeCreatedDomainEvent(result.Id, result.Name, result.IsByCatch, result.AnimalType, result.Order));
        }

        private void RaiseUpdatedEvent()
        {
            AddDomainEvent(new CatchTypeUpdatedDomainEvent(Id, Name, IsByCatch, AnimalType, Order));
        }
    }
}
