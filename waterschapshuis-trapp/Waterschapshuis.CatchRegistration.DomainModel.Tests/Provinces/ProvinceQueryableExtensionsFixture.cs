using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Provinces
{
    [Category("unit")]
    public class ProvinceQueryableExtensionsFixture
    {
        private IQueryable<Province> _queryable = null!;

        [SetUp]
        public void Setup()
        {
            _queryable = CreateQueryable();
        }

        [Test]
        public void QueryByCoordinates_InteriorPoint_ReturnsProvinces()
        {
            _queryable.QueryContainesCoordinates(50, 50)
                .ToList().Count.Should().Be(1);
        }

        [Test]
        public void QueryByCoordinates_ExteriorPoint_ReturnsEmpty()
        {
            _queryable.QueryContainesCoordinates(10, 10)
                .ToList().Count.Should().Be(0);
        }

        private IQueryable<Province> CreateQueryable()
        {
            return new List<Province>
                {
                    new ProvinceBuilder()
                        .WithName("test_queryable_1")
                        .WithRectangleGeometry(50, 50, 10),
                    new ProvinceBuilder()
                        .WithName("test_queryable_2")
                        .WithRectangleGeometry(150, 150, 20)
                }
                .AsQueryable();
        }
    }
}
