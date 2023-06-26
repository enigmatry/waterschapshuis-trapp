using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Npgsql;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogApplicationStartupInfo(this ILogger logger, IConfiguration configuration)
        {
            logger.LogInformation(
                $"Application { PlatformServices.Default.Application.ApplicationName } (v{ PlatformServices.Default.Application.ApplicationVersion }) started.");

            logger.LogInformation(
                $"Application arguments: { configuration.GetAppArguments() }");
        }

        public static void LogNpgsqlConnectionInfo(this ILogger logger, NpgsqlConnection connection)
        {
            var connectionInfo =
                $"V2 database connection information { Environment.NewLine }" +
                $"  host: { connection.Host }{ Environment.NewLine }" +
                $"  database: { connection.Database }";

            logger.LogInformation(connectionInfo);
        }

        public static void LogCatchRegistrationDbContextInfo(this ILogger logger, CatchRegistrationDbContext context)
        {
            var connection = context.Database.GetDbConnection();
            var connectionInfo =
                $"V3 database connection information { Environment.NewLine }" +
                $"  data source: { connection.DataSource }{ Environment.NewLine }" +
                $"  database: { connection.Database }";

            logger.LogInformation(connectionInfo);
        }

        public static void LogErrorSummary(this ILogger logger, Dictionary<string, int> errors)
        {
            if (errors.Any())
            {
                var log = $"{errors.Sum(e => e.Value)} errors occured during the import:";

                foreach (KeyValuePair<string, int> item in errors)
                {
                    log += $"\n - {item.Key} ({item.Value})";
                }

                logger.Log(LogLevel.Information, log);
            }
        }

        public static void LogImportSummary(this ILogger logger, Dictionary<Type, int> imports)
        {
            var log = "Import finished. Objects to be imported:";

            foreach (KeyValuePair<Type, int> import in imports)
            {
                log += $"\n - {import.Key.Name} ({import.Value})";
            }

            logger.Log(LogLevel.Information, log);
        }
    }
}
