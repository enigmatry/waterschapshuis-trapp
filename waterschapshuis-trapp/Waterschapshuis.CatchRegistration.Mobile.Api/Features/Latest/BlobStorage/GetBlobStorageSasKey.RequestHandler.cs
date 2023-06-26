using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Sas;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.BlobStorage;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.BlobStorage
{
    public static partial class GetBlobStorageSasKey
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IBlobStorageClient _blobStorageClient;

            public RequestHandler(IBlobStorageClient blobStorageClient)
            {
                _blobStorageClient = blobStorageClient;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Response { SasKey = await _blobStorageClient.GetSasKey(new List<AccountSasPermissions> { AccountSasPermissions.Read, AccountSasPermissions.List }) };
            }
        }
    }
}
