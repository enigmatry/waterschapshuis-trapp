using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.ReportData;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Provinces;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;
using Waterschapshuis.CatchRegistration.Scheduler.Jobs;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests.Jobs
{
    [Category("integration")]
    public class PopulateReportTablesJobFixture : SchedulerIntegrationFixtureBase
    {
        private PopulateReportTablesJob _populateReportTablesJob = null!;
        private IJobExecutionContext _jobContext = null!;
        private DateTimeOffset _catchRecordedDateTime;
        private Catch _catchInDb = null!;
        private TimeRegistration _timeRegistrationInDb = null!;

        [SetUp]
        public void SetUp()
        {
            _populateReportTablesJob = Resolve<PopulateReportTablesJob>();
            _jobContext = A.Fake<IJobExecutionContext>();
            _catchRecordedDateTime = Resolve<ITimeProvider>().Now.AddYears(-1);

            var subAreaHourSquare = CreateSubAreaHourSquareWithHierarchy();
            Province province = new ProvinceBuilder().WithName("TEST_PROVINCE");

            AddAndSaveChanges(province);

            _catchInDb = new CatchBuilder()
                .WithId(new Guid("e491bc27-b305-42f3-91e5-cce2dfd14563"))
                .WithNumberOfCatches(10)
                .WithStatus(CatchStatus.Written)
                .WithRecordedOn(_catchRecordedDateTime)
                .WithCatchType(new CatchTypeBuilder().WithId(CatchType.BeverratMoerOudId));

            Trap trap = new TrapBuilder()
                .WithGeoHierarchy(subAreaHourSquare, province)
                .WithCatches(_catchInDb);

            _timeRegistrationInDb = new TimeRegistrationBuilder()
                .WithId(new Guid("0af501af-3d79-46f7-b18c-3ec90944af31"))
                .WithHours(7.5)
                .WithDate(_catchRecordedDateTime)
                .WithUserId(TestPrincipal.TestUserId)
                .WithTrappingTypeId(TrappingType.BeverratId)
                .WithSubAreaHourSquareId(subAreaHourSquare.Id);

            AddAndSaveChanges(trap, _timeRegistrationInDb);

            _catchInDb = QueryDb<Catch>()
                .Include(x => x.CatchType)
                .Include(x => x.Trap.Province)
                .Include(x => x.Trap.TrapType.TrappingType)
                .Include(x => x.Trap.SubAreaHourSquare.HourSquare)
                .Include(x => x.Trap.SubAreaHourSquare.SubArea.WaterAuthority)
                .Include(x => x.Trap.SubAreaHourSquare.SubArea.CatchArea.Rayon)
                .Single(x => x.Id == _catchInDb.Id);

            _timeRegistrationInDb = QueryDb<TimeRegistration>()
                .Include(x => x.TrappingType)
                .Include(x => x.SubAreaHourSquare.HourSquare)
                .Include(x => x.SubAreaHourSquare.SubArea.WaterAuthority)
                .Include(x => x.SubAreaHourSquare.SubArea.CatchArea.Rayon)
                .Single(x => x.Id == _timeRegistrationInDb.Id);
        }

        [Test]
        public async Task Execute()
        {
            var allCatches = await QueryDb<Catch>().ToListAsync();
            var singleCatch = allCatches.SingleOrDefault();
            singleCatch.Should().NotBeNull();
            singleCatch.Id.Should().Be(_catchInDb.Id);

            var allTimeRegistrations = await QueryDb<TimeRegistration>().ToListAsync();
            var singleTimeRegistration = allTimeRegistrations.SingleOrDefault();
            singleTimeRegistration.Should().NotBeNull();
            singleTimeRegistration.Id.Should().Be(_timeRegistrationInDb.Id);

            var ownReportData = await QueryDb<OwnReportData>().ToListAsync();
            ownReportData.Should().BeEmpty();


            await _populateReportTablesJob.Execute(_jobContext);


            ownReportData = await QueryDb<OwnReportData>().ToListAsync();
            ownReportData.Count.Should().Be(
                allCatches.Count * 2 + allTimeRegistrations.Count * 2 + 2,
                "Double entries because of previous year fields, plus trap and kilometers data");

            // OwnReportData from Catches:
            var catchOwnReportDataOfCurrentYear = ownReportData
                .FirstOrDefault(x => x.IsFromCatchData() && x.RecordedOnYear == _catchRecordedDateTime.Year);
            catchOwnReportDataOfCurrentYear.Should().NotBeNull();
            catchOwnReportDataOfCurrentYear.CreatedFromCatch(_catchInDb, true);
            var catchOwnReportDataOfNextYear = ownReportData
                .FirstOrDefault(x => x.IsFromCatchData() && x.RecordedOnYear == _catchRecordedDateTime.Year + 1);
            catchOwnReportDataOfNextYear.Should().NotBeNull();
            catchOwnReportDataOfNextYear.CreatedFromCatch(_catchInDb, false);


            // OwnReportData from TimeRegistrations:
            var timeRegistrationOwnReportDataOfCurrentYear = ownReportData
                .FirstOrDefault(x => x.IsFromTimeRegistrationhData() && x.RecordedOnYear == _catchRecordedDateTime.Year);
            timeRegistrationOwnReportDataOfCurrentYear.Should().NotBeNull();
            timeRegistrationOwnReportDataOfCurrentYear.CreatedFromTimeRegistration(_timeRegistrationInDb, true);
            var timeRegistrationOwnReportDataOfNextYear = ownReportData
                .FirstOrDefault(x => x.IsFromTimeRegistrationhData() && x.RecordedOnYear == _catchRecordedDateTime.Year + 1);
            timeRegistrationOwnReportDataOfNextYear.Should().NotBeNull();
            timeRegistrationOwnReportDataOfNextYear.CreatedFromTimeRegistration(_timeRegistrationInDb, false);

            // Trap data:
            var trapData = ownReportData
                .FirstOrDefault(x => x.TrapTypeName == "Conibear");
            trapData.Should().NotBeNull();

        }


        #region Helpers
        private SubAreaHourSquare CreateSubAreaHourSquareWithHierarchy()
        {
            var organization = Organization
                .Create("TEST_ORGANIZATION", "TST_O", EntityWithGeometryBuilderBase.CreateRectangle(1, 1));
            organization.WithId(new Guid("2529ecef-05e2-4b07-b22e-19de4d94f027"));

            var waterAuthority = WaterAuthority
                .Create("TEST_WATER_AUTHORITY", "TST_WA", organization.Id, EntityWithGeometryBuilderBase.CreateRectangle(1, 1));
            waterAuthority.WithId(new Guid("0d758786-f784-4d0e-b249-164e35e2bb6e"));

            var rayon = Rayon
                .Create("TEST_RAYON", organization.Id, EntityWithGeometryBuilderBase.CreateRectangle(1, 1));
            rayon.WithId(new Guid("11a97c97-966a-4a1d-b771-e5371ab6aa9f"));

            var catchArea = CatchArea
                .Create("TEST_CATCH_AREA", rayon.Id, EntityWithGeometryBuilderBase.CreateRectangle(1, 1));
            catchArea.WithId(new Guid("2b2efae4-bb83-4fff-b4df-dad309c2d08e"));

            var subArea = SubArea
                .Create("T_SA", catchArea.Id, waterAuthority.Id, EntityWithGeometryBuilderBase.CreateRectangle(1, 1));
            subArea.WithId(new Guid("2103e3b9-a391-420f-980a-a274ec5887b1"));

            var hourSquare = HourSquare
                .Create("T_HS", EntityWithGeometryBuilderBase.CreateRectangle(1, 1));

            var subAreaHourSquare = SubAreaHourSquare
                .Create(subArea.Id, hourSquare.Id, 1, 1, 1, 1, EntityWithGeometryBuilderBase.CreateRectangle(1, 1), VersionRegionalLayout.V2IndelingId);
            subAreaHourSquare.WithId(new Guid("5e534068-1cf7-4ff4-b387-fdf5ae43eafe"));

            AddAndSaveChanges(organization, waterAuthority, rayon, catchArea, subArea, hourSquare, subAreaHourSquare);

            return subAreaHourSquare;
        }
        #endregion Helpers
    }
}
