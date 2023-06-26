using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.Observations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Observations
{
    public partial class GetObservations
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, PagedResponse<GetObservationDetails.ResponseItem>>
        {
            private readonly IRepository<Observation> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<Observation> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<GetObservationDetails.ResponseItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .SortBy(request.SortField, request.SortDirection)
                    .ProjectToPagedResponse<Observation, GetObservationDetails.ResponseItem>(
                        request.CurrentPage,
                        request.PageSize,
                        _mapper,
                        cancellationToken);
            }
        }
    }
}
