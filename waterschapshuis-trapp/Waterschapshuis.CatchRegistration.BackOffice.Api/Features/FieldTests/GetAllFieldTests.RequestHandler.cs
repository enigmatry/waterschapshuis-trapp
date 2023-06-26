using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest;
using Waterschapshuis.CatchRegistration.DomainModel.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.FieldTests
{
    public partial class GetAllFieldTests
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, PagedResponse<GetFieldTest.Response>>
        {
            private readonly IRepository<FieldTest> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<FieldTest> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<GetFieldTest.Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository
                    .QueryAllAsNoTracking()
                    .AsNoTracking()
                    .SortBy(request.SortField, request.SortDirection)
                    .QueryByKeyword(request.Keyword)
                    .ProjectToPagedResponse<FieldTest, GetFieldTest.Response>(
                        request.CurrentPage,
                        request.PageSize,
                        _mapper,
                        cancellationToken);
            }
        }
    }
}
