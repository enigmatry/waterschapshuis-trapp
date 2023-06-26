using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TrapTypes
{
    public static partial class GetTrapType
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
            /// GUID of trap type
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of the trap type
            /// </summary>
            public string Name { get; set; } = null!;

            /// <summary>
            /// GUID of the trapping type this trap type is used for
            /// </summary>
            public Guid TrappingTypeId { get; set; }
                
            /// <summary>
            /// Name of the trapping type this trap type is used for
            /// </summary>
            public string TrappingType { get; set; } = null!;
                
            /// <summary>
            /// Indicator whether trap type is active
            /// </summary>
            public bool Active { get; set; }

            /// <summary>
            /// Order number used for showing trap type in the list on mobile app
            /// </summary>
            public short Order { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<TrapType, Response>()
                    .ForMember(dest => dest.TrappingType,
                        opt => opt.MapFrom(
                            src => src.TrappingType.Name));
            }
        }

    }
}
