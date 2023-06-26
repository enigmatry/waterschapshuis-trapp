using System;
using Microsoft.Extensions.Configuration;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class ConfigurationExtensions
    {
        // Application arguments.
        private const string Import = nameof(Import);
        private const string File = nameof(File);
        private const string AutoSave = nameof(AutoSave);
        private const string Undefined = nameof(Undefined);

        public static string GetAppArguments(this IConfiguration configuration)
        {
            string import = configuration.GetImportArgumentValue();
            string file = configuration.GetFileName();
            string autoSave = configuration.IsAutoSaveEnabled().ToString();

            return $"{Import} = {import}, {File} = {file}, {AutoSave} = {autoSave}";
        }

        public static string GetImportArgumentValue(this IConfiguration configuration)
        {
            return GetConfigurationValueOrDefault(configuration, Import, Undefined);
        }

        public static string GetFileName(this IConfiguration configuration)
        {
            return GetConfigurationValueOrDefault(configuration, File, Undefined);
        }

        public static bool IsAutoSaveEnabled(this IConfiguration configuration)
        {
            return Boolean.TryParse(configuration[AutoSave], out var enabled) && enabled;
        }

        private static string GetConfigurationValueOrDefault(IConfiguration configuration,
            string valueName,
            string defaultValue)
        {
            var value = configuration[valueName];
            return String.IsNullOrEmpty(value) ? defaultValue : value;
        }
    }
}
