using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using DateTimeOffset = System.DateTimeOffset;

namespace Waterschapshuis.CatchRegistration.DomainModel
{

    public static class SeasonPeriod
    {
        private static readonly Dictionary<int, int[]> SeasonPeriods = new Dictionary<int, int[]>()
            {
                { 1, new []{4,5,6}},
                { 2, new []{7,8,9}},
                { 3, new []{10,11,12}},
                { 0, new []{13,1,2,3}}
            };


        private static int GetPeriodFromWeek(int week)
        {
            var result = (int)Math.Ceiling(week / 4.00);
            return result > 13 ? 13 : result;
        }

        public static int GetSeasonByPeriod(int period)
        {
            return SeasonPeriods.Single(x => x.Value.Contains(period)).Key;
        }

        public static List<int> SeasonsIncluded(int year, int numberOfYears)
        {
            var currentDate = DateTimeOffset.Now;
            var currentSeason = GetSeasonByPeriod(GetPeriodFromWeek(currentDate.GetWeekOfYearWithCustomRule().week));
            var currentYear = currentDate.Year;

            if (year == currentYear)
                return SeasonPeriods.Keys.Where(x => x < currentSeason).ToList();
            if (year == (currentYear - numberOfYears))
                return SeasonPeriods.Keys.Where(x => x >= currentSeason).ToList();
            return SeasonPeriods.Keys.ToList();
        }

    }
}
