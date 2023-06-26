using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps.Styles
{
    // We can have map styles for trap type (we have TrapTypeId).
    // There are also special cases for observation and archived observation
    // (in those special cases we don't have TrapTypeId).
    [PublicAPI]
    [DebuggerDisplay("LookupKeyCode = {LookupKeyCode}, TrapTypeId = {TrapTypeId}, TrapStatus = {TrapStatus}")]
    public sealed class MapStyleLookupKey : IEquatable<MapStyleLookupKey?>
    {
        public MapStyleLookupKey(MapStyleLookupKeyCode lookupKeyCode, Guid? trapTypeId, TrapStatus? trapStatus)
        {
            LookupKeyCode = lookupKeyCode;
            TrapTypeId = trapTypeId;
            TrapStatus = trapStatus;
        }

        public MapStyleLookupKeyCode LookupKeyCode { get; }
        public Guid? TrapTypeId { get; }
        public TrapStatus? TrapStatus { get; }

        public static MapStyleLookupKey CreateForTrapType(Guid trapTypeId, TrapStatus trapStatus) =>
            new MapStyleLookupKey(MapStyleLookupKeyCode.TrapType, trapTypeId, trapStatus);

        public static MapStyleLookupKey CreateForObservationLocation() =>
            new MapStyleLookupKey(MapStyleLookupKeyCode.ObservationLocation, null, null);

        public static MapStyleLookupKey CreateForArchivedObservationLocation() =>
            new MapStyleLookupKey(MapStyleLookupKeyCode.ArchivedObservationLocation, null, null);

        public static MapStyleLookupKey CreateForUserTracking() =>
            new MapStyleLookupKey(MapStyleLookupKeyCode.UserTracking, null, null);

        public static MapStyleLookupKey CreateForTrappersTracking() =>
            new MapStyleLookupKey(MapStyleLookupKeyCode.TrappersTracking, null, null);

        public override bool Equals(object? obj) => Equals(obj as MapStyleLookupKey);

        public bool Equals(MapStyleLookupKey? other) =>
            other != null &&
            EqualityComparer<MapStyleLookupKeyCode>.Default.Equals(LookupKeyCode, other.LookupKeyCode) &&
            EqualityComparer<Guid?>.Default.Equals(TrapTypeId, other.TrapTypeId) &&
            EqualityComparer<TrapStatus?>.Default.Equals(TrapStatus, other.TrapStatus);

        public override int GetHashCode() => HashCode.Combine(LookupKeyCode, TrapTypeId, TrapStatus);

        public static bool operator ==(MapStyleLookupKey? left, MapStyleLookupKey? right) =>
            EqualityComparer<MapStyleLookupKey>.Default.Equals(left, right);

        public static bool operator !=(MapStyleLookupKey? left, MapStyleLookupKey? right) => !(left == right);
    }
}
