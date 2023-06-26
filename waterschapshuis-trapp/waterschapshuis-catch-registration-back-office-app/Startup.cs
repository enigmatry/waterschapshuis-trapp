using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Waterschapshuis.CatchRegistration.BackOffice.App
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var hashRegex = new Regex(@"\.[\da-z]{20}\.",RegexOptions.Compiled);

            app.UseSpaStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    string path = context.Context.Request.Path.Value;
                    if (hashRegex.IsMatch(path))
                    {
                        // only cache files that contain a hash in the filename
                        context.Context.Response.Headers.Append(
                            "Cache-Control", "public, max-age=31536000");
                    }
                }
            });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "dist";
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "dist"; });
        }
    }
}
