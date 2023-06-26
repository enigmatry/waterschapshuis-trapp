using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterschapshuis.CatchRegistration.Data.Migrations
{
    public static class MigrationBuilderExtensions
    {
        private const string Folder = "Resources";

        public static void SqlScript(this MigrationBuilder builder, string fileName)
        {
            var path = Path.Combine(AppContext.BaseDirectory, Folder, fileName);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"Migration script not found. Path: {path}");
            }

            builder.Sql(File.ReadAllText(path));
        }
    }
}
