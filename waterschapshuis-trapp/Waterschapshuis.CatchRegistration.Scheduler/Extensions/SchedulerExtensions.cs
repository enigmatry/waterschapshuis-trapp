using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler.Extensions
{
    public static class SchedulerExtensions
    {
        public static async Task ScheduleCronJob(this IScheduler scheduler, IJob job, SchedulerSettings settings)
        {
            var jobSettings = settings.JobsSettings.SingleOrDefault(x => x.Name == job.GetType().Name)
                ?? throw new ArgumentNullException($"{job.GetType().Name} job settings could not be found!");

            if (!jobSettings.Enabled)
                return;

            const string jobGroup = "Waterschapshuis.CatchRegistration.Scheduler";

            IJobDetail jobDetail = JobBuilder
                .Create(job.GetType())
                .WithIdentity(jobSettings.Name, jobGroup)
                .Build();

            ITrigger trigger = TriggerBuilder
                .Create()
                .WithIdentity(jobSettings.Name, jobGroup)
                .WithCronSchedule(jobSettings.CronExpression)
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }

        public static void LogExecutionOfAllJobs(this IScheduler scheduler, ILogger log) =>
            scheduler.ListenerManager
                .AddJobListener(new ExecutionLoggingJobListener(log), EverythingMatcher<JobKey>.AllJobs());
    }
}
