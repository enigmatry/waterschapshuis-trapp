using System;
using FluentAssertions;
using NUnit.Framework;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests
{
    [Category("unit")]
    public class WeekPeriodFixture
    {
        // 2010
        [TestCase(2010, 1, 1, 2010, 1, 1)]
        [TestCase(2010, 1, 3, 2010, 1, 1)]
        [TestCase(2010, 1, 4, 2010, 1, 1)]
        [TestCase(2010, 1, 10, 2010, 1, 1)]
        [TestCase(2010, 1, 11, 2010, 1, 2)]
        [TestCase(2010, 1, 17, 2010, 1, 2)]
        [TestCase(2010, 1, 18, 2010, 1, 3)]
        [TestCase(2010, 12, 27, 2010, 13, 52)]
        [TestCase(2010, 12, 30, 2010, 13, 52)]
        [TestCase(2010, 12, 31, 2010, 13, 52)]
        // 2011
        [TestCase(2011, 1, 1, 2011, 1, 1)]
        [TestCase(2011, 1, 2, 2011, 1, 1)]
        [TestCase(2011, 1, 3, 2011, 1, 1)]
        [TestCase(2011, 12, 31, 2011, 13, 52)]
        // 2012
        [TestCase(2012, 1, 1, 2012, 1, 1)]
        [TestCase(2012, 1, 2, 2012, 1, 1)]
        [TestCase(2012, 12, 24, 2012, 13, 52)]
        [TestCase(2012, 12, 30, 2012, 13, 52)]
        [TestCase(2012, 12, 31, 2012, 13, 53)]
        // 2013
        [TestCase(2013, 1, 1, 2013, 1, 1)]
        [TestCase(2013, 1, 8, 2013, 1, 2)]
        [TestCase(2013, 12, 29, 2013, 13, 52)]
        [TestCase(2013, 12, 30, 2013, 13, 53)]
        [TestCase(2013, 12, 31, 2013, 13, 53)]
        // 2014
        [TestCase(2014, 1, 1, 2014, 1, 1)]
        [TestCase(2014, 12, 28, 2014, 13, 52)]
        [TestCase(2014, 12, 29, 2014, 13, 53)]
        // 2015
        [TestCase(2015, 1, 1, 2015, 1, 1)]
        [TestCase(2015, 12, 31, 2015, 13, 53)]
        // 2016
        [TestCase(2016, 1, 1, 2016, 1, 1)]
        [TestCase(2016, 1, 4, 2016, 1, 1)]
        [TestCase(2016, 12, 31, 2016, 13, 52)]
        // 2017
        [TestCase(2017, 1, 1, 2017, 1, 1)]
        [TestCase(2017, 1, 2, 2017, 1, 1)]
        [TestCase(2017, 12, 31, 2017, 13, 52)]
        // 2018
        [TestCase(2018, 1, 1, 2018, 1, 1)]
        [TestCase(2018, 12, 30, 2018, 13, 52)]
        [TestCase(2018, 12, 31, 2018, 13, 53)]
        // 2019
        [TestCase(2019, 1, 1, 2019, 1, 1)]
        [TestCase(2019, 12, 29, 2019, 13, 52)]
        [TestCase(2019, 12, 30, 2019, 13, 53)]
        [TestCase(2019, 12, 31, 2019, 13, 53)]
        // 2020
        [TestCase(2020, 1, 1, 2020, 1, 1)]
        [TestCase(2020, 1, 27, 2020, 2, 5)]
        [TestCase(2020, 2, 26, 2020, 3, 9)]
        [TestCase(2020, 6, 10, 2020, 6, 24)]
        [TestCase(2020, 12, 31, 2020, 13, 53)]
        // 2021
        [TestCase(2021, 1, 1, 2021, 1, 1)]
        [TestCase(2021, 1, 4, 2021, 1, 1)]
        [TestCase(2021, 12, 31, 2021, 13, 52)]
        // 2022
        [TestCase(2022, 1, 1, 2022, 1, 1)]
        [TestCase(2022, 12, 31, 2022, 13, 52)]
        // 2023
        [TestCase(2023, 1, 1, 2023, 1, 1)]
        [TestCase(2023, 12, 31, 2023, 13, 52)]
        // 2024
        [TestCase(2024, 1, 1, 2024, 1, 1)]
        [TestCase(2024, 12, 31, 2024, 13, 53)]
        // 2025
        [TestCase(2025, 1, 1, 2025, 1, 1)]
        [TestCase(2025, 12, 31, 2025, 13, 53)]
        // 2026
        [TestCase(2026, 1, 1, 2026, 1, 1)]
        [TestCase(2026, 12, 31, 2026, 13, 53)]
        // 2027
        [TestCase(2027, 1, 1, 2027, 1, 1)]
        [TestCase(2027, 12, 31, 2027, 13, 52)]
        // 2028
        [TestCase(2028, 1, 1, 2028, 1, 1)]
        [TestCase(2028, 12, 31, 2028, 13, 52)]
        // 2029
        [TestCase(2029, 1, 1, 2029, 1, 1)]
        [TestCase(2029, 12, 31, 2029, 13, 53)]
        public void Create_ReturnsCorrectWeekAndPeriod(
            int actualYear,
            int actualMonth,
            int actualDay,
            int expectedYear,
            int expectedPeriod,
            int expectedWeek)
        {
            YearWeekPeriod date = new WeekPeriodBuilder()
                .ForYear(actualYear)
                .ForMonth(actualMonth)
                .ForDay(actualDay);

            date.YearPeriodValue.Should().Be(FormatValue(expectedYear, expectedPeriod));
            date.YearWeekValue.Should().Be(FormatValue(expectedYear, expectedWeek));
        }

        [TestCase(2020, 1, 1, 2020, 12, 31, false, true, false)]
        [TestCase(2020, 1, 1, 2020, 1, 15, true, false, false)]
        [TestCase(2020, 12, 31, 2020, 1, 1, false, false, true)]
        public void ComparisonAndEqualityOfObjectsIsCorrect(
            int firstYear,
            int firstMonth,
            int firstDay,
            int secondYear,
            int secondMonth,
            int secondDay,
            bool expectedEquals,
            bool expectedLessThan,
            bool expectedGreaterThan)
        {
            YearPeriod first = new WeekPeriodBuilder()
                .ForYear(firstYear)
                .ForMonth(firstMonth)
                .ForDay(firstDay);

            YearPeriod second = new WeekPeriodBuilder()
                .ForYear(secondYear)
                .ForMonth(secondMonth)
                .ForDay(secondDay);

            (first == second).Should().Be(expectedEquals);
            (first < second).Should().Be(expectedLessThan);
            (first > second).Should().Be(expectedGreaterThan);
        }

        private static string FormatValue(int year, int value) =>
            String.Concat(year, '-', value.ToString().PadLeft(2, '0'));
    }
}
