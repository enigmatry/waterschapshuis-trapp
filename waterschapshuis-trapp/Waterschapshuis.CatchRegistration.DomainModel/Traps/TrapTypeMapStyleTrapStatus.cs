using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public class TrapTypeTrapStatusStyle
    {
        public TrapStatus TrapStatus { get; private set; }
        public string IconName { get; private set; } = String.Empty;

        public TrapType TrapType { get; private set; } = null!;
        public Guid TrapTypeId { get; private set; }

        public static TrapTypeTrapStatusStyle Create(Guid typeId, TrapStatus status, string iconName)
        {
            return new TrapTypeTrapStatusStyle
            {
                TrapTypeId = typeId,
                TrapStatus = status,
                IconName = iconName
            };
        }
    }
}
