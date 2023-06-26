using System;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Rayons;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.CatchAreas
{
    public class CatchAreaBuilder : EntityWithGeometryBuilderBase
    {
        private Rayon _rayon = null!;
        private string _name = String.Empty;
        private string _organizationName = String.Empty;
        private string _rayonName = String.Empty;

        public static implicit operator CatchArea(CatchAreaBuilder builder)
        {
            return builder.Build();
        }

        private CatchArea Build()
        {
            CatchArea result;

            if(_rayon == null){
                Rayon rayon = new RayonBuilder()
                    .WithOrganizationName(_organizationName)
                    .WithName(_rayonName);

                result = CatchArea.Create(_name, rayon, _geometry);
            }
            else
            {
                result = CatchArea.Create(_name, _rayon, _geometry);
            }

            return result;
        }

        public CatchAreaBuilder WithName(string value)
        {
            _name = value;
            return this;
        }
        public CatchAreaBuilder WithRayon(Rayon value)
        {
            _rayon = value;
            return this;
        }

        public CatchAreaBuilder WithGeometry(Geometry value)
        {
            _geometry = value;
            return this;
        }

        public CatchAreaBuilder WithGeoHierarchy(
           string organizationName,
           string rayonName)
        {
            _organizationName = organizationName;
            _rayonName = rayonName;
            return this;
        }
    }
}
