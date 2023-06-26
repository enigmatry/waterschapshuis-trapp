using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Styles;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Maps
{
    public partial class GetMapStyles
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IMediator _mediator;

            public RequestHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<Response> Handle(Query request,
                CancellationToken cancellationToken)
            {
                // just forward request to app service
                var response = await _mediator.Send(new GetMapStylesLookups.Query(), cancellationToken);

                return new Response { Items = response.Items };
            }
        }
    }
}
