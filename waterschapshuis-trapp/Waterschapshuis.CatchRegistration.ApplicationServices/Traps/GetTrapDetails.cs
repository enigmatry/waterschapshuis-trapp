using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Traps
{
    public static partial class GetTrapDetails
    {
        [PublicAPI]
        public class Query : IRequest<TrapItem>
        {
            public Guid Id { get; set; }

            public static Query ById(Guid id)
            {
                return new Query { Id = id };
            }
        }

        [PublicAPI]
        public class TrapItem
        {
            /// <summary>
            /// GUID of trap
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of the trap type
            /// </summary>
            public string Type { get; set; } = null!;

            /// <summary>
            /// GUID of the trap type
            /// </summary>
            public Guid TrapTypeId { get; set; }

            /// <summary>
            /// GUID of the trapping type
            /// </summary>
            public Guid TrappingTypeId { get; set; }

            /// <summary>
            /// Trap status: catching, non catching, deleted
            /// </summary>
            public TrapStatus Status { get; set; }

            /// <summary>
            /// Remarks entered by a trapper
            /// </summary>
            public string? Remarks { get; set; }

            /// <summary>
            /// Number of traps
            /// </summary>
            public int NumberOfTraps { get; set; }

            /// <summary>
            /// Longitude of trap where it is registered
            /// </summary>
            public double Longitude { get; set; }

            /// <summary>
            /// Latitude of trap where it is registered
            /// </summary>
            public double Latitude { get; set; }

            /// <summary>
            /// Name of user who created this trap
            /// </summary>
            public string CreatedBy { get; set; } = null!;

            /// <summary>
            /// Created on of record in back-office
            /// </summary>
            public DateTimeOffset CreatedOn { get; set; }

            /// <summary>
            /// Date when the trap has been created on mobile by trapper
            /// </summary>
            public DateTimeOffset RecordedOn { get; set; }

            /// <summary>
            /// Name of user who updated this trap
            /// </summary>
            public string UpdatedBy { get; set; } = null!;

            /// <summary>
            /// Updated on
            /// </summary>
            public DateTimeOffset UpdatedOn { get; set; }

            /// <summary>
            /// List of catch detail on this trap
            /// </summary>
            public IEnumerable<GetCatchDetails.CatchItem> Catches { get; set; } = new List<GetCatchDetails.CatchItem>();

            /// <summary>
            /// Rayon
            /// </summary>
            public string Rayon { get; set; } = null!;

            /// <summary>
            /// Sub Area
            /// </summary>
            public string SubArea { get; set; } = null!;

            /// <summary>
            /// Cath Area
            /// </summary>
            public string CatchArea { get; set; } = null!;

            /// <summary>
            /// GUID of the user who created this trap
            /// </summary>
            public Guid CreatedById { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // https://docs.automapper.org/en/stable/Queryable-Extensions.html#parameterization
                var parameters = MappingParameters.CreateEmpty();

                CreateMap<Trap, TrapItem>()
                    .ForMember(dest => dest.Catches, opt => opt.MapFrom(src => src.Catches.OrderByDescending(x => x.RecordedOn)))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TrapType.Name))
                    .ForMember(dest => dest.TrapTypeId, opt => opt.MapFrom(src => src.TrapType.Id))
                    .ForMember(dest => dest.TrappingTypeId, opt => opt.MapFrom(src => src.TrapType.TrappingTypeId))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location.X))
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location.Y))
                    .ForMember(dest => dest.Rayon, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name))
                    .ForMember(dest => dest.CatchArea, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.CatchArea.Name))
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapAnonymizedCreatedBy(parameters))
                    .ForMember(dest => dest.UpdatedBy, opt => opt.MapAnonymizedTrapUpdatedBy(parameters))
                    .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src =>
                        src.TrapHistories.Count > 0 ? src.TrapHistories.OrderByDescending(x => x.UpdatedOn).FirstOrDefault().UpdatedOn : src.UpdatedOn))
                    .ForMember(dest => dest.SubArea, opt => opt.MapFrom(src => src.SubAreaHourSquare.SubArea.Name));
            }
        }
    }
}
