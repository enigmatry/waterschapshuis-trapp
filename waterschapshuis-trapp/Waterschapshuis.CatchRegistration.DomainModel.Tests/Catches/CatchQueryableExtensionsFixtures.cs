using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches
{
    [Category("unit")]
    public class CatchQueryableExtensionsFixtures
    {
        private IQueryable<User> _testUsers = null!;
        private IQueryable<Catch> _testCatches = null!;

        [SetUp]
        public void SetUp()
        {
            _testUsers = CreateTestUserList();
            _testCatches = CreateTestCatchesList(_testUsers);
        }

        [Test]
        public void TestValidQuery_ByUserCreatedId()
        {
            var firstUser = _testUsers.Single(x => x.Name == "First User");

            var items = _testCatches.QueryByUserCreatedId(firstUser.Id);

            items.Count().Should().Be(1);
            items.First().CreatedById.Should().Be(firstUser.Id);
        }

        [Test]
        public void TestValidQuery_FromDateCreated()
        {
            var firstUser = _testUsers.Single(x => x.Name == "First User");

            var itemsFromYesterday = _testCatches.QueryFromDateRecorded(DateTimeOffset.Now.AddDays(-1));
            var itemsFromToday = _testCatches.QueryFromDateRecorded(DateTimeOffset.Now);

            itemsFromYesterday.Count().Should().Be(2);

            itemsFromToday.Count().Should().Be(1);
            itemsFromToday.First().CreatedById.Should().Be(firstUser.Id);
        }

        [Test]
        public void TestValidQuery_OnlyCatches()
        {
            var items = _testCatches.QueryByIsByCatch(false);

            items.Count().Should().Be(1);
            items.First().CatchType.Name.Should().Be("Test Catch");
        }

        [Test]
        public void TestValidQuery_OnlyByCatches()
        {
            var items = _testCatches.QueryByIsByCatch(true);

            items.Count().Should().Be(2);
            items.First().CatchType.Name.Should().Be("Test ByCatch");
        }

        [Test]
        public void Exists()
        {
            var existingCatchIds = _testCatches.Select(x => x.Id).ToArray();
            var notExistingCatchId = Guid.NewGuid();

            _testCatches.Exists(existingCatchIds).Should().BeTrue();
            _testCatches.Exists(notExistingCatchId).Should().BeFalse();
        }

        [Test]
        public void TestValidQuery_QueryByYearAndSeason()
        {
            var items = _testCatches.QueryByYear(2020).QueryBySeason(Season.Winter);
            items.Count().Should().Be(1);
        }

        private static IQueryable<User> CreateTestUserList()
        {
            User firstUser = new UserBuilder()
                .Name("First User");

            User secondUser = new UserBuilder()
                .Name("Second User");

            return new List<User> { firstUser, secondUser }.AsQueryable();
        }

        private static IQueryable<Catch> CreateTestCatchesList(IQueryable<User> users)
        {
            Trap trap = new TrapBuilder()
                .WithNumberOfTraps(2)
                .WithStatus(TrapStatus.Catching)
                .WithTrapTypeId(TrapType.ConibearBeverratId)
                .WithRemarks("Just a test Trap for which to add Catches");

            Catch firstCatch = new CatchBuilder();
            Catch secondCatch = new CatchBuilder();
            Catch thirdCatch = new CatchBuilder();

            CatchType firstCatchType = new CatchTypeBuilder()
                .WithName("Test Catch")
                .WithIsByCatch(false)
                .WithAnimalType(AnimalType.Mammal)
                .WithOrder(1);

            CatchType secondCatchType = new CatchTypeBuilder()
                .WithName("Test ByCatch")
                .WithIsByCatch(true)
                .WithAnimalType(AnimalType.Bird)
                .WithOrder(2);

            var firstUserId = users.First(x => x.Name == "First User").Id;
            var secondUserId = users.First(x => x.Name == "Second User").Id;

            firstCatch
                .WithTrap(trap)
                .WithCatchType(firstCatchType)
                .SetCreated(DateTimeOffset.Now, firstUserId);
            firstCatch.SetRecordedWithTimeReset(firstCatch.CreatedOn);

            secondCatch
                .WithTrap(trap)
                .WithCatchType(secondCatchType)
                .SetCreated(DateTimeOffset.Now.AddDays(-1), secondUserId);
            secondCatch.SetRecordedWithTimeReset(secondCatch.CreatedOn);

            thirdCatch
                .WithTrap(trap)
                .WithCatchType(secondCatchType)
                .SetCreated(new DateTimeOffset(new DateTime(2019, 11, 20)), secondUserId);
            thirdCatch.SetRecordedWithTimeReset(new DateTimeOffset(new DateTime(2019, 12, 20)));

            return new List<Catch> { firstCatch, secondCatch, thirdCatch }.AsQueryable();
        }
    }
}
