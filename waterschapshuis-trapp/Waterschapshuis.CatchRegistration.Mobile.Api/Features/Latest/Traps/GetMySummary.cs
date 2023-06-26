using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Traps
{
    public static partial class GetMySummary
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public bool IncludeDetails { get; set; }

            public static Query Create(bool includeDetails)
            {
                return new Query { IncludeDetails = includeDetails };
            }
        }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// Amount of catches this week
            /// </summary>
            public int CatchesThisWeek { get; set; }

            /// <summary>
            /// Amount of catching traps
            /// </summary>
            public int OutstandingTraps { get; set; }

            /// <summary>
            /// List of trap details
            /// </summary>
            public IEnumerable<TrapInfo> TrapDetails { get; set; } = new List<TrapInfo>();
        }

        [PublicAPI]
        public class TrapInfo
        {
            /// <summary>
            /// Label consisting of trap type and trapping type
            /// </summary>
            public string TypeLabel { get; set; } = String.Empty;

            /// <summary>
            /// Date when the trap is created
            /// </summary>
            public DateTimeOffset DateCreated { get; set; }

            /// <summary>
            /// Amount of day passed since last catch
            /// </summary>
            public int DaysSinceCatch { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Trap, TrapInfo>()
                    .ForMember(dest => dest.DateCreated,
                        opt => opt.MapFrom(src => src.RecordedOn))
                    .ForMember(dest => dest.TypeLabel,
                        opt => opt.MapFrom(src => $"{src.TrapType.Name} ({src.TrapType.TrappingType.Name})"))
                    .AfterMap<DaysSinceLastCatchAfterMapAction>();
            }
        }

        [UsedImplicitly]
        public class DaysSinceLastCatchAfterMapAction : IMappingAction<Trap, TrapInfo>
        {
            private readonly ITimeProvider _timeProvider;

            public DaysSinceLastCatchAfterMapAction(ITimeProvider timeProvider)
            {
                _timeProvider = timeProvider;
            }

            public void Process(Trap source, TrapInfo destination, ResolutionContext context)
            {
                destination.DaysSinceCatch = source.Catches.Any() 
                    ? (int)(_timeProvider.Now.Date - source.Catches.Max(x => x.RecordedOn).Date).TotalDays 
                    : -1;
            }
        }

        private static IQueryable<Trap> BuildInclude(this IQueryable<Trap> query)
        {
            return query
                .Include(c => c.Catches)
                .Include(c => c.TrapType)
                .ThenInclude(c => c.TrappingType);
        }
    }
}
