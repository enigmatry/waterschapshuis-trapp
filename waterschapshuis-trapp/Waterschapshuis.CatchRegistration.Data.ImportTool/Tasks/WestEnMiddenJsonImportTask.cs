using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public abstract class WestEnMiddenJsonImportTask : GeoJsonImportTask
    {
        protected WestEnMiddenJsonImportTask(
            ILogger logger, 
            IConfiguration configuration, 
            IServiceScopeFactory serviceScopeFactory) 
            : base(logger, configuration, serviceScopeFactory)
        {
        }

    }
}
