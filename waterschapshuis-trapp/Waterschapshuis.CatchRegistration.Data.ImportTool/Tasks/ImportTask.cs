using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Implementation;
using Npgsql;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;
using static System.Console;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public abstract class ImportTask : IImportTask, IDisposable
    {
        protected const int SridAmersfoort = 28992;
        private readonly Dictionary<string, int> _errors = new Dictionary<string, int>();
        private readonly Dictionary<Type, int> _imports = new Dictionary<Type, int>();

        protected ImportTask(
            ILogger logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            Logger = logger;
            Configuration = configuration;
            Scope = serviceScopeFactory.CreateScope();

            NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite(
                new DotSpatialAffineCoordinateSequenceFactory(Ordinates.XY), new PrecisionModel(PrecisionModels.Fixed));
        }

        protected IConfiguration Configuration { get; }
        protected ILogger Logger { get; }
        protected IServiceScope Scope { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract Task ExecuteImportAsync(CancellationToken cancellationToken);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Scope.Dispose();
            }
        }

        protected async Task FinalizeImportAsync(CancellationToken cancellationToken)
        {
            Logger.LogImportSummary(_imports);
            Logger.LogErrorSummary(_errors);

            if (Configuration.IsAutoSaveEnabled())
            {
                await SaveAsync(cancellationToken);
            }
            else
            {
                await ConfirmAndSaveAsync(cancellationToken);
            }
        }

        private async Task ConfirmAndSaveAsync(CancellationToken cancellationToken)
        {
            Write("Do you want to permanently save imported data? (Y/N) ");

            while (!cancellationToken.IsCancellationRequested)
            {
                ConsoleKeyInfo keyStroke = ReadKey();

                if (keyStroke.Key == ConsoleKey.Y)
                {
                    await SaveAsync(cancellationToken);
                    break;
                }
                
                if (keyStroke.Key == ConsoleKey.N)
                {
                    WriteLine("\nImported data is discarded.");
                    break;
                }
            }
        }

        private async Task SaveAsync(CancellationToken cancellationToken)
        {
            WriteLine("\nSaving imported data. This operation may take a few minutes to complete ...");

            await using var context = Scope.GetService<CatchRegistrationDbContext>();
            await context.SaveChangesAsync(cancellationToken);

            WriteLine("\nImported data successfully saved.");
        }

        protected void RegisterImportException(ImportException exception, object data = null)
        {
            if (data is {})
            {
                Logger.Log(LogLevel.Error, "Error occurred during the import: {message}\nData: {@data}",
                    exception.Message, data);
            }
            else
            {
                Logger.Log(LogLevel.Error, "Error occurred during the import: {message}", exception.Message);
            }

            if (_errors.ContainsKey(exception.Message))
            {
                _errors[exception.Message]++;
            }
            else
            {
                _errors.Add(exception.Message, 1);
            }
        }

        protected void AddEntities<TEntity>(params TEntity[] entities)
            where TEntity : Entity
        {
            Scope.GetService<IRepository<TEntity>>().AddRange(entities);

            if (_imports.ContainsKey(typeof(TEntity)))
            {
                _imports[typeof(TEntity)] += entities.Length;
            }
            else
            {
                _imports.Add(typeof(TEntity), entities.Length);
            }
        }

        protected User FindSystemUser(string externalId)
        {
            if (!Int64.TryParse(externalId, out var id))
            {
                throw ImportException.InvalidUser();
            }

            var userRepository = Scope.GetService<IRepository<User>>();

            return userRepository.QueryAll().SingleOrDefault(x => x.ExternalId == id)
                   ?? throw ImportException.NotFoundUser();
        }
    }
}
