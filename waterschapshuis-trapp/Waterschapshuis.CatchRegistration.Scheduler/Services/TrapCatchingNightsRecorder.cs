using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Scheduler.Services
{
    public class TrapCatchingNightsRecorder : ITrapCatchingNightsRecorder
    {
        private readonly ILogger<TrapCatchingNightsRecorder> _logger;
        private readonly CatchRegistrationDbContext _dbContext;

        public TrapCatchingNightsRecorder(
            ILogger<TrapCatchingNightsRecorder> logger, 
            CatchRegistrationDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task TryRecord()
        {
            try
            {
                var rows = await _dbContext.Database.ExecuteSqlRawAsync(
                    "UPDATE Trap SET CatchingNights = CatchingNights + NumberOfTraps WHERE Status = 1");

                _logger.LogInformation(
                    "Catching nights updated successfully. {rows} rows affected.", rows);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while updating trap catching nights: {msg}", e.Message);
            }
        }
    }
}
