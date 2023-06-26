using System;
using System.Collections.Generic;

namespace Waterschapshuis.CatchRegistration.Core.GeoJSON.Models
{
    public class Geometry
    {
        private Geometry()
        {
                
        }
        public string Type { get; set; } = String.Empty;
        public List<List<List<double>>> Coordinates { get; set; } = new List<List<List<double>>>();

        public static Geometry Create()
        {
            return new Geometry();
        }
    }
}
