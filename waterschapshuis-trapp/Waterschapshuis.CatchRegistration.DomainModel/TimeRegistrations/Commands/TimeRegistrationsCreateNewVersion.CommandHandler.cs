using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    public partial class TimeRegistrationsCreateNewVersion
    {
        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IRepository<Tracking> _trackingRepository;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly ILogger<CommandHandler> _logger;

            private readonly Result _result = Result.Create();
            private readonly List<SubAreaHourSquare> _currVersionSAHSes = new List<SubAreaHourSquare>();
            private readonly List<SubAreaHourSquare> _nextVersionSAHSes = new List<SubAreaHourSquare>();

            public CommandHandler(
                IRepository<Tracking> trackingRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService,
                ILogger<CommandHandler> logger)
            {
                _trackingRepository = trackingRepository;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
                _logger = logger;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await CreateNextVersionTimeRegistrations(request, cancellationToken);

                    if (_result.Succeed)
                    {
                        await LogCurrVersionSummary(cancellationToken);
                        LogNextVersionSummary();
                    }

                    return _result;
                }
                catch
                {
                    InvalidateResult("Failed creating next version TimeRegistrations");
                    throw;
                }
            }


            #region HELPERS
            private async Task CreateNextVersionTimeRegistrations(Command request, CancellationToken cancellationToken)
            {
                LogInfo("Creating next version TimeRegistrations ...");

                _nextVersionSAHSes.AddRange(request.NextVersionSubAreaHourSquares);
                _currVersionSAHSes.AddRange(await GetCurrentVersionSubAreaHourSquares(cancellationToken));

                if (_result.Succeed)
                    await TryCreateTimeRegistrationsOriginatedFromUserInput(cancellationToken);

                if (_result.Succeed)
                    await TryCreateTimeRegistrationsOriginatedFromTrackings(cancellationToken);
            }

            private async Task<List<SubAreaHourSquare>> GetCurrentVersionSubAreaHourSquares(CancellationToken cancellationToken)
            {
                var currVersionSahes = await _currentVersionRegionalLayoutService
                    .QuerySubAreaHourSquaresNoTracking(x => x.SubArea, x => x.HourSquare)
                    .ToListAsync(cancellationToken);
                var invalidSahses = currVersionSahes
                    .Where(x => !x.Geometry.IsValidPolygonOrMultiPolygon())
                    .ToList();

                if (invalidSahses.Any())
                {
                    LogWarning(
                        $"Found {invalidSahses.Count} SubAreaHourSquares with invalid geometry in current version " +
                        $"({String.Join(", ", invalidSahses.Select(x => x.GetSubAreaHourSquareName()))})"
                    );
                }

                return currVersionSahes;
            }

            private async Task TryCreateTimeRegistrationsOriginatedFromUserInput(CancellationToken cancellationToken)
            {
                LogInfo("Processing TimeRegistrations manually created by user ... ");

                var matchingSAHSes = TryFindMatchingSubAreaHourSquares();

                var pageSize = 10000;
                var pageNumber = 0;
                var currVersionTRsPage = new List<TimeRegistration>();

                do
                {

                    currVersionTRsPage = await _currentVersionRegionalLayoutService
                        .QueryTimeRegistrationsNoTracking()
                        .QueryNotCreatedFromTrackings()
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);

                    _logger.LogInformation($"Loaded {pageNumber + 1}. page with {pageSize} TimeRegistrations (manually created) ... ");

                    foreach (var currVersionTR in currVersionTRsPage)
                    {
                        var matchedSAHSes = matchingSAHSes.SingleOrDefault(x => x.Curr.Id == currVersionTR.SubAreaHourSquareId);

                        if (matchedSAHSes != null)
                        {
                            var nextVersionTR = TimeRegistration.Create(currVersionTR, matchedSAHSes.Next);
                            _result.AddOrUpdate(nextVersionTR);
                        }
                    }

                    ++pageNumber;
                }
                while (currVersionTRsPage.Count() == pageSize);
            }

            private List<SubAreaHourSquaresMap> TryFindMatchingSubAreaHourSquares()
            {
                var result = new List<SubAreaHourSquaresMap>();
                var currVersionSAHSPolyLabels = new List<SubAreaHourSquarePolyLabel>();

                _currVersionSAHSes
                    .Where(sahs => sahs.Geometry.IsValidPolygonOrMultiPolygon())
                    .Select(SubAreaHourSquarePolyLabel.Create).ToList()
                    .ForEach(currVersionSAHSPolyLabels.Add);

                foreach (var currVersionSAHSPolyLabel in currVersionSAHSPolyLabels)
                {
                    var matchingNextVersionSAHS = TryFindMatchingSubAreaHourSquare(currVersionSAHSPolyLabel);

                    if (matchingNextVersionSAHS == null)
                        LogWarning(
                            $"Current version SubAreaHourSquare ({currVersionSAHSPolyLabel.SubAreaHourSquare.GetSubAreaHourSquareName()}) " +
                            $"is not matched to any of the next version SubAreaHourSquares"
                        );
                    else
                        result.Add(SubAreaHourSquaresMap.Create(currVersionSAHSPolyLabel.SubAreaHourSquare, matchingNextVersionSAHS));
                }

                return result;
            }

            private SubAreaHourSquare? TryFindMatchingSubAreaHourSquare(SubAreaHourSquarePolyLabel currVersionSAHSPolyLabel) =>
                _nextVersionSAHSes
                    .Where(x => x.HourSquareId == currVersionSAHSPolyLabel.SubAreaHourSquare.HourSquareId)
                    .Select(sahs => new
                    {
                        SubAreaHourSquare = sahs,
                        MatchValue = sahs.GetPolyLabelLocationMatchValue(currVersionSAHSPolyLabel.PolyLabelLocations)
                    })
                    .Where(x => x.MatchValue > 0)
                    .OrderByDescending(x => x.MatchValue)
                    .Select(x => x.SubAreaHourSquare)
                    .FirstOrDefault();

            private async Task TryCreateTimeRegistrationsOriginatedFromTrackings(CancellationToken cancellationToken)
            {
                LogInfo("Processing TimeRegistrations created from tracking ... ");

                var sessionIds = await LoadSessionIds(cancellationToken);

                foreach (var sessionId in sessionIds)
                {
                    var sessionCache = new TrackingSessionCache(
                        _currVersionSAHSes,
                        _nextVersionSAHSes,
                        _currentVersionRegionalLayoutService);
                    var trackings = await LoadTrackings(sessionId, cancellationToken);
                    var prevTracking = trackings.First();
                    var prevTrackingSAHS = sessionCache.TryGetCurrVersionSubAreaHourSquare(prevTracking.Location);

                    _logger.LogInformation($"Loaded {trackings.Count} trackings of {sessionId} session ... ");

                    trackings.RemoveAt(0);

                    foreach (var currTracking in trackings)
                    {
                        var hoursDelta = currTracking.RecordedOn.Subtract(prevTracking.RecordedOn).TotalHours;
                        var currTrackingSAHS = sessionCache.TryGetCurrVersionSubAreaHourSquare(currTracking.Location);

                        // Ignore when 2 adjacent trackings have no time delta or when they belong to different SAHSes
                        if (hoursDelta > 0.0 && currTrackingSAHS != null && currTrackingSAHS.Id == prevTrackingSAHS?.Id)
                        {
                            var currVersionTR = sessionCache.TryGetCurrVersionTimeRegistration(currTracking, currTrackingSAHS);
                            var nextVersionSAHS = sessionCache.TryGetNextVersionSubAreaHourSquare(currTracking.Location);

                            // Ignore when currVersionTR is not originated from tracking or when there is no nextVersionSAHS containing tracking
                            if (currVersionTR != null && currVersionTR.IsCreatedFromTrackings && nextVersionSAHS != null)
                            {
                                var nextVersionTR = TimeRegistration.Create(
                                    currVersionTR.UserId,
                                    nextVersionSAHS.Id,
                                    currVersionTR.TrappingTypeId,
                                    currVersionTR.Date,
                                    hoursDelta,
                                    currVersionTR.Status,
                                    true
                                );
                                _result.AddOrUpdate(nextVersionTR);
                            }
                        }

                        prevTracking = currTracking;
                        prevTrackingSAHS = currTrackingSAHS;
                    }
                }
            }

            private async Task<List<Guid>> LoadSessionIds(CancellationToken cancellationToken) =>
                await _trackingRepository
                    .QueryAll().AsNoTracking()
                    .QueryByTimeWritting()
                    .Select(x => x.SessionId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

            private async Task<List<Tracking>> LoadTrackings(Guid sessionId, CancellationToken cancellationToken) =>
                await _trackingRepository
                    .QueryAll().AsNoTracking()
                    .QueryByTimeWritting()
                    .QueryBySessionId(sessionId)
                    .OrderBy(x => x.RecordedOn)
                    .ToListAsync(cancellationToken);

            private async Task LogCurrVersionSummary(CancellationToken cancellationToken)
            {
                var currVersionTRTrackingCount = await _currentVersionRegionalLayoutService
                    .QueryTimeRegistrationsNoTracking()
                    .QueryCreatedFromTracking()
                    .CountAsync(cancellationToken);
                var currVersionTRManualCount = await _currentVersionRegionalLayoutService
                    .QueryTimeRegistrationsNoTracking()
                    .QueryNotCreatedFromTrackings()
                    .CountAsync(cancellationToken);
                var currVersionTRTrackingHours = await _currentVersionRegionalLayoutService
                    .QueryTimeRegistrationsNoTracking()
                    .QueryCreatedFromTracking()
                    .SumAsync(x => x.Hours, cancellationToken);
                var currVersionTRManualHours = await _currentVersionRegionalLayoutService
                    .QueryTimeRegistrationsNoTracking()
                    .QueryNotCreatedFromTrackings()
                    .SumAsync(x => x.Hours, cancellationToken);

                LogInfo($"Current version '{_currentVersionRegionalLayoutService.Current.Name}' has:");
                LogInfo($"   - {currVersionTRTrackingCount + currVersionTRManualCount} TimeRegistrations " +
                    $"(tracking: {currVersionTRTrackingCount}, manual input: {currVersionTRManualCount})");
                LogInfo($"   - {currVersionTRTrackingHours + currVersionTRManualHours} total logged hours " +
                    $"(tracking: {currVersionTRTrackingHours}, manual input: {currVersionTRManualHours})");
            }

            private void LogNextVersionSummary()
            {
                var nextVersionTRTrackingCount = _result.TimeRegistrations.Count(x => x.IsCreatedFromTrackings);
                var nextVersionTRManualCount = _result.TimeRegistrations.Count(x => !x.IsCreatedFromTrackings);
                var nextVersionTRTrackingHours = _result.TimeRegistrations.Where(x => x.IsCreatedFromTrackings).Sum(x => x.Hours);
                var nextVersionTRManualHours = _result.TimeRegistrations.Where(x => !x.IsCreatedFromTrackings).Sum(x => x.Hours);

                LogInfo("Next version has:");
                LogInfo($"   - {nextVersionTRTrackingCount + nextVersionTRManualCount} TimeRegistrations " +
                    $"(tracking: {nextVersionTRTrackingCount}, manual input: {nextVersionTRManualCount})");
                LogInfo($"   - {nextVersionTRTrackingHours + nextVersionTRManualHours} total logged hours " +
                    $"(tracking: {nextVersionTRTrackingHours}, manual input: {nextVersionTRManualHours})");
            }

            private void LogInfo(string message)
            {
                _logger.LogInformation(message);
                _result.AddInfoMessage(message);
            }

            private void LogWarning(string message)
            {
                _logger.LogWarning(message);
                _result.AddWarnMessage(message);
            }

            private void LogError(string message)
            {
                _logger.LogError(message);
                _result.AddErrorMessage(message);
            }

            private void InvalidateResult(string errorMessage)
            {
                LogError(errorMessage);
                _result.Invalidate();
            }

            private class SubAreaHourSquaresMap
            {
                public SubAreaHourSquare Curr { get; set; } = null!;
                public SubAreaHourSquare Next { get; set; } = null!;

                public static SubAreaHourSquaresMap Create(SubAreaHourSquare curr, SubAreaHourSquare next)
                    => new SubAreaHourSquaresMap { Curr = curr, Next = next };
            }

            private class SubAreaHourSquarePolyLabel
            {
                public SubAreaHourSquare SubAreaHourSquare { get; set; } = null!;
                public Point[] PolyLabelLocations { get; set; } = new Point[0];

                public static SubAreaHourSquarePolyLabel Create(SubAreaHourSquare sahs) 
                    => new SubAreaHourSquarePolyLabel
                    {
                        SubAreaHourSquare = sahs,
                        PolyLabelLocations = sahs.Geometry.IsValidPolygon()
                            ? new[] { PolyLabel.FindPolyLabelPointIn((Polygon)sahs.Geometry) }
                            : PolyLabel.FindPolyLabelPointsIn((MultiPolygon)sahs.Geometry)
                    };
            }

            private class TrackingSessionCache
            {
                private readonly List<SubAreaHourSquare> _currVersionSAHSes;
                private readonly List<SubAreaHourSquare> _nextVersionSAHSes;
                private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

                private List<SubAreaHourSquare> _currVersionSAHSesCache = new List<SubAreaHourSquare>();
                private List<SubAreaHourSquare> _nextVersionSAHSesCache = new List<SubAreaHourSquare>();
                private List<TimeRegistration> _currVersionTRsCache = new List<TimeRegistration>();

                public TrackingSessionCache(
                    List<SubAreaHourSquare> currVersionSAHSes, 
                    List<SubAreaHourSquare> nextVersionSAHSes, 
                    ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
                {
                    _currVersionSAHSes = currVersionSAHSes;
                    _nextVersionSAHSes = nextVersionSAHSes;
                    _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
                }

                public SubAreaHourSquare? TryGetCurrVersionSubAreaHourSquare(Point trackingPoint)
                {
                    var result = TryFind(_currVersionSAHSesCache, trackingPoint)
                        ?? TryFind(_currVersionSAHSes, trackingPoint);
                    if (result != null)
                        _currVersionSAHSesCache.Add(result);
                    return result;
                }

                public SubAreaHourSquare? TryGetNextVersionSubAreaHourSquare(Point trackingPoint)
                {
                    var result = TryFind(_nextVersionSAHSesCache, trackingPoint)
                        ?? TryFind(_nextVersionSAHSes, trackingPoint);
                    if (result != null)
                        _nextVersionSAHSesCache.Add(result);
                    return result;
                }

                public TimeRegistration? TryGetCurrVersionTimeRegistration(Tracking tracking, SubAreaHourSquare sahs)
                {
                    var result = _currVersionTRsCache.AsQueryable()
                        .ExistingTimeRegistrationEntryEntity(
                            tracking.CreatedById,
                            sahs.Id,
                            tracking.TrappingTypeId,
                            tracking.RecordedOn.Date);

                    if (result == null)
                    {
                        result = _currentVersionRegionalLayoutService
                            .QueryTimeRegistrationsNoTracking()
                            .ExistingTimeRegistrationEntryEntity(
                                tracking.CreatedById,
                                sahs.Id,
                                tracking.TrappingTypeId,
                                tracking.RecordedOn.Date);
                        if (result != null)
                            _currVersionTRsCache.Add(result);
                    }
                    return result;
                }

                private SubAreaHourSquare? TryFind(List<SubAreaHourSquare> sahses, Point trackingPoint)
                {
                    var intersections = sahses.AsQueryable().QueryByLocation(trackingPoint).ToList();
                    return intersections.Count >= 1 ? intersections.First() : null;
                }
            }
            #endregion HELPERS
        }
    }
}
