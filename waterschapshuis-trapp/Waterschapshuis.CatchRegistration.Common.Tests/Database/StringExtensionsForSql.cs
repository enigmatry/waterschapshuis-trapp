using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.Common.Tests.Database
{
    public static class StringExtensionsForSql
    {
        public static string[] SplitStatements(this string sql)
        {
            var sqlBatch = String.Empty;
            var result = new List<string>();
            sql += "\nGO"; // make sure last batch is executed.

            foreach (string line in sql.Split(new[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.ToUpperInvariant().Trim() == "GO")
                {
                    result.Add(sqlBatch);
                    sqlBatch = string.Empty;
                }
                else
                {
                    sqlBatch += line + "\n";
                }
            }

            return result.Where(s => s.IsNotNullOrEmpty()).ToArray();
        }
    }
}
