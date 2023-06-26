using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles
{
    [PublicAPI]
    // encapsulates codes (strings) into a class
    [DebuggerDisplay("{" + nameof(Code) + "}")]
    public sealed class MapStyleLookupKeyCode : IEquatable<MapStyleLookupKeyCode?>
    {
        public MapStyleLookupKeyCode(string code) { Code = code; }

        [UsedImplicitly] public string Code { get; }

        public static MapStyleLookupKeyCode TrapType => new MapStyleLookupKeyCode("TT");
        public static MapStyleLookupKeyCode ObservationLocation => new MapStyleLookupKeyCode("OL");
        public static MapStyleLookupKeyCode ArchivedObservationLocation => new MapStyleLookupKeyCode("AOL");
        public static MapStyleLookupKeyCode UserTracking => new MapStyleLookupKeyCode("UTR");
        public static MapStyleLookupKeyCode TrappersTracking => new MapStyleLookupKeyCode("TTR");

        public override bool Equals(object? obj) => Equals(obj as MapStyleLookupKeyCode);

        public bool Equals(MapStyleLookupKeyCode? other) =>
            other != null &&
            Code == other.Code;

        public override int GetHashCode() => HashCode.Combine(Code);

        public static bool operator ==(MapStyleLookupKeyCode? left, MapStyleLookupKeyCode? right) => EqualityComparer<MapStyleLookupKeyCode>.Default.Equals(left, right);

        public static bool operator !=(MapStyleLookupKeyCode? left, MapStyleLookupKeyCode? right) => !(left == right);
    }
}
