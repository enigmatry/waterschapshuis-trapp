using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class CatchArea : Entity<Guid>, IEntityHasName<Guid>, IHasGeometry
    {
        public const int NameMaxLength = 50;

        private readonly List<SubArea> _subAreas = new List<SubArea>();

        private CatchArea() { }

        public string Name { get; private set; } = String.Empty;
        public Geometry Geometry { get; private set; } = Polygon.Empty;
        public Guid RayonId { get; private set; } = Guid.Empty;
        public Rayon Rayon { get; private set; } = null!;
        public IReadOnlyCollection<SubArea> SubAreas => _subAreas.AsReadOnly();

        public static CatchArea Create(string name, Guid rayonId, Geometry geometry) =>
            new CatchArea
            {
                Id = GenerateId(),
                Name = name,
                RayonId = rayonId,
                Geometry = geometry
            };

        public static CatchArea Create(string name, Rayon rayon, Geometry geometry) =>
            Create(name, rayon.Id, geometry).WithRayon(rayon);

        public CatchArea AddSubArea(SubArea subArea)
        {
            _subAreas.Add(subArea);
            return this;
        }

        private CatchArea WithRayon(Rayon rayon)
        {
            Rayon = rayon;
            return this;
        }

        public void UpdateGeometry(Geometry value)
        {
            Geometry = value;
        }
    }
}
