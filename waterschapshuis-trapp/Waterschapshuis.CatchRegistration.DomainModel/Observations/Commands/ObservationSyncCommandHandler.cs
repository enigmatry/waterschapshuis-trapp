using Azure.Storage.Sas;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.BlobStorage;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands
{
    [UsedImplicitly]
    public class ObservationSyncCommandHandler : IRequestHandler<ObservationSync.Command, ObservationSync.Result>
    {
        private readonly IRepository<Observation> _observationRepository;
        private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
        private readonly IBlobStorageClient _blobStorageClient;
        private readonly AzureBlobSettings _azureBlobSettings;
        private readonly ILogger<ObservationSyncCommandHandler> _logger;

        public ObservationSyncCommandHandler(
            IRepository<Observation> observationRepository,
            ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService,
            IBlobStorageClient blobStorageClient,
            AzureBlobSettings azureBlobSettings,
            ILogger<ObservationSyncCommandHandler> logger)
        {
            _observationRepository = observationRepository;
            _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            _blobStorageClient = blobStorageClient;
            _azureBlobSettings = azureBlobSettings;
            _logger = logger;
        }

        public async Task<ObservationSync.Result> Handle(ObservationSync.Command request, CancellationToken cancellationToken)
        {
            var observations = request.Observations
                .OrderBy(o => o.RecordedOn)
                .Select(CreateObservation)
                .ToList();
            var newObservations = observations.Where(IsNew).ToList();
            if (newObservations.Any())
                _observationRepository.AddRange(newObservations);

            var savedItems = observations.Select(x => new ObservationSync.Result.ResultItem { Id = x.Id, IsNew = newObservations.Any(y => y.Id == x.Id) });
            var sasKey = await _blobStorageClient.GetSasKey(new List<AccountSasPermissions> { AccountSasPermissions.Write });

            return ObservationSync.Result.CreateResult(savedItems, sasKey);
        }

        private Observation CreateObservation(ObservationSync.Command.ObservationItem observationItem)
        {
            var subAreaHourSquare = _currentVersionRegionalLayoutService
                .QuerySubAreaHourSquares()
                .FindByLongAndLat(observationItem.Longitude, observationItem.Latitude, _logger)
                    ?? throw new ArgumentException("There is no SubAreaHourSquare defined for specified location data.", nameof(observationItem));

            return Observation.Create(
                observationItem.Id,
                subAreaHourSquare,
                (ObservationType)observationItem.Type,
                observationItem.Remarks,
                observationItem.Longitude,
                observationItem.Latitude,
                observationItem.RecordedOn,
                observationItem.HasPhoto,
                _azureBlobSettings
            );
        }

        private bool IsNew(Observation observation)
        {
            var result = _observationRepository.FindById(observation.Id);
            return result == null;
        }
    }
}
