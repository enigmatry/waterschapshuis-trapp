using System;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    [UsedImplicitly]
    public class ApiConfigurationSettings
    {
        [UsedImplicitly] public int MaxItemsPerBatch { get; set; }
    }
}
