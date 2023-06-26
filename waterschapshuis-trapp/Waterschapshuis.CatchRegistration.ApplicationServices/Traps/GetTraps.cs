using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core.Pagination;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Traps
{
    public static partial class GetTraps
    {
        [PublicAPI]
        public class Query : BoundedBoxQuery, IRequest<PagedResponse<GetTrapDetails.TrapItem>>
        {
        }
    }
}
