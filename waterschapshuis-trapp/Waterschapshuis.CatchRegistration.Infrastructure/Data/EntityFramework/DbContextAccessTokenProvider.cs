﻿using System;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Settings;
using JetBrains.Annotations;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class DbContextAccessTokenProvider : IDbContextAccessTokenProvider
    {
        private readonly DbContextSettings _settings;
        private readonly ILogger<DbContextAccessTokenProvider> _logger;

        public DbContextAccessTokenProvider(DbContextSettings settings, ILogger<DbContextAccessTokenProvider> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            return _settings.UseAccessToken ? 
                await GetTokenFromAzureServiceTokenProvider() : 
                await Task.FromResult(String.Empty);
        }

        private async Task<string> GetTokenFromAzureServiceTokenProvider()
        {
            try
            {
                // No need to cache the token or check for expiration
                // according to the docs this is all done automatically:
                // https://docs.microsoft.com/en-us/azure/key-vault/service-to-service-authentication#using-the-library
                var accessToken = await new AzureServiceTokenProvider()
                    .GetAccessTokenAsync("https://database.windows.net/");

                if (!accessToken.IsNotNullOrEmpty())
                {
                    _logger.LogWarning("Getting access token for managed service identity: Token is empty.");
                }
                return accessToken;
            }
            catch (Exception e)
            {
                _logger.LogError("Error when getting access token for managed service identity", e);
                throw;
            }
        }
    }
}
