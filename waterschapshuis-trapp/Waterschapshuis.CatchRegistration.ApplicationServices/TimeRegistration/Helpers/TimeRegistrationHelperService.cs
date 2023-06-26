using System;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration.Helpers
{
    public class TimeRegistrationHelperService : ITimeRegistrationHelperService
    {
        private readonly IRepository<Catch> _catchRepository;
        private readonly IRepository<TimeRegistrationGeneral> _timeRegistrationGeneralRepository;
        private readonly ICurrentUserIdProvider _currentUserIdProvider;
        private readonly IRepository<User> _userRepository;
        private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

        public TimeRegistrationHelperService(
            IRepository<Catch> catchRepository,
            IRepository<TimeRegistrationGeneral> timeRegistrationGeneralRepository,
            ICurrentUserIdProvider currentUserIdProvider,
            IRepository<User> userRepository,
            ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
        {
            _catchRepository = catchRepository;
            _timeRegistrationGeneralRepository = timeRegistrationGeneralRepository;
            _currentUserIdProvider = currentUserIdProvider;
            _userRepository = userRepository;
            _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
        }
        public bool WeekClosedOrApprovedForDate(DateTimeOffset recordedOndate)
        {
            var userId =
                _currentUserIdProvider.FindUserId(_userRepository.QueryAll()) ??
                throw new InvalidOperationException("Cannot find user id.");
            (DateTimeOffset startDate, DateTimeOffset endDate) = recordedOndate.CurrentDateWeekRange();

            var timeRegItems = _currentVersionRegionalLayoutService
                .QueryTimeRegistrations()
                .AnyNotInStatusWritten(userId, startDate, endDate);

            var timeRegGenItems = _timeRegistrationGeneralRepository
                .QueryAll()
                .AnyNotInStatusWritten(userId, startDate, endDate);

            var catches = _catchRepository
                .QueryAll()
                .AnyNotInStatusWritten(userId, startDate, endDate);

            return !timeRegItems && !timeRegGenItems && !catches;
        }
    }
}
