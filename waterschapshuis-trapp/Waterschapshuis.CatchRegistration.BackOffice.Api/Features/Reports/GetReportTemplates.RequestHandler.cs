using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    public static partial class GetReportTemplates
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IRepository<ReportTemplate> _repository;
            private readonly IMapper _mapper;

            public RequestHandler(IRepository<ReportTemplate> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var items = await _repository
                    .QueryAll()
                    .QueryActive()
                    .QueryByExported(false)
                    .ProjectTo<Response.Item>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new Response {Items = items};
            }
        }
    }
}
