using System.Linq;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Catches
{
    public static partial class GetCatches
    {
        [PublicAPI]
        public class Query : CreatedOnQuery, IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public IQueryable<GetCatch.CatchItem> Catches { get; set; }

            public Response(IQueryable<GetCatch.CatchItem> catches)
            {
                Catches = catches;
            }
        }
    }
}
