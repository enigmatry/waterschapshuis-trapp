using System;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Observations;
using Waterschapshuis.CatchRegistration.Core.Pagination;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Observations
{
    public static partial class GetObservations
    {
        [PublicAPI]
        public class Query : PagedQuery, IRequest<PagedResponse<GetObservationDetails.ResponseItem>>
        {
            public string Keyword { get; set; } = String.Empty;
        }
    }
}
