using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class AutoMapperStartupExtensions
    {
        public static void AppAddAutoMapper(this IServiceCollection services, Assembly entryAssembly)
        {
            services.AddAutoMapper(entryAssembly, AssemblyFinder.ApplicationServicesAssembly);
        }
    }
}
