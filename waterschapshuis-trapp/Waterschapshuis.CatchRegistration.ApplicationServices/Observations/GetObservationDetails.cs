using System;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Observations
{
    public static partial class GetObservationDetails
    {
        [PublicAPI]
        public class Query : IRequest<ResponseItem>
        {
            public Guid Id { get; set; }

            public static Query ById(Guid id) => new Query { Id = id };
        }

        [PublicAPI]
        public class ResponseItem
        {
            /// <summary>
            /// GUID of the observation
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// GUID of the user who created observation
            /// </summary>
            public string CreatedBy { get; set; } = null!;

            /// <summary>
            /// Updated By
            /// </summary>
            public string UpdatedBy { get; set; } = null!;

            /// <summary>
            /// Type of observation (schade, other)
            /// </summary>
            public int Type { get; set; }

            /// <summary>
            /// URL on blob storage where image is stored
            /// </summary>
            public string PhotoUrl { get; set; } = null!;

            /// <summary>
            /// Remarks placed by trapper
            /// </summary>
            public string Remarks { get; set; } = String.Empty;

            /// <summary>
            /// Created on
            /// </summary>
            public DateTimeOffset CreatedOn { get; set; }

            /// <summary>
            /// Positon defined by names of Sub Area Hour Square, Rayon and Hour Square
            /// </summary>
            public string Position { get; set; } = null!;

            /// <summary>
            /// Indicator whether observation is archived
            /// </summary>
            public bool Archived { get; set; }

            /// <summary>
            /// Recorded on
            /// </summary>
            public DateTimeOffset RecordedOn { get; set; }

            /// <summary>
            /// Longitude of observation where it is registered
            /// </summary>
            public double Longitude { get; set; }

            /// <summary>
            /// Latitude of trap where it is registered
            /// </summary>
            public double Latitude { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // https://docs.automapper.org/en/stable/Queryable-Extensions.html#parameterization
                var parameters = MappingParameters.CreateEmpty();
                CreateMap<Observation, ResponseItem>()
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapAnonymizedCreatedBy(parameters))
                    .ForMember(dest => dest.UpdatedBy, opt => opt.MapAnonymizedUpdatedBy(parameters))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location.X))
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location.Y))
                    .ForMember(
                        dest => dest.Position,
                        opt => opt.MapFrom(
                            src =>
                                $"{src.SubAreaHourSquare.SubArea.CatchArea.Name} / {src.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name} / {src.SubAreaHourSquare.HourSquare.Name}")
                    );

            }
        }

    }
}
