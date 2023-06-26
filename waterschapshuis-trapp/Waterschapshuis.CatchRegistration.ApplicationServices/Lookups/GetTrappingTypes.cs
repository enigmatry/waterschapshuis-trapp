using System;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Lookups
{
    public static partial class GetTrappingTypes
    {
        [PublicAPI]
        public class Query : IRequest<ListResponse<ResponseItem>>
        {
            public static Query Create()
            {
                return new Query();
            }
        }

        [PublicAPI]
        public class ResponseItem : NamedEntity.Item
        {

        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<TrappingType, ResponseItem>();
            }
        }
    }
}
