using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class CompleteRegistrationDataJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SchedulerSettings _settings;
        private readonly ILogger<CompleteRegistrationDataJob> _logger;

        public CompleteRegistrationDataJob(IMediator mediator, IUnitOfWork unitOfWork, SchedulerSettings settings, ILogger<CompleteRegistrationDataJob> logger)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _settings = settings;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var requestDate = DateTimeOffset.Now.AddDays(_settings.CompleteRegistrationDataJobConfiguration.WeeksPeriodInDays)
                    .MondayDateInWeekOfDate().Date;

                await _mediator.Send(new AutocompleteRegistrationDataAfterPeriod.Command
                {
                    Date = requestDate,
                    TimeRegistrationStatus = TimeRegistrationStatus.Completed, 
                    CatchStatus = CatchStatus.Completed
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
