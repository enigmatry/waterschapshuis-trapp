using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public abstract class JsonImportTask : ImportTask
    {
        private readonly IJsonReader _reader;

        protected JsonImportTask(ILogger logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
            _reader = Scope.ServiceProvider.GetRequiredService<IJsonReader>();
        }

        public override async Task ExecuteImportAsync(CancellationToken cancellationToken)
        {
            await ExecuteJsonImportsAsync(cancellationToken);
            await FinalizeImportAsync(cancellationToken);
        }

        protected abstract Task ExecuteJsonImportsAsync(CancellationToken cancellationToken);

        protected async Task ExecuteJsonImportAsync<TData, TEntity>(
            Regex regex,
            Func<TData, CancellationToken, Task<IEnumerable<TEntity>>> createAsync,
            CancellationToken cancellationToken) where TEntity : Entity
        {
            var imported = new HashSet<TEntity>();

            await foreach (IJsonResult result in _reader.ReadAsync(GetFilePath(), regex,
                cancellationToken))
            {
                try
                {
                    foreach (TEntity entity in await createAsync(result.Parse<TData>(), cancellationToken))
                    {
                        imported.Add(entity);
                    }
                }
                catch (ImportException exception)
                {
                    RegisterImportException(exception, result.Stringify());
                }
            }

            AddEntities(imported.ToArray());
        }

        private string GetFilePath()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", Configuration.GetFileName());

            return File.Exists(path)
                ? path
                : throw new InvalidOperationException(
                    $"JSON file '{Configuration.GetFileName()}' does not exist in the Resources subfolder of the application.");
        }
    }
}
