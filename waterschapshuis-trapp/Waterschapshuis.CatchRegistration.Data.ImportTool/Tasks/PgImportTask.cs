using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Implementation;
using Npgsql;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public abstract class PgImportTask : ImportTask
    {
        private NpgsqlConnection _connection;
        private readonly IJsonConverter _jsonConverter;

        protected PgImportTask(
            ILogger logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
            NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite(
                new DotSpatialAffineCoordinateSequenceFactory(Ordinates.XY), new PrecisionModel(PrecisionModels.Fixed));

            _jsonConverter = Scope.GetService<IJsonConverter>();
        }

        protected abstract string ConnectionString { get; }

        public override async Task ExecuteImportAsync(CancellationToken cancellationToken)
        {
            await using var context = Scope.GetService<CatchRegistrationDbContext>();
            _connection = new NpgsqlConnection(ConnectionString);

            Logger.LogCatchRegistrationDbContextInfo(context);
            Logger.LogNpgsqlConnectionInfo(_connection);

            await _connection.OpenAsync(cancellationToken);
            _connection.TypeMapper.UseNetTopologySuite();
            await ExecutePgImportsAsync(cancellationToken);

            await FinalizeImportAsync(cancellationToken);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _connection?.Dispose();
            }
        }

        protected async Task ExecutePgImportAsync<TData, TEntity>(
            Func<TData, CancellationToken, Task<TEntity>> createAsync,
            string selectQuery,
            int batchSize,
            CancellationToken cancellationToken)
            where TEntity : Entity
        {
            var batch = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await ExecutePgImportAsync(
                        createAsync,
                        $"{selectQuery} ASC OFFSET {batchSize * batch++} LIMIT {batchSize}",
                        cancellationToken);
                }
                // Import exception will be thrown if there are no data for import.
                // Any other occurrence of this exception type is handled by overloaded method.
                catch (ImportException)
                {
                    if (batch == 0)
                    {
                        throw;
                    }

                    break;
                }
            }
        }

        protected async Task ExecutePgImportAsync<TData, TEntity>(
            Func<TData, CancellationToken, Task<TEntity>> createAsync,
            string select,
            CancellationToken cancellationToken)
            where TEntity : Entity
        {
            var imported = new HashSet<TEntity>();
            List<TData> data = (await _connection.QueryAsync<TData>(select)).ToList();

            if (!data.Any())
            {
                throw ImportException.Empty(nameof(TEntity).ToLower());
            }

            foreach (TData item in data)
            {
                try
                {
                    imported.Add(await createAsync(item, cancellationToken));
                }
                catch (ImportException exception)
                {
                    RegisterImportException(exception, _jsonConverter.ConvertToString(item));
                }
            }

            AddEntities(imported.ToArray());
        }

        protected abstract Task ExecutePgImportsAsync(CancellationToken cancellationToken);
    }
}
