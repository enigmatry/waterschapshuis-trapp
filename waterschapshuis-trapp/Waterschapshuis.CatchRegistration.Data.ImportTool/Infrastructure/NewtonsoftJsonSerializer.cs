using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure
{
    public class NewtonsoftJsonSerializer : IJsonReader, IJsonConverter
    {
        public async IAsyncEnumerable<IJsonResult> ReadAsync(string path, Regex regex = default,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var streamReader = File.OpenText(path);
            var jsonReader = new JsonTextReader(streamReader);

            while (await jsonReader.ReadAsync(cancellationToken))
            {
                if (jsonReader.TokenType == JsonToken.StartObject && (regex?.IsMatch(jsonReader.Path) ?? true))
                {
                    yield return new NewtonsoftJsonResult(await JToken.LoadAsync(jsonReader, cancellationToken));
                }
            }
        }

        public string ConvertToString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
