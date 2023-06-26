using System;
using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    [UsedImplicitly]
    public class
        TrapTypeCreateOrUpdateCommandHandler : IRequestHandler<TrapTypeCreateOrUpdate.Command, TrapTypeCreateOrUpdate.Result>
    {
        private readonly IRepository<TrapType> _trapTypeRepository;

        public TrapTypeCreateOrUpdateCommandHandler(
            IRepository<TrapType> trapTypeRepository)
        {
            _trapTypeRepository = trapTypeRepository;
        }

        public async Task<TrapTypeCreateOrUpdate.Result> Handle(
            TrapTypeCreateOrUpdate.Command request,
            CancellationToken cancellationToken)
        {

            TrapType trapType;

            if (request.Id.HasValue)
            {
                trapType = await _trapTypeRepository.FindByIdAsync(request.Id.Value);
                trapType.Update(request);
            }
            else
            {
                trapType = TrapType.Create(request);
                _trapTypeRepository.Add(trapType);
            }

            return TrapTypeCreateOrUpdate.Result.CreateResult(trapType.Id);
        }
    }
}
