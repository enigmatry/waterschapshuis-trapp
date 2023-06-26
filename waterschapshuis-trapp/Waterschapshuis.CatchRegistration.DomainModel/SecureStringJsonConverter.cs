using System;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel
{
    public class SecureStringJsonConverter : JsonConverter<string>
    {
        public int TruncateLength { get; }

        public SecureStringJsonConverter(int truncateLength)
        {
            TruncateLength = truncateLength;
        }

        public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
        {
            var secureValue = value;
            writer.WriteValue(secureValue.Truncate(TruncateLength));
        }

        public override string ReadJson(JsonReader reader,
            Type objectType,
            string? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }
    }
}
