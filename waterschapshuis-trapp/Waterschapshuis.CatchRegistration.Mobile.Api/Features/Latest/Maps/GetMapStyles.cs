using System.Collections.Generic;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Maps
{
    public static partial class GetMapStyles
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public IEnumerable<MapStyleLookup> Items { get; set; } = new List<MapStyleLookup>();
        }
    }
}
