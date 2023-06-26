using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Traps
{
    public static partial class GetCatchDetails
    {
        [PublicAPI]
        public class Query : IRequest<CatchItem>
        {
            public Guid Id { get; set; }

            public static Query ById(Guid id)
            {
                return new Query { Id = id };
            }
        }

        [PublicAPI]
        public class CatchItem
        {
            /// <summary>
            /// GUID of catch
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of the type of catch
            /// </summary>
            public string Type { get; set; } = null!;

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
            /// Number of catches of this type
            /// </summary>
            public int Number { get; set; }

            /// <summary>
            /// Indicator catch or by-catch
            /// </summary>
            public bool IsByCatch { get; set; }

            /// <summary>
            /// Name of the user who created the catch
            /// </summary>
            public string CreatedBy { get; set; } = null!;

            /// <summary>
            /// GUID of the user who created the catch
            /// </summary>
            public Guid CreatedById { get; set; }

            /// <summary>
            /// Date when the catch is stored in database
            /// </summary>
            public DateTimeOffset CreatedOn { get; set; }

            /// <summary>
            /// Date when the catch is registered on mobile
            /// </summary>
            public DateTimeOffset RecordedOn { get; set; }

            /// <summary>
            /// GUID of the catch type
            /// </summary>
            public Guid CatchTypeId { get; set; }

            /// <summary>
            /// Catch status: written, closed, completed
            /// </summary>
            public CatchStatus Status { get; set; }

            /// <summary>
            /// Indicator weather catch can be edited
            /// </summary>
            public bool CanBeEdited { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // https://docs.automapper.org/en/stable/Queryable-Extensions.html#parameterization
                var parameters = MappingParameters.CreateEmpty();

                CreateMap<Catch, CatchItem>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.CatchType.Name))
                    .ForMember(dest => dest.CatchArea, opts => opts.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea.CatchArea))
                    .ForMember(dest => dest.SubArea, opts => opts.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea))
                    .ForMember(dest => dest.HourSquare, opts => opts.MapFrom(src => src.Trap.SubAreaHourSquare.HourSquare))
                    .ForMember(dest => dest.IsByCatch, opt => opt.MapFrom(src => src.CatchType.IsByCatch))
                    .ForMember(dest => dest.CatchTypeId, opt => opt.MapFrom(src => src.CatchType.Id))
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapAnonymizedCreatedBy(parameters))
                    .ForMember(dest => dest.CanBeEdited, opt => opt.MapCatchCanBeEdited(parameters));
            }
        }

        private static IQueryable<Catch> BuildInclude(this IQueryable<Catch> query) =>
            query
                .Include(c => c.CatchType)
                .Include(x => x.Trap.SubAreaHourSquare.SubArea.CatchArea)
                .Include(x => x.Trap.SubAreaHourSquare.SubArea)
                .Include(x => x.Trap.SubAreaHourSquare.HourSquare)
                .Include(c => c.CreatedBy);
    }
}
