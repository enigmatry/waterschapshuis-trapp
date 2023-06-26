using System;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    [UsedImplicitly]
    public class DbContextSettings
    {
        public bool UseAccessToken { get; set; }

        public bool SensitiveDataLoggingEnabled { get; set; }

        public int ConnectionResiliencyMaxRetryCount { get; set; }

        public TimeSpan ConnectionResiliencyMaxRetryDelay { get; set; }
    }
}
