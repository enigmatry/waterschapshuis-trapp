using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    public static partial class TrapCreateOrUpdate
    {
        [UsedImplicitly]
        public class TrapCreateOrUpdateCommandHandler : IRequestHandler<Command, Result>
        {
            private readonly ILogger<TrapCreateOrUpdateCommandHandler> _logger;
            private readonly IRepository<Trap> _trapRepository;
            private readonly IRepository<Province> _provinceRepository;
            private readonly IRepository<CatchType> _catchTypeRepository;
            private readonly ICurrentUserProvider _currentUserProvider;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly ITimeProvider _timeProvider;

            public TrapCreateOrUpdateCommandHandler(
                ILogger<TrapCreateOrUpdateCommandHandler> logger,
                IRepository<Trap> trapRepository,
                IRepository<Province> provinceRepository,
                IRepository<CatchType> catchTypeRepository,
                ICurrentUserProvider currentUserProvider,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService,
                ITimeProvider timeProvider)
            {
                _logger = logger;
                _trapRepository = trapRepository;
                _provinceRepository = provinceRepository;
                _catchTypeRepository = catchTypeRepository;
                _currentUserProvider = currentUserProvider;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
                _timeProvider = timeProvider;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogDebug("TrapCreateOrUpdate started: remark = '{remark}'", request.Remarks);
                Stopwatch stopWatch = Stopwatch.StartNew();

                var subAreaHourSquare = _currentVersionRegionalLayoutService
                    .QuerySubAreaHourSquares()
                    .FindByLongAndLat(request.Longitude, request.Latitude, _logger)
                        ?? throw new InvalidOperationException("There is no SubAreaHourSquare defined for specified location data.");

                TimeSpan ts_sahs = stopWatch.Elapsed;

                var province =
                    _provinceRepository
                        .QueryAll()
                        .QueryContainesCoordinates(request.Longitude, request.Latitude)
                        .SingleOrDefault()
                    ?? throw new InvalidOperationException("There is no Province at these coordinates.");

                TimeSpan ts_province = stopWatch.Elapsed;

                var trapId = request.ShouldCreate
                    ? CreateTrap(request, subAreaHourSquare, province, GetCatchTypes(request))
                    : await UpdateTrapAsync(request, subAreaHourSquare, province, GetCatchTypes(request), cancellationToken);

                TimeSpan ts_create = stopWatch.Elapsed;

                _logger.LogDebug("TrapCreateOrUpdate ended: total time = {Time}s, getting SubAreaHourSquare = {sahs}, getting province = {province}s, creating trap = {trap}",
                                  ts_create.TotalSeconds,
                                  ts_sahs.TotalSeconds,
                                  ts_province.TotalSeconds - ts_sahs.TotalSeconds,
                                  ts_create.TotalSeconds - ts_province.TotalSeconds);

                return Result.CreateResult(trapId);
            }

            private List<CatchType> GetCatchTypes(Command request) =>
                request.Catches.Any()
                    ? _catchTypeRepository
                        .QueryAll()
                        .QueryByIds(request.Catches.Select(x => x.CatchTypeId).Distinct().ToArray()).ToList()
                    : new List<CatchType>();

            private Guid CreateTrap(
                Command request,
                SubAreaHourSquare subAreaHourSquare,
                Province province,
                List<CatchType> catchTypes)
            {
                var trap = Trap.Create(request, subAreaHourSquare, province, catchTypes);
                _trapRepository.Add(trap);
                return trap.Id;
            }

            private async Task<Guid> UpdateTrapAsync(
                Command request,
                SubAreaHourSquare subAreaHourSquare,
                Province province,
                List<CatchType> catchTypes,
                CancellationToken cancellationToken)
            {
                var trap = await _trapRepository
                    .QueryAll().BuildInclude()
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                        ?? throw new InvalidOperationException("Could not find trap to update.");

                if (request.CatchesToRemove.Any())
                {
                    var catchForRemovalIds = request.CatchesToRemove.Select(x => x.Id).ToList();
                    var catchesForRemoval = trap.Catches.Where(x => catchForRemovalIds.Contains(x.Id));

                    if (catchesForRemoval.Any(x => !x.CanBeRemovedFromMobile(_currentUserProvider.UserId, _timeProvider)))
                    {
                        throw new InvalidOperationException("Cannot remove catches created by other user, or not created on current date.");
                    }
                }

                trap.Update(request, subAreaHourSquare, province, catchTypes);
                return trap.Id;
            }
        }
    }
}
