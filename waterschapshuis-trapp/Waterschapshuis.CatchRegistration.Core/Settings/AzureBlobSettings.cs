using System;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    [UsedImplicitly]
    public class AzureBlobSettings
    {
        [UsedImplicitly] public string AccountName { get; set; } = String.Empty;
        [UsedImplicitly] public string AccountKey { get; set; } = String.Empty;
        [UsedImplicitly] public string ConnectionString { get; set; } = String.Empty;
        [UsedImplicitly] public TimeSpan SasKeyValidityPeriod { get; set; }
        [UsedImplicitly] public string Url { get; set; } = String.Empty;
        [UsedImplicitly] public string BaseObservationBlobContainer { get; set; } = String.Empty;
    }
}
