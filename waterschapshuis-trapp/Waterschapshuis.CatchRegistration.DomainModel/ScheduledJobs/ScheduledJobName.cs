using System.ComponentModel;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs
{
    public enum ScheduledJobName
    {
        VersionRegionalLayoutImport,
        [Description("Calculation of water areas")]
        CalculatingKmWaterways
    }
}
