using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core.Collections;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts
{
    /// <summary>
    /// A fast algorithm for finding polygon pole of inaccessibility (https://github.com/mapbox/polylabel), 
    /// the most distant internal point from the polygon outline (not to be confused with centroid). 
    /// Useful for optimal placement of a text label on a polygon. 
    /// MapBox JS solution: https://github.com/mapbox/polylabel/blob/master/polylabel.js
    /// C# version: https://github.com/mapbox/polylabel/issues/26
    /// </summary>
    public static class PolyLabel
    {
        private const float Epsilon = 1E-8f;

        public static Point[] FindPolyLabelPointsIn(MultiPolygon multiPolygon, float precision = 1f)
        {
            var points = new List<Point>();
            foreach (Polygon polygon in multiPolygon.Geometries)
            {
                points.Add(FindPolyLabelPointIn(polygon, precision));
            }
            return points.ToArray();
        }

        public static Point FindPolyLabelPointIn(Polygon polygon, float precision = 1f) =>
            GetPolyLabelLocation(ToFloatArray(polygon), precision);

        private static float[][][] ToFloatArray(Polygon polygon)
        {
            var poligonRings = new List<Coordinate[]> { polygon.ExteriorRing.Coordinates };
            foreach (var interiorRing in polygon.InteriorRings)
            {
                poligonRings.Add(interiorRing.Coordinates);
            }

            var result = new float[poligonRings.Count][][];

            for (int ringIndex = 0; ringIndex < poligonRings.Count; ringIndex++)
            {
                result[ringIndex] = new float[poligonRings[ringIndex].Length][];

                for (int pointIndex = 0; pointIndex < poligonRings[ringIndex].Length; pointIndex++)
                {
                    Coordinate point = poligonRings[ringIndex][pointIndex];
                    result[ringIndex][pointIndex] = new float[2];
                    result[ringIndex][pointIndex][0] = (float)point.X;
                    result[ringIndex][pointIndex][1] = (float)point.Y;
                }
            }

            return result;
        }

        private static Point GetPolyLabelLocation(float[][][] polygon, float precision = 1f)
        {
            // Find the bounding box of the outer ring
            float minX = 0, minY = 0, maxX = 0, maxY = 0;
            for (int i = 0; i < polygon[0].Length; i++)
            {
                float[] p = polygon[0][i];
                if (i == 0 || p[0] < minX)
                    minX = p[0];
                if (i == 0 || p[1] < minY)
                    minY = p[1];
                if (i == 0 || p[0] > maxX)
                    maxX = p[0];
                if (i == 0 || p[1] > maxY)
                    maxY = p[1];
            }

            float width = maxX - minX;
            float height = maxY - minY;
            float cellSize = Math.Min(width, height);
            float h = cellSize / 2;

            //A priority queue of cells in order of their "potential" (max distance to polygon)
            PriorityQueue<float, Cell> cellQueue = new PriorityQueue<float, Cell>();

            if (FloatEquals(cellSize, 0))
                return new Point(minX, minY);

            //Cover polygon with initial cells
            for (float x = minX; x < maxX; x += cellSize)
            {
                for (float y = minY; y < maxY; y += cellSize)
                {
                    Cell cell = new Cell(x + h, y + h, h, polygon);
                    cellQueue.Enqueue(cell.Max, cell);
                }
            }

            //Take centroid as the first best guess
            Cell bestCell = GetCentroidCell(polygon);

            //Special case for rectangular polygons
            Cell bboxCell = new Cell(minX + width / 2, minY + height / 2, 0, polygon);
            if (bboxCell.D > bestCell.D)
                bestCell = bboxCell;

            int numProbes = cellQueue.Count;

            while (cellQueue.Count > 0)
            {
                //Pick the most promising cell from the queue
                Cell cell = cellQueue.Dequeue();

                //Update the best cell if we found a better one
                if (cell.D > bestCell.D)
                {
                    bestCell = cell;
                }

                //Do not drill down further if there's no chance of a better solution
                if (cell.Max - bestCell.D <= precision)
                    continue;

                //Split the cell into four cells
                h = cell.H / 2;
                Cell cell1 = new Cell(cell.X - h, cell.Y - h, h, polygon);
                cellQueue.Enqueue(cell1.Max, cell1);
                Cell cell2 = new Cell(cell.X + h, cell.Y - h, h, polygon);
                cellQueue.Enqueue(cell2.Max, cell2);
                Cell cell3 = new Cell(cell.X - h, cell.Y + h, h, polygon);
                cellQueue.Enqueue(cell3.Max, cell3);
                Cell cell4 = new Cell(cell.X + h, cell.Y + h, h, polygon);
                cellQueue.Enqueue(cell4.Max, cell4);
                numProbes += 4;
            }

            return new Point(bestCell.X, bestCell.Y);
        }

        // signed distance from point to polygon outline (negative if point is outside)
        private static float PointToPolygonDist(float x, float y, float[][][] polygon)
        {
            bool inside = false;
            float minDistSq = Single.PositiveInfinity;

            for (int k = 0; k < polygon.Length; k++)
            {
                float[][] ring = polygon[k];

                for (int i = 0, len = ring.Length, j = len - 1; i < len; j = i++)
                {
                    float[] a = ring[i];
                    float[] b = ring[j];

                    if ((a[1] > y != b[1] > y) && (x < (b[0] - a[0]) * (y - a[1]) / (b[1] - a[1]) + a[0]))
                        inside = !inside;

                    minDistSq = Math.Min(minDistSq, GetSegDistSq(x, y, a, b));
                }
            }

            return (inside ? 1 : -1) * (float)Math.Sqrt(minDistSq);
        }

        // get squared distance from a point to a segment
        private static float GetSegDistSq(float px, float py, float[] a, float[] b)
        {
            float x = a[0];
            float y = a[1];
            float dx = b[0] - x;
            float dy = b[1] - y;

            if (!FloatEquals(dx, 0) || !FloatEquals(dy, 0))
            {
                float t = ((px - x) * dx + (py - y) * dy) / (dx * dx + dy * dy);
                if (t > 1)
                {
                    x = b[0];
                    y = b[1];
                }
                else if (t > 0)
                {
                    x += dx * t;
                    y += dy * t;
                }
            }
            dx = px - x;
            dy = py - y;
            return dx * dx + dy * dy;
        }

        // get polygon centroid
        private static Cell GetCentroidCell(float[][][] polygon)
        {
            float area = 0;
            float x = 0;
            float y = 0;
            float[][] ring = polygon[0];

            for (int i = 0, len = ring.Length, j = len - 1; i < len; j = i++)
            {
                float[] a = ring[i];
                float[] b = ring[j];
                float f = a[0] * b[1] - b[0] * a[1];
                x += (a[0] + b[0]) * f;
                y += (a[1] + b[1]) * f;
                area += f * 3;
            }

            return FloatEquals(area, 0)
                ? new Cell(ring[0][0], ring[0][1], 0, polygon)
                : new Cell(x / area, y / area, 0, polygon);
        }

        private static bool FloatEquals(float a, float b)
        {
            return Math.Abs(a - b) < Epsilon;
        }

        private class Cell
        {
            public float X { get; private set; }
            public float Y { get; private set; }
            public float H { get; private set; }
            public float D { get; private set; }
            public float Max { get; private set; }

            public Cell(float x, float y, float h, float[][][] polygon)
            {
                X = x;
                Y = y;
                H = h;
                D = PointToPolygonDist(X, Y, polygon);
                Max = D + H * (float)Math.Sqrt(2);
            }
        }
    }
}
