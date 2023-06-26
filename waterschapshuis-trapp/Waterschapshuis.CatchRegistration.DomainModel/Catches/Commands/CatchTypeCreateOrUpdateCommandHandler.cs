using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands
{
    [UsedImplicitly]
    public class
        CatchTypeCreateOrUpdateCommandHandler : IRequestHandler<CatchTypeCreateOrUpdate.Command, CatchTypeCreateOrUpdate.Result>
    {
        private readonly IRepository<CatchType> _catchTypeRepository;

        public CatchTypeCreateOrUpdateCommandHandler(
            IRepository<CatchType> catchTypeRepository
        )
        {
            _catchTypeRepository = catchTypeRepository;
        }

        public async Task<CatchTypeCreateOrUpdate.Result> Handle(CatchTypeCreateOrUpdate.Command request,
            CancellationToken cancellationToken)
        {
            CatchType catchType;
            if (request.Id.HasValue)
            {
                catchType = await _catchTypeRepository.FindByIdAsync(request.Id.Value);
                catchType.Update(request);
            }
            else
            {
                catchType = CatchType.Create(request);
                _catchTypeRepository.Add(catchType);
            }

            return CatchTypeCreateOrUpdate.Result.CreateResult(catchType.Id);
        }
    }
}
