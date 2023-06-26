using System;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations
{
    public class OrganizationBuilder : EntityWithGeometryBuilderBase
    {
        private string _name = String.Empty;
        private string _shortName = String.Empty;

        public static implicit operator Organization(OrganizationBuilder builder)
        {
            return builder.Build();
        }

        private Organization Build()
        {
            var result = Organization.Create(_name, _shortName, _geometry);
            return result;
        }

        public OrganizationBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public OrganizationBuilder WithShortName(string value)
        {
            _shortName = value;
            return this;
        }

        public OrganizationBuilder WithGeometry(Geometry value)
        {
            _geometry = value;
            return this;
        }
    }
}
