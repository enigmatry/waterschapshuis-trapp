using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using Waterschapshuis.CatchRegistration.ApplicationServices.Reports.Commands;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class SendWeeklyOverviewReportJob : IJob
    {
        private readonly IMediator _mediator;
        private readonly SchedulerSettings _settings;
        private readonly ILogger<SendWeeklyOverviewReportJob> _logger;

        public SendWeeklyOverviewReportJob(IMediator mediator, SchedulerSettings settings, ILogger<SendWeeklyOverviewReportJob> logger)
        {
            _mediator = mediator;
            _settings = settings;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await _mediator.Send(new WeeklyOverviewReportsGenerate.Command {BackOfficeAppUrl = _settings.BackOfficeAppUrl});
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}
