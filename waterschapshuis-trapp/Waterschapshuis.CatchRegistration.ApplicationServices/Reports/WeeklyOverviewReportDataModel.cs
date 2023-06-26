using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.ApplicationServices.Reports.Commands;
using Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports
{
    public class WeeklyOverviewReportDataModel
    {
        public int WeekNumber { get; private set; }
        public int Year { get; private set; }
        public string MapsPageUrl { get; private set; } = null!;
        public string TimeRegistrationPageUrl { get; private set; } = null!;
        public IEnumerable<GetTimeRegistrations.Response.Item> TimeRegistrations { get; private set; } = null!;
        public IEnumerable<CatchesPerDay> Catches { get; private set; } = null!;

        public string TotalTimeRegistrationsTime
        {
            get
            {
                var totalMinutes = TimeRegistrations.Sum(x => x.Hours) * 60 + TimeRegistrations.Sum(x => x.Minutes);
                return $"{totalMinutes / 60:00}:{totalMinutes % 60:00}";
            }
        }

        public int TotalCatchItems
        {
            get => Catches.SelectMany(x => x.CatchItemsPerRegion)
                    .SelectMany(x => x.CatchItems)
                    .Where(x => !x.IsByCatch)
                    .Sum(x => x.Number);
        }

        public int TotalByCatchItems
        {
            get => Catches.SelectMany(x => x.CatchItemsPerRegion)
                    .SelectMany(x => x.CatchItems)
                    .Where(x => x.IsByCatch)
                    .Sum(x => x.Number);
        }

        private WeeklyOverviewReportDataModel()
        {
        }

        public static WeeklyOverviewReportDataModel Create(
            int weekNumber,
            int year,
            string mapsPageUrl,
            string timeRegistrationPageUrl,
            IEnumerable<GetTimeRegistrations.Response.Item> timeRegistrations,
            IEnumerable<CatchesPerDay> catches
        )
        {
            return new WeeklyOverviewReportDataModel
            {
                WeekNumber = weekNumber,
                Year = year,
                MapsPageUrl = mapsPageUrl,
                TimeRegistrationPageUrl = timeRegistrationPageUrl,
                TimeRegistrations = timeRegistrations,
                Catches = catches
            };
        }
    }
}
