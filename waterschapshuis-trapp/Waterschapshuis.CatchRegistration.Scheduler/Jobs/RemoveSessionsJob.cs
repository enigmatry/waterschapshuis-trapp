using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class RemoveSessionsJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveSessionsJob> _logger;
        private readonly ITimeProvider _timeProvider;
        private readonly SchedulerSettings _settings;

        public RemoveSessionsJob(IMediator mediator,
            IUnitOfWork unitOfWork,
            ILogger<RemoveSessionsJob> logger,
            ITimeProvider timeProvider,
            SchedulerSettings settings)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _timeProvider = timeProvider;
            _settings = settings;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var createdBeforeDate = _timeProvider.Now
                    .Subtract(_settings.RemoveSessionsJobConfiguration.RemoveOlderThanTimespan);

                _logger.LogInformation("Remove expired user session data created before: {CreatedBefore}", createdBeforeDate);

                await _mediator.Send(new RemoveSessions.Command
                {
                    CreatedBeforeDate = createdBeforeDate
                });

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}
