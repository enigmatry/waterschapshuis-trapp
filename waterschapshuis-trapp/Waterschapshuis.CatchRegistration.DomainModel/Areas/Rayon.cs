using System;
using System.Collections.Generic;
using System.Linq;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class Rayon : Entity<Guid>, IEntityHasName<Guid>, IHasGeometry
    {
        public const int NameMaxLength = 50;

        private readonly List<CatchArea> _catchAreas = new List<CatchArea>();

        private Rayon() { }

        public string Name { get; private set; } = String.Empty;
        public Geometry Geometry { get; private set; } = Polygon.Empty;
        public Guid OrganizationId { get; private set; } = Guid.Empty;
        public Organization Organization { get; private set; } = null!;
        public IReadOnlyCollection<CatchArea> CatchAreas => _catchAreas.AsReadOnly();

        public static Rayon Create(string name, Guid organizationId, Geometry geometry) =>
            new Rayon 
            { 
                Id = GenerateId(),
                Name = name,
                OrganizationId = organizationId,
                Geometry = geometry
            };

        public static Rayon Create(string name, Organization organization, Geometry geometry) =>
            Create(name, organization.Id, geometry).WithOrganization(organization);
            
        private Rayon WithOrganization(Organization organization)
        {
            Organization = organization;
            return this;
        }

        public Rayon AddCatchArea(CatchArea catchArea)
        {
            _catchAreas.Add(catchArea);
            return this;
        }

        private Rayon WithCatchAreas(List<CatchArea> catchAreas)
        {
            _catchAreas.Clear();
            _catchAreas.AddRange(catchAreas);
            return this;
        }

        public void UpdateGeometry(Geometry value)
        {
            Geometry = value;
        }
    }
}
