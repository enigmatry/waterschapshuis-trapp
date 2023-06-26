using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.External.Api.Tests.TimeRegistrations
{
    [Category("integration")]
    public class TimeRegistrationsODataControllerFixture : ExternalApiIntegrationFixtureBase
    {
        private ICurrentUserProvider _currentUserProvider = null!;
        private ITimeProvider _timeProvider = null!;

        private SubAreaHourSquare _subAreaHourSquareOne = null!;
        private SubAreaHourSquare _subAreaHourSquareTwo = null!;

        private TimeRegistration _timeRegistrationOne = null!;
        private TimeRegistration _timeRegistrationTwo = null!;

        private DateTimeOffset _yesterday;
        private DateTimeOffset _today;
        private Guid _userId;

        [SetUp]
        public void SetUp()
        {
            _currentUserProvider = Resolve<ICurrentUserProvider>();
            _timeProvider = Resolve<ITimeProvider>();

            _subAreaHourSquareOne = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(4.899432, 52.379190);

            _subAreaHourSquareTwo = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(5.324237, 50.32467);

            _today = _timeProvider.Now;
            _yesterday = _today.AddDays(-1);
            _userId = _currentUserProvider.UserId.GetValueOrDefault();

            _timeRegistrationOne =
                new TimeRegistrationBuilder()
                    .WithSubAreaHourSquareId(_subAreaHourSquareOne.Id)
                    .WithTrappingTypeId(TrappingType.MuskusratId)
                    .WithDate(_yesterday)
                    .WithUserId(_userId)
                    .WithHours(3.75);

            _timeRegistrationTwo =
                new TimeRegistrationBuilder()
                    .WithSubAreaHourSquareId(_subAreaHourSquareTwo.Id)
                    .WithTrappingTypeId(TrappingType.BeverratId)
                    .WithDate(_yesterday)
                    .WithUserId(_userId)
                    .WithHours(2.5);

            AddAndSaveChanges(_subAreaHourSquareOne, _subAreaHourSquareTwo, _timeRegistrationOne, _timeRegistrationTwo);
        }

        [Test]
        public async Task TestGetTimeRegistrations()
        {
            var response = await Client.GetAsync<JObject>("odata/TimeRegistrations");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["value"].Should().NotBeEmpty();
            response["value"].Count().Should().Be(2);
        }

        [Test]
        public async Task TestGetTimeRegistrationById()
        {
            var response = await Client.GetAsync<JObject>($"odata/TimeRegistrations({_timeRegistrationOne.Id})");

            response.Should().NotBeNull();
            response["@odata.context"].Should().NotBeNull();

            response["Id"].Should().NotBeNull();
            response["Id"]!.ToString().Should().Be(_timeRegistrationOne.Id.ToString());
        }
    }
}
