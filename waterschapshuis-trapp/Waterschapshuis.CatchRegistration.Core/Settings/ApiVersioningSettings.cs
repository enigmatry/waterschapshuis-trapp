using System;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    public class ApiVersioningSettings
    {
        public bool Enabled { get; set; }
        public string LatestApiVersion { get; set; } = String.Empty;
        public bool UseVersionByNamespaceConvention { get; set; }
    }
}
