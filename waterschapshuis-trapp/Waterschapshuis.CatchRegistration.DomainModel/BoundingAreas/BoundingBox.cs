using System.Collections.Generic;
using JetBrains.Annotations;
using NetTopologySuite.Geometries;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas
{
    [PublicAPI]
    public class BoundingBox
    {
        public Location? Location { get; set; }
        public int WidthKilometers { get; set; }

        public Geometry Geometry
        {
            get
            {
                return Location != null
                    ? GeometryUtil.Factory.CreateRectangle(Location.Longitude, Location.Latitude,
                        WidthKilometers * 1000)
                    : Polygon.Empty;
            }
        }
       
    }
}
