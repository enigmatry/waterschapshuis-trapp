using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports
{
    public class CatchesPerArea
    {
        public string CatchAreaName { get; set; } = String.Empty;
        public string SubAreaName { get; set; } = String.Empty;
        public string HourSquareName { get; set; } = String.Empty;
        public IEnumerable<GetCatchDetails.CatchItem> CatchItems { get; set; } = new List<GetCatchDetails.CatchItem>();
    }
}
