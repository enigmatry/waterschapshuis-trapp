using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel
{
    public class YearWeekPeriod : YearPeriod
    {
        private YearWeekPeriod() { }

        public string YearWeekValue
        {
            get
            {
                return FormatValue(Year, Week);
            }
        }

        public int Week { get; private set; }

        protected override IEnumerable<object?> GetValues()
        {
            yield return Year;
            yield return Week;
            yield return Period;
        }

        public static new YearWeekPeriod Create(DateTimeOffset date)
        {
            var (year, week) = date.GetWeekOfYearWithCustomRule();

            var period = CalculatePeriodFromWeek(week);

            return new YearWeekPeriod
            {
                Year = year,
                Week = week,
                Period = period
            };
        }

        /// <summary>
        /// Parses the passed string value into YearWeekPeriod object 
        /// </summary>
        /// <param name="value">The "YYYY-WW" formatted value</param>
        /// <returns>Instance of the YearWeekPeriod class</returns>
        /// <remarks>It always uses the Year-Week string, not the Year-Period</remarks>
        public static new YearWeekPeriod Parse(string value)
        {
            var (year, week) = ParseFormattedValue(value);
            var period = CalculatePeriodFromWeek(week);

            return new YearWeekPeriod
            {
                Year = year,
                Week = week,
                Period = period
            };
        }

        public static new YearWeekPeriod Default()
        {
            return Parse(FormatValue(DefaultValueYear, DefaultValueWeek));
        }
    }
}
