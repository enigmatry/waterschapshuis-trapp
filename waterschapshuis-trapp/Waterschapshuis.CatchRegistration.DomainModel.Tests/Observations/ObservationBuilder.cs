using NetTopologySuite.Geometries;
using System;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Observations
{
    public class ObservationBuilder
    {
        private Guid _id = Guid.Empty;
        private string _organizationName = String.Empty;
        private string _rayonName = String.Empty;
        private string _catchAreaName = String.Empty;
        private string _subAreaName = String.Empty;
        private string _hourSquareName = String.Empty;
        private string _waterAuthorityName = String.Empty;
        private ObservationType _type = ObservationType.Overig;
        private string _remarks = String.Empty;
        private Point _location = GeometryUtil.Factory.CreatePoint(4.895431, 50.379189);
        private DateTimeOffset _recordedOn;
        private bool _hasPhoto = false;
        private AzureBlobSettings? _azureBlobSettings;

        public static implicit operator Observation(ObservationBuilder builder)
        {
            return builder.Build();
        }

        private Observation Build()
        {
            SubAreaHourSquare subAreaHourSquare = new SubAreaHourSquareBuilder()
                .WithGeoHierarchy(_organizationName, _rayonName, _catchAreaName, _subAreaName, _waterAuthorityName, _hourSquareName)
                .WithStartingCoordinate(_location.X, _location.Y);

            var observation = Observation.Create(_id, subAreaHourSquare, _type, _remarks, _location.X, _location.Y, _recordedOn, _hasPhoto, _azureBlobSettings);
            return observation;
        }

        public ObservationBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ObservationBuilder WithRemarks(string remarks)
        {
            _remarks = remarks;

            return this;
        }

        public ObservationBuilder WithLocation(double longitude, double latitude)
        {
            _location = GeometryUtil.Factory.CreatePoint(longitude, latitude);

            return this;
        }
        public ObservationBuilder WithGeoHierarchy(
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

        public ObservationBuilder WithRecordedOn(DateTimeOffset value)
        {
            _recordedOn = value;
            return this;
        }

        public ObservationBuilder WithAzureBlobSettings(AzureBlobSettings? value)
        {
            _azureBlobSettings = value;
            return this;
        }

        public ObservationBuilder WithHasPhoto(bool value)
        {
            _hasPhoto = value;
            return this;
        }
    }
}

