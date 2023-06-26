using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.BoundingAreas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps
{
    [Category("unit")]
    public class TrapQueryableExtensionsFixtures
    {
        private IQueryable<User> _testUsers = null!;
        private IQueryable<Trap> _testTrapsQuery = null!;
        private BoundingBox _boundingBox = null!;

        [SetUp]
        public void Setup()
        {
            _testUsers = CreateTestUserList();
            _testTrapsQuery = CreateTestTrapList(_testUsers);
            _boundingBox = CreateBoundingBox();
        }

        [Test]
        public void TestValidQuery_ByBoundingBox()
        {
            var items = _testTrapsQuery.QueryByOptionalBoundingBox(_boundingBox);
            items.Should().HaveCount(1);
        }

        [Test]
        public void TestValidQuery_ByUserCreatedId()
        {
            var firstUser = _testUsers.Single(x => x.Name == "First User");

            var items = _testTrapsQuery.QueryByUserCreatedId(firstUser.Id);

            items.Count().Should().Be(1);
            items.First().CreatedById.Should().Be(firstUser.Id);
        }

        [Test]
        public void TestValidQuery_StatusNotRemoved()
        {
            var items = _testTrapsQuery.QueryNotRemoved();

            items.Count().Should().Be(1);
            items.First().NumberOfTraps.Should().Be(2);
            items.First().Remarks.Should().Be("Test remarks");
        }

        private static IQueryable<Trap> CreateTestTrapList(IQueryable<User> users)
        {
            Trap firstTrap = new TrapBuilder()
                .WithStatus(TrapStatus.Removed);
            
            Trap secondTrap = new TrapBuilder()
                .WithCoordinates(100000, 100000)
                .WithNumberOfTraps(2)
                .WithRemarks("Test remarks")
                .WithStatus(TrapStatus.Catching)
                .WithTrapTypeId(new Guid("9F91A9D1-77D9-06D9-03A9-18F2EFCC0BCC"));

            firstTrap.SetCreated(DateTimeOffset.Now, users.Single(x => x.Name == "First User").Id);
            secondTrap.SetCreated(DateTimeOffset.Now, users.Single(x => x.Name == "Second User").Id);

            return new List<Trap> { firstTrap, secondTrap }.AsQueryable();
        }

        private static IQueryable<User> CreateTestUserList()
        {
            User firstUser = new UserBuilder()
                .Name("First User");

            User secondUser = new UserBuilder()
                .Name("Second User");

            return new List<User> { firstUser, secondUser }.AsQueryable();
        }

        private static BoundingBox CreateBoundingBox()
        {
            var bb = new BoundingBoxBuilder()
                .WithLocation(0, 0)
                .WithWidthKilometers(50);
            return bb;
        }
    }
}
