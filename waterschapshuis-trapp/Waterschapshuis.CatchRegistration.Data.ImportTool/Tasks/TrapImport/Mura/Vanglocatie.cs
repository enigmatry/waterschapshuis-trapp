using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Point = NetTopologySuite.Geometries.Point;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.TrapImport.Mura
{
    [UsedImplicitly]
    public class Vanglocatie
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public int TrapType { get; set; }
        public int NumberOfTraps { get; set; }
        public string Remarks { get; set; }
        public bool Removed { get; set; }
        public bool Active { get; set; }
        public string UserExternalId { get; set; } = String.Empty;
        public DateTimeOffset Date { get; set; }

        // Ignore serialization during logging.
        [JsonIgnore]
        public Point Location { get; set; } = Point.Empty;

        public TrapStatus GetStatus() =>
            Removed ? TrapStatus.Removed :
            Active ? TrapStatus.Catching :
            TrapStatus.NotCatching;
    }
}
