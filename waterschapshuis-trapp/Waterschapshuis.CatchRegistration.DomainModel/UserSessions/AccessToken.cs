using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions
{
    public class AccessToken : ValueObject
    {
        private const string AuthorizationHeaderName = "Authorization";
        private const string AuthorizationHeaderValuePrefix = "Bearer";

        [JsonConverter(typeof(SecureStringJsonConverter), 5)]
        // when serializing access token we do not want Value to be serialized fully
        // only first 5 characters we want to see in the log files for easier troubleshooting
        public string Value { get; }

        private AccessToken(string value)
        {
            Value = RemoveAuthorizationHeaderValuePrefix(value);
        }

        public static AccessToken CreateFromHeaders(IDictionary<string, StringValues> headers)
        {
            StringValues value = headers[AuthorizationHeaderName];
            return Create(value);
        }

        public static AccessToken Create(string value)
        {
            return new AccessToken(value);
        }

        protected override IEnumerable<object?> GetValues()
        {
            yield return Value;
        }

        // use method instead of property so it is not serialized
        public string GetValueWithAuthorizationHeaderPrefix()
        {
            return Value.Contains(AuthorizationHeaderValuePrefix)
                ? Value
                : $"{AuthorizationHeaderValuePrefix} {Value}";
        }

        private static string RemoveAuthorizationHeaderValuePrefix(string value)
        {
            return value.Replace(AuthorizationHeaderValuePrefix, String.Empty).TrimStart();
        }
    }
}
