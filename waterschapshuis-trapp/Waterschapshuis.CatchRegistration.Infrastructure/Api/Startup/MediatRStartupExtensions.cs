using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Waterschapshuis.CatchRegistration.Infrastructure.MediatR;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class MediatRStartupExtensions
    {
        public static IServiceCollection AppAddMediatR(this IServiceCollection services, Assembly entryAssembly)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceLoggingBehavior<,>));

            services.AddMediatR(
                entryAssembly,
                AssemblyFinder.DomainAssembly,
                AssemblyFinder.ApplicationServicesAssembly);

            return services;
        }
    }
}
