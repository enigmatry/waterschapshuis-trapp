using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration
{
    public partial class GetTimeRegistrations
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private const int NoAdditionalHours = 0;
            private readonly ICurrentUserIdProvider _currentUserIdProvider;
            private readonly IRepository<TimeRegistrationGeneral> _timeRegistrationGeneralRepository;
            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Catch> _catchRepository;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly IMapper _mapper;

            public RequestHandler(
                ICurrentUserIdProvider currentUserIdProvider,
                IRepository<User> userRepository,
                IMapper mapper,
                IRepository<TimeRegistrationGeneral> timeRegistrationGeneralRepository,
                IRepository<Catch> catchRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _currentUserIdProvider = currentUserIdProvider;
                _userRepository = userRepository;
                _mapper = mapper;
                _timeRegistrationGeneralRepository = timeRegistrationGeneralRepository;
                _catchRepository = catchRepository;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId =
                    _currentUserIdProvider.FindUserId(_userRepository.QueryAll()) ??
                    throw new InvalidOperationException("Cannot find user id.");

                var (startDate, endDate) = request.Date.CurrentDateWeekRange();

                var timeRegistrations = _currentVersionRegionalLayoutService
                    .QueryTimeRegistrations()
                    .BuildInclude()
                    .QueryByUser(userId)
                    .QueryByDateRangeExclusiveEnd(startDate, endDate)
                    .ToList();

                var timeRegistrationGeneralQuery = _timeRegistrationGeneralRepository
                    .QueryAll()
                    .QueryByUser(userId);

                var addDisabled = request.Date.OlderThanNumberOfWeeks(DomainModel.TimeRegistrations.TimeRegistration.WeeksEditEnabled) ||
                    (timeRegistrations
                    .Any(x => x.Status != TimeRegistrationStatus.Written) ||
                    await timeRegistrationGeneralQuery
                      .QueryByDateRangeExclusiveEnd(startDate, endDate)
                      .AnyAsync(x => x.Status != TimeRegistrationStatus.Written, cancellationToken) ||
                    await _catchRepository
                        .QueryAll()
                        .QueryByUserCreatedId(userId)
                        .QueryByDateRecordedRangeExclusiveEnd(startDate, endDate)
                        .AnyAsync(x => x.Status != CatchStatus.Written, cancellationToken));

                var timeRegistrationsForDate = timeRegistrations.QueryByDate(request.Date).ToList();

                var response = new Response
                {
                    Date = request.Date,
                    CanAddNew = !addDisabled,
                    Items = _mapper.Map<IEnumerable<DomainModel.TimeRegistrations.TimeRegistration>,IEnumerable<Response.Item>>
                        (timeRegistrationsForDate.QueryByOptionalSubAreaHourSquareId(request.SubAreaHourSquareId)),
                    GeneralItems = await timeRegistrationGeneralQuery
                        .QueryByDate(request.Date)
                        .ProjectTo<Response.GeneralItem>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken),
                    TotalTimeOfFilteredOutItems = request.SubAreaHourSquareId.HasValue ? 
                        GetTotalTimeOfFilteredOutItems(timeRegistrationsForDate, request.SubAreaHourSquareId) : NoAdditionalHours
                };

                return response;
            }

            private static int GetTotalTimeOfFilteredOutItems(IEnumerable<DomainModel.TimeRegistrations.TimeRegistration> timeRegistrations, Guid? requestSubAreaHourSquareId)
            {
                var filteredTimeRegistrations = timeRegistrations.NotInSubAreaHourSquare(requestSubAreaHourSquareId).ToList();
                return filteredTimeRegistrations.Sum(x => x.GetHours()) * 60 + filteredTimeRegistrations.Sum(x => x.GetMinutes());
            }
        }
    }
}
