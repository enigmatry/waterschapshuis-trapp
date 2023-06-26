using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework
{
    [UsedImplicitly]
    public class DbContextUnitOfWork : IUnitOfWork
    {
        private readonly CatchRegistrationDbContext _context;
        private readonly ILogger<DbContextUnitOfWork> _logger;
        private bool _cancelSaving;

        public DbContextUnitOfWork(CatchRegistrationDbContext context, ILogger<DbContextUnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int SaveChanges()
        {
            Task<int> task = Task.Run(async () => await SaveChangesAsync());
            return task.Result;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_cancelSaving)
            {
                _logger.LogWarning("Not saving database changes since saving was canceled.");
                return 0;
            }

            int numberOfChanges = await _context.SaveChangesAsync(cancellationToken);
            _logger.LogDebug(
                $"{numberOfChanges} changes were saved to database {_context.Database.GetDbConnection().Database}");
            return numberOfChanges;
        }

        public void CancelSaving()
        {
            _cancelSaving = true;
        }

        // EF Core v5 will introduce Clear() method: 
        // https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker.clear?view=efcore-5.0
        public void Detach()
        {
            _context.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);
        }
    }
}
