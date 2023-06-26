using Microsoft.Extensions.DependencyInjection;

namespace Waterschapshuis.CatchRegistration.Common.Tests.Api
{
    public static class ServiceScopeExtensions
    {
        public static T Resolve<T>(this IServiceScope scope)
        {
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}