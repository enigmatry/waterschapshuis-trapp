using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Utils;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public class TrapType : Entity<Guid>, IEntityHasName<Guid>
    {
        public const int NameMaxLength = 50;

        public TrapStatus[] AllTrapStatuses = Enum<TrapStatus>.GetAll().ToArray();

        public static readonly Guid ConibearMuskratId = Guid.Parse("A0A0503E-0CD7-0642-73AB-464E7CA0A28E");
        public static readonly Guid GrondklemMuskratId = Guid.Parse("1620509F-4BB2-90EA-637C-AF77B636964A");
        public static readonly Guid ConibearBeverratId = Guid.Parse("586729D8-980E-2A76-81F2-DBB5C57C9D6F");
        public static readonly Guid GrondklemBeverratId = Guid.Parse("54AF411E-25F6-2A11-4BBF-E7547E212E76");
        public static readonly Guid OtterId = Guid.Parse("FF7C880C-9AC6-433E-1B92-3563869A48E2");

        private readonly List<TrapTypeTrapStatusStyle> _trapTypeTrapStatusStyles = new List<TrapTypeTrapStatusStyle>();

        private TrapType()
        {
        }

        public string Name { get; private set; } = String.Empty;
        public Guid TrappingTypeId { get; private set; } = Guid.Empty;
        public TrappingType TrappingType { get; private set; } = null!;
        public bool Active { get; private set; }
        public short Order { get; private set; }
        public bool AllowNotCatching { get; private set; }

        public TrapStatus[] AllowedStatuses =>
            AllowNotCatching
                ? AllTrapStatuses
                : AllTrapStatuses.Except(new []{TrapStatus.NotCatching}).ToArray();

        public IReadOnlyCollection<TrapTypeTrapStatusStyle> TrapTypeTrapStatusStyles => _trapTypeTrapStatusStyles.AsReadOnly();

        public static TrapType Create(string name, Guid trappingTypeId, bool active, short order, bool allowNotCatching)
        {
            return new TrapType
            {
                Id = GenerateId(),
                Name = name,
                TrappingTypeId = trappingTypeId,
                Active = active,
                Order = order,
                AllowNotCatching = allowNotCatching
            };
        }

        public static TrapType Create(TrapTypeCreateOrUpdate.Command command)
        {
            var result = new TrapType
            {
                    Id = GenerateId(),
                    Name = command.Name,
                    TrappingTypeId = command.TrappingTypeId,
                    Active = command.Active,
                    Order = command.Order,
                    AllowNotCatching = command.AllowNotCatching
            };

            result.AddDomainEvent(new TrapTypeCreatedDomainEvent(result.Id, result.Name, result.TrappingTypeId, result.Active, result.Order));

            return result;
        }

        public void Update(TrapTypeCreateOrUpdate.Command command)
        {
            Name = command.Name;
            TrappingTypeId = command.TrappingTypeId;
            Active = command.Active;
            Order = command.Order;
            AllowNotCatching = command.AllowNotCatching;

            AddDomainEvent(new TrapTypeUpdatedDomainEvent(Id, Name, TrappingTypeId, Active, Order));
        }
    }
}
