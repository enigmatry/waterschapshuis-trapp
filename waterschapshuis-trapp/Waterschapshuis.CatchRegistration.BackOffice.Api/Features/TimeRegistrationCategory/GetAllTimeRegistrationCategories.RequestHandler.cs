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

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TimeRegistrationCategory
{
    public partial class GetAllTimeRegistrationCategories
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, PagedResponse<GetTimeRegistrationCategory.Response>>
        {
            private readonly IRepository<DomainModel.TimeRegistrations.TimeRegistrationCategory> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<DomainModel.TimeRegistrations.TimeRegistrationCategory> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<GetTimeRegistrationCategory.Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .SortBy(request.SortField, request.SortDirection)
                    .QueryByKeyword(request.Keyword)
                    .ProjectToPagedResponse<DomainModel.TimeRegistrations.TimeRegistrationCategory, GetTimeRegistrationCategory.Response>(
                        request.CurrentPage,
                        request.PageSize,
                        _mapper,
                        cancellationToken);
            }
        }
    }
}
