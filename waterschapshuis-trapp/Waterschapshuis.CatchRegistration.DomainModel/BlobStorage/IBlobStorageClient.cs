using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Sas;

namespace Waterschapshuis.CatchRegistration.DomainModel.BlobStorage
{
    public interface IBlobStorageClient
    {
        Task<string> GetSasKey(IList<AccountSasPermissions> permissions);
    }
}
