using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using System;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TrapHistories
{
    public static partial class GetTrapHistories
    {
        [PublicAPI]
        public class Query : PagedQuery, IRequest<PagedResponse<HistoryItem>>
        {
            public Guid TrapId { get; set; }
        }

        [PublicAPI]
        public class HistoryItem
        {
            /// <summary>
            /// GUID of user who created by
            /// </summary>
            public string CreatedBy { get; set; } = String.Empty;

            /// <summary>
            /// Recorded on
            /// </summary>
            public DateTimeOffset RecordedOn { get; set; }

            /// <summary>
            /// Message while creation
            /// </summary>
            public string Message { get; set; } = String.Empty;
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // https://docs.automapper.org/en/stable/Queryable-Extensions.html#parameterization
                var parameters = MappingParameters.CreateEmpty();

                CreateMap<TrapHistory, HistoryItem>()
                    .ForMember(dest => dest.CreatedBy, opt => opt.MapAnonymizedCreatedBy(parameters));
            }
        }
    }
}
