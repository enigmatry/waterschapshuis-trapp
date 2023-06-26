using System.Linq;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Pagination;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.TimeRegistrations
{
    public static partial class GetTimeRegistrations
    {
        [PublicAPI]
        public class Query : CreatedOnQuery, IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response : PagedResponse<GetTimeRegistration.TimeRegistrationItem>
        {
            public IQueryable<GetTimeRegistration.TimeRegistrationItem> TimeRegistrations { get; set; }

            public Response(IQueryable<GetTimeRegistration.TimeRegistrationItem> timeRegistrations)
            {
                TimeRegistrations = timeRegistrations;
            }
        }
    }
}
