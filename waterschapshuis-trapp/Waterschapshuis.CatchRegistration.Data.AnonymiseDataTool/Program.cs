using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Waterschapshuis.CatchRegistration.Data.AnonymiseDataTool.Services;
using Waterschapshuis.CatchRegistration.Infrastructure.MediatR;

namespace Waterschapshuis.CatchRegistration.Data.AnonymiseDataTool
{
    internal class Program
    {
        private const string AppSettings = "appsettings.json";

        private static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddCommandLine(args);

                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile(AppSettings, optional: true);
                    configApp.AddJsonFile(
                        $"appsettings.{ hostContext.HostingEnvironment.EnvironmentName }.json",
                        optional: true);

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configApp.Build())
                        .CreateLogger();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<LifetimeEventsHostedService>();
                    services.AddScoped<IDataService, DataService>();

                    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                })
                .UseSerilog()
                .UseConsoleLifetime();
    }
}
