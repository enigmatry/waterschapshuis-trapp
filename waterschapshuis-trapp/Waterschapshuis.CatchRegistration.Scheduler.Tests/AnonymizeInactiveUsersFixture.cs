using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests
{
    [Category("integration")]
    public class AnonymizeInactiveUsersFixture : SchedulerIntegrationFixtureBase
    {
        private IMediator _mediator = null!;

        [SetUp]
        public void SetUp()
        {
            _mediator = Resolve<IMediator>();
        }

        [Test]
        public async Task GivenThereAreNoUsersToBeAnonymised()
        {
            var command = new AnonymizeInactiveUsers.Command();

            AnonymizeInactiveUsers.Result result = await Send(command);

            result.UsersAnonymized.Should().Be(0);
        }

        [Test]
        public async Task GivenThereAreTwoUsers_OneToBeAnonymised()
        {
            var inactiveDate = DateTimeOffset.Now.AddYears(-5);

            var user1EligibleForAnonymizationId = CreateUser("user1", "user1@asd.dsa", "use1", "r1", false, inactiveDate.AddMinutes(-1));
            var user2NotEligibleForAnonymizationId = CreateUser("user2", "user2@asd.dsa", "use2", "r2", false, inactiveDate.AddMinutes(1));
            var command = new AnonymizeInactiveUsers.Command
            {
                InactiveBefore = inactiveDate
            };

            AnonymizeInactiveUsers.Result result = await Send(command);

            result.UsersAnonymized.Should().Be(1);

            AssertUser(user1EligibleForAnonymizationId, $"{User.AnonymizedName}", $"{user1EligibleForAnonymizationId}{User.AnonymizedEmailSuffix}", $"{User.AnonymizedGivenName}", $"{User.AnonymizedSurname}", false);
            AssertUser(user2NotEligibleForAnonymizationId, "user2", "user2@asd.dsa", "use2", "r2", false);
        }

        [Test]
        public async Task GivenThereAreOneInactiveUser_NotToBeAnonymised()
        {
            var inactiveDate = DateTimeOffset.Now.AddYears(-5);

            var user3NotEligibleForAnonymizationId = CreateUser("user3", "user3@asd.dsa", "use3", "r3", false);
            var command = new AnonymizeInactiveUsers.Command
            {
                InactiveBefore = inactiveDate
            };

            AnonymizeInactiveUsers.Result result = await Send(command);

            result.UsersAnonymized.Should().Be(0);

            AssertUser(user3NotEligibleForAnonymizationId, "user3", "user3@asd.dsa", "use3", "r3", false);
        }

        [Test]
        public async Task GivenThereAreOneActiveUser_NotToBeAnonymised()
        {
            var inactiveDate = DateTimeOffset.Now.AddYears(-5);

            var user4NotEligibleForAnonymizationId = CreateUser("user4", "user4@asd.dsa", "use4", "r4", true);

            var command = new AnonymizeInactiveUsers.Command
            {
                InactiveBefore = inactiveDate
            };

            AnonymizeInactiveUsers.Result result = await Send(command);

            result.UsersAnonymized.Should().Be(0);

            AssertUser(user4NotEligibleForAnonymizationId, "user4", "user4@asd.dsa", "use4", "r4", true);
        }

        private void AssertUser(Guid userId, string name, string email, string givenName, string surname, bool authorized)
        {
            var user = QueryDb<User>().FirstOrDefault(u => u.Id == userId);

            user.Should().NotBeNull();
            user?.Email.Should().Be(email);
            user?.Name.Should().Be(name);
            user?.GivenName.Should().Be(givenName);
            user?.Surname.Should().Be(surname);
            user?.Authorized.Should().Be(authorized);
        }

        private async Task<AnonymizeInactiveUsers.Result> Send(AnonymizeInactiveUsers.Command command)
        {
            return await _mediator.Send(command);
        }
        
        private Guid CreateUser(string name, string email, string givenName, string surname, bool authorized, DateTimeOffset? inactiveOn = null)
        {
            User user = new UserBuilder()
                .Name(name)
                .GivenName(givenName)
                .Surname(surname)
                .Email(email)
                .InactiveOn(inactiveOn)
                .Authorized(authorized);

            AddAndSaveChanges(user);

            return user.Id;
        }

    }
}
