using System;
using System.Collections.Generic;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueByKeyIgnoreCase<TValue>(
            this Dictionary<string, TValue> dictionary,
            string key)
        {
            return dictionary.SingleOrDefault(x => 
                    x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }
}
