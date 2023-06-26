using System.Linq;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Observations
{
    public static partial class GetObservations
    {
        [PublicAPI]
        public class Query : CreatedOnQuery, IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public IQueryable<GetObservation.ObservationItem> Observations { get; set; }

            public Response(IQueryable<GetObservation.ObservationItem> observations)
            {
                Observations = observations;
            }
        }
    }
}
