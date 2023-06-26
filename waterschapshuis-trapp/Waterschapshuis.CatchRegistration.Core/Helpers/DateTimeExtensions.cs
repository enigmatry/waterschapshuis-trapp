using System;
using System.Globalization;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Core.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset MondayDateInWeekOfDate(this DateTimeOffset dt)
        {
            return dt.PreviousDayOfWeekDate(DayOfWeek.Monday);
        }

        public static bool OlderThanNumberOfWeeks(this DateTimeOffset dt, int numberOfPassedWeeks)
        {
            var monday = dt.PreviousDayOfWeekDate(DayOfWeek.Monday);
            var startDate = DateTimeOffset.Now;

            return (int)((startDate - monday).TotalDays / 7) > numberOfPassedWeeks;
        }

        public static (DateTimeOffset startDate, DateTimeOffset endDate) CurrentDateWeekRange(this DateTimeOffset dt)
        {
            return (dt.PreviousDayOfWeekDate(DayOfWeek.Monday), dt.PreviousDayOfWeekDate(DayOfWeek.Monday).AddDays(7));
        }

        private static DateTimeOffset PreviousDayOfWeekDate(this DateTimeOffset dt, DayOfWeek weekStartDay)
        {
            while (dt.DayOfWeek != weekStartDay)
                dt = dt.AddDays(-1);
            return dt;
        }

        public static DateTimeOffset GetLastDayOfCurrentWeek(this DateTimeOffset dt)
        {
            var thisWeekStart = dt.AddDays(-(int)dt.DayOfWeek);
            return thisWeekStart.AddDays(8).AddSeconds(-1);
        }

        public static DateTimeOffset BeginningOfDay(this DateTimeOffset source)
        {
            var date = new DateTime(source.Year, source.Month, source.Day, 0, 0, 0);
            return new DateTimeOffset(date);
        }

        // ISO 8601 - Weeks starts on Monday and Week 1 is the 1st week of the year with a Thursday in it.
        public static (int year, int week) GetIso8601WeekOfYear(this DateTime value)
        {
            var week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var year = value.Year;

            // if date is between January 1st and 3rd check if the day is before Thursday, because if it is then this date is part of last year's last week
            // id date is between December 29th and 31st check if the day is before Thursday, because if it is the this date is part of the next year's first week

            DayOfWeek dayOfWeek = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(value);

            switch (value.Month)
            {
                case 1 when value.Day <= 3:
                {
                    if (dayOfWeek > DayOfWeek.Thursday || dayOfWeek == DayOfWeek.Sunday)
                    {
                        year -= 1;
                    }
                    break;
                }
                case 12 when value.Day >= 29:
                {
                    if (dayOfWeek < DayOfWeek.Thursday && dayOfWeek != DayOfWeek.Sunday)
                    {
                        year += 1;
                        week = 1;
                    }
                    break;
                }
            }

            return (year, week);
        }

        public static (int year, int week) GetAbsoluteWeekOfYear(this DateTime value)
        {
            var year = value.Year;
            var week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            return (year, week);
        }

        /// <summary>
        /// Used for Catches and Time Registration week calculation
        /// </summary>
        public static (int year, int week) GetWeekOfYearWithCustomRule(this DateTimeOffset value)
        {
            return value.Date.GetWeekOfYearWithCustomRule();
        }

        // If some or all of the first three days of January are assigned to week 52 or 53,
        // as according to ISO 8601, then those days will be assigned to the week number 1 instead
        // If some or all of the last three days of December are assigned to week 1, as according to ISO 8601,
        // then those days will be assigned to the week number that is 1 higher than 7 days earlier
        public static (int year, int week) GetWeekOfYearWithCustomRule(this DateTime value)
        {
            var week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var year = value.Year;

            DayOfWeek dayOfWeek = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(value);

            switch (value.Month)
            {
                case 1 when value.Day <= 3:
                {
                    if (dayOfWeek > DayOfWeek.Thursday || dayOfWeek == DayOfWeek.Sunday)
                    {
                        week = 1;
                    }
                    break;
                }
                case 12 when value.Day >= 29:
                {
                    week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(new DateTime(year, 12, value.Day - 7), 
                        CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + 1;
                    break;
                }
            }

            return (year, week);
        }

        public static (int year, int period) GetPeriodOfYear(this DateTimeOffset date, Func<int, int> weekToPeriod)
        {
            return date.Date.GetPeriodOfYear(weekToPeriod);
        }
        private static (int year, int period) GetPeriodOfYear(this DateTime date, Func<int, int> weekToPeriod)
        {
            var (year, week) = date.GetAbsoluteWeekOfYear();

            var period = weekToPeriod(week);

            return (year, period);
        }

        public static bool NotInFuture(this DateTimeOffset value) => value <= DateTimeOffset.Now;

        public static bool SameAs(this DateTimeOffset value, DateTimeOffset other, string? format = null) =>
            format == null
                ? value == other
                : value.ToString(format) == other.ToString(format);

        /// <summary>
        /// Supported formats: dd/MM/yyyy HH:mm:ss zzz, dd/MM/yyyy HH:mm:ss, dd/MM/yyyy HH:mm, dd/MM/yyyy
        ///                    dd-MM-yyyy HH:mm:ss zzz, dd-MM-yyyy HH:mm:ss, dd-MM-yyyy HH:mm, dd-MM-yyyy
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static DateTimeOffset AppToDateTimeOffset(this string request)
        {
            var result = new DateTimeOffset();
            string[] supportedFormats =
            {
                "dd/MM/yyyy HH.mm.ss zzz",
                "dd/MM/yyyy HH:mm:ss",
                "dd/MM/yyyy HH:mm",
                "dd/MM/yyyy",
                "dd-MM-yyyy HH.mm.ss zzz",
                "dd-MM-yyyy HH:mm:ss",
                "dd-MM-yyyy HH:mm",
                "dd-MM-yyyy",
            };

            if (!supportedFormats.Any(x => DateTimeOffset.TryParseExact(request, x, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)))
            {
                throw new ArgumentException("Unrecognized date format");
            }

            return result;
        }
    }
}
