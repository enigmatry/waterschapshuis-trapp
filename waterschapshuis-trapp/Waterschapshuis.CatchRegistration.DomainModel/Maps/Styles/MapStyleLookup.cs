using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles
{
    // Models the key and the style that can be found by that key
    [PublicAPI]
    [DebuggerDisplay("Key = {Key}, IconName = {IconName}")]
    public class MapStyleLookup
    {
        public MapStyleLookup()
        {
        }

        private MapStyleLookup(MapStyleLookupKey key, string iconName)
        {
            Key = key;
            IconName = iconName;
        }

        public static MapStyleLookup Create(MapStyleLookupKey key, string iconName) =>
            new MapStyleLookup(key, iconName);

        /// <summary>
        /// key identifier
        /// </summary>
        public MapStyleLookupKey Key { get; set; } = null!;

        /// <summary>
        /// Icon that should be used for this style
        /// </summary>
        public string IconName { get; set; } = String.Empty;
    }
}
