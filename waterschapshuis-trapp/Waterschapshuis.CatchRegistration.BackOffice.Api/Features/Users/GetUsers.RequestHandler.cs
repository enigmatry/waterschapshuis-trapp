using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users
{
    public partial class GetUsers
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, PagedResponse<ResponseItem>>
        {
            private readonly IRepository<User> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<User> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<ResponseItem>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAll()
                    .SortBy(request.SortField, request.SortDirection)
                    .QueryByKeyword(request.Keyword)
                    .ProjectToPagedResponse<User, ResponseItem>(
                        request.CurrentPage, 
                        request.PageSize, 
                        _mapper, 
                        cancellationToken);
            }
        }
    }
}
