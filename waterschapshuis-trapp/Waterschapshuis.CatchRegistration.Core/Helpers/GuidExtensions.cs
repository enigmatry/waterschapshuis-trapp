using System;

namespace Waterschapshuis.CatchRegistration.Core.Helpers
{
    public static class GuidExtensions
    {
        public static bool IsValidGuid(this Guid? guid)
        {
            return guid.HasValue && Guid.TryParse(guid.ToString(), out Guid _);
        }

        public static bool IsEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value.IsEmpty();
        }

        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool NotEmpty(this Guid guid) => !IsEmpty(guid);
    }
}
