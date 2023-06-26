using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands
{
    public class CatchImport
    {
        public int Number { get; private set; }
        public CatchStatus Status { get; private set; } = CatchStatus.Completed;
        public Guid TrapId { get; private set; }
        public Guid CatchTypeId { get; private set; }

        public CatchImport(int number, CatchStatus status, Guid trapId, Guid catchTypeId)
        {
            Number = number;
            Status = status;
            TrapId = trapId;
            CatchTypeId = catchTypeId;
        }
    }
}
