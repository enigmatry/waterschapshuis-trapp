using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.Infrastructure.ApplicationInsights;
using Waterschapshuis.CatchRegistration.Infrastructure.Configuration;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class SerilogProgramHelper
    {
        public static void AppConfigureSerilog(string environmentParameterPrefix)
        {
            LoggerConfiguration config = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration(environmentParameterPrefix))
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.With(new OperationIdEnricher())
                .Enrich.WithProperty("AppVersion", PlatformServices.Default.Application.ApplicationVersion);

            AddAppInsightsToSerilog(config, environmentParameterPrefix);

            Log.Logger = config.CreateLogger();

            // for enabling self diagnostics see https://github.com/serilog/serilog/wiki/Debugging-and-Diagnostics,
            // i.e. Serilog.Debugging.SelfLog.Enable(Console.Error);  //NOSONAR
        }

        private static IConfiguration Configuration(string environmentParameterPrefix) =>
            new ConfigurationBuilder() // needed because of Serilog file configuration.
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable($"{environmentParameterPrefix}ENVIRONMENT") ?? "Production"}.json",
                    true)
                .Build();

        private static void AddAppInsightsToSerilog(LoggerConfiguration config, string environmentParameterPrefix)
        {
            var settings = Configuration(environmentParameterPrefix).ReadApplicationInsightsSettings();
            if (settings.InstrumentationKey.IsNotNullOrEmpty())
            {
                config.WriteTo.ApplicationInsights(settings.InstrumentationKey, new TraceTelemetryConverter(), settings.SerilogLogsRestrictedToMinimumLevel);
            }
        }
    }
}
