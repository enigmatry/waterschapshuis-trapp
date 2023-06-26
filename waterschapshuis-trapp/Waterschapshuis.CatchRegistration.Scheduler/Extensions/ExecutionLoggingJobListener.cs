using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Scheduler.Extensions
{
    public class ExecutionLoggingJobListener : IJobListener
    {
        private ILogger Log { get; }

        public ExecutionLoggingJobListener(ILogger log) => Log = log ?? throw new ArgumentNullException(nameof(log));

        public string Name => nameof(ExecutionLoggingJobListener);

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            Log.LogInformation($"{context.JobDetail.Key.Name}, to be executed!");
            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            Log.LogInformation($"{context.JobDetail.Key.Name}, execution vetoed!");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = new CancellationToken())
        {
            Log.LogInformation($"{context.JobDetail.Key.Name}, was executed!");
            return Task.CompletedTask;
        }
    }
}
