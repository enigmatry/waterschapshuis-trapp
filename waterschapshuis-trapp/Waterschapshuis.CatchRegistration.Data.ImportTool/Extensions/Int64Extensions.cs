using System;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class Int64Extensions
    {
        public static DateTimeOffset AsDateTimeOffset(this long value)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(value);
        }
    }
}
