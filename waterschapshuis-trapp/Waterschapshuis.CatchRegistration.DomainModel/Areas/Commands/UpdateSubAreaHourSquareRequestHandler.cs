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
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;
using Waterschapshuis.CatchRegistration.DomainModel.WaterAreas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Areas.Commands
{
    public partial class UpdateSubareaHoursquare
    {
        [UsedImplicitly]
        public class UpdateSubAreaHourSquareRequestHandler : IRequestHandler<Command, ScheduledJobExecute.Result>
        {
            private static readonly ScheduledJobExecute.Result Response = ScheduledJobExecute.Result.Create();
            private readonly IRepository<WaterLine> _waterLineRepository;
            private readonly IRepository<WaterPlane> _waterPlaneRepository;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;
            private readonly ILogger<UpdateSubAreaHourSquareRequestHandler> _logger;
            private const double FailedCalculationValue = -1;
            private const string SucceedMessage = "Calculation of water areas finished";
            private const string ErrorMessage = "Calculation of water areas failed";

            public UpdateSubAreaHourSquareRequestHandler(
                ILogger<UpdateSubAreaHourSquareRequestHandler> logger,
                IRepository<WaterLine> waterLineRepository,
                IRepository<WaterPlane> waterPlaneRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _logger = logger;
                _waterLineRepository = waterLineRepository;
                _waterPlaneRepository = waterPlaneRepository;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
            }

            public async Task<ScheduledJobExecute.Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    Response.AddInfoMessage(
                        $"Calculation of water areas started for '{_currentVersionRegionalLayoutService.Current.Name}' version ...");
                    _logger.LogInformation(
                        $"Calculation of water areas started for '{_currentVersionRegionalLayoutService.Current.Name}' version ...");

                    var currVersionSAHSes = await _currentVersionRegionalLayoutService
                        .QuerySubAreaHourSquares()
                        .ToListAsync(cancellationToken);

                    if (!currVersionSAHSes.Any())
                    {
                        Response.Invalidate();
                        Response.AddErrorMessage($"SubAreaHourSquares not found in '{_currentVersionRegionalLayoutService.Current.Name}' version");
                        _logger.LogError($"SubAreaHourSquares not found in '{_currentVersionRegionalLayoutService.Current.Name}' version");
                    }

                    foreach (var sahs in currVersionSAHSes)
                    {
                        await ProcessSubAreaHourSquare(sahs, cancellationToken);
                    }

                    Response.AddInfoMessage(Response.Succeed
                        ? SucceedMessage
                        : ErrorMessage);
                    _logger.LogInformation(Response.Succeed
                        ? SucceedMessage
                        : ErrorMessage);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Calculation of water areas failed - {e}");
                    Response.Invalidate();
                    Response.AddErrorMessage(ErrorMessage);
                }
                return Response;
            }

            private async Task ProcessSubAreaHourSquare(SubAreaHourSquare sahs, CancellationToken cancellationToken)
            {
                if (sahs.Geometry.IsValidPolygonOrMultiPolygon())
                {
                    var waterLines = await GetWaterLines(sahs.Id, cancellationToken);
                    var waterPlanes = await GetWaterPlanes(sahs, cancellationToken);
                    _logger.LogInformation($"SAHS Id: {sahs.Id} ---- waterLines = {waterLines.Count}, waterPlanes = {waterPlanes.Count}");

                    try
                    {
                        var (ditch, wetDitch) = WaterAreaCalculator
                            .Create(sahs.Geometry, waterPlanes, waterLines)
                            .Calculate();
                        sahs.Update(ditch, wetDitch);

                    }
                    catch (TopologyException ex)
                    {
                        sahs.Update(FailedCalculationValue, FailedCalculationValue);

                        var subAreaHourSquareName = GetSubareaHourSquareName(sahs.Id);

                        Response.AddErrorMessage(
                            $"Calculation for SubAreaHourSquare: {subAreaHourSquareName} " +
                            $"failed because of error in imported water area topologies.");
                        _logger.LogError(
                            $"Calculation for SubAreaHourSquare: {subAreaHourSquareName} failed." +
                            Environment.NewLine + ex.Message);
                    }
                }
                else
                {
                    _logger.LogError($"SubAreaHourSquare {sahs.Id} is not valid");
                    var subAreaHourSquareName = GetSubareaHourSquareName(sahs.Id);
                    Response.AddErrorMessage(
                        $"Geometry for SubAreaHourSquare: {subAreaHourSquareName} is of type " +
                        $"{sahs.Geometry.GeometryType} that is invalid and it is skipped in calculation.");
                    _logger.LogError(
                        $"Geometry for SubAreaHourSquare: {subAreaHourSquareName} is of type " +
                        $"{sahs.Geometry.GeometryType} that is invalid and it is skipped in calculation.");
                }
            }

            private async Task<List<WaterPlane>> GetWaterPlanes(SubAreaHourSquare sahs, CancellationToken cancellationToken) =>
                 await _waterPlaneRepository
                    //.QueryAll()
                    //.Where(x => 
                    //    (TypeWater.LakesPuddlesAndChannels.Contains(x.Type) || TypeWater.DitchesAndDryDitches.Contains(x.Type))
                    //    && sahs.Geometry.Intersects(x.Geometry))
                    .ExecuteRawSql<WaterPlane>(
                        "SELECT wp.lokaalid, wp.ogr_geometry, wp.typewater " + 
                        "FROM water_planes wp " +
                        "WHERE typewater in ('waterloop','meer, plas') " +
                        "and (select geometry from SubAreaHourSquare where Id = {0}).STIntersects(wp.ogr_geometry) = 1",
                        sahs.Id)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);


            private async Task<List<WaterLine>> GetWaterLines(Guid sahsId, CancellationToken cancellationToken) =>
                 await _waterLineRepository
                    .ExecuteRawSql<WaterLine>("EXECUTE GetWaterLinesForSubareaHourSquare {0}", sahsId)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

            private string GetSubareaHourSquareName(Guid sahsId)
            {
                try
                {
                    var sahsWithRelatedEntities = _currentVersionRegionalLayoutService
                                .QuerySingleSubAreaHourSquareAsNoTracking(sahsId);
                    return sahsWithRelatedEntities?.GetSubAreaHourSquareName() ?? sahsId.ToString();
                } catch (Exception e)
                {
                    _logger.LogError(e.StackTrace);
                    return sahsId.ToString();
                }
            }
        }
    }
}
