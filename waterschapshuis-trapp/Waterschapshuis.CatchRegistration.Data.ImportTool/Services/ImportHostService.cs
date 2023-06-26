using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Services
{
    [UsedImplicitly]
    internal class ImportHostService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IImportDataService _importService;

        public ImportHostService(
            ILogger<ImportHostService> logger,
            IConfiguration configuration,
            IHostApplicationLifetime appLifetime,
            IImportDataService importService)
        {
            _logger = logger;
            _configuration = configuration;
            _appLifetime = appLifetime;
            _importService = importService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogApplicationStartupInfo(_configuration);

            await _importService.RunImportAsync(stoppingToken);

            _appLifetime.StopApplication();
        }

        //public async Task StartAsync(CancellationToken cancellationToken)
        //{
        //    RegisterApplicationEventHanlders();
        //    _logger.LogApplicationStartupInfo(_configuration);

        //    await _importService.RunImportAsync(cancellationToken);

        //    _appLifetime.StopApplication();
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    return Task.CompletedTask;
        //}

        //private void RegisterApplicationEventHanlders()
        //{
        //    _appLifetime.ApplicationStarted.Register(OnStarted);
        //    _appLifetime.ApplicationStopping.Register(OnStopping);
        //    _appLifetime.ApplicationStopped.Register(OnStopped);
        //}

        //private void OnStarted()
        //{
        //    _logger.LogDebug("OnStarted has been called.");
        //}

        //private void OnStopping()
        //{
        //    _logger.LogDebug("OnStopping has been called.");
        //}

        //private void OnStopped()
        //{
        //    _logger.LogDebug("OnStopped has been called.");
        //}
    }
}
