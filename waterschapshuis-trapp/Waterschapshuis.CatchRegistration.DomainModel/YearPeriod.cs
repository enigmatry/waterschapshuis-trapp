using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel
{
    public class YearPeriod : ValueObject, IComparable, IComparable<YearPeriod>
    {
        public const int DefaultValueYear = 2020;
        public const int DefaultValueWeek = 1;
        public const int DefaultValuePeriod = 1;
        public const string PeriodRegEx = "^[0-9]{4}-[0-9]{2}$";

        private const int YearNumericalValue = 15;

        protected YearPeriod() { }

        public string YearPeriodValue
        {
            get
            {
                return FormatValue(Year, Period);
            }
        }

        public int Year { get; protected set; }
        public int Period { get; protected set; }

        public int CompareTo(object? obj)
        {
            return CalculateNumericalValue()
                .CompareTo((obj as YearPeriod)?.CalculateNumericalValue() ?? 0);
        }

        public int CompareTo(YearPeriod? other)
        {
            return CalculateNumericalValue()
                .CompareTo(other?.CalculateNumericalValue() ?? 0);
        }

        protected override IEnumerable<object?> GetValues()
        {
            yield return Year;
            yield return Period;
        }

        protected virtual int CalculateNumericalValue()
        {
            return YearNumericalValue * Year + Period;
        }

        public static YearPeriod Create(DateTimeOffset date)
        {
            var (year, period) = date.GetPeriodOfYear(CalculatePeriodFromWeek);

            return new YearPeriod
            {
                Year = year,
                Period = period
            };
        }

        /// <summary>
        /// Parses the passed string value into YearWeekPeriod object 
        /// </summary>
        /// <param name="value">The "YYYY-WW" formatted value</param>
        /// <returns>Instance of the YearWeekPeriod class</returns>
        /// <remarks>It always uses the Year-Week string, not the Year-Period</remarks>
        public static YearPeriod Parse(string value)
        {
            var (year, period) = ParseFormattedValue(value);

            return new YearPeriod
            {
                Year = year,
                Period = period
            };
        }

        public static YearPeriod Default()
        {
            return Parse(FormatValue(DefaultValueYear, DefaultValuePeriod));
        }

        protected static (int year, int value) ParseFormattedValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!Regex.IsMatch(value, PeriodRegEx))
            {
                throw new ArgumentException("Value must be in format \"YYYY-WW\".");
            }

            var parts = value.Split('-');

            return (Int32.Parse(parts[0]), Int32.Parse(parts[1]));
        }

        public static bool operator <(YearPeriod first, YearPeriod second)
        {
            return first.CompareTo(second) < 0;
        }

        public static bool operator >(YearPeriod first, YearPeriod second)
        {
            return first.CompareTo(second) > 0;
        }

        protected static string FormatValue(int year, int value, int? maxValue = default)
        {
            return $"{ year }-{ (maxValue.HasValue ? (value < maxValue ? value : maxValue.Value) : value).ToString().PadLeft(2, '0') }";
        }

        protected static int CalculatePeriodFromWeek(int week)
        {
            var result = (int)Math.Ceiling(week / 4.00);
            return result > 13 ? 13 : result;
        }
    }
}
