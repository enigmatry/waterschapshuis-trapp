using System;
using System.Collections.Generic;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Lookups
{
    public static partial class GetTrapTypes
    {
        [PublicAPI]
        public class Query :  IRequest<ListResponse<ResponseItem>>
        {
        }

        [PublicAPI]
        public class ResponseItem : NamedEntity.Item
        {
            public Guid TrappingTypeId { get; set; }

            public TrapStatus[] AllowedStatuses { get; set; } = new TrapStatus[0];
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<TrapType, ResponseItem>();
            }
        }
    }
}
