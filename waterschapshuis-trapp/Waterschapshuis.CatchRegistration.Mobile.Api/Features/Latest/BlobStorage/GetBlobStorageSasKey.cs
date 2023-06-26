using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.BlobStorage
{
    public static partial class GetBlobStorageSasKey
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public string SasKey { get; internal set; } = null!;
        }
    }
}
