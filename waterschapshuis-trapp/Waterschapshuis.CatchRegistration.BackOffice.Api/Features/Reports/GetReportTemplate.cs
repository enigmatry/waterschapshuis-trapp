using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    public class GetReportTemplate
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public Guid TemplateId { get; set; }
        }

        [PublicAPI]
        public class Response
        {
            public GetReportTemplates.Response.Item Item { get; set; } = null!;
        }

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
                var item = await _repository
                    .QueryAll()
                    .QueryActive()
                    .SingleOrDefaultAsync(x => x.Id == request.TemplateId, cancellationToken);

                return new Response { Item = _mapper.Map<GetReportTemplates.Response.Item>(item) };
            }
        }
    }
}
