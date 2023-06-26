using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands
{
    [UsedImplicitly]
    public class
        UpdateStatusAndRemarksCommandHandler : IRequestHandler<UpdateStatusAndRemarks.Command,
            UpdateStatusAndRemarks.Result>
    {
        private readonly IRepository<Observation> _repository;

        public UpdateStatusAndRemarksCommandHandler(IRepository<Observation> repository)
        {
            _repository = repository;
        }

        public async Task<UpdateStatusAndRemarks.Result> Handle(UpdateStatusAndRemarks.Command request,
            CancellationToken cancellationToken)
        {
            var observation = await _repository.FindByIdAsync(request.Id);
            observation.UpdateStatusAndRemarks(request);

            return UpdateStatusAndRemarks.Result.CreateResult(observation.Id);
        }
    }
}
