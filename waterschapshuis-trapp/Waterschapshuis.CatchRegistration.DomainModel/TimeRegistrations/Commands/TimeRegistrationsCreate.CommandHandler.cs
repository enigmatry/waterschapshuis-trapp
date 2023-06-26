using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Prepared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    public static partial class TimeRegistrationsCreate
    {
        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly ILogger<CommandHandler> _logger;
            private readonly IRepository<Tracking> _trackingRepository;
            private readonly IRepository<TimeRegistration> _timeRegistrationRepository;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

            private readonly List<TimeRegistration> _timeRegistrations = new List<TimeRegistration>();

            public CommandHandler(
                ILogger<CommandHandler> logger,
                IRepository<Tracking> trackingRepository,
                IRepository<TimeRegistration> timeRegistrationRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _logger = logger;
                _trackingRepository = trackingRepository;
                _timeRegistrationRepository = timeRegistrationRepository;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.TrackingItems.Count == 0)
                    return Unit.Value;

                var orderedTrackingItems = request.TrackingItems.OrderBy(x => x.RecordedOn).ToList();

                var firstTracking = orderedTrackingItems.First();
                var previousTracking = TryFindPreviousUserTrackingForTheDay(
                    firstTracking.TrackingId,
                    firstTracking.UserId,
                    firstTracking.RecordedOn);
                var previousItem = previousTracking == null
                    ? null
                    : TrackingItem.Create(previousTracking, previousTracking.CreatedById);
                var allTrackingItems = previousItem == null!
                    ? orderedTrackingItems
                    : orderedTrackingItems.Append(previousItem);

                var subAreaHourSquares = await GetSubAreaHourSquaresAsync(allTrackingItems, cancellationToken);

                foreach (var currentItem in orderedTrackingItems)
                {
                    var hours = 0.0;
                    var currentSubAreaHourSquare = TryFindSubAreaHourSquareForPoint(subAreaHourSquares, currentItem.Location);

                    if (currentSubAreaHourSquare == null)
                    {
                        continue;
                    }

                    if (previousItem != null!)
                    {
                        var previousSubAreaHourSquare = TryFindSubAreaHourSquareForPoint(subAreaHourSquares, previousItem.Location);

                        if (previousSubAreaHourSquare == null)
                        {
                            //previousItem = currentItem;
                            continue;
                        }

                        if (previousItem.SessionId == currentItem.SessionId &&
                            previousItem.TrappingTypeId == currentItem.TrappingTypeId &&
                            previousSubAreaHourSquare.Id == currentSubAreaHourSquare.Id)
                        {
                            hours = currentItem.RecordedOn.Subtract(previousItem.RecordedOn).TotalHours;
                        }
                    }

                    if (!hours.Equals(0.0) && CanBeUpdated(currentItem.UserId, currentItem.RecordedOn))
                    {
                        CreateOrUpdateTimeRegistration(previousItem, currentItem, currentSubAreaHourSquare.Id, hours);
                    }

                    previousItem = currentItem;
                }

                return Unit.Value;
            }

            private async Task<List<SubAreaHourSquare>> GetSubAreaHourSquaresAsync(
               IEnumerable<TrackingItem> trackings,
               CancellationToken cancellationToken) =>
               await _currentVersionRegionalLayoutService
                    .QuerySubAreaHourSquares()
                    .QueryByMultiplePoint(trackings.Select(e => e.Location))
                    .ToListAsync(cancellationToken);

            private Tracking? TryFindPreviousUserTrackingForTheDay(Guid trackingId, Guid userId, DateTimeOffset date) =>
                _trackingRepository
                    .QueryAll()
                    .PreviousTrackingForTheDayEntity(trackingId, userId, date);

            private SubAreaHourSquare? TryFindSubAreaHourSquareForPoint(IEnumerable<SubAreaHourSquare> subAreaHourSquares, Geometry location)
            {
                var result = subAreaHourSquares
                    .Where(x => PreparedGeometryFactory.Prepare(x.Geometry).Contains(location)).ToList();
                var loc = new { Longitude = location.Coordinate.X, Latitude = location.Coordinate.Y };

                if (result.Count == 0)
                {
                    _logger.LogWarning("Nothing found for {@Location}", loc);
                }

                if (result.Count > 1)
                {
                    var overlappingSubAreaHourSquareIds = result.Select(i => i.Id);
                    _logger.LogWarning("{@OverlappingSubAreaHourSquareIds} found for {@Location}",
                        String.Join(",", overlappingSubAreaHourSquareIds), loc);
                }

                return result.FirstOrDefault();
            }

            private void CreateOrUpdateTimeRegistration(
                TrackingItem? previousTracking,
                TrackingItem currentTracking,
                Guid subAreaHourSquareId,
                double hours)
            {
                var existing = TryFindExisitngTimeRegistration(
                    currentTracking.UserId,
                    subAreaHourSquareId,
                    currentTracking.TrappingTypeId,
                    currentTracking.RecordedOn);

                var logMessage =
                    $"Previous Tracking Id {previousTracking?.TrackingId.ToString() ?? "null"}, " +
                    $" time {previousTracking?.RecordedOn.ToString("yyyy-MM-dd HH:mm:ss") ?? "null"}; " +
                    $"Current Tracking Id {currentTracking.TrackingId}, " +
                    $" time {currentTracking?.RecordedOn.ToString("yyyy-MM-dd HH:mm:ss") ?? "null"}; " +
                    $"Existing TimeRegistration Id {existing?.Id.ToString() ?? "null"}, " +
                    $" hours {existing?.Hours.ToString(CultureInfo.InvariantCulture) ?? "null"}; ";

                if (existing == null)
                {
                    var timeRegistration = TimeRegistration.Create(
                        currentTracking!.UserId,
                        subAreaHourSquareId,
                        currentTracking.TrappingTypeId,
                        currentTracking.RecordedOn,
                        hours,
                        TimeRegistrationStatus.Written,
                        true);

                    _timeRegistrationRepository.Add(timeRegistration);
                    TryAddTimeRegistration(timeRegistration);

                    logMessage += $"Resulting TimeRegistration Id {timeRegistration.Id}, hours {timeRegistration.Hours.ToString(CultureInfo.InvariantCulture)};";
                }
                else
                {
                    existing.IncrementHours(hours);
                    TryAddTimeRegistration(existing);

                    logMessage += $"Resulting TimeRegistration Id {existing.Id}, hours {existing.Hours.ToString(CultureInfo.InvariantCulture)};";
                }

                _logger.LogDebug("TIME-REGISTRATION-FROM-TRACKING; " + logMessage);
            }

            private TimeRegistration? TryFindExisitngTimeRegistration(Guid userId, Guid subAreaHourSquareId, Guid trappingTypeId, DateTimeOffset date) =>
                _timeRegistrations
                    .AsQueryable()
                    .ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeId, date)
                ?? _currentVersionRegionalLayoutService
                    .QueryTimeRegistrations()
                    .ExistingTimeRegistrationEntryEntity(userId, subAreaHourSquareId, trappingTypeId, date);

            public bool CanBeUpdated(Guid userId,
                DateTimeOffset date)
            {
                var (startDate, endDate) = date.CurrentDateWeekRange();

                var current =
                    _timeRegistrations
                        .AsQueryable()
                        .ExistingTimeRegistrationByWeek(userId, startDate.BeginningOfDay(), endDate.BeginningOfDay());

                if (!current.Any())
                    current = _currentVersionRegionalLayoutService
                        .QueryTimeRegistrations()
                        .ExistingTimeRegistrationByWeek(userId, startDate.BeginningOfDay(), endDate.BeginningOfDay());

                if (current.Any() && current.AsEnumerable().All(x => !CanStatusBeUpdated(x.Status)))
                       return false;

                return true;
            }

            private void TryAddTimeRegistration(TimeRegistration timeRegistration)
            {
                if (!_timeRegistrations.Contains(timeRegistration))
                {
                    _timeRegistrations.Add(timeRegistration);
                }
            }

            private static bool CanStatusBeUpdated(
                TimeRegistrationStatus status) => status != TimeRegistrationStatus.Completed && status != TimeRegistrationStatus.Closed;
        }
    }
}
