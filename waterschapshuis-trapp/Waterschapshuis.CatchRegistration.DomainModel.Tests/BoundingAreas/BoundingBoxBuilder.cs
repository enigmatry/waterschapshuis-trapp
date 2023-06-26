using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas;
using Location = Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas.Location;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.BoundingAreas
{
    public class BoundingBoxBuilder
    {

        private Location? _location;
        private int _widthKilometers;


        public BoundingBoxBuilder WithLocation(double longitude, double latitude)
        {
            _location = new Location() {Longitude = longitude, Latitude = latitude};
            return this;
        }

        public BoundingBoxBuilder WithWidthKilometers(int value)
        {
            _widthKilometers = value;
            return this;
        }

        public static implicit operator BoundingBox(BoundingBoxBuilder builder)
        {
            return builder.Build();
        }

        private BoundingBox Build()
        {
            var result = new BoundingBox() { Location = _location, WidthKilometers = _widthKilometers };

            return result;
        }
    }
}
