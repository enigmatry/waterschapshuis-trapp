using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;
using Waterschapshuis.CatchRegistration.Common.Tests;
using System.Net;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.Traps
{
    [Category("integration")]
    public class TrapsControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private Trap _trapInDb = null!;
        private Trap _emptyTrapInDb = null!;
        private Organization _otherOrganization = null!;
        private CatchType _catchType = null!;
        private CatchType _byCatchCatchType = null!;
        private CatchType _otherByCatchCatchType = null!;
        private readonly Guid _trapTypeId = TrapType.ConibearBeverratId;
        private readonly Guid _otherTrapTypeId = TrapType.GrondklemBeverratId;
        private User _otherUser = null!;


        [SetUp]
        public void SetUp()
        {
            _catchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.BeverratMoerOudId);
            _byCatchCatchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.ByCatchVlaamseGaaiId);
            _otherByCatchCatchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.ByCatchGroteModderkruiperId);

            _otherOrganization = new OrganizationBuilder()
                .WithName("Other Organization");

            _emptyTrapInDb = new TrapBuilder()
                .WithId(Guid.NewGuid())
                .WithTrapTypeId(_trapTypeId);

            Catch c1 = new CatchBuilder()
                .WithCatchType(_byCatchCatchType)
                .WithNumberOfCatches(1);

            Catch c2 = new CatchBuilder()
                .WithCatchType(_catchType)
                .WithNumberOfCatches(2);

            Catch c3 = new CatchBuilder()
                .WithCatchType(_otherByCatchCatchType)
                .WithNumberOfCatches(3);

            _trapInDb = new TrapBuilder()
                .WithId(Guid.NewGuid())
                .WithCoordinates(1, 1)
                .WithNumberOfTraps(2)
                .WithRemarks("Test remarks")
                .WithStatus(TrapStatus.Catching)
                .WithTrapTypeId(_trapTypeId)
                .WithCatches(c1, c2, c3);

            _otherUser = User.Create("OtherUser", "other.user@email.com", null);

            AddAndSaveChanges(_otherOrganization, _trapInDb, _emptyTrapInDb, _otherUser);
        }

        #region GET
        [Test]
        public async Task GivenValidTrapId_GetMultipleById_ReturnsTrapDetails()
        {
            string url = QueryHelpers.AddQueryString("traps", "ids", _trapInDb.Id.ToString());
            var response = await Client.GetAsync<IEnumerable<GetTrapDetails.TrapItem>>(url);

            response = response.ToList();

            response.Should().NotBeNull();
            response.Should().NotBeEmpty();
            response.Count().Should().Be(1);

            var trap = response.First();

            trap.Type.Should().Be("Conibear");
            trap.NumberOfTraps.Should().Be(2);
            trap.Remarks.Should().Be("Test remarks");
            trap.CreatedBy.Should().Be(TestPrincipal.TestUserName);
            trap.UpdatedBy.Should().Be(TestPrincipal.TestUserName);

            trap.Catches.Where(x => x.IsByCatch).Sum(x => x.Number).Should().Be(4);
            trap.Catches.Where(x => !x.IsByCatch).Sum(x => x.Number).Should().Be(2);
            trap.Catches.All(x => x.CreatedBy == TestPrincipal.TestUserName).Should().BeTrue();
        }

        [Test]
        public async Task GivenValidTrapIds_GetMultipleById_ReturnsTrapDetails()
        {
            string url = $"/traps?ids={ _trapInDb.Id }&ids={ _emptyTrapInDb.Id }";

            var response = await Client.GetAsync<IEnumerable<GetTrapDetails.TrapItem>>(url);

            response = response.ToList();

            response.Should().NotBeNull();
            response.Should().NotBeEmpty();

            response.Count().Should().Be(2);

            response.Count(tl => tl.NumberOfTraps == 0).Should().Be(1);
            response.Count(tl => tl.NumberOfTraps != 0).Should().Be(1);

            response.All(x => x.CreatedBy == TestPrincipal.TestUserName).Should().BeTrue();
            response.All(x => x.UpdatedBy == TestPrincipal.TestUserName).Should().BeTrue();
        }

        [Test]
        public async Task GivenValidTrapIds_GetMultipleById_ReturnsTrapDetails_Anonymized()
        {
            ChangeCurrentUserId(_otherUser.Id);
            ChangeCurrentUserOrganization(_otherOrganization);
            ChangeCurrentUserRoleIds(Role.SeniorUserId);
            SaveChanges();

            string url = $"/traps?ids={ _trapInDb.Id }&ids={ _emptyTrapInDb.Id }";

            var response = await Client.GetAsync<IEnumerable<GetTrapDetails.TrapItem>>(url);

            response = response.ToList();
            response.All(x => x.CreatedBy == User.AnonymizedName).Should().BeTrue();
            response.All(x => x.UpdatedBy == User.AnonymizedName).Should().BeTrue();
            response.SelectMany(x => x.Catches)
                .All(x => x.CreatedBy == User.AnonymizedName).Should().BeTrue();
        }
        #endregion GET

        #region PUT
        [Test]
        public async Task Update()
        {
            var updateCommand = new TrapUpdate.Command
            {
                Id = _emptyTrapInDb.Id,
                TrapTypeId = _otherTrapTypeId,
                Remarks = _emptyTrapInDb.Remarks + "1",
                Status = _emptyTrapInDb.Status,
                Longitude = _emptyTrapInDb.Location.X,
                Latitude = _emptyTrapInDb.Location.Y
            };

            var response = await Client.PutAsync<TrapUpdate.Command, GetTrapDetails.TrapItem>("traps", updateCommand);

            response.Should().NotBeNull();
            response.Id.Should().Be(updateCommand.Id);
            response.TrapTypeId.Should().Be(updateCommand.TrapTypeId);
            response.Remarks.Should().Be(updateCommand.Remarks);

            response.Status.Should().Be(_emptyTrapInDb.Status);
            response.NumberOfTraps.Should().Be(_emptyTrapInDb.NumberOfTraps);
            response.RecordedOn.Should().Be(_emptyTrapInDb.RecordedOn);
            response.Longitude.Should().Be(_emptyTrapInDb.Location.X);
            response.Latitude.Should().Be(_emptyTrapInDb.Location.Y);
            response.Catches.Should().BeEmpty();
        }

        [Test]
        public async Task Update_TrapLocation()
        {
            var updateCommand = new TrapUpdate.Command
            {
                Id = _emptyTrapInDb.Id,
                TrapTypeId = _otherTrapTypeId,
                Remarks = _emptyTrapInDb.Remarks + "1",
                Status = _emptyTrapInDb.Status,
                Longitude = 104020,
                Latitude = 382787   
            };

            var response = await Client.PutAsync<TrapUpdate.Command, GetTrapDetails.TrapItem>("traps", updateCommand);

            response.Should().NotBeNull();
            response.Id.Should().Be(updateCommand.Id);
            response.TrapTypeId.Should().Be(updateCommand.TrapTypeId);
            response.Remarks.Should().Be(updateCommand.Remarks);

            response.Status.Should().Be(_emptyTrapInDb.Status);
            response.NumberOfTraps.Should().Be(_emptyTrapInDb.NumberOfTraps);
            response.RecordedOn.Should().Be(_emptyTrapInDb.RecordedOn);
            response.Longitude.Should().Be(updateCommand.Longitude);
            response.Latitude.Should().Be(updateCommand.Latitude);
            response.Catches.Should().BeEmpty();
        }

        [Test]
        public async Task Update_NotUniqueCatchId_BadRequest()
        {
            var updateCommand = new TrapUpdate.Command
            {
                Id = _trapInDb.Id,
                TrapTypeId = TrapType.ConibearMuskratId,
                Remarks = _trapInDb.Remarks,
                Status = TrapStatus.NotCatching,
                Longitude = _trapInDb.Location.X,
                Latitude = _trapInDb.Location.Y
            };

            updateCommand.TrapTypeId.Should().Be(TrapType.ConibearMuskratId);

            var content = new StringContent(JsonConvert.SerializeObject(updateCommand), Encoding.UTF8, "application/json");
            var response = await Client.PutAsync("traps", content);

            response.StatusCode.Should()
                .Be(HttpStatusCode.BadRequest, "ConibearMuskrat trap type does not allow status NotCatching");
            (await response.Content.ReadAsStringAsync())
                .Should().Contain("Trap status is not allowed for selected trap type");
        }
        #endregion PUT
    }
}
