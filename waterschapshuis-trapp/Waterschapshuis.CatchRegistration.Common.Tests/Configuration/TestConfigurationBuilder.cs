using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Common.Tests.Configuration
{
    public class TestConfigurationBuilder
    {
        private const string _dbContextName = "CatchRegistrationContext";
        private Dictionary<string, string> _baseSettings = new Dictionary<string, string>
            {
                {"UseDeveloperExceptionPage", "true"},
                {"DbContext:SensitiveDataLoggingEnabled", "true"},
                {"DbContext:ConnectionResiliencyMaxRetryCount", "10"},
                {"DbContext:ConnectionResiliencyMaxRetryDelay", "0.00:00:30"},
                {$"ConnectionStrings:{_dbContextName}", GetConnectionString()},
                {"App:GeoServer:Url", "https://someGeoserverDomain/geoserver"},
                {"App:GeoServer:AccessKey", "geoserver_access_key"},
                {"App:GeoServer:MobileUser", "geoserver_mobile_user"}, 
                {"App:GeoServer:BackOfficeUser", "geoserver_back_office_user"},
                {"App:ServiceBus:AzureServiceBusEnabled", "false"},
                {"App:Localization:CacheDuration", "0:00:00:30"},
                {"App:AzureAd:Enabled", "true"},
                {"App:AzureAd:Instance", "https://login.microsoftonline.com"},
                {"App:Smtp:UsePickupDirectory", "true"},
                {"App:Smtp:PickupDirectoryLocation", GetSmtpPickupDirectoryLocation()},
                {"ApplicationInsights:InstrumentationKey", String.Empty},
                {"App:ApiConfiguration:MaxItemsPerBatch", "1000"},
                {"App:AzureBlob:Url", "https://wscatchregistrationdev.blob.core.windows.net"},
                {"App:AzureBlob:BaseObservationBlobContainer", "observations"},
                {"App:EasyAuth:Enabled", "true"},
                {"App:ApiVersioning:Enabled", "true"},
                {"App:ApiVersioning:LatestApiVersion", "1.0"},
                {"HealthCheck:", "100000"}
            };

        public IConfiguration Build()
        {
            EnsureParametersBeforeBuild();
            var configurationBuilder = new ConfigurationBuilder();
            AddSettingsTo(configurationBuilder);
            return configurationBuilder.Build();
        }

        public void AddSettingsTo(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddInMemoryCollection(_baseSettings);
        }

        public TestConfigurationBuilder WithAdditionalSettings(Dictionary<string, string> additionalSettings)
        {
            if (additionalSettings.Any())
            {
                additionalSettings.ToList().ForEach(setting => _baseSettings.Add(setting.Key, setting.Value));
            }
            return this;
        }

        private static string GetSmtpPickupDirectoryLocation() => TestContext.CurrentContext.TestDirectory;

        private void EnsureParametersBeforeBuild()
        {
            if (String.IsNullOrWhiteSpace(_dbContextName))
            {
                throw new InvalidOperationException("Missing db context name");
            }
        }

        private static string GetConnectionString()
        {
            string? connectionString = Environment.GetEnvironmentVariable("IntegrationTestsConnectionString");
            if (!String.IsNullOrEmpty(connectionString))
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                WriteLine("Connection string read from environment variable.");
                WriteLine($"Integration Tests using database: {builder.DataSource}");
                return connectionString;
            }

            const string dbName = "waterschapshuis-catch-registration-integration-testing";
            WriteLine($"Integration Tests using database: {dbName}");
            return $"Server=.;Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        private static void WriteLine(string message) => TestContext.WriteLine(message);

    }
}
