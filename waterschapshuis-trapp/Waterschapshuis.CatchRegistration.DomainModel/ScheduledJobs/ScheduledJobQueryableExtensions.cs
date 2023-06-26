using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs
{
    public static class ScheduledJobQueryableExtensions
    {
        public static IQueryable<ScheduledJob> QueryByJobName(this IQueryable<ScheduledJob> query, ScheduledJobName jobName) =>
            query.Where(x => x.Name == jobName);

        public static IQueryable<ScheduledJob> QueryScheduledJobs(this IQueryable<ScheduledJob> query) =>
            query.Where(x => x.State == ScheduledJobState.Scheduled);

        public static bool AllowCreatingJob(this IQueryable<ScheduledJob> query, ScheduledJobName name)
        {
            var scheduledJobs = query.Where(x => x.Name == name && 
                                            (x.State == ScheduledJobState.Scheduled || x.State == ScheduledJobState.Started));
            return !scheduledJobs.Any();
        }
    }
}
