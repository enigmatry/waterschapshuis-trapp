using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts.Commands
{
    public static partial class VersionRegionalLayoutCreate
    {
        public class CreateVersionRegionalLayoutRequestHandler : IRequestHandler<Command, Result>
        {
            private readonly Result _result = Result.Create(true);
            private readonly WKTReader _wktReader = new WKTReader()
            {
                Factory = GeometryUtil.Factory,
                DefaultSRID = GeometryUtil.Factory.SRID
            };
            private VersionRegionalLayout _currVersionRegionalLayout = null!;
            private VersionRegionalLayoutImport _versionRegionalLayoutImport = null!;

            private readonly IMediator _mediator;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRepository<Trap> _trapRepository;
            private readonly IRepository<HourSquare> _hourSquareRepository;
            private readonly IRepository<Observation> _observationRepository;
            private readonly IRepository<TimeRegistration> _timeRegistrationRepository;
            private readonly IRepository<VersionRegionalLayout> _versionRegionalLayoutRepository;
            private readonly IRepository<VersionRegionalLayoutImport> _versionRegionalLayoutImportRepository;
            private readonly ICurrentUserProvider _currentUserProvider;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly ILogger<CreateVersionRegionalLayoutRequestHandler> _logger;

            public CreateVersionRegionalLayoutRequestHandler(
                IMediator mediator,
                IUnitOfWork unitOfWork,
                IRepository<Trap> trapRepository,
                IRepository<HourSquare> hourSquareRepository,
                IRepository<Observation> observationRepository,
                IRepository<TimeRegistration> timeRegistrationRepository,
                IRepository<VersionRegionalLayout> versionRegionalLayoutRepository,
                IRepository<VersionRegionalLayoutImport> versionRegionalLayoutImportRepository,
                ICurrentUserProvider currentUserProvider,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService,
                ILogger<CreateVersionRegionalLayoutRequestHandler> logger)
            {
                _mediator = mediator;
                _unitOfWork = unitOfWork;
                _trapRepository = trapRepository;
                _hourSquareRepository = hourSquareRepository;
                _observationRepository = observationRepository;
                _timeRegistrationRepository = timeRegistrationRepository;
                _versionRegionalLayoutRepository = versionRegionalLayoutRepository;
                _versionRegionalLayoutImportRepository = versionRegionalLayoutImportRepository;
                _currentUserProvider = currentUserProvider;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
                _logger = logger;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await TryStartVersionRegionalLayoutImport(request, cancellationToken);

                    if (_result.Succeed)
                        LogInfo($"Next VersionRegionalLayout ({request.Name}) import process started by {_versionRegionalLayoutImport.StartedBy} ...");

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    if (!_result.Succeed)
                        return _result;
                    
                    await CreateNextVersion(request, cancellationToken);

                    if (_result.Succeed)
                        _logger.LogInformation("Next VersionRegionalLayout saved");
                        //await _unitOfWork.SaveChangesAsync(cancellationToken);
                    else
                        await TryFailVersionRegionalLayoutImport(cancellationToken);
                }
                catch
                {
                    await TryFailVersionRegionalLayoutImport(cancellationToken);
                    throw;
                }

                return _result;
            }


            #region Helpers
            private async Task TryStartVersionRegionalLayoutImport(Command request, CancellationToken cancellationToken)
            {
                _versionRegionalLayoutImport = await _versionRegionalLayoutImportRepository
                    .QueryAll()
                    .SingleOrDefaultAsync(cancellationToken);

                if (_versionRegionalLayoutImport == null)
                {
                    _versionRegionalLayoutImport = VersionRegionalLayoutImport.Create(_currentUserProvider.Name, request.Name);
                    _versionRegionalLayoutImportRepository.Add(_versionRegionalLayoutImport);
                }
                else if (_versionRegionalLayoutImport.State == VersionRegionalLayoutImportState.Started)
                {
                    InvalidateResult("Next version import process already in progress");
                    return;
                }

                _versionRegionalLayoutImport.Start(_currentUserProvider.Name, request.Name);
            }

            private async Task CreateNextVersion(Command request, CancellationToken cancellationToken)
            {
                _currVersionRegionalLayout = _currentVersionRegionalLayoutService.Current;
                LogInfo($"Current topology configuration ({_currVersionRegionalLayout.GetNameAndDate()}) loaded");

                // 1. Create next version WaterAuthorities/Rayons/CatchAreas:
                LogInfo("Creating next version WaterAuthorities/Rayons/CatchAreas ...");
                var nextVersionWaterAuthorities = await CreateWaterAuthorities(cancellationToken);
                var nextVersionRayons = await CreateRayons(cancellationToken);
                var nextVersionCatchAreas = CreateCatchAreas(request, nextVersionRayons);
                if (_result.Failed)
                    return;
                LogInfo(
                    $"{nextVersionWaterAuthorities.Count} WaterAuthorities, " +
                    $"{nextVersionRayons.Count} Rayons, " +
                    $"{nextVersionCatchAreas.Count} CatchAreas created for next version"
                );

                // 2. Create next version SubAreas
                LogInfo("Creating next version SubAreas ...");
                var nextVersionSubAreas = new List<SubArea>();
                request.SubAreaCsvRecords
                    .Select(record => TryCreateSubArea(record, nextVersionWaterAuthorities, nextVersionCatchAreas)).ToList()
                    .ForEach(subArea =>
                    {
                        if (subArea != null)
                            nextVersionSubAreas.Add(subArea);
                    });
                if (_result.Failed)
                    return;
                LogInfo($"{nextVersionSubAreas.Count} next version SubAreas created");

                // 3. Create next version SubAreaHourSquares:
                LogInfo("Creating next version SubAreaHourSquares ...");
                var hourSquares = await _hourSquareRepository.QueryAll().AsNoTracking().ToListAsync(cancellationToken);
                var nextVersionSubAreaHourSquares = new List<SubAreaHourSquare>();
                hourSquares.ForEach(hourSquare =>
                {
                    nextVersionSubAreas
                        .Where(subArea => subArea.Geometry.Intersects(hourSquare.Geometry))
                        .Select(subArea => TryCreateSubAreaHourSquare(subArea, hourSquare)).ToList()
                        .ForEach(newSubAreaHourSquare =>
                        {
                            if (newSubAreaHourSquare != null)
                                nextVersionSubAreaHourSquares.Add(newSubAreaHourSquare);
                        });
                });
                if (_result.Failed)
                    return;
                LogInfo($"{nextVersionSubAreaHourSquares.Count} next version SubAreaHourSquares created");

                // 4. Create next VersionRegionalLayout:
                var nextVersionRegionalLayout = VersionRegionalLayout.Create(
                    request.Name,
                    DateTimeOffset.Now,
                    nextVersionSubAreaHourSquares);

                // 5. Recalculate next version WaterAuthorities/Rayons/CatchAreas Geometries from next version SubAreaHourSquares:
                LogInfo("Recalculating next version WaterAuthorities/Rayons/CatchAreas geometries ...");
                TryRecalculateAreaGeomteries(nextVersionRegionalLayout);
                if (_result.Failed)
                    return;
                LogInfo($"Next version WaterAuthorities/Rayons/CatchAreas geometries recalculated");

                // 6. Create next version TimeRegistrations:
                var newxtVersionTRs = await AttachTimeRegistrationsToSubAreaHourSquares(
                    nextVersionSubAreaHourSquares,
                    cancellationToken);
                if (_result.Failed)
                    return;

                // 7. Update Traps/Observations with next version SubAreaHourSquares:
                LogInfo("Updating Traps/Observations with next version SubAreaHourSquares ...");
                var trapsCount = await AttachEntitiesToSubAreaHourSquares(nextVersionSubAreaHourSquares, _trapRepository, cancellationToken);
                var observationsCount = await AttachEntitiesToSubAreaHourSquares(nextVersionSubAreaHourSquares, _observationRepository, cancellationToken);
                if (_result.Failed)
                    return;
                LogInfo($"{trapsCount} Traps & {observationsCount} Observations were updated with next version SubAreaHourSquares");

                // 8. Persist next VersionRegionalLayout & TimeRegistrations:
                SaveNextVersion(nextVersionRegionalLayout, trapsCount, observationsCount, newxtVersionTRs);
            }

            private async Task<List<WaterAuthority>> CreateWaterAuthorities(CancellationToken cancellationToken)
            {
                var waterAuthorities = await _currentVersionRegionalLayoutService
                    .QueryWaterAuthoritiesNoTracking().ToListAsync(cancellationToken);

                LogInfo($"{waterAuthorities.Count} WaterAuthorities found in current version");

                return waterAuthorities
                    .Select(x => WaterAuthority.Create(x.Name, x.CodeUvw, x.OrganizationId, Polygon.Empty))
                    .ToList();
            }

            private async Task<List<Rayon>> CreateRayons(CancellationToken cancellationToken)
            {
                var rayons = await _currentVersionRegionalLayoutService
                    .QueryRayonsNoTracking().ToListAsync(cancellationToken);

                LogInfo($"{rayons.Count} Rayons found in current version");

                return rayons
                    .Select(x => Rayon.Create(x.Name, x.OrganizationId, Polygon.Empty))
                    .ToList();
            }

            private List<CatchArea> CreateCatchAreas(Command request, List<Rayon> rayons)
            {
                var catchAreas = new List<CatchArea>();
                var rayonGroups = request.SubAreaCsvRecords.GroupBy(x => x.RayonName);

                LogInfo($"{_currentVersionRegionalLayoutService.QueryCatchAreas().Count()} CatchAreas found in current version");

                foreach (var rayonGroup in rayonGroups)
                {
                    var rayon = rayons.Single(x => x.Name == rayonGroup.Key);
                    var rayonCatcAreaNames = rayonGroup.Select(x => x.CatchAreaName).Distinct();

                    foreach (var catchAreaName in rayonCatcAreaNames)
                    {
                        var nextVersionCatchArea = CatchArea.Create(catchAreaName, rayon, Polygon.Empty);
                        rayon.AddCatchArea(nextVersionCatchArea);
                        catchAreas.Add(nextVersionCatchArea);
                    }
                }

                return catchAreas;
            }

            private SubArea? TryCreateSubArea(SubAreaCsvRecord record, List<WaterAuthority> nextVersionWAs, List<CatchArea> nextVersionCAs)
            {
                var geometry = TryParseWktToGeometry(record);
                return geometry == null
                    ? null
                    : SubArea.Create(
                        record.Name,
                        nextVersionCAs.Single(x => x.Name == record.CatchAreaName),
                        nextVersionWAs.Single(x => x.Name == record.WaterAuthorityName),
                        geometry
                    );
            }

            private Geometry? TryParseWktToGeometry(SubAreaCsvRecord record)
            {
                try
                {
                    var geometry = _wktReader.Read(record.GeometryAsWKT);

                    if (!geometry.IsValidPolygonOrMultiPolygon())
                    {
                        InvalidateResult($"SubArea '{record.Name}' WKT geometry must be of type Polygon or MultiPolygon");
                        return null;
                    }

                    return geometry;
                }
                catch (ParseException)
                {
                    InvalidateResult($"SubArea '{record.Name}' WKT geometry is not in valid format");
                    return null;
                }
            }

            private SubAreaHourSquare? TryCreateSubAreaHourSquare(SubArea nextVersionSA, HourSquare hourSquare)
            {
                var geometry = TryFindIntersection(nextVersionSA, hourSquare);
                return geometry == null
                    ? null
                    : SubAreaHourSquare.Create(
                        nextVersionSA,
                        hourSquare.Id,
                        geometry
                    );
            }

            private Geometry? TryFindIntersection(SubArea nextVersionSA, HourSquare hourSquare)
            {
                var nextVersionSAHSName = $"{nextVersionSA.Name}/{hourSquare.Name}";
                Geometry? intersection = nextVersionSA.Geometry.Intersection(hourSquare.Geometry);

                if (intersection == null)
                    return null;

                intersection = intersection.IsGeometryCollection()
                    ? ((GeometryCollection)intersection).TryExtractPolygonOrMultiPolygon()
                    : intersection;

                if (intersection == null)
                {
                    InvalidateResult($"'{nextVersionSAHSName}' intersection geometry is not valid polygon or multi-polygon");
                    return null;
                }

                return intersection.IsValidPolygonOrMultiPolygon()
                    ? intersection
                    : null; // ignore all other geometries
            }

            private void TryRecalculateAreaGeomteries(VersionRegionalLayout nextVersionRegionalLayout)
            {
                nextVersionRegionalLayout
                    .GetWaterAuthorities()
                    .ForEach(nextVersionWA =>
                    {
                        var nextVersionSAGeometries = nextVersionRegionalLayout.GetSubAreas()
                            .Where(x => x.WaterAuthorityId == nextVersionWA.Id)
                            .Select(x => x.Geometry).ToList();
                        TryUpdateAreaGeometry(nextVersionWA, nextVersionSAGeometries);
                    });
                nextVersionRegionalLayout
                    .GetCatchAreas()
                    .ForEach(nextVersionCA =>
                    {
                        var nextVersionSAGeometries = nextVersionRegionalLayout.GetSubAreas()
                            .Where(x => x.CatchAreaId == nextVersionCA.Id)
                            .Select(x => x.Geometry).ToList();
                        TryUpdateAreaGeometry(nextVersionCA, nextVersionSAGeometries);
                    });
                nextVersionRegionalLayout
                    .GetRayons()
                    .ForEach(nextVersionR =>
                    {
                        var nextVersionCAGeometries = nextVersionRegionalLayout.GetCatchAreas()
                            .Where(x => x.RayonId == nextVersionR.Id)
                            .Select(x => x.Geometry).ToList();
                        TryUpdateAreaGeometry(nextVersionR, nextVersionCAGeometries);
                    });
            }

            private void TryUpdateAreaGeometry<TArea>(TArea areaEntity, List<Geometry> childGeometries)
                where TArea : IEntityHasName<Guid>, IHasGeometry
            {
                try
                {
                    Geometry? union = null;

                    childGeometries.ForEach(geometry => union = union == null ? geometry : union.Union(geometry));
                    union = (union != null && union.IsGeometryCollection())
                        ? ((GeometryCollection)union).TryExtractPolygonOrMultiPolygon()
                        : union;
                    union = union != null 
                        ? union.TryRemoveSliver() 
                        : union;

                    if (union == null || !union.IsValidPolygonOrMultiPolygon()) // TODO: just Polygon ???
                    {
                        InvalidateResult($"{areaEntity.GetType().Name} '{areaEntity.Name}' recalculated geometry is not valid polygon");
                        return;
                    }

                    areaEntity.UpdateGeometry(union);
                }
                catch (TopologyException)
                {
                    InvalidateResult($"Failed recalculating geometry for {areaEntity.GetType().Name} '{areaEntity.Name}'");
                    return;
                }
            }

            private async Task<int> AttachEntitiesToSubAreaHourSquares<TEntity>(
                List<SubAreaHourSquare> nextVersionSAHSes,
                IRepository<TEntity> repository,
                CancellationToken cancellationToken)
                where TEntity : Entity<Guid>, IHasLocation
            {
                var interectionCounter = 0;
                var entityType = typeof(TEntity).Name;
                var entities = await repository.QueryAll().ToListAsync(cancellationToken);

                LogInfo($"{entities.Count} {entityType}s found in current version");

                entities.ForEach(entity =>
                {
                    var entityLocation = $"{entity.Location.X}, {entity.Location.Y}";
                    var intersections = nextVersionSAHSes
                        .AsQueryable()
                        .QueryByLocation(entity.Location)
                        .ToList();

                    if (intersections.Count >= 1)
                    {
                        var firstIntercection = intersections.First();
                        entity.InsertNewVersionOfSubAreaHourSquareId(firstIntercection.Id);
                        ++interectionCounter;

                        if (intersections.Count > 1)
                        {
                            var SAHSNames = String.Join(",", intersections.Select(x => x.GetSubAreaHourSquareName()));
                            LogWarning($"{entityType} at [{entityLocation}] contained in multiple SubAreaHourSquares: {SAHSNames}");
                        }
                    }
                    else
                    {
                        InvalidateResult($"{entityType} at [{entityLocation}] not contained in any of SubAreHourSquares");
                    }
                });

                return interectionCounter;
            }

            private async Task<List<TimeRegistration>> AttachTimeRegistrationsToSubAreaHourSquares(
                List<SubAreaHourSquare> newVersionSAHSes,
                CancellationToken cancellationToken)
            {
                var result = await _mediator.Send(
                    TimeRegistrationsCreateNewVersion.Command.Create(newVersionSAHSes), 
                    cancellationToken);

                if (!result.Succeed)
                {
                    _result.Succeed = false;
                }

                _versionRegionalLayoutImport.AddMessages(result.ValidationMessages);

                return result.TimeRegistrations;
            }

            private void SaveNextVersion(
                VersionRegionalLayout nextVersionRegionalLayout,
                int updatedTrapsCount,
                int updatedObservationsCount,
                List<TimeRegistration> nextVersionTRs)
            {
                _timeRegistrationRepository.AddRange(nextVersionTRs);
                _versionRegionalLayoutRepository.Add(nextVersionRegionalLayout);

                LogInfo($"Next VersionRegionalLayout ({nextVersionRegionalLayout.GetNameAndDate()}) import process succeed");
                LogInfo($"   - {nextVersionRegionalLayout.SubAreaHourSquares.Count} SubAreaHourSquares imported");
                LogInfo($"   - {nextVersionRegionalLayout.GetSubAreas().Count} SubArea imported");
                LogInfo($"   - {nextVersionRegionalLayout.GetWaterAuthorities().Count} WaterAuthorities imported");
                LogInfo($"   - {nextVersionRegionalLayout.GetCatchAreas().Count} CatchAreas imported");
                LogInfo($"   - {nextVersionRegionalLayout.GetRayons().Count} Rayons imported");
                LogInfo($"   - {nextVersionTRs.Count} TimeRegistrations imported");
                LogInfo($"   - {updatedTrapsCount} Traps updated");
                LogInfo($"   - {updatedObservationsCount} Observations updated");

                _versionRegionalLayoutImport.Finish(VersionRegionalLayoutImportState.Succeed);
            }

            private void InvalidateResult(string errorMessage)
            {
                LogError(errorMessage);
                _result.Succeed = false;
            }

            private void LogInfo(string message)
            {
                _logger.LogInformation(message);
                _versionRegionalLayoutImport.AddInfo(message);
            }

            private void LogWarning(string message)
            {
                _logger.LogWarning(message);
                _versionRegionalLayoutImport.AddWarning(message);
            }

            private void LogError(string message)
            {
                _logger.LogError(message);
                _versionRegionalLayoutImport.AddError(message);
            }

            private async Task TryFailVersionRegionalLayoutImport(CancellationToken cancellationToken)
            {
                if (_versionRegionalLayoutImport != null)
                {
                    _unitOfWork.Detach();
                    var trackied = await _versionRegionalLayoutImportRepository.QueryAll().SingleAsync(cancellationToken);
                    trackied.Update(_versionRegionalLayoutImport);
                    trackied.Finish(VersionRegionalLayoutImportState.Failed);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
            #endregion Helpers
        }
    }
}
