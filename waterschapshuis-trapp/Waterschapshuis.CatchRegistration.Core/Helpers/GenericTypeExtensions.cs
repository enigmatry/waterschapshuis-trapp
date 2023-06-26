using System;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Core.Helpers
{
    public static class GenericTypeExtensions
    {
        public static string GetGenericTypeName(this Type type, bool includeNamespace = false)
        {
            string typeName;

            if (type.IsGenericType)
            {
                var genericTypes = 
                    String.Join(",", 
                        type.GetGenericArguments()
                            .Select(t => t.GetGenericTypeName())
                            .ToArray());
                typeName = 
                    $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return includeNamespace ? $"{type.Namespace}.{typeName}" : typeName;
        }

        public static string GetGenericTypeName(this object? @object, bool includeNamespace = false)
        {
            return @object?.GetType().GetGenericTypeName(includeNamespace) ?? typeof(object).GetGenericTypeName(includeNamespace);
        }
    }
}
