using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands
{
    public static partial class TrackingSync
    {
        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IMediator _mediator;
            private readonly IRepository<Tracking> _trackingRepository;
            private readonly ICurrentUserProvider _currentUserProvider;

            public CommandHandler(
                IMediator mediator, 
                IRepository<Tracking> trackingRepository, 
                ICurrentUserProvider currentUserProvider)
            {
                _mediator = mediator;
                _trackingRepository = trackingRepository;
                _currentUserProvider = currentUserProvider;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            { 
                 // TODO API Patch: change accepted parameter from Guid to String so that we can avoid bad request
                var trackingLocations = request.TrackingLocations
                    .Where(tl => !String.IsNullOrEmpty(tl.Id) && Guid.TryParse(tl.Id, out var id))
                    .ToList();

                var trackings = trackingLocations
                    .OrderBy(x => x.RecordedOn)
                    .Select(Tracking.Create)
                    .ToList();

                var trackingsDb = await _trackingRepository
                    .QueryAll()
                    .ContainingIds(trackingLocations.Select(t => Guid.Parse(t.Id)).ToList())
                    .ToListAsync(cancellationToken);

                var notSynchronizedTrackings = trackings
                    .Where(tracking => !trackingsDb.Exists(t => t.Id == tracking.Id));

                _trackingRepository.AddRange(notSynchronizedTrackings);

                var userId = _currentUserProvider.UserId.GetValueOrDefault();
                var command = TimeRegistrationsCreate.Command.Create(
                    notSynchronizedTrackings.Where(x => x.IsTimewriting).ToList(),
                    userId
                );

                await _mediator.Send(command, cancellationToken);
            
                return Result.CreateResult();
            }
        }
    }
}
