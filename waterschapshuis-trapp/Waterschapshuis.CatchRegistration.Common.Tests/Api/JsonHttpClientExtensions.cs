﻿using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;

namespace Waterschapshuis.CatchRegistration.Common.Tests.Api
{
    public static class JsonHttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient client, string uri)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            return await response.DeserializeWithStatusCodeCheckAsync<T>();
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, Uri uri, params KeyValuePair<string, string>[] parameters)
        {
            var resourceUri = uri.AppendParameters(parameters);

            HttpResponseMessage response = await client.GetAsync(resourceUri);
            return await response.DeserializeWithStatusCodeCheckAsync<T>();
        }

        public static async Task PutAsync<T>(this HttpClient client, string uri, T content)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(uri, content);
            await response.EnsureSuccessStatusCodeAsync();
        }

        public static async Task<TResponse> PutAsync<T, TResponse>(this HttpClient client, string uri, T content)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(uri, content);
            return await response.DeserializeWithStatusCodeCheckAsync<TResponse>();
        }

        public static async Task PostAsync<T>(this HttpClient client, string uri, T content)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, content);
            await response.EnsureSuccessStatusCodeAsync();
        }

        public static async Task<TResponse> PostAsync<T, TResponse>(this HttpClient client, string uri, T content)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, content);
            return await response.DeserializeWithStatusCodeCheckAsync<TResponse>();
        }

        public static async Task<TResponse> PostAsync<TResponse>(this HttpClient client, string uri)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, new { });
            return await response.DeserializeWithStatusCodeCheckAsync<TResponse>();
        }

        public static async Task<TResponse> DeleteAsync<TResponse>(this HttpClient client, string uri)
        {
            HttpResponseMessage response = await client.DeleteAsync(uri);
            return await response.DeserializeWithStatusCodeCheckAsync<TResponse>();
        }
    }
}
