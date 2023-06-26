using System;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Lookups
{
    public static partial class GetCatchTypes
    {
        [PublicAPI]
        public class Query : IRequest<ListResponse<ResponseItem>>
        {
        }

        [PublicAPI]
        public class ResponseItem : NamedEntity.Item
        {
            /// <summary>
            /// Is this catch type used for catch or by-catch
            /// </summary>
            public bool IsByCatch { get; set; }

            /// <summary>
            /// Animal type (Mammal, Bird, Fish or Other)
            /// </summary>
            public AnimalType AnimalType { get; set; }

            /// <summary>
            /// Order to be used in listing on mobile
            /// </summary>
            public short Order { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CatchType, ResponseItem>();
            }
        }
    }
}
