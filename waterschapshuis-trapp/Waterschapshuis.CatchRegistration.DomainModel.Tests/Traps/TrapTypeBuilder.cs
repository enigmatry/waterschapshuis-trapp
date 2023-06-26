using System;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps
{
    public class TrapTypeBuilder
    {
        private string _name = String.Empty;
        private Guid _trappingTypeId = Guid.Empty;
        private bool _active;
        private short _order;

        public TrapTypeBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public TrapTypeBuilder WithTrappingTypeId(Guid value)
        {
            _trappingTypeId = value;
            return this;
        }

        public TrapTypeBuilder WithActive(bool value)
        {
            _active = value;
            return this;
        }

        public TrapTypeBuilder WithOrder(short value)
        {
            _order = value;
            return this;
        }

        public static implicit operator TrapType(TrapTypeBuilder builder)
        {
            return builder.Build();
        }


        private TrapType Build()
        {
            var result = TrapType.Create(
                new TrapTypeCreateOrUpdate.Command
                {
                    Name = _name,
                    Order = _order,
                    TrappingTypeId = _trappingTypeId,
                    Active = _active
                });

            return result;
        }
    }
}
