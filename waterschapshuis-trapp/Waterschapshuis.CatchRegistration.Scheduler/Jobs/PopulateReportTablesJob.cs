using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;
using Waterschapshuis.CatchRegistration.Scheduler.Services;

namespace Waterschapshuis.CatchRegistration.Scheduler.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class PopulateReportTablesJob : IJob
    {
        private readonly CatchRegistrationDbContext _dbContext;
        private readonly ILogger<PopulateReportTablesJob> _logger;
        private readonly SchedulerSettings _settings;
        private readonly ITrapCatchingNightsRecorder _trapCatchingNightsRecorder;

        public PopulateReportTablesJob(CatchRegistrationDbContext dbContext,
            ILogger<PopulateReportTablesJob> logger,
            SchedulerSettings settings,
            ITrapCatchingNightsRecorder trapCatchingNightsRecorder)
        {
            _dbContext = dbContext;
            _logger = logger;
            _settings = settings;
            _trapCatchingNightsRecorder = trapCatchingNightsRecorder;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(_settings.PopulateReportTablesJobConfiguration.DbTimoutInMin));

            await _trapCatchingNightsRecorder.TryRecord();
            await TryExecuteStoredProcedure("[report].[PopulateCatchData]");
            await TryExecuteStoredProcedure("[report].[PopulateTimeRegistrationData]");
            await TryExecuteStoredProcedure("[report].[PopulateOwnReportData]");
        }

        private async Task TryExecuteStoredProcedure(string storedProcedureName)
        {
            try
            {
                var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync($"EXEC {storedProcedureName}");
                _logger.LogInformation(
                    "Finished executing stored procedure: {storedProcedureName}, rows: {RowsAffected}",
                    storedProcedureName, rowsAffected);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}
