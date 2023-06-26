using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Lookups
{
    public static partial class GetTimeRegistrationCategories
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
            public bool Active { get; set; }
        }

        [UsedImplicitly]
            public class MappingProfile : Profile
            {
                public MappingProfile()
                {
                    CreateMap<TimeRegistrationCategory, ResponseItem>();
                }
            }
    }
}
