using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.ApplicationServices.Users;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;
using Waterschapshuis.CatchRegistration.Common.Tests;
namespace Waterschapshuis.CatchRegistration.Mobile.Api.Tests.Users
{
    [Category("integration")]
    public class UsersControllerFixture : MobileApiIntegrationFixtureBase
    {
        private User _user = null!;
        private Organization _organization = null!;

        [SetUp]
        public void SetUp()
        {
            CreateOrganization();
            CreateAndSaveUser();
        }


        [Test]
        public async Task GivenValidUserId_GetById_ReturnsUserDetails()
        {
            var user = await Client.GetAsync<GetUserDetails.Response>($"users/{_user.Id}");

            user.Should().NotBeNull();

            user.Name.Should().Be("John Doe");
            user.Email.Should().Be("john_doe@john.doe");
            user.Authorized.Should().BeTrue();
            user.GivenName.Should().Be("John");
            user.Surname.Should().Be("Doe");
        }

        [Test]
        public async Task GivenNonExistingUserId_GetById_ReturnsNotFound()
        {
            var response = await Client.GetAsync($"users/{Guid.NewGuid()}");

            response.Should().BeNotFound();
        }

        [Test]
        public async Task TestUpdateConfidentiality()
        {
            var command = new UserUpdateConfidentiality.Command {Id = _user.Id, ConfidentialityConfirmed = true};
            GetUserDetails.Response user =
                await Client.PostAsync<UserUpdateConfidentiality.Command, GetUserDetails.Response>("users", command);
            
            user.ConfidentialityConfirmed.Should().Be(true);
        }


        private void CreateAndSaveUser()
        {
            _user = new UserBuilder()
                .Email("john_doe@john.doe")
                .Name("John Doe")
                .GivenName("John")
                .Surname("Doe")
                .Authorized(true)
                .ConfidentialityConfirmed(false)
                .Organization(_organization.Id);

            AddAndSaveChanges(_user);
        }

        private void CreateOrganization()
        {
            _organization = new OrganizationBuilder().WithName("Nultien").WithShortName("NT")
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(575000, 140000));
            AddAndSaveChanges(_organization);
        }
    }
}
