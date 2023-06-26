using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class AnonymizeInactiveUsersJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SchedulerSettings _settings;
        private readonly ILogger<AnonymizeInactiveUsersJob> _logger;
        private readonly ITimeProvider _timeProvider;

        public AnonymizeInactiveUsersJob(IMediator mediator,
            IUnitOfWork unitOfWork,
            SchedulerSettings settings,
            ILogger<AnonymizeInactiveUsersJob> logger,
            ITimeProvider timeProvider)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _settings = settings;
            _logger = logger;
            _timeProvider = timeProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var requestDate = _timeProvider.Now.AddYears(
                    -_settings.AnonymizeInactiveUsersJobConfiguration.InactivePeriodBeforeAnonymizationInYears);

                _logger.LogInformation(
                    $"Anonymize inactive users before: {requestDate}");

                await _mediator.Send(new AnonymizeInactiveUsers.Command
                {
                    InactiveBefore = requestDate
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
