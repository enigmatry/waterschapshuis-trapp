using System;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    [UsedImplicitly]
    public class GeoServerSettings
    {
        [UsedImplicitly] public string Url { get; set; } = String.Empty;
        [UsedImplicitly] public string AccessKey { get; set; } = String.Empty;
        [UsedImplicitly] public string BackOfficeUser { get; set; } = String.Empty;
        [UsedImplicitly] public string MobileUser { get; set; } = String.Empty;
    }
}
