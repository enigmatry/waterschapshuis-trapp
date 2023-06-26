using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Infrastructure.Api;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;
using Waterschapshuis.CatchRegistration.Scheduler.Services;

[assembly: InternalsVisibleTo("Waterschapshuis.CatchRegistration.Scheduler.Tests")]

namespace Waterschapshuis.CatchRegistration.Scheduler
{
    internal static class Program
    {
        private const string EnvironmentParameterPrefix = "DOTNET_";

        private static async Task Main(string[] args)
        {
            SerilogProgramHelper.AppConfigureSerilog(EnvironmentParameterPrefix);

            try
            {
                Log.Information("Starting service host...");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Service host terminated unexpectedly!");
            }
            finally
            {
                Log.Information("Stopping service host.");
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);
            return BuildHost<RazorConsoleStartup>(builder, containerBuilder => { });
        }

        internal static IHostBuilder BuildHost<TStartup>(IHostBuilder builder, Action<ContainerBuilder> configureContainer) where TStartup : class
        {
            return builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddTransient<ITrapCatchingNightsRecorder, TrapCatchingNightsRecorder>()
                        .AddHostedService<SchedulerHostedService>()
                        .AppAddMediatR(AssemblyFinder.DomainAssembly)
                        .AppAddAutoMapper(AssemblyFinder.ApplicationServicesAssembly);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<TStartup>(); })
                .ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    containerBuilder.AppRegisterModules();
                    containerBuilder.RegisterModule<SchedulerModule>();
                    configureContainer(containerBuilder);
                })
                .UseSerilog()
                .UseConsoleLifetime();
        }
    }
}
