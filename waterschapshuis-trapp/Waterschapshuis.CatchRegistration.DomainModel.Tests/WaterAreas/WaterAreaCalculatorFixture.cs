using System.Collections.Generic;
using FluentAssertions;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.WaterAreas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.WaterAreas
{
    [Category("unit")]
    public class WaterAreaCalculatorFixture
    {
        private readonly string DITCH = TypeWater.DitchesAndDryDitches[0];

        private WaterAreaCalculator _model = null!;
        private Polygon _sahs = null!;
        private WaterLine _waterLine = null!;
        private WaterLine _waterLineOutside = null!;
        private WaterPlane _waterAreaCrossingSquare1 = null!;
        private WaterPlane _waterAreaCrossingSquare2 = null!;
        private WaterPlane _waterAreaTouchingAngleOfSahs = null!;
        private WaterPlane _waterAreaUnderSahsSharingOneEdge = null!;

        [SetUp]
        public void SetUp()
        {
            _sahs = (Polygon)GeometryUtil.Factory.CreateRectangle(2, 2, 2);

            // line has intersection with sahs
            _waterLine = WaterLine.Create(GeometryUtil.Factory.CreateLine(new[] { new Coordinate(-2, 3), new Coordinate(2, 3) })).WithType(DITCH);
            // line is outside sahs
            _waterLineOutside = WaterLine.Create(GeometryUtil.Factory.CreateLine(new[] { new Coordinate(-2, -3), new Coordinate(2, -3) })).WithType(DITCH); 
            
            _waterAreaCrossingSquare1 = WaterPlane.Create((Polygon)GeometryUtil.Factory.CreateRectangle(0, 0, 1)).WithType(DITCH);
            _waterAreaCrossingSquare2 = WaterPlane.Create((Polygon)GeometryUtil.Factory.CreateRectangle(4, 4, 1)).WithType(DITCH);
            _waterAreaTouchingAngleOfSahs = WaterPlane.Create(GeometryUtil.Factory.CreatePolygon(new[]
            {
                new Coordinate(4, 4), new Coordinate(4, 2), new Coordinate(6, 2), new Coordinate(6, 6),
                new Coordinate(2, 6), new Coordinate(2, 4), new Coordinate(4, 4)
            })).WithType(DITCH);
            _waterAreaUnderSahsSharingOneEdge = WaterPlane.Create().WithGeometry(1, -1, 1).WithType(DITCH);

        }


        [TestCase(3, 3, 1, 8, TestName = "Plane is sharing two edges with sahs, and whole surface is within sahs")]
        [TestCase(5, -1, 1, 0, TestName = "Plane is touching sahs in one point")]
        [TestCase(5, 1, 1, 0, TestName = "Plane is sharing edge with sahs, but totally outside the sahs")]
        [TestCase(0, 0, 1, 2, TestName = "Plane is crossing with sahs")]
        [TestCase(-2, 3, 1, 0, TestName = "Plane is outside sahs")]
        public void Calculate_ForGivenPlanesCoordinatesAndSize_ReturnsExpectedDitchLength(double x, double y, double distance, int expectedLength)
        {
            var waterPlanes = new List<WaterPlane>() { WaterPlane.Create((Polygon)GeometryUtil.Factory.CreateRectangle(x, y, distance)).WithType(DITCH) };
            _model = WaterAreaCalculator.Create(_sahs, waterPlanes, new List<WaterLine>());

            _model.Calculate().ditch.Should().Be(expectedLength);
        }

        [Test]
        public void Calculate_MultipleTouchingPlanesMerged_ReturnsExpectedDitchLength()
        {
            var waterPlanes = new List<WaterPlane>()
            {
                WaterPlane.Create().WithGeometry(3, 3, 1).WithType(DITCH),
                WaterPlane.Create().WithGeometry(3, 1, 1).WithType(DITCH),
                WaterPlane.Create().WithGeometry(1, 0, 1).WithType(DITCH),
                WaterPlane.Create().WithGeometry(3, 5, 1).WithType(DITCH)
            };
            _model = WaterAreaCalculator.Create(_sahs, waterPlanes, new List<WaterLine>());

            _model.Calculate().ditch.Should().Be(12);
        }

        [Test]
        public void Calculate_MultiplePlanesOnEdgesOfSahsMerged_ReturnsZeroDitchLength()
        {
            var waterPlanes = new List<WaterPlane>() { _waterAreaTouchingAngleOfSahs, _waterAreaUnderSahsSharingOneEdge };
            _model = WaterAreaCalculator.Create(_sahs, waterPlanes, new List<WaterLine>());

            _model.Calculate().ditch.Should().Be(0);
        }

        [Test]
        public void Calculate_MultipleWaterLines_ReturnsExpectedDitchLength()
        {
            var waterLines = new List<WaterLine>() { _waterLine, _waterLineOutside };
            _model = WaterAreaCalculator.Create(_sahs, new List<WaterPlane>(), waterLines);

            _model.Calculate().ditch.Should().Be(2);
        }

        [Test]
        public void Calculate_MultipleWaterLinesAndWaterPlanes_ReturnsExpectedDitchLength()
        {
            var waterLines = new List<WaterLine>() { _waterLine, _waterLineOutside };
            var waterPlanes = new List<WaterPlane>() { _waterAreaCrossingSquare1, _waterAreaCrossingSquare2 };
            _model = WaterAreaCalculator.Create(_sahs, waterPlanes, waterLines);

            _model.Calculate().ditch.Should().Be(6);
        }
    }
}
