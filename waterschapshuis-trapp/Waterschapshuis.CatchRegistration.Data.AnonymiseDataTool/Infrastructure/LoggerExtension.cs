using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework;

namespace Waterschapshuis.CatchRegistration.Data.AnonymiseDataTool.Infrastructure
{
    public static class LoggerExtensions
    {
        public static ILogger LogApplicationStartupInfo(this ILogger logger, IConfiguration configuration)
        {
            logger.LogInformation(
                $"Application { PlatformServices.Default.Application.ApplicationName } (v{ PlatformServices.Default.Application.ApplicationVersion }) started.");

            return logger;
        }
    }
}
