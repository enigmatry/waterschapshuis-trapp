using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands
{
    public static partial class CatchDelete
    {
        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly IRepository<Trap> _trapRepository;
            private readonly ITimeProvider _timeProvider;

            public CommandHandler(
                IRepository<Trap> trapRepository,
                ITimeProvider timeProvider)
            {
                _trapRepository = trapRepository;
                _timeProvider = timeProvider;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                var trap = await _trapRepository
                    .QueryAll().BuildInclude()
                    .TryFindByCatchId(command.Id, cancellationToken)
                        ?? throw new InvalidOperationException("Trap with catch doesn't exist, for given CatchId.");

                var catchForRemoval = trap.Catches.Single(x => x.Id == command.Id);

                if (!catchForRemoval.CreatedToday(_timeProvider))
                    throw new InvalidOperationException("Cannot delete catch that is not created today.");

                trap.RemoveCatch(catchForRemoval.Id);

                return Unit.Value;
            }
        }
    }
}
