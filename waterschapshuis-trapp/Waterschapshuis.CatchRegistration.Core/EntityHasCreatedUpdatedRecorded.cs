using System;

namespace Waterschapshuis.CatchRegistration.Core
{
    public class EntityHasCreatedUpdatedRecorded<TId> : EntityHasCreatedUpdated<TId>, IEntityHasCreatedUpdatedRecorded
    {
        public DateTimeOffset RecordedOn { get; private set; }

        public void SetRecordedWithTimeReset(DateTimeOffset recordedOn)
        {
            SetRecorded(recordedOn.Date == DateTimeOffset.Now.Date
                ? recordedOn
                : new DateTimeOffset(recordedOn.Year, recordedOn.Month, recordedOn.Day, 7, 0, 0, recordedOn.Offset));
        }

        public virtual void SetRecorded(DateTimeOffset recordedOn)
        {
            RecordedOn = recordedOn;
        }
    }
}
