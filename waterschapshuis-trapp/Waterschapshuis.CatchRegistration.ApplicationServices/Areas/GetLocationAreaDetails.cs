using System;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Areas
{
    public static partial class GetLocationAreaDetails
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            private Query()
            {
            }

            public double Longitude { get; private set; }
            public double Latitude { get; private set; }

            public static Query Create(double longitude, double latitude)
            {
                return new Query { Longitude = longitude, Latitude = latitude };
            }
        }

        [PublicAPI]
        public class Response
        {
            public Guid SubAreaHourSquareId { get; set; } = Guid.Empty;

            public NamedEntity.Item HourSquare { get; set; } = null!;
            public NamedEntity.Item SubArea { get; set; } = null!;
            public NamedEntity.Item CatchArea { get; set; } = null!;
            public NamedEntity.Item Rayon { get; set; } = null!;
            public NamedEntity.Item Organization { get; set; } = null!;
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<SubAreaHourSquare, Response>()
                    .ForMember(dest => dest.SubAreaHourSquareId, opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.CatchArea, opts => opts.MapFrom(src => src.SubArea.CatchArea))
                    .ForMember(dest => dest.Rayon, opts => opts.MapFrom(src => src.SubArea.CatchArea.Rayon))
                    .ForMember(dest => dest.Organization, opts => opts.MapFrom(src => src.SubArea.CatchArea.Rayon.Organization));
            }
        }
    }
}
