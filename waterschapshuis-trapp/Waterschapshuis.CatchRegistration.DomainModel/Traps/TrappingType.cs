using System;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public class TrappingType : Entity<Guid>, IEntityHasName<Guid>
    {
        public const int NameMaxLength = 50;
        public static readonly Guid MuskusratId = Guid.Parse("A2BA2913-77D6-47D9-B893-F9D0CC0432BB");
        public static readonly Guid BeverratId = Guid.Parse("76B4E3E7-CE9D-4EED-AB39-0BA2C6395B2B");

        private TrappingType()
        {
        }

        public string Name { get; private set; } = String.Empty;
       
        public static TrappingType Create(string name)
        {
            return new TrappingType
            {
                Id = GenerateId(),
                Name = name
            };
        }
    }
}
