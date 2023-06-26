using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Topologies
{
    public class GetVersionRegionalLayouts
    {
        [PublicAPI]
        public class Query : IRequest<ListResponse<VersionRegionalLayoutResponse>> { }

        [PublicAPI]
        public class VersionRegionalLayoutResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = String.Empty;
            public DateTimeOffset StartDate { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<VersionRegionalLayout, VersionRegionalLayoutResponse>();
        }

        [UsedImplicitly]
        public class GetVersionRegionalLayoutsRequestHanlder : IRequestHandler<Query, ListResponse<VersionRegionalLayoutResponse>>
        {
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly IMapper _mapper;

            public GetVersionRegionalLayoutsRequestHanlder(
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService,
                IMapper mapper)
            {
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
                _mapper = mapper;
            }

            public Task<ListResponse<VersionRegionalLayoutResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var allVersionRegionalLayouts = _currentVersionRegionalLayoutService
                    .All
                    .OrderBy(x => x.StartDate)
                    .ProjectToListResponse<VersionRegionalLayout, VersionRegionalLayoutResponse>(_mapper);
                return Task.FromResult(allVersionRegionalLayouts);
            }
        }
    }
}
