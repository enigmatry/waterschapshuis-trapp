using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands
{
    [UsedImplicitly]
    public class ScheduledJobCreateCommandHandler : IRequestHandler<ScheduledJobCreate.Command,
        ScheduledJobCreate.Result>
    {
        private readonly IRepository<ScheduledJob> _scheduledJobRepository;
        private readonly ILogger<ScheduledJobCreateCommandHandler> _logger;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITimeProvider _timeProvider;

        public ScheduledJobCreateCommandHandler(
            IRepository<ScheduledJob> scheduledJobRepository,
            ICurrentUserProvider currentUserProvider,
            ITimeProvider timeProvider,
            ILogger<ScheduledJobCreateCommandHandler> logger)
        {
            _scheduledJobRepository = scheduledJobRepository;
            _currentUserProvider = currentUserProvider;
            _timeProvider = timeProvider;
            _logger = logger;
        }

        public Task<ScheduledJobCreate.Result> Handle(ScheduledJobCreate.Command request,
            CancellationToken cancellationToken)
        {
            ScheduledJob job = ScheduledJob.Create(request);
            job.SetCreated(_timeProvider.Now, _currentUserProvider.GetUserId());
            _scheduledJobRepository.Add(job);
            _logger.LogInformation($"New job {request.Name} scheduled");
            return Task.FromResult(ScheduledJobCreate.Result.Create(job.Id));
        }
    }
}
