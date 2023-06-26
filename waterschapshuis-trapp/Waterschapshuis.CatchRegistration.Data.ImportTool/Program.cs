using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Services;
using Waterschapshuis.CatchRegistration.Infrastructure;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;
using Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;
using Waterschapshuis.CatchRegistration.Infrastructure.MediatR;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool
{
    internal static class Program
    {
        private const string Prefix = "WCR_";
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
                    config.AddEnvironmentVariables(prefix: Prefix);
                    config.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddEnvironmentVariables(prefix: Prefix);
                    configApp.AddCommandLine(args);

                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile(AppSettings, optional: true);
                    configApp.AddJsonFile(
                        $"appsettings.{ hostContext.HostingEnvironment.EnvironmentName }.json",
                        optional: true);

                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                        .ReadFrom.Configuration(configApp.Build())
                        .CreateLogger();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ImportHostService>();
                    services.AddScoped<IImportDataService, ImportDataService>();

                    services.AddTransient<IJsonReader, NewtonsoftJsonSerializer>();
                    services.AddTransient<IJsonConverter, NewtonsoftJsonSerializer>();

                    services.AddScoped<ITimeProvider, TimeProvider>();

                    services.AddScoped<IDbContextAccessTokenProvider, DbContextAccessTokenProvider>(); // use app.settings to configure

                    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                    services.AddMediatR(AssemblyFinder.DomainAssembly, AssemblyFinder.ApplicationServicesAssembly);

                    services.AddAutoMapper(AssemblyFinder.ImportToolAssembly, AssemblyFinder.ApplicationServicesAssembly);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule<ImportModule>(); // this allows certain components to be overridden

                    builder.RegisterModule(new EntityFrameworkModule { RegisterMigrationsAssembly = false });
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                })
                .UseSerilog()
                .UseConsoleLifetime();
    }
}
