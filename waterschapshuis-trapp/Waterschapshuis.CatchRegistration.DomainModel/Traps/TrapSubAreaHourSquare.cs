using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public class TrapSubAreaHourSquare : Entity
    {
        public Guid TrapId { get; set; }
        public Trap Trap { get; set; } = null!;
        public Guid SubAreaHourSquareId { get; set; }
        public SubAreaHourSquare SubAreaHourSquare { get; set; } = null!;

        public static TrapSubAreaHourSquare Create(Guid trapId, Guid subAreaHourSquareId) =>
            new TrapSubAreaHourSquare { TrapId = trapId, SubAreaHourSquareId = subAreaHourSquareId };
    }
}
