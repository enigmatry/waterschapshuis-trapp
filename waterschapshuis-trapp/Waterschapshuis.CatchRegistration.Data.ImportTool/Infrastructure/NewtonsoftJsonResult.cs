using System;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure
{
    public class NewtonsoftJsonResult : IJsonResult
    {
        [NotNull] private readonly JToken _token;

        public NewtonsoftJsonResult([NotNull] JToken token)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public T Parse<T>()
        {
            return _token.ToObject<T>();
        }

        public string Stringify()
        {
            return _token.ToString();
        }
    }
}
