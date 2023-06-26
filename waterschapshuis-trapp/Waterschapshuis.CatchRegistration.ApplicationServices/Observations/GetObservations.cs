using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Pagination;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Observations
{
    public static partial class GetObservations
    {
        [PublicAPI]
        public class Query : BoundedBoxQuery, IRequest<PagedResponse<GetObservationDetails.ResponseItem>>
        {
        }
    }
}
