﻿using System;
using JetBrains.Annotations;
using Serilog.Events;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    [UsedImplicitly]
    public class ApplicationInsightsSettings
    {
        [UsedImplicitly]
        public string InstrumentationKey { get; set; } = String.Empty;

        [UsedImplicitly]
        public LogEventLevel SerilogLogsRestrictedToMinimumLevel { get; set; }
    }
}
