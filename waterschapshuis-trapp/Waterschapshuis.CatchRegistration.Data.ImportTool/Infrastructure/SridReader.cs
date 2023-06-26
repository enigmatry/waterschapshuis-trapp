using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GeoAPI.CoordinateSystems;
using ProjNet.Converters.WellKnownText;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure
{
    public static class SridReader
    {
        private const string FileName = "SRID.csv"; //Change this to point to the SRID.CSV file.
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", FileName);

        private static readonly Dictionary<int, ICoordinateSystem> CoordinateSystems = new Dictionary<int, ICoordinateSystem>();

        private struct WellKnownTextString
        {
            /// <summary>Well-known ID</summary>
            public int Id { get; set; }
            /// <summary>Well-known Text</summary>
            public string Text { get; set; }
        }

        /// <summary>Enumerates all SRID's in the SRID.csv file.</summary>
        /// <returns>Enumerator</returns>
        private static IEnumerable<WellKnownTextString> GetSridList()
        {
            using StreamReader sr = File.OpenText(FilePath);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    continue;
                }
                int split = line.IndexOf(';');
                if (split > -1)
                {
                    yield return new WellKnownTextString
                    {
                        Id = Int32.Parse(line.Substring(0, split)),
                        Text = line.Substring(split + 1)
                    };
                }
            }
            sr.Close();
        }
        /// <summary>Gets a coordinate system from the SRID.csv file</summary>
        /// <param name="id">EPSG ID</param>
        /// <returns>Coordinate system, or null if SRID was not found.</returns>
        public static ICoordinateSystem GetCoordinateSystemById(int id)
        {
            if (CoordinateSystems.ContainsKey(id))
            {
                return CoordinateSystems[id];
            }

            foreach (WellKnownTextString wkt in GetSridList())
            {
                if (wkt.Id != id)
                {
                    continue;
                }
                var result = CoordinateSystemWktReader.Parse(wkt.Text, Encoding.UTF8) as ICoordinateSystem;
                CoordinateSystems.Add(id, result);
                return result;
            }

            return null;
        }
    }
}
