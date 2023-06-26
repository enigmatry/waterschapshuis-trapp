using Microsoft.Extensions.DependencyInjection;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class ServiceScopeExtensions
    {
        public static T GetService<T>(this IServiceScope scope)
        {
            return scope.ServiceProvider.GetService<T>();
        }
    }
}
