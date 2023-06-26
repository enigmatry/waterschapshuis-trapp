using System;
using System.Collections.Generic;
using System.Net.Http;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.Common.Tests.Api
{
    public class ApiIntegrationFixtureBase<TStartup> : IntegrationFixtureBase where TStartup : class
    {
        protected HttpClient Client = null!;
        private TestServer _server = null!;
        private string _currentApiVersion = null!;

        [SetUp]
        protected void Setup()
        {
            BuildConfiguration(GetAdditionalSettings());

            IWebHostBuilder webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddAutofac())
                .UseConfiguration(_configuration)
                .UseStartup<TStartup>();

            _server = new TestServer(webHostBuilder);
            CreateDatabase();

            Client = _server.CreateClient(_currentApiVersion, GetAccessToken());
            _testScope = CreateScope();
            AddCurrentUserToDb();
        }

        protected override IServiceScope CreateScope()
        {
            return _server.Host.Services.CreateScope();
        }

        [TearDown]
        public new void Teardown()
        {
            Client.Dispose();
            _server.Dispose();
        }

        protected void EnableApiVersioning(string currentApiVersion)
        {
            _currentApiVersion = currentApiVersion;
        }

        protected virtual AccessToken GetAccessToken()
        {
            return AccessToken.Create(String.Empty);
        }

        protected virtual Dictionary<string, string> GetAdditionalSettings()
        {
            return new Dictionary<string, string>();
        }
    }
}
