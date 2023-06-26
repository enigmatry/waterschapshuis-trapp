using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.CatchImport.Mura;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.GeoImport;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.ProvinceImport;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationGeneralImport;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TimeRegistrationImport.Mura;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TrapImport;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TrapImport.Mura;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.UserImport;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Services
{
    public sealed class ImportDataService : IImportDataService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, IImportTask> _importTasks;
        private readonly ILogger _logger;
        private readonly IServiceScope _scope;

        public ImportDataService(
            ILogger<ImportDataService> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            _logger = logger;
            _configuration = configuration;
            _scope = serviceScopeFactory.CreateScope();
            _importTasks = GetAvailableImportTasks();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public async Task RunImportAsync(CancellationToken cancellationToken)
        {
            try
            {
                IImportTask importTask =
                    _importTasks.GetValueByKeyIgnoreCase(_configuration.GetImportArgumentValue()) ??
                    throw new InvalidOperationException(
                        $"Unknown Import argument value: {_configuration.GetImportArgumentValue()}");

                await importTask.ExecuteImportAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Critical error happened during Import. Application will shut down.");
            }
        }

        private Dictionary<string, IImportTask> GetAvailableImportTasks()
        {
            return new Dictionary<string, IImportTask>
            {
                {ImportNames.Geo, _scope.GetService<GeoImportTask>()},
                {ImportNames.Users, _scope.GetService<UserImportTask>()},
                {ImportNames.Provinces, _scope.GetService<ProvinceImportTask>()},
                {ImportNames.TrapsScheldestromen, _scope.GetService<ScheldestromenTrapImportTask>()},
                {ImportNames.TrapsWestEnMidden, _scope.GetService<WestEnMiddenTrapImportTask>()},
                {ImportNames.TrapsMura, _scope.GetService<MuraTrapImportTask>()},
                {ImportNames.TrapsLimburg, _scope.GetService<LimburgTrapImportTask>()},
                {ImportNames.TrapsBrabant, _scope.GetService<BrabantTrapImportTask>()},
                {ImportNames.TrapsFryslan, _scope.GetService<FryslanTrapImportTask>()},
                {ImportNames.TrapsMrb, _scope.GetService<RivierenlandTrapImportTask>()},
                {ImportNames.TrapsNoordoostnederland, _scope.GetService<NonlTrapImportTask>()},
                {ImportNames.TrapsZuiderzeeland, _scope.GetService<ZuiderzeelandTrapImportTask>()},
                {
                    ImportNames.CatchesScheldestromen,
                    _scope.GetService<ScheldestromenCatchImportTask>()
                },
                {ImportNames.CatchesWestEnMidden, _scope.GetService<WestEnMiddenCatchImportTask>()},
                {ImportNames.CatchesMura, _scope.GetService<MuraCatchImportTask>()},
                {ImportNames.CatchesLimburg, _scope.GetService<LimburgCatchImportTask>()},
                {ImportNames.CatchesBrabant, _scope.GetService<BrabantCatchImportTask>()},
                {ImportNames.CatchesFryslan, _scope.GetService<FryslanCatchImportTask>()},
                {ImportNames.CatchesMrb, _scope.GetService<RivierenlandCatchImportTask>()},
                {ImportNames.CatchesNoordoostnederland, _scope.GetService<NonlCatchImportTask>()},
                {ImportNames.CatchesZuiderzeeland, _scope.GetService<ZuiderzeelandCatchImportTask>()},

                {
                    ImportNames.TimeRegistrationsScheldestromen,
                    _scope.ServiceProvider.GetRequiredService<ScheldestromenTimeRegistrationImportTask>()
                },
                {
                    ImportNames.TimeRegistrationsWestEnMidden,
                    _scope.ServiceProvider.GetRequiredService<WestEnMiddenTimeRegistrationImportTask>()
                },
                {
                    ImportNames.TimeRegistrationsLimburg,
                    _scope.ServiceProvider.GetRequiredService<LimburgTimeRegistrationImportTask>()
                },
                {
                    ImportNames.TimeRegistrationsMura, 
                    _scope.GetService<MuraTimeRegistrationImportTask>()
                },
                {ImportNames.TimeRegistrationsBrabant, _scope.GetService<BrabantTimeRegistrationImportTask>()},
                {ImportNames.TimeRegistrationsFryslan, _scope.GetService<FryslanTimeRegistrationImportTask>()},
                {ImportNames.TimeRegistrationsMrb, _scope.GetService<RivierenlandTimeRegistrationImportTask>()},
                {ImportNames.TimeRegistrationsNoordoostnederland, _scope.GetService<NonlTimeRegistrationImportTask>()},
                {ImportNames.TimeRegistrationsZuiderzeeland, _scope.GetService<ZuiderzeelandTimeRegistrationImportTask>()},
                {
                    ImportNames.TimeRegistrationGeneralWestEnMidden,
                    _scope.ServiceProvider.GetRequiredService<WestEnMiddenTimeRegistrationGeneralImportTask>()
                }
            };
        }
    }
}
