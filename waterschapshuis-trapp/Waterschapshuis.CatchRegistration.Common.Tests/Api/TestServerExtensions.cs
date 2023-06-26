using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Net.Http.Headers;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.Common.Tests.Api
{
    public static class TestServerExtensions
    {
        public static HttpClient CreateClient(this TestServer server, string apiVersion, AccessToken accessToken)
        {
            var httpClient = apiVersion.IsNotNullOrEmpty()
                ? new HttpClient(server.CreateHandler()) {BaseAddress = new Uri(server.BaseAddress, $"{apiVersion}/")}
                : server.CreateClient();

            httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, accessToken.Value);

            return httpClient;
        }
    }
}
