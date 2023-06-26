using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.CatchTypes
{
    public static partial class GetCatchType
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
            /// GUID of catch type
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// name of catchtype
            /// </summary>
            public string Name { get; set; } = String.Empty;

            /// <summary>
            /// Indicator of the catch type is for catch or by-catch
            /// </summary>
            public bool IsByCatch { get; set; }

            /// <summary>
            /// Animal type (Mammal, Bird, Fish or Other)
            /// </summary>
            public AnimalType AnimalType { get; set; }

            /// <summary>
            /// Order used for showing catch type on mobile
            /// </summary>
            public short Order { get; set; }

        }
        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<CatchType, Response>();
        }



    }
}
