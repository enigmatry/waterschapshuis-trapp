using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.Reports;
using Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;
using Waterschapshuis.CatchRegistration.Infrastructure.Templating;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests
{
    [Category("unit")]
    public class RazorTemplatingEngineFixture
    {
        private RazorTemplatingEngine _templatingEngine = null!;

        [SetUp]
        public void Setup()
        {
            IHost host = BuildHost();
            IServiceScopeFactory scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            IServiceScope serviceScope = scopeFactory.CreateScope();
            _templatingEngine = serviceScope.ServiceProvider.GetRequiredService<RazorTemplatingEngine>();
        }

        [Test]
        public async Task TestRenderFromFile()
        {
            string result = await _templatingEngine.RenderFromFileAsync("~/Reports/WeeklyOverviewReport.cshtml",
                WeeklyOverviewReportDataModel.Create(1, 2, "", "", Enumerable.Empty<GetTimeRegistrations.Response.Item>(), Enumerable.Empty<CatchesPerDay>()));

            result.Should().Contain("Samenvatting");
        }

        private static IHost BuildHost()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<RazorTestStartup>();
                }).Build();
        }
    }
}
