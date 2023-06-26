using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.AnonymiseDataTool.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Data.AnonymiseDataTool.Services
{
    internal class LifetimeEventsHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IDataService _dataService;

        private CancellationToken _cancellationToken = CancellationToken.None;

        public LifetimeEventsHostedService(
            ILogger<LifetimeEventsHostedService> logger,
            IConfiguration configuration,
            IHostApplicationLifetime appLifetime,
            IDataService dataService)
        {
            _logger = logger;
            _configuration = configuration;
            _appLifetime = appLifetime;
            _dataService = dataService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            _cancellationToken = cancellationToken;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogDebug("OnStarted has been called.");
            _logger.LogApplicationStartupInfo(_configuration);

            _dataService.RunProcessingAsync(_cancellationToken).GetAwaiter().GetResult();

            _appLifetime.StopApplication();
        }

        private void OnStopping()
        {
            _logger.LogDebug("OnStopping has been called.");

            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            _logger.LogDebug("OnStopped has been called.");

            // Perform post-stopped activities here
        }
    }
}
