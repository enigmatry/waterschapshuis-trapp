using System;

namespace Waterschapshuis.CatchRegistration.Core
{
    public interface IEntityHasCreatedUpdatedRecorded : IEntityHasCreatedUpdated
    {
        DateTimeOffset RecordedOn { get; }

        void SetRecordedWithTimeReset(DateTimeOffset recordedOn);
        void SetRecorded(DateTimeOffset recordedOn);
    }
}
