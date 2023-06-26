using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests
{
    public class WeekPeriodBuilder
    {
        private int _year;
        private int _month;
        private int _day;

        public WeekPeriodBuilder()
        {
            _year = DateTimeOffset.Now.Year;
            _month = DateTimeOffset.Now.Month;
            _day = DateTimeOffset.Now.Day;
        }

        public WeekPeriodBuilder ForYear(int year)
        {
            if (year < 0) throw new ArgumentOutOfRangeException();
            if (year > 0) _year = year;

            return this;
        }

        public WeekPeriodBuilder ForMonth(int month)
        {
            if (month < 0) throw new ArgumentOutOfRangeException();
            if (month > 0) _month = month;

            return this;
        }

        public WeekPeriodBuilder ForDay(int day)
        {
            if (day < 0) throw new ArgumentOutOfRangeException();
            if (day > 0) _day = day;

            return this;
        }

        private YearWeekPeriod BuildWeekPeriod()
        {
            var date = new DateTimeOffset(new DateTime(_year, _month, _day));
            return YearWeekPeriod.Create(date);
        }

        private YearPeriod BuildPeriod()
        {
            var date = new DateTimeOffset(new DateTime(_year, _month, _day));
            return YearPeriod.Create(date);
        }

        public static implicit operator YearWeekPeriod(WeekPeriodBuilder builder)
        {
            return builder.BuildWeekPeriod();
        }

        public static implicit operator YearPeriod(WeekPeriodBuilder builder)
        {
            return builder.BuildPeriod();
        }
    }
}
