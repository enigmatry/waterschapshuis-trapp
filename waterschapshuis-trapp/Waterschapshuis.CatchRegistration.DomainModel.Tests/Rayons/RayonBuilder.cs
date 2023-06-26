using System;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Rayons
{
    public class RayonBuilder : EntityWithGeometryBuilderBase
    {
        private Organization _organization = null!;
        private string _name = String.Empty;
        private string _organizationName = String.Empty;

        public static implicit operator Rayon(RayonBuilder builder)
        {
            return builder.Build();
        }

        private Rayon Build()
        {
            Rayon result;
            if (_organization == null)
            {
                Organization organization = new OrganizationBuilder()
                    .WithName(_organizationName);
                result = Rayon.Create(_name, organization, _geometry);
            }
            else
            {
                result = Rayon.Create(_name, _organization, _geometry);
            }

            return result;
        }

        public RayonBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public RayonBuilder WithOrganizationName(string value)
        {
            _organizationName = value;
            return this;
        }

        public RayonBuilder WithOrganization(Organization value)
        {
            _organization = value;
            return this;
        }

        public RayonBuilder WithGeometry(Geometry value)
        {
            _geometry = value;
            return this;
        }
    }
}
