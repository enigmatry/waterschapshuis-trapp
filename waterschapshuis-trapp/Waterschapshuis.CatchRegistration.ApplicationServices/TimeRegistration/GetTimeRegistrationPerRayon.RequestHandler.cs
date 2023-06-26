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
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration
{
    public partial class GetTimeRegistrationPerRayon
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly ICurrentUserIdProvider _currentUserIdProvider;
            private readonly IRepository<TimeRegistrationGeneral> _timeRegistrationGeneralRepository;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Catch> _catchRepository;
            private readonly IMapper _mapper;

            public RequestHandler(
                ICurrentUserIdProvider currentUserIdProvider,
                IRepository<User> userRepository,
                IMapper mapper,
                IRepository<Catch> catchRepository,
                IRepository<TimeRegistrationGeneral> timeRegistrationGeneralRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _currentUserIdProvider = currentUserIdProvider;
                _userRepository = userRepository;
                _mapper = mapper;
                _catchRepository = catchRepository;
                _timeRegistrationGeneralRepository = timeRegistrationGeneralRepository;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId =
                    _currentUserIdProvider.FindUserId(_userRepository.QueryAll()) ??
                    throw new InvalidOperationException("Cannot find user id.");

                var organizationId = _userRepository.FindById(userId).OrganizationId ??
                    throw new InvalidOperationException("Cannot find users organization id.");

                var rayons = await _currentVersionRegionalLayoutService
                    .QueryRayons()
                    .QueryByOrganization(organizationId)
                    .OrderBy(x=> x.Name)
                   .ToListAsync(cancellationToken);

                var usersInOrganization = await _userRepository
                    .QueryAll()
                    .QueryByOrganizationId(organizationId)
                    .OrderBy(x=> x.Name)
                    .ProjectTo<Response.TimeRegistrationsPerRayon.User>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var timeRegistrations = await _currentVersionRegionalLayoutService
                   .QueryTimeRegistrations()
                   .BuildInclude()
                   .QueryByDateRangeExclusiveEnd(request.StartDate, request.EndDate)
                   .QueryByOrganization(organizationId)
                   .ToListAsync(cancellationToken);

                var timeRegistrationGenerals = await _timeRegistrationGeneralRepository
                    .QueryAll()
                    .BuildInclude()
                    .QueryByDateRangeExclusiveEnd(request.StartDate, request.EndDate)
                    .QueryByOrganization(organizationId)
                    .ToListAsync(cancellationToken);

                var catches = await _catchRepository
                    .QueryAll()
                    .BuildInclude()
                    .QueryByDateRecordedRangeExclusiveEnd(request.StartDate, request.EndDate)
                    .QueryByOrganization(organizationId)
                    .ToListAsync(cancellationToken);

                var response = new Response
                {
                    UsersWithRegisteredTimePerRayon = rayons.Select(
                        rayon => new Response.TimeRegistrationsPerRayon
                        {
                            RayonId = rayon.Id,
                            RayonName = rayon.Name,
                            Users = GetDistinctUsersWithTimeOrCatchRegistration(rayon.Name, timeRegistrations, catches, request.EndDate)
                        }),
                    UsersWithTimeRegistrationGeneralItems = GetDistinctUsersWithTimeRegistrationGeneralItems(timeRegistrationGenerals, timeRegistrations, catches, request.EndDate),
                    UsersInOrganization = usersInOrganization
                };

                return response;
            }

            private IEnumerable<Response.TimeRegistrationsPerRayon.User> GetDistinctUsersWithTimeOrCatchRegistration(
                string rayonName,
                IList<DomainModel.TimeRegistrations.TimeRegistration> timeRegistrations,
                IList<Catch> catches,
                DateTimeOffset endDate)
            {
                var filteredTimeRegistrationsByRayon =
                    timeRegistrations.Where(x => x.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name == rayonName).ToList();

                return filteredTimeRegistrationsByRayon.Select(x => x.User)
                    .Union(catches.Where(x => x.Trap.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name == rayonName).Select(x => x.CreatedBy))
                    .Select(user => new Response.TimeRegistrationsPerRayon.User
                    {
                        Id = user.Id,
                        Name = OlderThanFiveYears(endDate) ? User.AnonymizedName : user.Name,
                        WeekCompleted = AllItemsInStatusCompleted(filteredTimeRegistrationsByRayon, catches, user),
                        WeekActive = AllItemsInStatusWritten(timeRegistrations, catches, user)
                    }).OrderBy(x=> x.Name);
            }

            private IEnumerable<Response.TimeRegistrationsPerRayon.User> GetDistinctUsersWithTimeRegistrationGeneralItems(
                IList<TimeRegistrationGeneral> timeRegistrationGeneralItems,
                IList<DomainModel.TimeRegistrations.TimeRegistration> timeRegistrations,
                IList<Catch> catches,
                DateTimeOffset endDate)
            {
                return timeRegistrationGeneralItems
                    .Select(x=> x.User)
                    .Distinct()
                    .Select(user => new Response.TimeRegistrationsPerRayon.User
                    {
                        Id = user.Id,
                        Name = OlderThanFiveYears(endDate) ? User.AnonymizedName : user.Name,
                        WeekCompleted = AllGeneralItemsInStatusCompleted(timeRegistrationGeneralItems, user),
                        WeekActive = AllGeneralItemsInStatusWritten(timeRegistrationGeneralItems, user)
                    })
                    .Where(u => 
                        timeRegistrations.All(tr => tr.UserId != u.Id) && catches.All(c => c.CreatedById != u.Id))
                    .OrderBy(x => x.Name);
            }

            private static bool AllItemsInStatusWritten(
                IList<DomainModel.TimeRegistrations.TimeRegistration> timeRegistrations,
                IList<Catch> catches,
                User user)
            {
                return timeRegistrations.Where(t => t.UserId == user.Id).All(x => x.Status == TimeRegistrationStatus.Written)
                       && catches.Where(c => c.CreatedById == user.Id).All(x => x.Status == CatchStatus.Written);
            }

            private static bool AllItemsInStatusCompleted(
                IEnumerable<DomainModel.TimeRegistrations.TimeRegistration> timeRegistrations,
                IEnumerable<Catch> catches,
                User user)
            {
                return timeRegistrations.Where(t => t.UserId == user.Id).All(x => x.Status == TimeRegistrationStatus.Completed)
                       && catches.Where(c => c.CreatedById == user.Id).All(x => x.Status == CatchStatus.Completed);
            }

            private static bool AllGeneralItemsInStatusWritten(
                IList<TimeRegistrationGeneral> timeRegistrationGeneralItems,
                User user)
            {
                return timeRegistrationGeneralItems.Where(t => t.UserId == user.Id)
                        .All(x => x.Status == TimeRegistrationStatus.Written);
            }

            private static bool AllGeneralItemsInStatusCompleted(
                IEnumerable<TimeRegistrationGeneral> timeRegistrationGeneralItems,
                User user)
            {
                return timeRegistrationGeneralItems.Where(t => t.UserId == user.Id)
                    .All(x => x.Status == TimeRegistrationStatus.Completed);
            }


            private static bool OlderThanFiveYears(DateTimeOffset date)
            {
                var today = DateTimeOffset.Now;
                return date.AddYears(5).Date <= today.Date;
            }
        }
    }
}
