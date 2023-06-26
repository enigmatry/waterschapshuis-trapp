using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TrapTypes
{
    public partial class GetAllTrapTypes
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, PagedResponse<GetTrapType.Response>>
        {
            private readonly IRepository<TrapType> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<TrapType> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<GetTrapType.Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .SortBy(request.SortField, request.SortDirection)
                    .QueryByKeyword(request.Keyword)
                    .ProjectToPagedResponse<TrapType, GetTrapType.Response>(
                        request.CurrentPage,
                        request.PageSize,
                        _mapper,
                        cancellationToken);
            }
        }
    }
}
