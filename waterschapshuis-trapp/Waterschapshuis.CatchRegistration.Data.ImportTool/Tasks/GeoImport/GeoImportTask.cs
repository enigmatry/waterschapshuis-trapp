using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waterschapshuis.CatchRegistration.Data.ImportTool.Extensions;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks.GeoImport
{
    [UsedImplicitly]
    public sealed class GeoImportTask : PgImportTask
    {
        private readonly Dictionary<long, Organization> _importedOrganizations;
        private readonly Dictionary<long, DomainModel.Areas.Rayon> _importedRayons;
        private readonly Dictionary<long, CatchArea> _importedCatchAreas;
        private readonly Dictionary<long, WaterAuthority> _importedWaterAuthorities;
        private readonly Dictionary<long, HourSquare> _importedHourSquares;
        private readonly Dictionary<long, SubArea> _importedSubAreas;

        public GeoImportTask(
            ILogger<GeoImportTask> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, configuration, serviceScopeFactory)
        {
            _importedHourSquares = new Dictionary<long, HourSquare>();
            _importedOrganizations = new Dictionary<long, Organization>();
            _importedRayons = new Dictionary<long, DomainModel.Areas.Rayon>();
            _importedCatchAreas = new Dictionary<long, CatchArea>();
            _importedWaterAuthorities = new Dictionary<long, WaterAuthority>();
            _importedSubAreas = new Dictionary<long, SubArea>();
        }

        protected override string ConnectionString => Configuration.GetConnectionString("PostgresV2");

        protected override async Task ExecutePgImportsAsync(CancellationToken cancellationToken)
        {
            await ExecutePgImportAsync<Organisatie, Organization>(
                MapOrganization,
                "select id, naam as name, naamkort as shortname, geom as geometry from organisatie order by id",
                cancellationToken);

            await ExecutePgImportAsync<Rayon, DomainModel.Areas.Rayon>(
                MapRayon,
                "select id, naam as name, fk_organisatie as organizationid, geom as geometry from rayon order by id",
                cancellationToken);

            await ExecutePgImportAsync<Vanggebied, CatchArea>(
                MapCatchArea,
                "select id, naam as name, fk_rayon as rayonid, geom as geometry from vanggebied order by id",
                cancellationToken);

            await ExecutePgImportAsync<Waterschap, WaterAuthority>(
                MapWaterAuthority,
                "select id, naam as name, code_uvw as codeuvw, geom as geometry from waterschap order by id",
                cancellationToken);

            await ExecutePgImportAsync<Deelgebied, SubArea>(
                MapSubArea,
                @"select id, naam as name, fk_waterschapid as waterauthorityid, fk_vanggebiedid as catchareaid, 
                geom as geometry from deelgebied order by id",
                cancellationToken);

            await ExecutePgImportAsync<Uurhok, HourSquare>(
                MapHourSquare,
                "select id, uurhok as name, geom as geometry, type from uurhok order by uurhok",
                cancellationToken);

            await ExecutePgImportAsync<DeelgebiedUurhok, SubAreaHourSquare>(
                MapSubAreaHourSquare,
                @"select du.id, d.id as subareaid, u.id as hoursquareid, 
                du.kmwatergang as kmwaterway, du.pcgreppel as percentageditch, du.greppel as ditch, 
                nattesloot as wetditch, du.geom as geometry from deelgebied_uurhok as du 
                inner join deelgebied as d on du.deelgebied = d.naam 
                inner join uurhok as u on du.uurhok = u.uurhok order by du.id",
                cancellationToken);
        }

        private Task<Organization> MapOrganization(Organisatie model, CancellationToken cancellationToken)
        {
            var organization = Organization.Create(
                model.Name,
                model.ShortName,
                model.Geometry.GetValidatedBuffer());

            _importedOrganizations.Add(model.Id, organization);

            return Task.FromResult(organization);
        }

        private Task<DomainModel.Areas.Rayon> MapRayon(Rayon model, CancellationToken cancellationToken)
        {
            var rayon = DomainModel.Areas.Rayon.Create(
                model.Name,
                _importedOrganizations[model.OrganizationId],
                model.Geometry.GetValidatedBuffer());

            _importedRayons.Add(model.Id, rayon);
            return Task.FromResult(rayon);
        }

        private Task<CatchArea> MapCatchArea(Vanggebied model, CancellationToken cancellationToken)
        {
            var area = CatchArea.Create(
                model.Name,
                _importedRayons[model.RayonId],
                model.Geometry.GetValidatedBuffer());

            _importedCatchAreas.Add(model.Id, area);

            return Task.FromResult(area);
        }

        private Task<WaterAuthority> MapWaterAuthority(Waterschap model, CancellationToken cancellationToken)
        {
            var entity = WaterAuthority.Create(
                model.Name,
                model.CodeUvw,
                Guid.Empty,
                model.Geometry.GetValidatedBuffer());

            _importedWaterAuthorities.Add(model.Id, entity);

            return Task.FromResult(entity);
        }

        private Task<SubArea> MapSubArea(Deelgebied model, CancellationToken cancellationToken)
        {
            var subArea = SubArea.Create(
                model.Name,
                _importedCatchAreas[model.CatchAreaId],
                _importedWaterAuthorities[model.WaterAuthorityId],
                model.Geometry.GetValidatedBuffer());

            // hack to create the relation between the WaterAuthority and Organization which is not in current db
            subArea.CatchArea.Rayon.Organization.AddWaterAuthority(subArea.WaterAuthority);

            _importedSubAreas.Add(model.Id, subArea);

            return Task.FromResult(subArea);
        }

        private Task<HourSquare> MapHourSquare(Uurhok model, CancellationToken cancellationToken)
        {
            var result = HourSquare.Create(
                model.Name,
                model.Geometry.GetValidatedBuffer());

            _importedHourSquares.Add(model.Id, result);

            return Task.FromResult(result);
        }

        private Task<SubAreaHourSquare> MapSubAreaHourSquare(DeelgebiedUurhok model, CancellationToken cancellationToken)
        {
            var result = SubAreaHourSquare.Create(
                _importedSubAreas[model.SubAreaId],
                _importedHourSquares[model.HourSquareId],
                (int)model.KmWaterway,
                (short)model.PercentageDitch,
                model.Ditch,
                model.WetDitch,
                model.Geometry.GetValidatedBuffer());

            return Task.FromResult(result);
        }
    }
}
