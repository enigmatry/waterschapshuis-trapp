using System;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class StringExtensions
    {
        public static string AsOrganizationPrefixed(this string value, string organization)
        {
            return $"{organization}:{value}";
        }

        public static DateTimeOffset? AsDateTimeOffset(this string value)
        {
            if (DateTimeOffset.TryParse(value, out var date))
            {
                return date;
            }

            return null;
        }
    }
}
