using Microsoft.Extensions.DependencyInjection;
using Waterschapshuis.CatchRegistration.Core.Settings;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Startup
{
    public static class TypedHttpClientsStartupExtensions
    {
        public static void AppAddTypedHttpClients(this IServiceCollection services, AppSettings settings)
        {
            // Here you register http clients to ServiceCollection
        }
    }
}
