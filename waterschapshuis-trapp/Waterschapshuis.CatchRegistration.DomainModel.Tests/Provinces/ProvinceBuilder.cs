using System;
using NetTopologySuite.Geometries;
using NetTopologySuite.Utilities;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Provinces
{
    public class ProvinceBuilder : EntityWithGeometryBuilderBase
    {
        private string _name = String.Empty;

        public static implicit operator Province(ProvinceBuilder builder)
        {
            return builder.Build();
        }

        public ProvinceBuilder WithRectangleGeometry(double longitude, double latitude, double distance)
        {
            _geometry = CreateRectangle(longitude, latitude, distance);
            return this;
        }

        public ProvinceBuilder WithGeometry(Geometry value)
        {
            _geometry = value;
            return this;
        }

        public ProvinceBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        private Province Build()
        {
            var result = Province.Create(_name, _geometry);
            return result;
        }
    }
}
