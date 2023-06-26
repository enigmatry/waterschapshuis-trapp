using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Waterschapshuis.CatchRegistration.Scheduler.Infrastructure
{
    [UsedImplicitly]
    public class SchedulerSettings
    {
        public string BackOfficeAppUrl { get; set; } = String.Empty;
        public CreateTrackingLineJobConfiguration CreateTrackingLineJobConfiguration { get; set; } = new CreateTrackingLineJobConfiguration();
        public CompleteRegistrationDataJobConfiguration CompleteRegistrationDataJobConfiguration { get; set; } = new CompleteRegistrationDataJobConfiguration();
        public PopulateReportTablesJobConfiguration PopulateReportTablesJobConfiguration { get; set; } = new PopulateReportTablesJobConfiguration();
        public AnonymizeInactiveUsersJobConfiguration AnonymizeInactiveUsersJobConfiguration { get; set; } = new AnonymizeInactiveUsersJobConfiguration();
        public RemoveSessionsJobConfiguration RemoveSessionsJobConfiguration { get; set; } = new RemoveSessionsJobConfiguration();
        public CalculateKmWaterwaysJobConfiguration CalculateKmWaterwaysJobConfiguration { get; set; } = new CalculateKmWaterwaysJobConfiguration();
        public List<JobSettings> JobsSettings { get; set; } = new List<JobSettings>();
    }

    [UsedImplicitly]
    public class CreateTrackingLineJobConfiguration
    {
        public int CurrentDateDeltaInDays { get; set; }
        public int DbTimoutInMin { get; set; }
    }

    [UsedImplicitly]
    public class CompleteRegistrationDataJobConfiguration
    {
        public int WeeksPeriodInDays { get; set; }
    }

    [UsedImplicitly]
    public class PopulateReportTablesJobConfiguration
    {
        public int DbTimoutInMin { get; set; }
    }


    [UsedImplicitly]
    public class AnonymizeInactiveUsersJobConfiguration
    {
        public int InactivePeriodBeforeAnonymizationInYears { get; set; }
    }

    [UsedImplicitly]
    public class RemoveSessionsJobConfiguration
    {
        public TimeSpan RemoveOlderThanTimespan { get; set; }
    }

    [UsedImplicitly]
    public class CalculateKmWaterwaysJobConfiguration
    {
        public int DbTimoutInMin { get; set; }
    }

    [UsedImplicitly]
    public class JobSettings
    {
        public string Name { get; set; } = String.Empty;
        public string CronExpression { get; set; } = String.Empty;
        public bool Enabled { get; set; } = true;
    }
}
