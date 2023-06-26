using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.Autofac;
using Waterschapshuis.CatchRegistration.Common.Tests.Configuration;
using Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests
{
    public abstract class SchedulerIntegrationFixtureBase : IntegrationFixtureBase
    {
        private IHost _host = null!;

        [SetUp]
        protected void Setup()
        {
            var testConfigBuilder = new TestConfigurationBuilder().WithAdditionalSettings(_schedulerSettings);

            IHostBuilder hostBuilder = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) => testConfigBuilder.AddSettingsTo(config));

            Program.BuildHost<RazorTestStartup>(hostBuilder, configureContainer =>
            {
                // this allows certain components to be overridden
                configureContainer.RegisterModule<TestModule>();
                // API does not depend on migrations assembly, test are
                configureContainer.RegisterModule(new EntityFrameworkModule { RegisterMigrationsAssembly = true });
            });

            _host = hostBuilder.Build();

            CreateDatabase();

            _testScope = CreateScope();
            AddCurrentUserToDb();
        }

        protected override IServiceScope CreateScope()
        {
            return _host.Services.CreateScope();
        }

        private readonly Dictionary<string, string> _schedulerSettings = new Dictionary<string, string>
            {
                {"SchedulerSettings:BackOfficeAppUrl", "https://scheduler-intergation-test-backoffice.com/"},
                {"SchedulerSettings:CreateTrackingLineJobConfiguration:CurrentDateDeltaInDays", "-1"},
                {"SchedulerSettings:CreateTrackingLineJobConfiguration:DbTimoutInMin", "5"},
                {"SchedulerSettings:CompleteRegistrationDataJobConfiguration:WeeksPeriodInDays", "-42"},
                {"SchedulerSettings:PopulateReportTablesJobConfiguration:DbTimoutInMin", "60"},
                {"SchedulerSettings:AnonymizeInactiveUsersJobConfiguration:InactivePeriodBeforeAnonymizationInYears", "60"},
                {"SchedulerSettings:RemoveSessionsJobConfiguration:RemoveBeforeCreatedOnDeltaInMin", "1440"},
                {"SchedulerSettings:JobsSettings", @"[
                    { ""Name"": ""CreateTrackingLinesJob"", ""CronExpression"": ""0/5 * * * * ?"", ""Enabled"": true },
                    { ""Name"": ""SendWeeklyOverviewReportJob"", ""CronExpression"": ""0/5 * * * * ?"", ""Enabled"": true },
                    { ""Name"": ""CompleteRegistrationDataJob"", ""CronExpression"": ""0/5 * * * * ?"", ""Enabled"": true },
                    { ""Name"": ""PopulateReportTablesJob"", ""CronExpression"": ""0/5 * * * * ?"", ""Enabled"": true },
                    { ""Name"": ""AnonymizeInactiveUsersJob"", ""CronExpression"": ""0/5 * * * * ?"", ""Enabled"": true },
                    { ""Name"": ""RemoveSessionsJob"", ""CronExpression"": ""0/5 * * * * ?"", ""Enabled"": true }]"
                }
            };
    }
}
