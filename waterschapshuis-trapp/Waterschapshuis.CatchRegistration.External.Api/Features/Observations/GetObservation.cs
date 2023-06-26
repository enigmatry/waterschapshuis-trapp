using System;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Observations
{
    public static partial class GetObservation
    {
        [PublicAPI]
        public class Query: IRequest<Response>
        {
            /// <summary>
            /// ID of observation
            /// </summary>
            public Guid Id { get; set; }

            public Query(Guid id)
            {
                Id = id;
            }
        }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// Observation
            /// </summary>
            public ObservationItem? Observation { get; set; }

            public Response(ObservationItem? observation)
            {
                Observation = observation;
            }
        }

        [PublicAPI]
        public class ObservationItem
        {
            /// <summary>
            /// ID of observation
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of user who created this observation
            /// </summary>
            public string CreatedBy { get; set; } = String.Empty;

            /// <summary>
            /// Damage or other
            /// </summary>
            public string Type { get; set; } = String.Empty;

            /// <summary>
            /// URL to an image of observation on blob storage
            /// </summary>
            public string PhotoUrl { get; set; } = String.Empty;

            /// <summary>
            /// Remarks by this observation
            /// </summary>
            public string Remarks { get; set; } = String.Empty;

            /// <summary>
            /// Longitude (X coordinate) of observation where it is registered
            /// </summary>
            public double Longitude { get; set; }

            /// <summary>
            /// Latitude (Y coordinate) of observation where it is registered
            /// </summary>
            public double Latitude { get; set; }

            /// <summary>
            /// Determines whether observation is archived or still active
            /// </summary>
            public bool Archived { get; set; }

            /// <summary>
            /// Created On
            /// </summary>
            public DateTimeOffset CreatedOn { get; set; }

            /// <summary>
            /// Organization
            /// </summary>
            public string OrganizationName { get; set; } = String.Empty;

            /// <summary>
            /// Water Authority
            /// </summary>
            public string WaterAuthorityName { get; set; } = String.Empty;

            /// <summary>
            /// Rayon
            /// </summary>
            public string RayonName { get; set; } = String.Empty;

            /// <summary>
            /// Catch Area
            /// </summary>
            public string CatchAreaName { get; set; } = String.Empty;

            /// <summary>
            /// Sub Area
            /// </summary>
            public string SubAreaName { get; set; } = String.Empty;

            /// <summary>
            /// Hour Square (Atlas Block)
            /// </summary>
            public string HourSquareName { get; set; } = String.Empty;
        }

        [UsedImplicitly]
        public class ObservationMappingProfile : Profile
        {
            public ObservationMappingProfile(){}
            public ObservationMappingProfile(bool hasPrivatePermission)
            {
                CreateMap<Observation, ObservationItem>()
                    .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.RecordedOn))
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => hasPrivatePermission ? src.CreatedBy.Name : "Geanonimiseerd"))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location.X))
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location.Y))
                    .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.WaterAuthority.Organization.Name))
                    .ForMember(dest => dest.WaterAuthorityName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.WaterAuthority.Name))
                    .ForMember(dest => dest.RayonName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name))
                    .ForMember(dest => dest.CatchAreaName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.CatchArea.Name))
                    .ForMember(dest => dest.SubAreaName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.Name))
                    .ForMember(dest => dest.HourSquareName, opt => opt.MapFrom(src => src.SubAreaHourSquare.HourSquare.Name));
            }
        }
    }
}
