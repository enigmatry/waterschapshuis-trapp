using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Trackings
{
    [Category("unit")]
    public class TrackingQueryableExtensionsFixture
    {
        private IQueryable<Tracking> _query = null!;

        private readonly Guid _userIdOne = Guid.NewGuid();
        private readonly Guid _userIdTwo = Guid.NewGuid();
        private readonly Guid _userIdThree = Guid.NewGuid();
        private readonly Guid _trappingTypeIdOne = Guid.NewGuid();
        private readonly Guid _trappingTypeIdTwo = Guid.NewGuid();
        private readonly double _longitude = 4.853503;
        private readonly double _latitude = 53.372135;
        private readonly DateTimeOffset _date = DateTimeOffset.Now.AddDays(-1).Date;

        private Tracking _firstUserOneTracking = null!;
        private Tracking _lastUserOneTracking = null!;
        private Tracking _secondToLastUserOneTracking = null!;

        private Tracking _firstUserTwoTracking = null!;
        private Tracking _lastUserTwoTracking = null!;
        private Tracking _secondToLastUserTwoTracking = null!;

        private Tracking _firstUserThreeTracking = null!;
        private Tracking _lastUserThreeTracking = null!;

        [SetUp]
        public void Setup()
        {
            _query =
                GetUserOneTrackings()
                    .Union(GetUserTwoTrackings())
                    .Union(GetUserThreeTrackings())
                    .AsQueryable();
        }

        [Test]
        public void TestPreviousUserTrackingForTheDayEntity_IsValidSecondToLastEntity()
        {
            var usrOnePreviousTracking = _query.PreviousTrackingForTheDayEntity(_lastUserOneTracking.Id, _lastUserOneTracking.CreatedById, _lastUserOneTracking.RecordedOn);
            var usrTwoPreviousTracking = _query.PreviousTrackingForTheDayEntity(_lastUserTwoTracking.Id, _lastUserTwoTracking.CreatedById, _lastUserTwoTracking.RecordedOn);

            usrOnePreviousTracking.Should().NotBeNull();
            usrOnePreviousTracking?.Id.Should().Be(_secondToLastUserOneTracking.Id);
            usrOnePreviousTracking?.CreatedBy.Should().Be(_secondToLastUserOneTracking.CreatedBy);
            usrOnePreviousTracking?.TrappingTypeId.Should().Be(_secondToLastUserOneTracking.TrappingTypeId);
            usrOnePreviousTracking?.RecordedOn.Should().Be(_secondToLastUserOneTracking.RecordedOn);
            usrOnePreviousTracking?.Location.Should().Be(_secondToLastUserOneTracking.Location);

            usrTwoPreviousTracking.Should().NotBeNull();
            usrTwoPreviousTracking?.Id.Should().Be(_secondToLastUserTwoTracking.Id);
            usrTwoPreviousTracking?.CreatedBy.Should().Be(_secondToLastUserTwoTracking.CreatedBy);
            usrTwoPreviousTracking?.TrappingTypeId.Should().Be(_secondToLastUserTwoTracking.TrappingTypeId);
            usrTwoPreviousTracking?.RecordedOn.Should().Be(_secondToLastUserTwoTracking.RecordedOn);
            usrTwoPreviousTracking?.Location.Should().Be(_secondToLastUserTwoTracking.Location);
        }

        [Test]
        public void TestPreviousUserTrackingForTheDayEntity_DoesNotGetEntityRecordedOnAfter()
        {
            var usrOnePreviousTracking = _query.PreviousTrackingForTheDayEntity(_secondToLastUserOneTracking.Id, _secondToLastUserOneTracking.CreatedById, _secondToLastUserOneTracking.RecordedOn);
            var usrTwoPreviousTracking = _query.PreviousTrackingForTheDayEntity(_secondToLastUserTwoTracking.Id, _secondToLastUserTwoTracking.CreatedById, _secondToLastUserTwoTracking.RecordedOn);

            usrOnePreviousTracking.Should().NotBeNull();
            usrOnePreviousTracking?.Id.Should().NotBe(_lastUserOneTracking.Id);

            usrTwoPreviousTracking.Should().NotBeNull();
            usrTwoPreviousTracking?.Id.Should().NotBe(_lastUserTwoTracking.Id);
        }

        [Test]
        public void TestPreviousUserTrackingForTheDayEntity_GetsNoPreviousThanFirst()
        {
            var usrOnePreviousTracking = _query.PreviousTrackingForTheDayEntity(_firstUserOneTracking.Id, _firstUserOneTracking.CreatedById, _firstUserOneTracking.RecordedOn);
            var usrTwoPreviousTracking = _query.PreviousTrackingForTheDayEntity(_firstUserTwoTracking.Id, _firstUserTwoTracking.CreatedById, _firstUserTwoTracking.RecordedOn);

            usrOnePreviousTracking.Should().BeNull();

            usrTwoPreviousTracking.Should().BeNull();
        }

        [Test]
        public void TestPreviousUserTrackingForTheDayEntity_GetsNoPreviousDayEntity()
        {
            var usrThreePreviousThanFirstTracking = _query.PreviousTrackingForTheDayEntity(_firstUserThreeTracking.Id, _firstUserThreeTracking.CreatedById, _firstUserThreeTracking.RecordedOn);
            var usrThreePreviousThanLastTracking = _query.PreviousTrackingForTheDayEntity(_lastUserThreeTracking.Id, _lastUserThreeTracking.CreatedById, _lastUserThreeTracking.RecordedOn);

            usrThreePreviousThanFirstTracking.Should().BeNull();

            usrThreePreviousThanLastTracking.Should().BeNull();
        }

        private IEnumerable<Tracking> GetUserOneTrackings()
        {
            var trackings = new List<Tracking>();
            var timeStartOne = _date.AddHours(-1);
            var timeStartTwo = timeStartOne.AddMinutes(30);

            for (int i = 0; i < 3; i++)
            {
                trackings.Add(CreateTracking(_userIdOne, _trappingTypeIdOne, timeStartOne.AddMinutes(i * 30)));
                trackings.Add(CreateTracking(_userIdOne, _trappingTypeIdTwo, timeStartTwo.AddHours(i)));
            }

            _firstUserOneTracking = trackings.OrderBy(x => x.RecordedOn).First();
            _lastUserOneTracking = trackings.OrderBy(x => x.RecordedOn).Last();
            _secondToLastUserOneTracking = trackings.OrderBy(x => x.RecordedOn).Skip(trackings.Count - 2).First();

            return trackings;
        }

        private IEnumerable<Tracking> GetUserTwoTrackings()
        {
            var trackings = new List<Tracking>();
            var timeStartOne = _date.AddMinutes(-30);
            var timeStartTwo = timeStartOne.AddMinutes(30);

            for (int i = 0; i < 6; i++)
            {
                trackings.Add(CreateTracking(_userIdTwo, _trappingTypeIdOne, timeStartOne.AddMinutes(i * 15)));
                trackings.Add(CreateTracking(_userIdTwo, _trappingTypeIdTwo, timeStartTwo.AddHours(i)));
            }

            _firstUserTwoTracking = trackings.OrderBy(x => x.RecordedOn).First();
            _lastUserTwoTracking = trackings.OrderBy(x => x.RecordedOn).Last();
            _secondToLastUserTwoTracking = trackings.OrderBy(x => x.RecordedOn).Skip(trackings.Count - 2).First();

            return trackings;
        }

        private IEnumerable<Tracking> GetUserThreeTrackings()
        {
            var time = _date.AddHours(1);

            var trackings = new List<Tracking>
            {
                CreateTracking(_userIdThree, _trappingTypeIdOne, time.AddDays(-2)),
                CreateTracking(_userIdThree, _trappingTypeIdTwo, time.AddDays(-1)),
                CreateTracking(_userIdThree, _trappingTypeIdTwo, time)
            };

            _firstUserThreeTracking = trackings.OrderBy(x => x.RecordedOn).First();
            _lastUserThreeTracking = trackings.OrderBy(x => x.RecordedOn).Last();

            return trackings;
        }


        private Tracking CreateTracking(Guid userId, Guid trappingTypeId, DateTimeOffset recordedOn)
        {
            Tracking tracking = new TrackingBuilder()
                .WithUserId(userId)
                .WithTrappingTypeId(trappingTypeId)
                .WithRecordedOn(recordedOn)
                .WithLocation(_longitude, _latitude);

            return tracking;
        }
    }
}
