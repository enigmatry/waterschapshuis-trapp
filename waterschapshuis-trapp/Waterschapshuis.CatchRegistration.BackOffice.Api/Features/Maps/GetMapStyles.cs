using System.Collections.Generic;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.ApplicationServices.Maps.Styles;
using Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Maps
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
            /// <summary>
            /// List of map styles
            /// </summary>
            public IEnumerable<MapStyleLookup> Items { get; set; } = new List<MapStyleLookup>();
        }
    }
}
