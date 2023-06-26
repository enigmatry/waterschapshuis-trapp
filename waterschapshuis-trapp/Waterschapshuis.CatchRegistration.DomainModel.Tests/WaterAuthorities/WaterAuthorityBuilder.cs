using System;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.WaterAuthorities
{
    public class WaterAuthorityBuilder
    {
        private string _name = String.Empty;
        private string _codeUvw = String.Empty;
        private string _organizationName = String.Empty;
        private Geometry _geometry = Polygon.Empty;

        public static implicit operator WaterAuthority(WaterAuthorityBuilder builder)
        {
            return builder.Build();
        }

        private WaterAuthority Build()
        {
            Organization organization = new OrganizationBuilder()
                .WithName(_organizationName);

            var result = WaterAuthority.Create(_name, _codeUvw, organization, _geometry);

            return result;
        }

        public WaterAuthorityBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public WaterAuthorityBuilder WithCodeUvw(string value)
        {
            _codeUvw = value;
            return this;
        }

        public WaterAuthorityBuilder WithGeometry(Geometry value)
        {
            _geometry = value;
            return this;
        }

        public WaterAuthorityBuilder WithGeoHierarchy(
            string organizationName)
        {
            _organizationName = organizationName;
            return this;
        }
    }
}
