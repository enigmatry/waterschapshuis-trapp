using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Configuration;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public abstract class MuraPgImportTask : PgImportTask
    {
        public MuraPgImportTask(
            ILogger logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        { }

        protected int DefaultBatchSize => Configuration.GetValue<int>("Settings:MuraDbReadBatchSize");
        protected override string ConnectionString => Configuration.GetConnectionString("Mura");

        protected virtual bool DatePassesOrganizationConstrain(DateTimeOffset date)
        {
            return date.PassesOrganizationDateConstrain(OrganizationNames.Mura);
        }
    }
}
