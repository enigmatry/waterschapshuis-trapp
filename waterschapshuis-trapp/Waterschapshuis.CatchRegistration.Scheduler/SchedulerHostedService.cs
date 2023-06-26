using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Scheduler.Extensions;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler
{
    public class SchedulerHostedService : IHostedService
    {
        private readonly IScheduler _scheduler;
        private readonly IEnumerable<IJob> _jobs;
        private readonly SchedulerSettings _settings;
        private readonly ILogger<SchedulerHostedService> _logger;

        public SchedulerHostedService(IScheduler scheduler, IEnumerable<IJob> jobs, SchedulerSettings settings, ILogger<SchedulerHostedService> logger)
        {
            _scheduler = scheduler;
            _jobs = jobs;
            _settings = settings;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler.LogExecutionOfAllJobs(_logger);

            foreach (var job in _jobs.ToList())
            {
                await _scheduler.ScheduleCronJob(job, _settings);
            }

            await _scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_scheduler == null)
                return;
            await _scheduler.Shutdown(cancellationToken);
        }
    }
}
