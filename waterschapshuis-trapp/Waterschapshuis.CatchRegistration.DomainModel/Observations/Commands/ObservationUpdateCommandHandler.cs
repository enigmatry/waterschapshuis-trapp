using JetBrains.Annotations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands
{
    [UsedImplicitly]
    public class ObservationUpdateCommandHandler
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<ObservationUpdate.Command, ObservationUpdate.Result>
        {
            private readonly IRepository<Observation> _repository;

            public RequestHandler(IRepository<Observation> repository)
            {
                _repository = repository;
            }

            public async Task<ObservationUpdate.Result> Handle(ObservationUpdate.Command request, CancellationToken cancellationToken)
            {
                var item = await _repository.FindByIdAsync(request.Id);
                item.Update(request);

                return ObservationUpdate.Result.CreateResult(item.Id);
            }
        }
    }
}
