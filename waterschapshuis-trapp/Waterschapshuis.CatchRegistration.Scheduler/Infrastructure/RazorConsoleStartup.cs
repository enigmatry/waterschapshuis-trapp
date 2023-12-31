﻿using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Waterschapshuis.CatchRegistration.Infrastructure.Templating;

namespace Waterschapshuis.CatchRegistration.Scheduler.Infrastructure
{
    // example startup class - show how to initialize Razor in console application and RazorTemplatingEngine class.
    // This class can be used for email templating purposes.
    public class RazorConsoleStartup
    {
        private readonly IHostEnvironment _environment;

        public RazorConsoleStartup(IConfiguration configuration, IHostEnvironment environment)
        {
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRazorPages()
                .AddRazorRuntimeCompilation(options =>
                {
                    options.FileProviders.Clear();
                    options.FileProviders.Add(new PhysicalFileProvider(_environment.ContentRootPath));
                });

            services.AddSingleton<RazorTemplatingEngine>();
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
