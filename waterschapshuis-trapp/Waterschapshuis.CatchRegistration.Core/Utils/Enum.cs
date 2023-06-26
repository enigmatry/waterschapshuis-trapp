using System;
using System.Collections.Generic;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Core.Utils
{
    public static class Enum<TEnum> where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        public static TEnum GetMatchingEnum(string match)
        {
            TEnum result = GetAll().FirstOrDefault(x => 
                Convert.ToInt32(x).ToString() == match ||
                x.ToString() == match || 
                x.FindCode() == match);
            return result;
        }

        public static IList<TEnum> GetAll()
        {
            Type t = typeof(TEnum);
            return !t.IsEnum
                ? new List<TEnum>()
                : Enum.GetValues(t).Cast<TEnum>().ToList();
        }
    }
}
