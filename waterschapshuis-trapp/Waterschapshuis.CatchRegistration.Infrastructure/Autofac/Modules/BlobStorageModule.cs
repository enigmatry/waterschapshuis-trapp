using Autofac;
using Azure.Storage.Blobs;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.BlobStorage;
using Waterschapshuis.CatchRegistration.Infrastructure.AzureBlobStorage;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    public class BlobStorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var azureBlobSettings = context.Resolve<AzureBlobSettings>();
                var blobServiceClient =
                    new BlobServiceClient(azureBlobSettings.ConnectionString);

                return blobServiceClient;
            });
            builder.RegisterType<AzureBlobStorageClient>().As<IBlobStorageClient>().InstancePerLifetimeScope();
        }
    }
}
