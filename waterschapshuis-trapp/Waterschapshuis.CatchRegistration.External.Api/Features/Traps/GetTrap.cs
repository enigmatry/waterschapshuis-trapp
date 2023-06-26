using System;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Traps
{
    public static partial class GetTrap
    {
        [PublicAPI]
        public class Query: IRequest<Response>
        {
            /// <summary>
            /// ID of trap
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
            /// Trap
            /// </summary>
            public TrapItem? Trap { get; set; }

            public Response(TrapItem? trap)
            {
                Trap = trap;
            }
        }

        [PublicAPI]
        public class TrapItem
        {
            /// <summary>
            /// ID of trap
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// TrapType by name (Conibear, Postklem, ...)
            /// </summary>
            public string TrapType { get; set; } = String.Empty;

            /// <summary>
            /// Determines if the trap is catching (=1), non-catching (=2) or deleted (=3)
            /// </summary>
            public TrapStatus Status { get; set; }

            /// <summary>
            /// Remarks that apply to this trap, entered by a trapper
            /// </summary>
            public string? Remarks { get; set; }

            /// <summary>
            /// Number of traps
            /// </summary>
            public int NumberOfTraps { get; set; }

            /// <summary>
            /// Longitude (X coordinate) of trap where it is registered
            /// </summary>
            public double Longitude { get; set; }

            /// <summary>
            /// Latitude (Y coordinate) of trap where it is registered
            /// </summary>
            public double Latitude { get; set; }

            /// <summary>
            /// Name of user who created this trap
            /// </summary>
            public string CreatedBy { get; set; } = String.Empty;

            /// <summary>
            /// Created on
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
        public class TrapMappingProfile : Profile
        {
            public TrapMappingProfile(){}
            public TrapMappingProfile(bool hasPrivatePermission)
            {
                CreateMap<Trap, TrapItem>()
                    .ForMember(dest => dest.TrapType, opt => opt.MapFrom(src => src.TrapType.Name))
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
