using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    [UsedImplicitly]
    public class TrapDeleteCommandHandler : IRequestHandler<TrapDelete.Command, bool>
    {
        private readonly IRepository<Trap> _trapRepository;
        private readonly ICurrentUserProvider _currentUserProvider;

        public TrapDeleteCommandHandler(
            IRepository<Trap> trapRepository,
            ICurrentUserProvider currentUserProvider)
        {
            _trapRepository = trapRepository;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<bool> Handle(TrapDelete.Command request, CancellationToken cancellationToken)
        {
            var trap = await _trapRepository
                .QueryAllIncluding(t => t.Catches)
                .QueryById(request.Id)
                .SingleOrDefaultAsync(cancellationToken)
                       ?? throw new InvalidOperationException("Trap doesn't exist.");

            if (trap.Catches.Count > 0)
                throw new InvalidOperationException("Cannot delete trap with catches.");

            if (!trap.CreatedToday)
                throw new InvalidOperationException("Cannot delete trap that is not created today.");

            if (trap.CreatedById != _currentUserProvider.UserId)
                throw new InvalidOperationException("Cannot delete trap that is created by another user.");

            _trapRepository.Delete(trap);

            return true;
        }
    }
}
