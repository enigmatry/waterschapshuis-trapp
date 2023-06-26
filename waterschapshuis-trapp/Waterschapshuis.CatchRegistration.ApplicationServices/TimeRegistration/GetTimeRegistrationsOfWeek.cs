using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration
{
    public static partial class GetTimeRegistrationsOfWeek
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public DateTimeOffset StartDate { get; set; }
            public DateTimeOffset EndDate { get; set; }
            public Guid? UserId { get; set; } = null!;
            public Guid? RayonId { get; set; } = null!;

            public static Query Create(DateTimeOffset startDate, DateTimeOffset endDate, Guid? userId, Guid? rayonId)
            {
                return new Query { StartDate = startDate, EndDate = endDate, UserId = userId, RayonId = rayonId };
            }
        }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// List of hours per day for the given week per user
            /// </summary>
            public IEnumerable<TimeRegistrationsOfDate> DaysOfWeek { get; set; } = Enumerable.Empty<TimeRegistrationsOfDate>();

            /// <summary>
            /// List of catches for the given week per user
            /// </summary>
            public IEnumerable<CatchItem> Catches { get; set; } = Enumerable.Empty<CatchItem>();

            /// <summary>
            /// List of general time registrations for the given week per user
            /// </summary>
            public IEnumerable<TimeRegistrationGeneral> TimeRegistrationGeneralItems { get; set; } = Enumerable.Empty<TimeRegistrationGeneral>();

            [PublicAPI]
            public class TimeRegistrationsOfDate
            {
                /// <summary>
                /// Day for which time registrations are listed
                /// </summary>
                public DateTimeOffset Date { get; set; }
                public IEnumerable<TimeRegistration> TimeRegistrations { get; set; } = Enumerable.Empty<TimeRegistration>();
            }

            [PublicAPI]
            public class TimeRegistration
            {
                /// <summary>
                /// GUID of time registration
                /// </summary>
                public Guid? Id { get; set; }

                /// <summary>
                /// Day for which time registration is entered
                /// </summary>
                public DateTimeOffset Date { get; set; }

                /// <summary>
                /// Catch Area where time is loged
                /// </summary>
                public NamedEntity.Item CatchArea { get; set; } = null!;

                /// <summary>
                /// Sub Area where time is loged
                /// </summary>
                public NamedEntity.Item SubArea { get; set; } = null!;

                /// <summary>
                /// Hour Square (Atlas Block) where time is loged
                /// </summary>
                public NamedEntity.Item HourSquare { get; set; } = null!;

                /// <summary>
                /// Trapping type the time is spent ton
                /// </summary>
                public NamedEntity.Item TrappingType { get; set; } = null!;

                /// <summary>
                /// Amount of spent hours
                /// </summary>
                public int? Hours { get; set; }

                /// <summary>
                /// Amount of spent minutes
                /// </summary>
                public int? Minutes { get; set; }

                /// <summary>
                /// Status of the time registration entry: written, closed, completed
                /// </summary>
                public TimeRegistrationStatus Status { get; set; }
            }

            [PublicAPI]
            public class CatchItem
            {
                /// <summary>
                /// GUID of the catch
                /// </summary>
                public Guid Id { get; set; }

                /// <summary>
                /// Date when the catch is stored in database
                /// </summary>
                public DateTimeOffset CreatedOn { get; set; }

                /// <summary>
                /// Date when the catch is registered on mobile
                /// </summary>
                public DateTimeOffset RecordedOn { get; set; }

                /// <summary>
                /// Catch Area where catch is logged
                /// </summary>
                public NamedEntity.Item CatchArea { get; set; } = null!;

                /// <summary>
                /// Sub Area where catch is logged
                /// </summary>
                public NamedEntity.Item SubArea { get; set; } = null!;

                /// <summary>
                /// Hour Square (Atlas Block) where catch is logged
                /// </summary>
                public NamedEntity.Item HourSquare { get; set; } = null!;

                /// <summary>
                /// Guid of catch type of catch
                /// </summary>
                public Guid CatchTypeId { get; set; }

                /// <summary>
                /// Name of catch type of catch
                /// </summary>
                public NamedEntity.Item CatchType { get; set; } = null!;

                /// <summary>
                /// Number of catches
                /// </summary>
                public int Number { get; set; }

                /// <summary>
                /// Status of catch: written, closed, completed
                /// </summary>
                public CatchStatus Status { get; set; }

                /// <summary>
                /// Indicator for catch or by-catch
                /// </summary>
                public bool IsByCatch { get; set; }
            }

            [PublicAPI]
            public class TimeRegistrationGeneral
            {
                /// <summary>
                /// GUID of the time registration general item
                /// </summary>
                public Guid Id { get; set; }
                /// <summary>
                /// Category of time registration general item
                /// </summary>
                public NamedEntity.Item Category { get; set; } = null!;
                /// <summary>
                /// Date time registration general item was entered
                /// </summary>
                public DateTimeOffset Date { get; set; }
                /// <summary>
                /// Status of time registration general item: written, closed, completed
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
                    CreateMap<DomainModel.TimeRegistrations.TimeRegistration, TimeRegistration>()
                        .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date))
                        .ForMember(dest => dest.CatchArea,
                            opts => opts.MapFrom(src => src.SubAreaHourSquare.SubArea.CatchArea))
                        .ForMember(dest => dest.SubArea, opts => opts.MapFrom(src => src.SubAreaHourSquare.SubArea))
                        .ForMember(dest => dest.HourSquare,
                            opts => opts.MapFrom(src => src.SubAreaHourSquare.HourSquare))
                        .ForMember(dest => dest.Hours, opts => opts.MapFrom(src => src.GetHours()))
                        .ForMember(dest => dest.Minutes, opts => opts.MapFrom(src => src.GetMinutes()))
                        .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

                    CreateMap<Catch, CatchItem>()
                        .ForMember(dest => dest.IsByCatch,
                            opts => opts.MapFrom(src => src.CatchType.IsByCatch))
                        .ForMember(dest => dest.CatchArea,
                            opts => opts.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea.CatchArea))
                        .ForMember(dest => dest.SubArea, opts => opts.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea))
                        .ForMember(dest => dest.HourSquare,
                            opts => opts.MapFrom(src => src.Trap.SubAreaHourSquare.HourSquare));

                    CreateMap<DomainModel.TimeRegistrations.TimeRegistrationGeneral, TimeRegistrationGeneral>()
                        .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.TimeRegistrationCategory))
                        .ForMember(dest => dest.Hours, opts => opts.MapFrom(src => src.GetHours()))
                        .ForMember(dest => dest.Minutes, opts => opts.MapFrom(src => src.GetMinutes()));

                }
            }
        }
    }
}
