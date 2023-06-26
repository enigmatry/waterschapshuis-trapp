using System.Collections.Generic;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Styles
{
    public static partial class GetMapStylesLookups
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
