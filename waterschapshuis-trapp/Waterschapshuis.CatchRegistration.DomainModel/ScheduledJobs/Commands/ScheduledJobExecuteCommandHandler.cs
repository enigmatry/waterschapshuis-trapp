using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands
{
    [UsedImplicitly]
    public class ScheduledJobExecuteCommandHandler : IRequestHandler<ScheduledJobExecute.Command, ScheduledJobExecute.Result>
    {
        private readonly IRepository<ScheduledJob> _scheduledJobRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<ScheduledJobExecuteCommandHandler> _logger;
        private readonly ITimeProvider _timeProvider;
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public ScheduledJobExecuteCommandHandler(
            IRepository<ScheduledJob> scheduledJobRepository,
            IMediator mediator,
            ITimeProvider timeProvider,
            IUnitOfWork unitOfWork,
            ILogger<ScheduledJobExecuteCommandHandler> logger)
        {
            _scheduledJobRepository = scheduledJobRepository;
            _mediator = mediator;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ScheduledJobExecute.Result> Handle(ScheduledJobExecute.Command request,
            CancellationToken cancellationToken)
        {

            ScheduledJob scheduled = await _scheduledJobRepository
                .QueryAll()
                .QueryByJobName(request.Name)
                .QueryScheduledJobs()
                .SingleOrDefaultAsync(cancellationToken);

            ScheduledJobExecute.Result response = ScheduledJobExecute.Result.Create();

            if (scheduled == null)
            {
                return response;
            }

            try
            {
                await UpdateJob(scheduled, ScheduledJobState.Started, GetStartedMessage(request.Name), cancellationToken);

                response = await _mediator.Send(GetJobCommand(request.Name), cancellationToken);

                await UpdateJob(scheduled, response.Succeed ? ScheduledJobState.Succeed : ScheduledJobState.Failed, response.Output, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Error during calculating waterways - {ex}");
                await UpdateJob(scheduled, ScheduledJobState.Failed, GetErrorMessage(request.Name), cancellationToken);
            }
            return response;
        }

        private async Task UpdateJob(ScheduledJob job, 
            ScheduledJobState state, 
            string output,
            CancellationToken cancellationToken)
        {
            if (state == ScheduledJobState.Failed)
            {
                _unitOfWork.Detach();
            }
            job.Update(state, output);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static IRequest<ScheduledJobExecute.Result> GetJobCommand(ScheduledJobName jobName) =>
            jobName switch
            {
                ScheduledJobName.CalculatingKmWaterways => new UpdateSubareaHoursquare.Command(),
                _ => throw new ArgumentOutOfRangeException(nameof(jobName),
                    $"Not expected job name value: {jobName}")
            };

        private static string GetStartedMessage(ScheduledJobName jobName)
        {
            return $"INFO {DateTime.Now.ToString(DateTimeFormat)}: {jobName.GetDescription()} is in progress...";
        }

        private static string GetErrorMessage(ScheduledJobName jobName)
        {
            return $"ERROR {DateTime.Now.ToString(DateTimeFormat)}: {jobName.GetDescription()} failed";
        }
    }
}
