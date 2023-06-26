using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    [UsedImplicitly]
    public class LocationAreaDataSummary
    {
        public double? Value { get; set; }
        public string? ValueType { get; set; }
        public Type SummaryType { get; set; }


        public enum Type
        {
            CatchAreaCatchingTraps = 1,
            SubAreaCatchingTraps = 2,
            CatchAreaLastWeekCatches = 3,
            SubAreaLastWeekCatches = 4,
            CatchAreaLastWeekByCatches = 5,
            SubAreaLastWeekByCatches = 6,
            CatchAreaLastWeekHours = 7,
            SubAreaLastWeekHours = 8
        }
    }
}
