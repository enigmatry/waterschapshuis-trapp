using System;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.CatchAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.WaterAuthorities;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreas
{
    public class SubAreaBuilder
    {
        private string _name = String.Empty;
        private string _organizationName = String.Empty;
        private string _rayonName = String.Empty;
        private CatchArea _catchArea = null!;
        private string _catchAreaName = String.Empty;
        private string _waterAuthorityName = String.Empty;
        private Geometry _geometry = Polygon.Empty;

        public static implicit operator SubArea(SubAreaBuilder builder)
        {
            return builder.Build();
        }

        private SubArea Build()
        {
            CatchArea catchArea = new CatchAreaBuilder()
                .WithName(_catchAreaName)
                .WithGeoHierarchy(_organizationName, _rayonName);

            WaterAuthority waterAuthority = new WaterAuthorityBuilder()
                .WithName(_waterAuthorityName);

            SubArea result;
            if (_catchArea == null)
            {
                result = SubArea.Create(_name, catchArea, waterAuthority, _geometry);
            }
            else
            {
                result = SubArea.Create(_name, _catchArea, waterAuthority, _geometry);
            }

            result.CatchArea.Rayon.Organization.AddWaterAuthority(waterAuthority);
            catchArea.AddSubArea(result);
            waterAuthority.AddSubArea(result);

            return result;
        }

        public SubAreaBuilder WithName(string value)
        {
            _name = value;
            return this;
        }

        public SubAreaBuilder WithCatchAreaId(CatchArea value)
        {
            _catchArea = value;
            return this;
        }

        public SubAreaBuilder WithGeometry(Geometry value)
        {
            _geometry = value;
            return this;
        }

        public SubAreaBuilder WithGeoHierarchy(
           string organizationName,
           string rayonName,
           string catchAreaName,
           string waterAuthorityName)
        {
            _organizationName = organizationName;
            _rayonName = rayonName;
            _catchAreaName = catchAreaName;
            _waterAuthorityName = waterAuthorityName;
            return this;
        }
    }
}
