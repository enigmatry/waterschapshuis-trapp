using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Areas
{
    public static partial class GetLocationAreaData
    {
        public class Query : IRequest<Response>
        {
            public Guid CatchAreaId { get; set; }
            public Guid SubAreaId { get; set; }
        }

        public class Response
        {
            public Area CatchArea { get; set; } = new Area();
            public Area SubArea { get; set; } = new Area();

            public class Area
            {
                public TimeSummary LastWeekTimeTotal { get; set; } = null!;

                public double CatchingTrapsTotal => CatchingTraps.Sum(t => t.Number);
                public IList<TrapSummary> CatchingTraps { get; set; } = new List<TrapSummary>();

                public double LastWeekCatchesTotal => LastWeekCatches.Sum(c => c.Number);
                public IList<TrapSummary> LastWeekCatches { get; set; } = new List<TrapSummary>();

                public double LastWeekByCatchesTotal => LastWeekByCatches.Sum(c => c.Number);
                public IList<TrapSummary> LastWeekByCatches { get; set; } = new List<TrapSummary>();

                public class TrapSummary
                {
                    public TrapSummary(double? number, string? type)
                    {
                        Number = number ?? 0;
                        Type = type ?? String.Empty;
                    }

                    public double Number { get; set; }
                    public string Type { get; set; }
                }

                public class TimeSummary
                {
                    public TimeSummary(double? hours)
                    {
                        if (hours > 0)
                        {
                            var (h, m) = TimeFormatter.ToHoursAndMinutes(hours.Value);
                            Hours = h;
                            Minutes = m;
                        }
                        else
                        {
                            Hours = 0;
                            Minutes = 0;
                        }
                    }

                    public long Hours { get; set; }
                    public short Minutes { get; set; }
                }
            }
        }
    }
}
