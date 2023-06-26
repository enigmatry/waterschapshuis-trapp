using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares
{
    [Category("unit")]
    public class SubAreaHourSquareQueryableExtensionsFixtures
    {
        private IQueryable<SubAreaHourSquare> _testSubAreaHourSquareQuery = null!;

        [SetUp]
        public void Setup()
        {
            _testSubAreaHourSquareQuery = CreateTestSubAreaHourSquare();
        }

        [Test]
        public void TestInvalidQuery_ByLongitude()
        {
            var item = _testSubAreaHourSquareQuery.QueryByLongAndLat(0, 5);

            item.Should().HaveCount(0);
        }

        [Test]
        public void TestInvalidQuery_ByLatitude()
        {
            var item = _testSubAreaHourSquareQuery.QueryByLongAndLat(5, 7);

            item.Should().HaveCount(0);
        }

        [Test]
        public void TestInvalidQuery_ByLongitudeAndLatitude()
        {
            var item = _testSubAreaHourSquareQuery.QueryByLongAndLat(0, 0);

            item.Should().HaveCount(0);
        }

        [Test]
        public void TestValidQuery_ByLongitudeAndLatitude()
        {
            var item = _testSubAreaHourSquareQuery.QueryByLongAndLat(5, 5);

            item.Should().HaveCount(1);
        }

        [Test]
        public void TestInvalidExist_ByLongitude()
        {
            var exists = _testSubAreaHourSquareQuery.ExistAtLocation(0, 5);

            exists.Should().Be(false);
        }

        [Test]
        public void TestInvalidExist_ByLatitude()
        {
            var exists = _testSubAreaHourSquareQuery.ExistAtLocation(5, 7);

            exists.Should().Be(false);
        }

        [Test]
        public void TestInvalidExist_ByLongitudeAndLatitude()
        {
            var exists = _testSubAreaHourSquareQuery.ExistAtLocation(0, 0);

            exists.Should().Be(false);
        }

        [Test]
        public void TestValidExist_ByLongitudeAndLatitude()
        {
            var exists = _testSubAreaHourSquareQuery.ExistAtLocation(5, 5);

            exists.Should().Be(true);
        }

        //TODO WVR-1541: Revert changes in this test after fixing the reported issue

        [Test]
        public void TestValidFind_NoResultsReturned()
        {
            var item = _testSubAreaHourSquareQuery.FindByLongAndLat(15, 15, NullLogger.Instance);

            item.Should().BeNull();
        }

        [Test]
        public void TestValidFind_OneResultReturned()
        {
            var item = _testSubAreaHourSquareQuery.FindByLongAndLat(5, 4.5, NullLogger.Instance);

            item.Should().NotBeNull();
            item?.Geometry.Centroid.X.Should().Be(5);
            item?.Geometry.Centroid.Y.Should().Be(5);
        }


        private static IQueryable<SubAreaHourSquare> CreateTestSubAreaHourSquare()
        {
            var subArSqHr = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(5, 5, 1);

            return new List<SubAreaHourSquare> { subArSqHr }.AsQueryable();
        }
    }
}
