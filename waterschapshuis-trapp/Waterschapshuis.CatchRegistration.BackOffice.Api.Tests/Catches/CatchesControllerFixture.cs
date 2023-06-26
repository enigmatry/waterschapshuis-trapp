using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Traps;
using Waterschapshuis.CatchRegistration.Common.Tests;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Common.Tests.TestImpersonation;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.Catches
{
    [Category("integration")]
    public class CatchesControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private DateTimeOffset _createdOn;
        private readonly double _longitude = 4.899431;
        private readonly double _latitude = 52.379189;
        private Catch _catchInDb = null!;
        private CatchType _catchType = null!;
        private CatchType _otherCatchType = null!;
        private Organization _otherOrganization = null!;
        private User _otherUser = null!;

        [SetUp]
        public void SetUp()
        {
            _createdOn = Resolve<ITimeProvider>().Now;

            _catchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.BeverratMoerOudId);
            _otherCatchType = QueryDb<CatchType>().Single(x => x.Id == CatchType.ByCatchVlaamseGaaiId);

            _catchInDb = new CatchBuilder()
                .WithId(Guid.NewGuid())
                .WithRecordedOn(_createdOn)
                .WithCreatedOn(_createdOn)
                .WithCatchType(_catchType)
                .WithNumberOfCatches(5)
                .WithStatus(CatchStatus.Written)
                .WithTrap(new TrapBuilder()
                    .WithStatus(TrapStatus.Catching)
                    .WithNumberOfTraps(2)
                    .WithTrapTypeId(TrapType.ConibearMuskratId)
                    .WithRemarks("Test Trap")
                    .WithCoordinates(_longitude, _latitude)
                );

            _otherOrganization = new OrganizationBuilder().WithName("Other Organization");

            _otherUser = User.Create("OtherUser", "other.user@email.com", null);

            AddAndSaveChanges(_catchInDb, _otherOrganization, _otherUser);
        }

        #region GET
        [Test]
        public async Task GivenValidCatchId_GetById_ReturnsCatchDetails()
        {
            var response = await Client.GetAsync<GetCatchDetails.CatchItem>($"catches/{_catchInDb.Id}");

            response.Should().NotBeNull();

            response.CatchTypeId.Should().Be(_catchInDb.CatchTypeId);
            response.Type.Should().Be("Beverrat moer oud (>1jr)");
            response.Number.Should().Be(5);
            response.RecordedOn.Should().Be(_createdOn);
            response.CreatedBy.Should().Be(TestPrincipal.TestUserName);
            response.CanBeEdited.Should().Be(false);
        }

        [Test]
        public async Task GivenValidCatchId_GetById_ReturnsCatchDetails_Anonymized()
        {
            ChangeCurrentUserId(_otherUser.Id);
            ChangeCurrentUserOrganization(_otherOrganization);
            ChangeCurrentUserRoleIds(Role.SeniorUserId, Role.TrapperRoleId);
            SaveChanges();

            var response = await Client.GetAsync<GetCatchDetails.CatchItem>($"catches/{_catchInDb.Id}");

            response.Should().NotBeNull();
            response.CreatedBy.Should().Be(User.AnonymizedName);
            response.CanBeEdited.Should().Be(true);
        }

        [Test]
        public async Task GivenNonExistingCatchId_GetById_ReturnsNotFound()
        {
            var response = await Client.GetAsync($"catches/{Guid.NewGuid()}");

            response.Should().BeNotFound();
        }
        #endregion GET

        #region PUT
        [Test]
        public async Task Update()
        {
            ChangeCurrentUserRoleIds(Role.TrapperRoleId);
            SaveChanges();

            var updateCommand = new CatchCreateOrUpdate.Command
            {
                Id = _catchInDb.Id,
                TrapId = _catchInDb.TrapId,
                Number = _catchInDb.Number,
                CatchTypeId = _catchInDb.CatchTypeId,
                RecordedOn = _catchInDb.RecordedOn,
                Status = CatchStatus.Closed
            };

            var response = await Client.PutAsync<CatchCreateOrUpdate.Command, GetCatchDetails.CatchItem>("catches", updateCommand);

            response.Status.Should().Be(_catchInDb.Status);
        }
        #endregion PUT

        #region DELETE
        [Test]
        public async Task? GivenValidCatchId_FromTheSameUser_DeleteIsSuccess()
        {
            QueryDbSkipCache<Catch>().Where(x => x.Id == _catchInDb.Id)
                .Should().NotBeEmpty("Catch is in DB");


            var response = await Client.DeleteAsync($"catches/{_catchInDb.Id}");


            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            QueryDbSkipCache<Catch>().Where(x => x.Id == _catchInDb.Id)
                .Should().BeEmpty("Catch was removed");

            var allTrapHistoryMessages = QueryDbSkipCache<TrapHistory>()
                .Where(x => x.TrapId == _catchInDb.TrapId).Select(x => x.Message).ToList();
            allTrapHistoryMessages.Should()
                .Contain(TrapHistoryDomainEvent.OnCatchCreate(_catchInDb).Messages.Single(), "Catch creation HistoryEntry must remain");
            allTrapHistoryMessages.Should()
                .Contain(TrapHistoryDomainEvent.OnCatchRemove(_catchInDb).Messages.Single(), "Catch removal HistoryEntry must be added");
        }

        [Test]
        public async Task? GivenValidCatchId_NotCreatedToday_DeleteIsFailure()
        {
            _catchInDb.SetCreated(DateTimeOffset.Now.AddDays(-1), _catchInDb.CreatedById);
            SaveChanges();

            var response = await Client.DeleteAsync($"catches/{_catchInDb.Id}");

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        #endregion
    }
}
