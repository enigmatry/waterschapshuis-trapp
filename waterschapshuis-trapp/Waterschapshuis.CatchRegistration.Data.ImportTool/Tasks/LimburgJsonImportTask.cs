using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public abstract class LimburgJsonImportTask : GeoJsonImportTask
    {
        public LimburgJsonImportTask(ILogger logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory) : base(logger, configuration, serviceScopeFactory)
        {
        }
    }
}
