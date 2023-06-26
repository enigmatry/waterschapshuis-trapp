using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.CatchTypes
{
    public partial class GetAllCatchTypes
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, PagedResponse<GetCatchType.Response>>
        {
            private readonly IRepository<CatchType> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<CatchType> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<GetCatchType.Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .SortBy(request.SortField, request.SortDirection)
                    .QueryByKeyword(request.Keyword)
                    .ProjectToPagedResponse<CatchType, GetCatchType.Response>(
                        request.CurrentPage,
                        request.PageSize,
                        _mapper,
                        cancellationToken);
            }
        }
    }
}
