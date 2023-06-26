using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.TrapHistories;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;
using Waterschapshuis.CatchRegistration.Common.Tests;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Provinces;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;
using Waterschapshuis.CatchRegistration.Mobile.Api.Features.Latest.Traps;

namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Traps
{
    [Category("integration")]
    public class TrapsControllerFixture : MobileApiIntegrationFixtureBase
    {
        private DateTimeOffset _createdOn;
        private readonly double _centerLongitude = 10.0;
        private readonly double _centerLatitude = 50.0;
        private readonly double _otherLongitude = 20;
        private readonly double _otherLatitude = 60;
        private readonly double _diameter = 2.0;
        private CatchType _catchType = null!;
        private CatchType _byCatchCatchType = null!;
        private readonly Guid _trapTypeId = TrapType.ConibearMuskratId;
        private readonly Guid _otherTrapTypeId = TrapType.OtterId;

        private Trap _emptyTrapInDb = null!;
        private Trap _trapInDb = null!;
        private Catch _catchInDb = null!;
        private Catch _catchToCreate = null!;

        [SetUp]
        public void SetUp()
        {
            _createdOn = Resolve<ITimeProvider>().Now;
            _catchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.BeverratMoerOudId);
            _byCatchCatchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.ByCatchVlaamseGaaiId);

            SubAreaHourSquare centerSubAreaHourSquare = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(_centerLongitude, _centerLatitude, _diameter)
                .WithGeoHierarchy("org", "ray", "car", "sar", "wau", "hsq")
                .WithWaterwayValues(10, 10, 10, 10);
            SubAreaHourSquare otherSubAreaHourSquare = new SubAreaHourSquareBuilder()
                .WithStartingCoordinate(_otherLongitude, _otherLatitude, _diameter)
                .WithGeoHierarchy("org", "ray", "car", "sar", "wau", "hsq")
                .WithWaterwayValues(10, 10, 10, 10);

            Province centerProvince = new ProvinceBuilder()
                .WithName("traps_controller_fixture")
                .WithRectangleGeometry(_centerLongitude, _centerLatitude, _diameter);
            Province otherProvince = new ProvinceBuilder()
                .WithName("traps_controller_fixture")
                .WithRectangleGeometry(_otherLongitude, _otherLatitude, _diameter);

            _catchInDb = new CatchBuilder()
                .WithCatchType(_catchType)
                .WithNumberOfCatches(2)
                .WithRecordedOn(_createdOn)
                .WithCreatedOn(_createdOn);

            _trapInDb = new TrapBuilder()
                .WithId(Guid.NewGuid())
                .WithNumberOfTraps(3)
                .WithRemarks("Trap Doe")
                .WithCoordinates(_otherLongitude, _otherLatitude)
                .WithRecordedOn(_createdOn)
                .WithCatches(_catchInDb)
                .WithGeoHierarchy(otherSubAreaHourSquare, otherProvince);

            _emptyTrapInDb = new TrapBuilder()
                .WithId(Guid.NewGuid())
                .WithNumberOfTraps(3)
                .WithRemarks("Empty Trap Doe")
                .WithCoordinates(_centerLongitude, _centerLatitude)
                .WithRecordedOn(_createdOn.AddMinutes(10))
                .WithGeoHierarchy(centerSubAreaHourSquare, centerProvince);

            AddAndSaveChanges(_trapInDb, _emptyTrapInDb);

            _catchToCreate = new CatchBuilder()
                .WithId(Guid.NewGuid())
                .WithCatchType(_catchType)
                .WithNumberOfCatches(10)
                .WithStatus(CatchStatus.Written)
                .WithRecordedOn(_createdOn.AddDays(-5));
        }

        #region GET
        [Test]
        public async Task GivenNoBoundingBox_ReturnsTrapDetails()
        {
            var response = await Client.GetAsync<PagedResponse<GetTrapDetails.TrapItem>>("traps/get-traps");

            var items = response.Items.ToList();

            items.Should().NotBeNull();
            items.Count().Should().Be(2);
        }

        [Test]
        public async Task GivenNonExistingTrapId_GetById_ReturnsNotFound()
        {
            var response = await Client.GetAsync($"traps/{Guid.NewGuid()}");

            response.Should().BeNotFound();
        }

        [Test]
        public async Task GivenValidTrapId_GetById_ReturnsTrapDetails()
        {
            var trap = await Client.GetAsync<GetTrapDetails.TrapItem>($"traps/{_trapInDb.Id}");

            trap.Should().NotBeNull();

            trap.Type.Should().Be("Conibear");
            trap.NumberOfTraps.Should().Be(3);
            trap.Status.Should().Be(1);
            trap.Remarks.Should().Be("Trap Doe");
            trap.RecordedOn.Should().Be(_trapInDb.RecordedOn);
        }

        [Test]
        public async Task GivenValidTrapId_GetMultipleByIds_ReturnsTrapDetails()
        {
            string url = QueryHelpers.AddQueryString("traps", "ids", _trapInDb.Id.ToString());
            var response = await Client.GetAsync<IEnumerable<GetTrapDetails.TrapItem>>(url);

            response = response.ToList();

            response.Should().NotBeNull();
            response.Should().NotBeEmpty();
            response.Count().Should().Be(1);

            var trap = response.First();
            trap.Type.Should().Be("Conibear");
            trap.NumberOfTraps.Should().Be(3);
            trap.Status.Should().Be(1);
            trap.Remarks.Should().Be("Trap Doe");
        }

        [Test]
        public async Task GivenValidQuery_IncludeDetails_ReturnsThisWeekUserSummary()
        {
            string url = QueryHelpers.AddQueryString("traps/my-summary", "includeDetails", true.ToString());
            var response = await Client.GetAsync<GetMySummary.Response>(url);

            response.Should().NotBeNull();
            response.OutstandingTraps.Should().Be(2);
            response.CatchesThisWeek.Should().Be(2);

            response.TrapDetails.Should().NotBeEmpty();
            response.TrapDetails.Count().Should().Be(2);
            response.TrapDetails.OrderBy(x => x.DateCreated).First().DateCreated.Should().Be(_createdOn);
            response.TrapDetails.OrderBy(x => x.DateCreated).First().DaysSinceCatch.Should().Be(0);
        }

        [Test]
        public async Task GivenValidQuery_DoNotIncludeDetails_ReturnsThisWeekUserSummary()
        {
            string url = QueryHelpers.AddQueryString("traps/my-summary", "includeDetails", false.ToString());
            var response = await Client.GetAsync<GetMySummary.Response>(url);

            response.Should().NotBeNull();
            response.OutstandingTraps.Should().Be(2);
            response.CatchesThisWeek.Should().Be(2);
            response.TrapDetails.Should().BeEmpty();
        }

        [Test]
        public async Task Get_TrapHistories()
        {
            // Update trap:
            var command = GetUpdateCommand(_trapInDb);
            await PostCommandAsync(command);

            // Update catch
            var catchToUpdate = QueryDb<Catch>().Single(x => x.Id == _catchInDb.Id);
            var updateCommand = new CatchCreateOrUpdate.Command
            {
                CatchTypeId = _byCatchCatchType.Id,
                Number = catchToUpdate.Number + 10,
                Status = CatchStatus.Completed,
                RecordedOn = catchToUpdate.RecordedOn.AddDays(1)
            };
            catchToUpdate.Update(updateCommand, _byCatchCatchType);
            SaveChanges();


            var uri = $"traps/histories?trapId={_trapInDb.Id}&pageSize=20&sortDirection=desc";
            var response = await Client.GetAsync<PagedResponse<GetTrapHistories.HistoryItem>>(uri);


            response.Should().NotBeNull();
            response.ItemsTotalCount.Should().Be(5);

            response.Items.First().Message.Should().Be(
                TrapHistory.GetTrapCreatedMessage(3),
                "When creating Trap & Catch, trap creation history message goes first");

            response.Items.Skip(1).First().Message.Should().Be(
                $"Vangst {_catchInDb.Number}x {_catchInDb.CatchType.Name}",
                "When creating Trap & Catch, catch creation history message goes second");

            response.Items.Skip(2).First().Message.Should().Be(
                TrapHistory.GetTrapNumberChangedMessage(4),
                "When changing number of traps, trap number changed history message goes third");

            response.Items.Skip(3).Take(2).Select(x => x.Message).All(message =>
                new[] {
                    TrapHistory.MessageOnTrapLocationUpdate,
                    $"Vangmiddel staat {TrapStatus.NotCatching.GetDescription().ToLower()}"
                }.Contains(message))
                .Should()
                .BeTrue("Changing trap Location & Status results in 2 messages at the end");
        }
        #endregion GET

        #region POST
        [Test]
        public async Task Create_OnSameSubAreaHourSquare()
        {
            var command = GetCreateCommand("OnSameSubAreaHourSquare", _trapInDb.Location.X + 0.05, _trapInDb.Location.Y);

            var response = await PostCommandAsync(command);

            AssertTrapTypeAndRecordedOn(response, command);
            AssertTrap(response, command);
            response.Remarks.Should().Be(command.Remarks);
        }

        [Test]
        public async Task Create_OnDifferentSubAreaHourSquare()
        {
            var command = GetCreateCommand("OnDifferentSubAreaHourSquare", _centerLongitude + 0.05, _centerLatitude + 0.05);

            var response = await PostCommandAsync(command);

            AssertTrapTypeAndRecordedOn(response, command);
            AssertTrap(response, command);
            response.Remarks.Should().Be(command.Remarks);
        }

        [Test]
        public async Task Create_WithCatch()
        {
            var command = GetCreateCommand(
                "Create_WithCatch",
                _centerLongitude,
                _centerLatitude,
                CatchCreateOrUpdate.Command.CreateFrom(_catchToCreate));

            var response = await PostCommandAsync(command);

            AssertTrapTypeAndRecordedOn(response, command);
            AssertTrap(response, command, command.Catches.Count());
            response.Remarks.Should().Be(command.Remarks);
        }

        [Test]
        public async Task Create_NotUniqueCatchId_BadRequest()
        {
            var command = GetCreateCommand(
                "Create_WithCatch",
                _centerLongitude,
                _centerLatitude,
                CatchCreateOrUpdate.Command.CreateFrom(_catchInDb));

            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("traps", content);

            response.Should().BeBadRequest("Nieuwe vangsten moeten een uniek id hebben");
            (await response.Content.ReadAsStringAsync())
                .Should().Contain("Nieuwe vangsten moeten een uniek id hebben");
        }

        [Test]
        public async Task Create_WrongStatusForTrapType_BadRequest()
        {
            var command = GetCreateCommand(
                "Create_WithCatch",
                _centerLongitude,
                _centerLatitude,
                CatchCreateOrUpdate.Command.CreateFrom(_catchInDb));
            command.Status = TrapStatus.NotCatching;

            command.TrapTypeId.Should().Be(TrapType.ConibearMuskratId);

            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("traps", content);

            response.Should()
                .BeBadRequest("ConibearMuskrat trap type does not allow status NotCatching");
            (await response.Content.ReadAsStringAsync())
                .Should().Contain("Vangmiddelstatus is niet toegestaan voor dit vangmiddel type");
        }

        [Test]
        public async Task Update_EmptyTrap_AddCatch()
        {
            var command = GetUpdateCommand(_emptyTrapInDb, CatchCreateOrUpdate.Command.CreateFrom(_catchToCreate));

            await AssertTrapState(_emptyTrapInDb);

            var afterUpdate = await PostCommandAsync(command);

            afterUpdate.TrapTypeId.Should().Be(command.TrapTypeId);
            afterUpdate.RecordedOn.Should().Be(_emptyTrapInDb.RecordedOn);

            AssertTrap(afterUpdate, command, command.Catches.Count() + _emptyTrapInDb.Catches.Count);
            afterUpdate.Remarks.Should().Be(command.Remarks);
        }

        [Test, Description("Must not update TrapType and RecordedOn, because trap has catches")]
        public async Task Update_TrapWithCatches()
        {
            var beforeUpdateCommand = TrapCreateOrUpdate.Command.CreateFrom(_trapInDb, false);
            var updateCommand = GetUpdateCommand(_trapInDb, CatchCreateOrUpdate.Command.CreateFrom(_catchToCreate));

            await AssertTrapState(_trapInDb);

            var afterUpdate = await PostCommandAsync(updateCommand);

            // Should not update, thus asserting with beforeUpdateCommand:
            AssertTrapTypeAndRecordedOn(afterUpdate, beforeUpdateCommand);
            // Should update, thus asserting with updateCommand:
            AssertTrap(afterUpdate, updateCommand, updateCommand.Catches.Count() + _trapInDb.Catches.Count);
            afterUpdate.Remarks.Should().Be(updateCommand.Remarks);
        }

        [Test, Description("Must only update remarks, because trap is in Removed state")]
        public async Task Update_RemovedTrap()
        {
            var beforeUpdateCommand = TrapCreateOrUpdate.Command.CreateFrom(_emptyTrapInDb, false);
            var updateCommand = GetUpdateCommand(_emptyTrapInDb, CatchCreateOrUpdate.Command.CreateFrom(_catchToCreate));

            beforeUpdateCommand.Status = TrapStatus.Removed;
            _emptyTrapInDb.Update(beforeUpdateCommand, _emptyTrapInDb.SubAreaHourSquare, _emptyTrapInDb.Province, new CatchType[0].ToList());
            SaveChanges();

            await AssertTrapState(_emptyTrapInDb);

            var afterUpdate = await PostCommandAsync(updateCommand);

            // Should not update, thus asserting with beforeUpdateCommand:
            AssertTrapTypeAndRecordedOn(afterUpdate, beforeUpdateCommand);
            // Should not update, thus asserting with beforeUpdateCommand:
            AssertTrap(afterUpdate, beforeUpdateCommand);
            afterUpdate.Remarks.Should().Be(updateCommand.Remarks);
        }

        [Test, Description("Must remove Catch, but persist Catch related TrapHistory entries")]
        public async Task Update_RemoveCatch()
        {
            var updateCommand = GetUpdateCommand(_trapInDb, CatchCreateOrUpdate.Command.CreateFrom(_catchInDb));
            updateCommand.Catches.Single().MarkedForRemoval = true;


            var afterUpdate = await PostCommandAsync(updateCommand);


            AssertTrap(afterUpdate, updateCommand);
            afterUpdate.Catches.Should().BeEmpty("Catch was removed");

            var allTrapHistoryMessages = QueryDbSkipCache<TrapHistory>()
                .Where(x => x.TrapId == _trapInDb.Id).Select(x => x.Message).ToList();
            allTrapHistoryMessages.Should()
                .Contain(TrapHistoryDomainEvent.OnCatchCreate(_catchInDb).Messages.Single(), "Catch creation HistoryEntry must remain");
            allTrapHistoryMessages.Should()
                .Contain(TrapHistoryDomainEvent.OnCatchRemove(_catchInDb).Messages.Single(), "Catch removal HistoryEntry must be added");
        }
        #endregion POST

        #region DELETE
        [Test]
        public async Task Delete()
        {
            var result = await Client.DeleteAsync<bool>($"traps/{_emptyTrapInDb.Id}");

            result.Should().BeTrue();

            var deletedTrap = QueryDbSkipCache<Trap>().FirstOrDefault(t => t.Id == _emptyTrapInDb.Id);

            deletedTrap.Should().BeNull();
        }

        [Test, Description("Cannot delete trap with catches")]
        public async Task Delete_WithCatches_InternalServerError()
        {
            var response = await Client.DeleteAsync($"traps/{_trapInDb.Id}");

            response.Should().BeInternalServerError("Cannot delete trap with catches.");
        }


        [Test, Description("Cannot delete trap that is created by another user.")]
        public async Task Delete_CreatedByAnotherUser_InternalServerError()
        {
            ChangeCurrentUserId(User.SystemUserId);
            var response = await Client.DeleteAsync($"traps/{_emptyTrapInDb.Id}");

            response.Should().BeInternalServerError("Cannot delete trap that is created by another user.");
        }

        #endregion DELETE

        #region Helpers
        private TrapCreateOrUpdate.Command GetCreateCommand(
            string remarks,
            double longitude,
            double latitude,
            params CatchCreateOrUpdate.Command[] catches)
        {
            var response = new TrapCreateOrUpdate.Command
            {
                Id = Guid.NewGuid(),
                TrapTypeId = _trapTypeId,
                NumberOfTraps = 3,
                Status = TrapStatus.Catching,
                Remarks = remarks,
                Longitude = longitude,
                Latitude = latitude,
                RecordedOn = _createdOn,
                Catches = catches,
                ShouldCreate = true
            };
            response.Catches.ToList().ForEach(c => c.TrapId = response.Id);
            return response;
        }

        private TrapCreateOrUpdate.Command GetUpdateCommand(
            Trap trap,
            params CatchCreateOrUpdate.Command[] catches)
        {
            var response = new TrapCreateOrUpdate.Command
            {
                Id = trap.Id,
                Remarks = "GetUpdateCommand",
                TrapTypeId = _otherTrapTypeId,
                Status = TrapStatus.NotCatching,
                NumberOfTraps = trap.NumberOfTraps + 1,
                RecordedOn = trap.RecordedOn.AddDays(-1),
                Longitude = trap.Location.X + 0.05,
                Latitude = trap.Location.Y + 0.05,
                Catches = catches,
                ShouldCreate = false
            };
            response.Catches.ToList().ForEach(c => c.TrapId = response.Id);
            return response;
        }

        private async Task<GetTrapDetails.TrapItem> PostCommandAsync(TrapCreateOrUpdate.Command command) =>
            await Client.PostAsync<TrapCreateOrUpdate.Command, GetTrapDetails.TrapItem>("traps", command);

        private void AssertCatch(GetCatchDetails.CatchItem response, CatchCreateOrUpdate.Command? command)
        {
            if (command == null)
                return;
            response.Id.Should().Be(command.Id);
            response.Number.Should().Be(command.Number);
            response.CatchTypeId.Should().Be(command.CatchTypeId);
            response.RecordedOn.AssertRecordedOn(command.RecordedOn);
        }

        private async Task AssertTrapState(Trap trap)
        {
            var response = await Client.GetAsync<GetTrapDetails.TrapItem>($"traps/{trap.Id}");

            AssertTrapTypeAndRecordedOn(response, TrapCreateOrUpdate.Command.CreateFrom(trap, false));
            AssertTrap(response, TrapCreateOrUpdate.Command.CreateFrom(trap, false), trap.Catches.Count);
        }

        private void AssertTrapTypeAndRecordedOn(GetTrapDetails.TrapItem response, TrapCreateOrUpdate.Command command)
        {
            response.TrapTypeId.Should().Be(command.TrapTypeId);
            response.RecordedOn.AssertRecordedOn(command.RecordedOn);
        }

        private void AssertTrap(GetTrapDetails.TrapItem response, TrapCreateOrUpdate.Command command, int numberOfCatches = 0)
        {
            response.Should().NotBeNull();
            response.Id.Should().Be(command.Id);
            response.NumberOfTraps.Should().Be(command.NumberOfTraps);
            response.Status.Should().Be(command.Status);
            response.Longitude.Should().Be(command.Longitude);
            response.Latitude.Should().Be(command.Latitude);

            response.Catches.Count().Should().Be(numberOfCatches);
            response.Catches.ToList()
                .ForEach(c => AssertCatch(c, command.Catches.SingleOrDefault(cc => cc.Id == c.Id)));
        }

        #endregion Helpers
    }
}
