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
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands
{
    [UsedImplicitly]
    public class TrackingLineCreateHandler : IRequestHandler<TrackingLineCreate.Command, TrackingLineCreate.Result>
    {
        private readonly IRepository<Tracking> _trackingRepository;
        private readonly IRepository<TrackingLine> _trackingLineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TrackingLineCreateHandler> _logger;

        public TrackingLineCreateHandler(
            IRepository<Tracking> trackingRepository,
            IRepository<TrackingLine> trackingLineRepository,
            IUnitOfWork unitOfWork,
            ILogger<TrackingLineCreateHandler> logger)
        {
            _trackingRepository = trackingRepository;
            _trackingLineRepository = trackingLineRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TrackingLineCreate.Result> Handle(TrackingLineCreate.Command request, CancellationToken cancellationToken)
        {
            var createdTrackingLinesCounter = 0;
            var sessionIds = await GetTrackingSessionIdsOnDate(request.Date, cancellationToken);

            _logger.LogInformation(
                $"Found {sessionIds.Count} tracking sessions on {request.Date.Date.ToShortDateString()}.");

            foreach (var sessionId in sessionIds)
            {
                var recordedDate = await GetRecordedDateOfTrackings(request.Date, sessionId, cancellationToken);
                var isCreated = await CreateTrackingLine(recordedDate, sessionId, cancellationToken);
                if (isCreated)
                    createdTrackingLinesCounter++;
            }

            _logger.LogInformation(
                $"Created {createdTrackingLinesCounter} tracking lines on {request.Date.Date.ToShortDateString()}.");
            if (sessionIds.Count != createdTrackingLinesCounter)
            {
                _logger.LogInformation(
                $"Failed creation of {sessionIds.Count- createdTrackingLinesCounter} tracking lines on {request.Date.Date.ToShortDateString()}.");
            }

            return TrackingLineCreate.Result.CreateResult();
        }

        private async Task<bool> CreateTrackingLine(DateTimeOffset date,
            Guid sessionId,
            CancellationToken cancellationToken)
        {
            var lineSuccessfullyCreated = false;
            try
            {
                var lineString = await TryCreateLineString(sessionId, cancellationToken);
                if (lineString != null)
                {
                    await PersistTrackingLine(sessionId, date, lineString, cancellationToken);
                    lineSuccessfullyCreated = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                     $"Error during creation tracking line for sessionId {sessionId} on {date} - {ex}");
            }
            return lineSuccessfullyCreated;
        }


        private async Task<List<Guid>> GetTrackingSessionIdsOnDate(DateTimeOffset date, CancellationToken cancellationToken) =>
            await _trackingRepository.QueryAll()
                    .QueryByCreatedOnDay(date)
                    .Select(x => x.SessionId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

        private async Task<DateTimeOffset> GetRecordedDateOfTrackings(
            DateTimeOffset date,
            Guid sessionId,
            CancellationToken cancellationToken) => await _trackingRepository.QueryAll()
                .QueryByCreatedOnDay(date)
                .QueryBySessionId(sessionId)
                .Select(x => x.RecordedOn)
                .Distinct()
                .Take(1)
                .SingleOrDefaultAsync(cancellationToken);

        private async Task<LineString?> TryCreateLineString(
            Guid sessionId,
            CancellationToken cancellationToken)
        {
            var orderedTrackings = await _trackingRepository
                .QueryAll()
                .QueryByIsTrackingMap(true)
                .QueryBySessionId(sessionId)
                .OrderBy(x => x.RecordedOn)
                .ToListAsync(cancellationToken);

            if (orderedTrackings.Count <= 1)
            {
                return null;
            }

            var trackingLineCoordinates = orderedTrackings.Select(x => x.Location.Coordinate).ToArray();
            var lineString = GeometryUtil.Factory.CreateLineString(trackingLineCoordinates);

            if (!lineString.IsValid)
            {
                _logger.LogError($"Invalid tracking line for session {sessionId}!");
                return null;
            }

            return lineString;
        }

        private async Task PersistTrackingLine(
            Guid sessionId,
            DateTimeOffset requestDate,
            LineString lineString,
            CancellationToken cancellationToken)
        {
            var existingTrackingLine = _trackingLineRepository.QueryAll().TryFindBySessionId(sessionId);
            var trackingLineDate = requestDate;

            if (existingTrackingLine != null)
            {
                trackingLineDate = existingTrackingLine.Date;
                _trackingLineRepository.Delete(existingTrackingLine);
            }

            _trackingLineRepository.Add(TrackingLine.Create(trackingLineDate, sessionId, lineString));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
