using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.TimeRegistrationCategory
{
    public static partial class GetTimeRegistrationCategory
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
            /// GUID of the time registration category
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Name of the time registration category
            /// </summary>
            public string Name { get; set; } = null!;

            /// <summary>
            /// Indicator whether time registration category is active
            /// </summary>
            public bool Active { get; set; }
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<DomainModel.TimeRegistrations.TimeRegistrationCategory, Response>();
            }
        }

    }
}
