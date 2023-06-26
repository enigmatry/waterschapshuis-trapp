using System;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.HourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreas;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares
{
    public class SubAreaHourSquareBuilder : EntityWithGeometryBuilderBase
    {
        private string _organizationName = String.Empty;
        private string _rayonName = String.Empty;
        private string _catchAreaName = String.Empty;
        private string _subAreaName = String.Empty;
        private string _hourSquareName = String.Empty;
        private string _waterAuthorityName = String.Empty;
        private double _longitude;
        private double _latitude;
        private double _distance;
        private int _kmWaterway;
        private short _percentageDitch;
        private long _ditch;
        private long _wetDitch;
        private Guid _versionRegionalLayoutId = VersionRegionalLayout.V2IndelingId;

        private SubArea _subArea = null!;
        private HourSquare _hourSquare = null!;

        public static implicit operator SubAreaHourSquare(SubAreaHourSquareBuilder builder)
        {
            return builder.Build();
        }

        private SubAreaHourSquare Build()
        {
            SubArea subArea = 
                _subArea ??
                new SubAreaBuilder()
                .WithName(_subAreaName)
                .WithGeoHierarchy(_organizationName, _rayonName, _catchAreaName, _waterAuthorityName);

            HourSquare hourSquare = 
                _hourSquare ??
                new HourSquareBuilder()
                .WithName(_hourSquareName);

            _geometry = CreateRectangle(_longitude, _latitude, _distance);

            var result = SubAreaHourSquare.Create(
                subArea,
                hourSquare,
                _kmWaterway,
                _percentageDitch,
                _ditch,
                _wetDitch,
                _geometry,
                _versionRegionalLayoutId);

            return result;
        }

        public SubAreaHourSquareBuilder WithSubArea(SubArea subArea)
        {
            _subArea = subArea;
            return this;
        }

        public SubAreaHourSquareBuilder WithVersionRegionalLayoutId(Guid value)
        {
            _versionRegionalLayoutId = value;
            return this;
        }

        public SubAreaHourSquareBuilder WithHourSquare(HourSquare hourSquare)
        {
            _hourSquare = hourSquare;
            return this;
        }

        public SubAreaHourSquareBuilder WithStartingCoordinate(double longitude, double latitude, double distance = 0.1)
        {
            (_longitude, _latitude, _distance) = (longitude, latitude, distance);
            return this;
        }

        public SubAreaHourSquareBuilder WithWaterwayValues(
            int kmWaterway,
            short percentageDitch,
            long ditch,
            long wetDitch)
        {
            _kmWaterway = kmWaterway;
            _percentageDitch = percentageDitch;
            _ditch = ditch;
            _wetDitch = wetDitch;
            return this;
        }

        public SubAreaHourSquareBuilder WithGeoHierarchy(
           string organizationName,
           string rayonName,
           string catchAreaName,
           string subAreaName,
           string waterAuthorityName,
           string hourSquareName)
        {
            _organizationName = organizationName;
            _rayonName = rayonName;
            _catchAreaName = catchAreaName;
            _subAreaName = subAreaName;
            _waterAuthorityName = waterAuthorityName;
            _hourSquareName = hourSquareName;
            return this;
        }
    }
}
