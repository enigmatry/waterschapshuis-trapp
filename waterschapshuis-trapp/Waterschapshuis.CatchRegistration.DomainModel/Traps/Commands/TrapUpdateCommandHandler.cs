using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    [UsedImplicitly]
    public class TrapUpdateCommandHandler : IRequestHandler<TrapUpdate.Command, TrapCreateOrUpdate.Result>
    {
        private readonly IRepository<Trap> _trapRepository;

        public TrapUpdateCommandHandler(IRepository<Trap> trapRepository)
        {
            _trapRepository = trapRepository;
        }

        public async Task<TrapCreateOrUpdate.Result> Handle(TrapUpdate.Command request, CancellationToken cancellationToken)
        {
            var trap = await _trapRepository
                .QueryAllIncluding(x => x.Catches)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            trap.Update(request);

            return TrapCreateOrUpdate.Result.CreateResult(trap.Id);
        }
    }
}
