using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using NetTopologySuite.IO;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Topologies
{
    public class GetSubAreaCsvRecords
    {
        [PublicAPI]
        public class Query : IRequest<ListResponse<SubAreaCsvRecord>> { }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                var wktWriter = new WKTWriter()
                {
                    PrecisionModel = GeometryUtil.Factory.PrecisionModel
                };

                CreateMap<SubArea, SubAreaCsvRecord>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.CatchAreaName, opt => opt.MapFrom(src => src.CatchArea.Name))
                    .ForMember(dest => dest.RayonName, opt => opt.MapFrom(src => src.CatchArea.Rayon.Name))
                    .ForMember(dest => dest.WaterAuthorityName, opt => opt.MapFrom(src => src.WaterAuthority.Name))
                    .ForMember(dest => dest.GeometryAsWKT, opt => opt.MapFrom(src => wktWriter.Write(src.Geometry)));
            }
        }

        [UsedImplicitly]
        public class GetSubAreaCsvRecordsRequestHanlder : IRequestHandler<Query, ListResponse<SubAreaCsvRecord>>
        {
            private readonly IMapper _mapper;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

            public GetSubAreaCsvRecordsRequestHanlder(
                IMapper mapper,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _mapper = mapper;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<ListResponse<SubAreaCsvRecord>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _currentVersionRegionalLayoutService
                    .QuerySubAreasNoTracking(x => x.WaterAuthority, x => x.CatchArea.Rayon)
                    .ProjectToListResponse<SubArea, SubAreaCsvRecord>(_mapper, cancellationToken);
            }
        }
    }
}
