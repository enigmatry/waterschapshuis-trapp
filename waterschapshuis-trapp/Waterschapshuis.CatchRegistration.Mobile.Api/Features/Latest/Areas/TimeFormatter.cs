using System;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Areas
{
    public static class TimeFormatter
    {
        public static (long hours, short minutes) ToHoursAndMinutes(double hours)
        {
            hours = Math.Round(hours, 2);
            return ((long)Math.Floor(hours), (short)Math.Round(hours * 100 % 100 / 100 * 60));
        }

        public static double ToHours(int hours, int minutes)
        {
            return Math.Round(hours + (minutes > 0 ? (double) minutes / 60 : 0), 2);
        }
    }
}
