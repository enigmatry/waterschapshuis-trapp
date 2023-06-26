using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports
{
    public class CatchesPerDay
    {
        public CatchesPerDay(DateTime day, IEnumerable<GetCatchDetails.CatchItem> catchItems)
        {
            Day = day;
            CatchItemsPerRegion = catchItems.OrderBy(x => x.Type)
                .GroupBy(x => new
                {
                    CatchAreaName = x.CatchArea.Name,
                    SubAreaName = x.SubArea.Name,
                    HourSquareName = x.HourSquare.Name
                })
                .Select(x => new CatchesPerArea
                {
                    CatchAreaName = x.Key.CatchAreaName,
                    SubAreaName = x.Key.SubAreaName,
                    HourSquareName = x.Key.HourSquareName,
                    CatchItems = x.Select(c => c)
                });
        }
        public DateTime Day { get; set; }
        public IEnumerable<CatchesPerArea> CatchItemsPerRegion { get; set; } = new List<CatchesPerArea>();
    }
}
