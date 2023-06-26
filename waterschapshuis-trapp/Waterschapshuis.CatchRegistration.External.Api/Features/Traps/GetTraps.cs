using System.Linq;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Traps
{
    public static partial class GetTraps
    {
        [PublicAPI]
        public class Query : CreatedOnQuery, IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public IQueryable<GetTrap.TrapItem> Traps { get; set; }

            public Response(IQueryable<GetTrap.TrapItem> traps)
            {
                Traps = traps;
            }
        }
    }
}
