using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Helpers
{ 
    public interface ITimeRegistrationHelperService
    {
        bool WeekClosedOrApprovedForDate(DateTimeOffset recordedOndate);
    }
}
