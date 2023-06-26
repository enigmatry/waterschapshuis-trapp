using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Autofac.Modules
{
    public static class ConfigurationExtensions
    {
        public static bool SensitiveDataLoggingEnabled(this IConfiguration configuration) =>
            configuration.GetValue("SensitiveDataLoggingEnabled", false);

        public static bool ImplementsInterface(this Type interfaceType, Type concreteType) =>
            concreteType.GetInterfaces().Any(t =>
                (interfaceType.IsGenericTypeDefinition && t.IsGenericType
                    ? t.GetGenericTypeDefinition()
                    : t) == interfaceType);
    }
}
