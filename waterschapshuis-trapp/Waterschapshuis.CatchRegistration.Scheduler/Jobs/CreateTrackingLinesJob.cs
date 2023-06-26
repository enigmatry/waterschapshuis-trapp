using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class CreateTrackingLinesJob : IJob
    {
        private readonly CatchRegistrationDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly SchedulerSettings _settings;
        private readonly ILogger<CreateTrackingLinesJob> _logger;

        public CreateTrackingLinesJob(CatchRegistrationDbContext dbContext,
            IMediator mediator, 
            SchedulerSettings settings, 
            ILogger<CreateTrackingLinesJob> logger)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _settings = settings;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(_settings.CreateTrackingLineJobConfiguration.DbTimoutInMin));

            try
            {
                var requestDate = DateTimeOffset.Now.Date.Date.AddDays(_settings.CreateTrackingLineJobConfiguration.CurrentDateDeltaInDays);
                await _mediator.Send(new TrackingLineCreate.Command { Date = requestDate });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}
