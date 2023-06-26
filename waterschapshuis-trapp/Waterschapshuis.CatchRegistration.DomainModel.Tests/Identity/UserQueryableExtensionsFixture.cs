using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity
{
    [Category("unit")]
    public class UserQueryableExtensionsFixture
    {
        private IQueryable<User> _query = null!;
        private User _user = null!;
        private User _user2 = null!;

        private const string Session1Date = "01/04/2020";
        private const string Session2Date = "02/04/2020";
        private const string Session3Date = "03/04/2020";

        [SetUp]
        public void Setup()
        {
            _user = new UserBuilder()
                .Email("username1@mail.com")
                .Name("name")
                .Authorized(true);
            _user2 = new UserBuilder()
                .Email("username2@mail.com")
                .Name("name2")
                .Authorized(false)
                .InactiveOn(Session2Date.AppToDateTimeOffset());;

            _query = new List<User> { _user, _user2 }.AsQueryable();
        }

        [Test]
        public void TestQueryEmptyList()
        {
            List<User> result = new List<User>().AsQueryable().QueryByEmail("some").ToList();
            result.Should().BeEmpty();
        }

        [TestCase("username1@mail.com", true)]
        [TestCase("username2@mail.com", true)]
        [TestCase("userName1@mail.com", false)]
        [TestCase("userName2@mail.com", false)]
        [TestCase("xyz", false)]
        public void TestQueryByEmail(string email, bool expectedToFind)
        {
            List<User> result = _query.QueryByEmail(email).ToList();

            result.Count.Should().Be(expectedToFind ? 1 : 0);
        }

        [TestCase(Session1Date, false)]
        [TestCase(Session3Date, true)]
        public void TestQueryInactiveBeforeDate(string inactiveOn, bool expectedToFind)
        {
            List<User> result = _query.QueryInactiveBeforeDate(inactiveOn.AppToDateTimeOffset()).ToList();

            result.Count.Should().Be(expectedToFind ? 1 : 0);
        }

        [TestCase(true)]
        public void TestQueryUnauthorizedOnly(bool expectedToFind)
        {
            List<User> result = _query.QueryUnauthorizedOnly().ToList();

            result.Count.Should().Be(expectedToFind ? 1 : 0);
        }
    }
}
