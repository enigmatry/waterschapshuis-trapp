using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    [UsedImplicitly]
    public class AzureAdSettings
    {
        public bool Enabled { get; set; }
        public string ClientId { get; set; } = String.Empty;
        public string TenantId { get; set; } = String.Empty;
        public string Instance { get; set; } = String.Empty;
        public string AllowedTenants { get; set; } = String.Empty;
        public string ApiScopes { get; set; } = String.Empty;

        public IEnumerable<string> AllowedTenantIds => AllowedTenants.Split(",").Select(v => v.Trim());
    }
}
