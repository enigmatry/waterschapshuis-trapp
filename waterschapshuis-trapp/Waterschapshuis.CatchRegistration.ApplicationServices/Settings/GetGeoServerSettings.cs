using System;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Settings
{
    public static partial class GetGeoServerSettings
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public string Url { get; set; } = String.Empty;
            public string AccessKey { get; set; } = String.Empty;
            public string BackOfficeUser { get; set; } = String.Empty;
            public string MobileUser { get; set; } = String.Empty;
        }
    }
}
