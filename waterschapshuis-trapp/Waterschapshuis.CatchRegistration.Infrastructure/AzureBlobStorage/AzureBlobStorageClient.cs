using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Sas;
using Serilog;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.BlobStorage;

namespace Waterschapshuis.CatchRegistration.Infrastructure.AzureBlobStorage
{
    public class AzureBlobStorageClient : IBlobStorageClient
    {
        private readonly AzureBlobSettings _azureBlobSettings;

        public AzureBlobStorageClient(
            AzureBlobSettings azureBlobSettings)
        {
            _azureBlobSettings = azureBlobSettings;
        }

        public Task<string> GetSasKey(IList<AccountSasPermissions> permissions)
        {
            var sasBuilder = new AccountSasBuilder
            {
                Services = AccountSasServices.Blobs,
                ResourceTypes = AccountSasResourceTypes.All,
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.Add(_azureBlobSettings.SasKeyValidityPeriod)
            };

            var rawPermissions = GetRawPermissions(permissions);
            sasBuilder.SetPermissions(rawPermissions);

            Log.Debug("Azure Blob AccountName {0}", _azureBlobSettings.AccountName);

            var credential = new StorageSharedKeyCredential(_azureBlobSettings.AccountName, _azureBlobSettings.AccountKey);

            return Task.FromResult(sasBuilder.ToSasQueryParameters(credential).ToString());
        }

        private string GetRawPermissions(IList<AccountSasPermissions> permissions)
        {
           return string.Join("", permissions.Select(permission => permission.ToString().ToLower().FirstOrDefault()));
        }
    }
}
