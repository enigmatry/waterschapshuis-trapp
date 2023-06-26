using System;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches
{
    public class CatchTypeBuilder
    {
        private Guid? _id;
        private string _name = String.Empty;
        private bool _isByCatch = false;
        private AnimalType _animalType = AnimalType.Other;
        private short _order;

        public CatchTypeBuilder WithId(Guid value)
        {
            _id = value;
            return this;
        }

        public CatchTypeBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public CatchTypeBuilder WithIsByCatch(bool value)
        {
            _isByCatch = value;
            return this;
        }

        public CatchTypeBuilder WithAnimalType(AnimalType value)
        {
            _animalType = value;
            return this;
        }

        public CatchTypeBuilder WithOrder(short value)
        {
            _order = value;
            return this;
        }

        public static implicit operator CatchType(CatchTypeBuilder builder)
        {
            return builder.Build();
        }

        private CatchType Build()
        {
            var result = CatchType.Create(
                new CatchTypeCreateOrUpdate.Command
                {
                    Id = _id,
                    Name = _name,
                    Order = _order,
                    AnimalType = _animalType,
                    IsByCatch = _isByCatch
                });

            return result;
        }
    }
}
