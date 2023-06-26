using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.ApplicationServices.Common;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Traps
{
    public static partial class GetMySummary
    {
        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly ICurrentUserIdProvider _currentUserIdProvider;
            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Trap> _trapRepository;
            private readonly IRepository<Catch> _catchRepository;
            private readonly ITimeProvider _timeProvider;
            private readonly IMapper _mapper;

            public RequestHandler(
                ICurrentUserIdProvider currentUserIdProvider,
                IRepository<User> userRepository,
                IRepository<Trap> trapRepository,
                IRepository<Catch> catchRepository,
                ITimeProvider timeProvider,
                IMapper mapper)
            {
                _currentUserIdProvider = currentUserIdProvider;
                _userRepository = userRepository;
                _trapRepository = trapRepository;
                _catchRepository = catchRepository;
                _timeProvider = timeProvider;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId =
                    _currentUserIdProvider.FindUserId(_userRepository.QueryAll()) ??
                    throw new InvalidOperationException("Cannot find user id.");

                var dateFrom = _timeProvider.Now.MondayDateInWeekOfDate();

                var result = new Response
                {
                    CatchesThisWeek =
                        await _catchRepository
                            .QueryAllAsNoTracking()
                            .QueryByUserCreatedId(userId)
                            .QueryFromDateRecorded(dateFrom)
                            .QueryByIsByCatch(false)
                            .SumAsync(x => x.Number, cancellationToken)
                };

                var outstandingTrapsQuery =
                    _trapRepository
                        .QueryAllAsNoTracking()
                        .BuildInclude()
                        .QueryByUserCreatedId(userId)
                        .QueryNotRemoved();

                if (request.IncludeDetails)
                {
                    result.TrapDetails =
                        await outstandingTrapsQuery
                            .ToListMappedAsync<Trap, TrapInfo>(_mapper, cancellationToken);

                    result.OutstandingTraps = result.TrapDetails.Count();
                }
                else
                {
                    result.OutstandingTraps =
                        await outstandingTrapsQuery
                            .CountAsync(cancellationToken);
                }

                return result;
            }
        }
    }
}
