using FluentAssertions;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.VersionRegionalLayouts
{
    [Category("unit")]
    public class PolyLabelFixture
    {
        [Test]
        public void PolyLabelOfRectangle()
        {
            Polygon rectPolygon = (Polygon)GeometryUtil.Factory.CreateRectangle(0, 0, 10);

            var result = PolyLabel.FindPolyLabelPointIn(rectPolygon);

            result.Should().NotBeNull();
            result.X.Should().Be(0);
            result.Y.Should().Be(0);
        }

        /// <summary>
        /// ****************
        /// ****************
        /// ****************
        /// *****      *****
        /// *****      *****
        /// *****      *****
        /// *****      *****
        /// ****************
        /// ****************
        /// ****************
        /// </summary>
        [Test]
        public void PolyLabelOfRctangleRing()
        {
            var outerRectPolygon = (Polygon)GeometryUtil.Factory.CreateRectangle(0, 0, 10);
            var innerRectPolygon = (Polygon)GeometryUtil.Factory.CreateRectangle(0, 0, 5);
            Polygon rectRingPolygon = (Polygon)outerRectPolygon.Difference(innerRectPolygon);

            rectRingPolygon.Should().NotBeNull();

            var result = PolyLabel.FindPolyLabelPointIn(rectRingPolygon);

            result.Should().NotBeNull();
            rectRingPolygon.Contains(new Point(result.X, result.Y)).Should().BeTrue();
            innerRectPolygon.Contains(new Point(result.X, result.Y)).Should().BeFalse();
        }

        /// <summary>
        /// **
        /// **
        /// **    
        /// **  
        /// ** 
        /// **  
        /// ** 
        /// **
        /// **
        /// ****************
        /// ****************
        /// </summary>
        [Test]
        public void PolyLabelOfShapeL()
        {
            var outerRectPolygon = (Polygon)GeometryUtil.Factory.CreateRectangle(0, 0, 10);
            var innerRectPolygon = (Polygon)GeometryUtil.Factory.CreateRectangle(2, 2, 8);
            Polygon lShapePolygon = (Polygon)outerRectPolygon.Difference(innerRectPolygon);

            lShapePolygon.Should().NotBeNull();

            var result = PolyLabel.FindPolyLabelPointIn(lShapePolygon);

            result.Should().NotBeNull();
            lShapePolygon.Contains(new Point(result.X, result.Y)).Should().BeTrue();
            innerRectPolygon.Contains(new Point(result.X, result.Y)).Should().BeFalse();
        }

        /// <summary>
        /// ****************
        /// ****************
        /// **    
        /// **  
        /// ** 
        /// **  
        /// ** 
        /// **
        /// **
        /// ****************
        /// ****************
        /// </summary>
        [Test]
        public void PolyLabelOfShapeC()
        {
            var outerRectPolygon = (Polygon)GeometryUtil.Factory.CreateRectangle(0, 0, 10);
            var innerRectPolygon = (Polygon)GeometryUtil.Factory.CreateRectangle(2, 0, 8);
            Polygon cShapePolygon = (Polygon)outerRectPolygon.Difference(innerRectPolygon);

            cShapePolygon.Should().NotBeNull();

            var result = PolyLabel.FindPolyLabelPointIn(cShapePolygon);

            result.Should().NotBeNull();
            cShapePolygon.Contains(new Point(result.X, result.Y)).Should().BeTrue();
            innerRectPolygon.Contains(new Point(result.X, result.Y)).Should().BeFalse();
        }
    }
}
