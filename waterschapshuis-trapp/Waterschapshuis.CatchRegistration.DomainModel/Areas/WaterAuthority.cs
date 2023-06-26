using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas
{
    public class WaterAuthority : Entity<Guid>, IEntityHasName<Guid>, IHasGeometry
    {
        public const int NameMaxLength = 60;
        public const int CodeUvwMaxLength = 10;

        private readonly List<SubArea> _subAreas = new List<SubArea>();

        private WaterAuthority() { }

        public string Name { get; private set; } = String.Empty;
        public string CodeUvw { get; private set; } = String.Empty;
        public Geometry Geometry { get; private set; } = Polygon.Empty;
        public Guid OrganizationId { get; private set; } = Guid.Empty;
        public Organization Organization { get; private set; } = null!;
        public IReadOnlyCollection<SubArea> SubAreas => _subAreas.AsReadOnly();

        public static WaterAuthority Create(string name, string codeUvw, Guid organizationId, Geometry geometry) =>
            new WaterAuthority
            {
                Id = GenerateId(),
                Name = name,
                CodeUvw = codeUvw,
                OrganizationId = organizationId,
                Geometry = geometry
            };

        public static WaterAuthority Create(string name, string codeUvw, Organization organization, Geometry geometry) =>
            Create(name, codeUvw, organization.Id, geometry).WithOrganization(organization);

        public WaterAuthority WithOrganization(Organization organization)
        {
            Organization = organization;
            return this;
        }

        public WaterAuthority AddSubArea(SubArea subArea)
        {
            _subAreas.Add(subArea);
            return this;
        }

        public void UpdateGeometry(Geometry value)
        {
            Geometry = value;
        }
    }
}
