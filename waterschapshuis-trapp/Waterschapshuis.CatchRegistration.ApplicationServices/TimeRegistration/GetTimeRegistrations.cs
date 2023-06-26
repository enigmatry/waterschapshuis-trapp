using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration
{
    public static partial class GetTimeRegistrations
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public DateTimeOffset Date { get; set; }
            public DateTimeOffset? EndDate { get; set; }
            public Guid? SubAreaHourSquareId { get; set; }

            public static Query Create(DateTimeOffset date, DateTimeOffset? endDate = null)
            {
                return new Query { Date = date, EndDate = endDate };
            }

            public static Query Create(DateTimeOffset date, Guid? subAreaHourSquareId)
            {
                return new Query { Date = date, SubAreaHourSquareId = subAreaHourSquareId };
            }
        }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// Day this time registration applies to
            /// </summary>
            public DateTimeOffset Date { get; set; }

            public bool CanAddNew { get; set; }

            /// <summary>
            /// List of time registrations for this day
            /// </summary>
            public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();

            /// <summary>
            /// List of general time registrations for this day
            /// </summary>
            public IEnumerable<GeneralItem> GeneralItems { get; set; } = Enumerable.Empty<GeneralItem>();

            public int TotalTimeOfFilteredOutItems { get; set; }

            [PublicAPI]
            public class Item
            {
                /// <summary>
                /// GUID of the time registration record
                /// </summary>
                public Guid Id { get; set; }

                /// <summary>
                /// Category
                /// </summary>
                public NamedEntity.Item Category { get; set; } = null!;

                /// <summary>
                /// Catch Area
                /// </summary>
                public NamedEntity.Item CatchArea { get; set; } = null!;

                /// <summary>
                /// Sub Area
                /// </summary>
                public NamedEntity.Item SubArea { get; set; } = null!;

                /// <summary>
                /// Hour Square (Atlas Block)
                /// </summary>
                public NamedEntity.Item HourSquare { get; set; } = null!;

                /// <summary>
                /// Trapping type
                /// </summary>
                public NamedEntity.Item TrappingType { get; set; } = null!;

                /// <summary>
                /// Day for which this time is logged
                /// </summary>
                public DateTimeOffset Date { get; set; }

                /// <summary>
                /// Status of this time registration record
                /// </summary>
                public TimeRegistrationStatus Status { get; set; }

                /// <summary>
                /// Amount of hours
                /// </summary>
                public int Hours { get; set; }

                /// <summary>
                /// Amount of minutes
                /// </summary>
                public int Minutes { get; set; }
            }

            public class GeneralItem
            {
                /// <summary>
                /// GUID of the general time registration record
                /// </summary>
                public Guid Id { get; set; }

                /// <summary>
                /// Category
                /// </summary>
                public NamedEntity.Item Category { get; set; } = null!;

                /// <summary>
                /// Day for which this time is logged
                /// </summary>
                public DateTimeOffset Date { get; set; }
                /// <summary>
                /// Status of this time general registration record
                /// </summary>
                public TimeRegistrationStatus Status { get; set; }
                /// <summary>
                /// Amount of spent hours
                /// </summary>
                public int Hours { get; set; }
                /// <summary>
                /// Amount of spent minutes
                /// </summary>
                public int Minutes { get; set; }
            }

            [UsedImplicitly]
            public class MappingProfile : Profile
            {
                public MappingProfile()
                {
                    CreateMap<DomainModel.TimeRegistrations.TimeRegistration, Item>()
                        .ForMember(dest => dest.CatchArea, opts => opts.MapFrom(src => src.SubAreaHourSquare.SubArea.CatchArea))
                        .ForMember(dest => dest.SubArea, opts => opts.MapFrom(src => src.SubAreaHourSquare.SubArea))
                        .ForMember(dest => dest.HourSquare, opts => opts.MapFrom(src => src.SubAreaHourSquare.HourSquare))
                        .ForMember(dest => dest.Hours, opts => opts.MapFrom(src => src.GetHours()))
                        .ForMember(dest => dest.Minutes, opts => opts.MapFrom(src => src.GetMinutes()));

                    CreateMap<TimeRegistrationGeneral, GeneralItem>()
                        .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.TimeRegistrationCategory))
                        .ForMember(dest => dest.Hours, opts => opts.MapFrom(src => src.GetHours()))
                        .ForMember(dest => dest.Minutes, opts => opts.MapFrom(src => src.GetMinutes()));

                    CreateMap<TimeRegistrationGeneral, Item>()
                        .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.TimeRegistrationCategory))
                        .ForMember(dest => dest.Hours, opts => opts.MapFrom(src => src.GetHours()))
                        .ForMember(dest => dest.Minutes, opts => opts.MapFrom(src => src.GetMinutes()));
                }
            }
        }
        private static IQueryable<DomainModel.TimeRegistrations.TimeRegistration> BuildInclude(this IQueryable<DomainModel.TimeRegistrations.TimeRegistration> query)
        {
            return query.Include(x => x.SubAreaHourSquare.SubArea.CatchArea)
                .Include(x => x.SubAreaHourSquare.SubArea)
                .Include(x => x.SubAreaHourSquare.HourSquare)
                .Include(x => x.TrappingType);
        }
    }
}
