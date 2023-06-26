using System;
using System.Runtime.Serialization;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.TimeRegistrations
{
    public static partial class GetTimeRegistration
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            /// <summary>
            /// ID of time registration
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
            /// Time registration
            /// </summary>
            public TimeRegistrationItem? TimeRegistration { get; set; }

            public Response(TimeRegistrationItem? timeRegistration)
            {
                TimeRegistration = timeRegistration;
            }
        }

        [PublicAPI]
        public class TimeRegistrationItem
        {
            /// <summary>
            /// ID of time registrations
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of user
            /// </summary>
            public string User { get; set; } = String.Empty;
            [IgnoreDataMember]
            public DateTimeOffset DateOffset { get; set; }

            /// <summary>
            /// Date
            /// </summary>
            public string Date { get; set; } = String.Empty;

            /// <summary>
            /// Number of worked hours for this day
            /// </summary>
            public int Hours { get; set; }

            /// <summary>
            /// Number of worked minutes for this day
            /// </summary>
            public int Minutes { get; set; }

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
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<TimeRegistration, TimeRegistrationItem>()
                    .ForMember(dest => dest.User, opts => opts.MapFrom(src => src.User.Name))
                    .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.WaterAuthority.Organization.Name))
                    .ForMember(dest => dest.WaterAuthorityName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.WaterAuthority.Name))
                    .ForMember(dest => dest.RayonName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name))
                    .ForMember(dest => dest.CatchAreaName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.CatchArea.Name))
                    .ForMember(dest => dest.SubAreaName, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.Name))
                    .ForMember(dest => dest.DateOffset, opts => opts.MapFrom(src => src.Date))
                    .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString("MM/dd/yyyy")))
                    .ForMember(dest => dest.Hours, opts => opts.MapFrom(src => src.GetHours()))
                    .ForMember(dest => dest.Minutes, opts => opts.MapFrom(src => src.GetMinutes()))
                    .ForMember(dest => dest.HourSquareName, opt => opt.MapFrom(src => src.SubAreaHourSquare.HourSquare.Name));
            }
        }
    }
}
