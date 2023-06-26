using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.FieldTests
{
    public static partial class GetFieldTest
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public Guid Id { get; set; }

            public static Query ById(Guid id) => new Query { Id = id };
        }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// GUID of field test
            /// </summary>
            public Guid Id { get; set; } = Guid.Empty;

            /// <summary>
            /// Name of the field test
            /// </summary>
            public string Name { get; set; } = String.Empty;

            /// <summary>
            /// Begin date of field test
            /// </summary>
            public string StartPeriod { get; set; } = String.Empty;

            /// <summary>
            /// End date of field test
            /// </summary>
            public string EndPeriod { get; set; } = String.Empty;

            /// <summary>
            /// List of hour squares (atlas blocks) incorporated in field test
            /// </summary>
            public IEnumerable<HourSquare> HourSquares { get; set; } = new List<HourSquare>();

            public class HourSquare
            {
                /// <summary>
                /// GUID of hour square (atlas block)
                /// </summary>
                public Guid Id { get; set; }

                /// <summary>
                /// Name of hour square (atlas block)
                /// </summary>
                public string Name { get; set; } = String.Empty;
            }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<FieldTest, Response>()
                    .ForMember(dest => dest.StartPeriod,
                        opt => opt.MapFrom(
                            src => src.StartPeriod.YearPeriodValue))
                    .ForMember(dest => dest.EndPeriod,
                        opt => opt.MapFrom(
                            src => src.EndPeriod.YearPeriodValue))
                    .ForMember(dest => dest.HourSquares,
                        opt => opt.MapFrom(
                            src => src.FieldTestHourSquares.Select(x => x.HourSquare)));

                CreateMap<HourSquare, Response.HourSquare>();
            }
        }
    }
}
