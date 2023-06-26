using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Common
{
    [PublicAPI]
    public class BoundedBoxQuery
    {
        public double? LocationLatitude { get; set; }
        public double? LocationLongitude { get; set; }
        public int WidthKilometers { get; set; }

        public BoundingBox? MapToBoundingBox()
        {
            if (!LocationLongitude.HasValue || !LocationLatitude.HasValue)
            {
                return null;
            }
            return new BoundingBox
            {
                Location = new Location {Latitude = LocationLatitude.Value, Longitude = LocationLongitude.Value},
                WidthKilometers = WidthKilometers
            };
        }

        public int PageSize { get; set; } = 1000;
        public int CurrentPage { get; set; } = 1;
    }
}
