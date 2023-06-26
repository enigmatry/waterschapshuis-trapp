using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.TimeRegistration
{
    public partial class GetTimeRegistrationsOfWeek
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly ICurrentUserIdProvider _currentUserIdProvider;
            private readonly IRepository<TimeRegistrationGeneral> _timeRegistrationGeneralRepository;
            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Catch> _catchRepository;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly IMapper _mapper;

            public RequestHandler(
                ICurrentUserIdProvider currentUserIdProvider,
                IRepository<TimeRegistrationGeneral> timeRegistrationGeneralRepository,
                IRepository<User> userRepository,
                IMapper mapper,
                IRepository<Catch> catchRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _currentUserIdProvider = currentUserIdProvider;
                _timeRegistrationGeneralRepository = timeRegistrationGeneralRepository;
                _userRepository = userRepository;
                _mapper = mapper;
                _catchRepository = catchRepository;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = request.UserId ?? _currentUserIdProvider.FindUserId(_userRepository.QueryAll()) ??
                    throw new InvalidOperationException("Cannot find user id.");

                var queryableTimeRegistrations = _currentVersionRegionalLayoutService
                   .QueryTimeRegistrations()
                   .QueryByUser(userId)
                   .QueryByDateRangeExclusiveEnd(request.StartDate, request.EndDate);


                var queryableCatches = _catchRepository
                    .QueryAll()
                    .QueryByUserCreatedId(userId)
                    .QueryByDateRecordedRangeExclusiveEnd(request.StartDate.Date, request.EndDate.Date);
                  

                if (request.RayonId != null)
                {
                    queryableTimeRegistrations = queryableTimeRegistrations.QueryByRayon(request.RayonId.Value);
                    queryableCatches = queryableCatches.QueryByRayon(request.RayonId.Value);
                }

                var timeRegistrations = await queryableTimeRegistrations
                   .OrderBy(x => x.Date)
                   .ProjectTo<Response.TimeRegistration>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

                var catches = await queryableCatches.OrderBy(x => x.RecordedOn)
                    .ProjectTo<Response.CatchItem>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var generalTimeRegistrations = await _timeRegistrationGeneralRepository
                    .QueryAll()
                    .QueryByUser(userId)
                    .QueryByDateRangeExclusiveEnd(request.StartDate, request.EndDate)
                    .OrderBy(x => x.Date)
                    .ProjectTo<Response.TimeRegistrationGeneral>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var groupedTimeRegistrations = timeRegistrations.GroupBy(x => x.Date).Select(x=> new
                {
                    Date = x.Key,
                    Items = x.ToList()
                });

                var response = new Response();
                foreach (var timeRegistration in groupedTimeRegistrations)
                {
                    response.DaysOfWeek = response.DaysOfWeek.Append(new Response.TimeRegistrationsOfDate
                    {
                        Date = timeRegistration.Date,
                        TimeRegistrations = timeRegistration.Items
                    });
                }
                response.TimeRegistrationGeneralItems = generalTimeRegistrations;
                response.Catches = catches;
                return response;
            }
        }
    }
}
