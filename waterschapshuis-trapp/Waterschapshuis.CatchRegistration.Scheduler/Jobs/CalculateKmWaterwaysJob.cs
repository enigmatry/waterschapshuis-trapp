using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class CalculateKmWaterwaysJob: IJob
    {
        private readonly CatchRegistrationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly ILogger<CalculateKmWaterwaysJob> _logger;
        private readonly SchedulerSettings _settings;

        public CalculateKmWaterwaysJob(CatchRegistrationDbContext dbContext,
            IMediator mediator,
            ILogger<CalculateKmWaterwaysJob> logger,
            SchedulerSettings settings)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _logger = logger;
            _settings = settings;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(_settings.CalculateKmWaterwaysJobConfiguration.DbTimoutInMin));
                await _mediator.Send(new ScheduledJobExecute.Command
                {
                   Name = ScheduledJobName.CalculatingKmWaterways
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}
