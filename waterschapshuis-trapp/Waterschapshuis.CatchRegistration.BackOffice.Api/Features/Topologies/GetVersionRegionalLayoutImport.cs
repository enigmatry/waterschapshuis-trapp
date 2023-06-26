using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Topologies
{
    public class GetVersionRegionalLayoutImport
    {
        [PublicAPI]
        public class Query : IRequest<Response> { }

        [PublicAPI]
        public class Response
        {
            public Guid? Id { get; set; }
            public VersionRegionalLayoutImportState State { get; set; }
            public string StartedBy { get; set; } = String.Empty;
            public DateTimeOffset StartedAt { get; set; }
            public DateTimeOffset? FinishedAt { get; set; }
            public string NextVersionRegionalLayoutName { get; set; } = String.Empty;
            public List<string> OutputMessages { get; set; } = new List<string>();
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() =>
                CreateMap<VersionRegionalLayoutImport, Response>()
                    .ForMember(dest => dest.OutputMessages, src => src.MapFrom(opt => opt.GetOutputMessages()));
        }

        [UsedImplicitly]
        public class RequestHanlder : IRequestHandler<Query, Response>
        {
            private readonly IRepository<VersionRegionalLayoutImport> _versionRegionalLayoutImportRepository;
            private readonly IMapper _mapper;

            public RequestHanlder(
                IRepository<VersionRegionalLayoutImport> versionRegionalLayoutImportRepository,
                IMapper mapper)
            {
                _versionRegionalLayoutImportRepository = versionRegionalLayoutImportRepository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var versionRegionalLayoutImports = await _versionRegionalLayoutImportRepository
                    .QueryAll().ToListAsync(cancellationToken);

                if (versionRegionalLayoutImports.Count > 1)
                {
                    throw new InvalidOperationException($"Found {versionRegionalLayoutImports.Count} VersionRegionalLayout import processes");
                }

                return versionRegionalLayoutImports.Any()
                    ? _mapper.Map<Response>(versionRegionalLayoutImports.First())
                    : new Response();
            }
        }
    }
}
