using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class Province : Entity<Guid>, IEntityHasName<Guid>
    {
        public const int NameMaxLength = 50;
        private readonly HashSet<Trap> _traps = new HashSet<Trap>();

        public Geometry Geometry { get; private set; } = Polygon.Empty;
        public string Name { get; private set; } = String.Empty;
        public IReadOnlyCollection<Trap> Traps => _traps;

        public static Province Create(string name, Geometry geometry)
        {
            return new Province {Id = GenerateId(), Name = name, Geometry = geometry};
        }
    }
}
