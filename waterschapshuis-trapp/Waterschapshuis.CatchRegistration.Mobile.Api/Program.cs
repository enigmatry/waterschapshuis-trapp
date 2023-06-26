using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup;

namespace Waterschapshuis.CatchRegistration.Mobile.Api
{
    public static class Program
    {
        private const string EnvironmentParameterPrefix = "ASPNETCORE_";

        public static void Main(string[] args)
        {
            SerilogProgramHelper.AppConfigureSerilog(EnvironmentParameterPrefix);
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.Information("Stopping web host");
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                        {
                            options.AddServerHeader = false;
                        })
                        .UseStartup<Startup>()
                        .UseSerilog();
                });
        }
    }
}
