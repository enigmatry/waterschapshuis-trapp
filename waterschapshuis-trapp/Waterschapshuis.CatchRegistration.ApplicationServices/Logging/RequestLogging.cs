using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts.Commands;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Logging
{
    public static class RequestLogging
    {
        private static readonly Type[] NotLoggedRequestTypes =
        {
            typeof(TimeRegistrationsCreateNewVersion.Command),
            typeof(VersionRegionalLayoutCreate.Command),
        };

        public static bool ShouldRequestTypeBeLogged(Type requestType)
        {
            return NotLoggedRequestTypes.All(x => x != requestType);
        }

        public static string SerializeRequest(object? request)
        {
            return JsonConvert.SerializeObject(request,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Converters = new JsonConverter[]
                    {
                        new SimpleGeometryJsonConverter()
                    }
                });
        }

        private class SimpleGeometryJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type typeToConvert)
            {
                return typeof(Geometry).IsAssignableFrom(typeToConvert);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                writer.WriteValue((value as Geometry)?.ToText());
            }
        }
    }
}
