using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.ApplicationServices.Users;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Users;
using Waterschapshuis.CatchRegistration.Common.Tests;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.Core.Pagination;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Permissions;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests.Users
{
    [Category("integration")]
    public class UsersControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private readonly Guid _developerUserId = new Guid("16b691d5-da79-49ef-8f67-514b15754071");
        private readonly Guid _role1Id = new Guid("ee4aadf1-8d0f-47bc-b45d-3cbd7c3db2c2");
        private readonly Guid _role2Id = new Guid("ad37ea68-864c-4860-8851-1576a1af947f");
        private User _user = null!;
        private Role _role1 = null!;
        private Role _role2 = null!;
        private Organization _organization = null!;

        [SetUp]
        public void SetUp()
        {
            CreateAndSaveRoles();
            CreateOrganization();
            CreateAndSaveUser();
        }

        [Test]
        public async Task TestGetAll()
        {
            var response = await Client.GetAsync<PagedResponse<GetUsers.ResponseItem>>("users?pageSize=100");

            var users = response.Items.ToList();

            users.Count.Should().Be(4);
            response.ItemsTotalCount.Should().Be(4);
            GetUsers.ResponseItem user = users.Single(u => u.Email == "john_doe@john.doe");
            user.Name.Should().Be("John Doe");
            user.GivenName.Should().Be("John");
            user.Surname.Should().Be("Doe");
            user.Roles.Count().Should().Be(2);
            user.OrganizationName.Should().Be("Nultien");

            AssertUsersRoles(user.Roles);
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

            AssertUsersRoles(user.Roles);
        }

        private void AssertUsersRoles(GetUsers.ResponseItem.Role[] roles)
        {
            roles.FirstOrDefault(r => r.Name == _role1.Name).Should().NotBeNull();
            roles.First(r => r.Name == _role2.Name).Should().NotBeNull();
        }

        private void AssertUsersRoles(GetUserDetails.Response.Role[] roles)
        {
            var returnedRole1 = roles.First(r => r.Name == _role1.Name);
            var returnedRole2 = roles.First(r => r.Name == _role2.Name);
            returnedRole1.Permissions.Select(x => x.Id).Should()
                .BeEquivalentTo(_role1.RolePermissions.Select(rp => rp.PermissionId));
            returnedRole2.Permissions.Select(x => x.Id).Should()
                .BeEquivalentTo(_role2.RolePermissions.Select(rp => rp.PermissionId));
        }

        [Test]
        public async Task TestDeveloperUser()
        {
            var user = await Client.GetAsync<GetUserDetails.Response>($"users/{_developerUserId}");

            user.Should().NotBeNull();

            user.Name.Should().Be("Developer");
            user.Email.Should().Be("developer@enigmatry.com");
            user.Authorized.Should().BeTrue();
            user.GivenName.Should().BeNullOrEmpty();
            user.Surname.Should().BeNullOrEmpty();
        }

        [Test]
        public async Task GivenNonExistingUserId_GetById_ReturnsNotFound()
        {
            var response = await Client.GetAsync($"users/{Guid.NewGuid()}");

            response.Should().BeNotFound();
        }

        [Test]
        public async Task TestUpdate()
        {
            var command = new UserUpdate.Command { Id = _user.Id, Authorized = true, OrganizationId = _organization.Id };
            GetUserDetails.Response user =
                await Client.PostAsync<UserUpdate.Command, GetUserDetails.Response>("users", command);

            user.CreatedOn.Date.Should().Be(_user.CreatedOn.Date);
            user.UpdatedOn.Date.Should().Be(DateTime.Now.Date);
            user.Authorized.Should().Be(true);
            user.OrganizationName.Should().Be("Nultien");

            user.Roles.Length.Should().Be(2);
            user.Roles.FirstOrDefault(r => r.Id == _role1Id).Should().NotBeNull();
            user.Roles.FirstOrDefault(r => r.Id == _role2Id).Should().NotBeNull();
        }


        [Test]
        public async Task TestUpdateConfidentiality()
        {
            var command = new UserUpdateConfidentiality.Command { Id = _user.Id, ConfidentialityConfirmed = true };
            GetUserDetails.Response user =
                await Client.PostAsync<UserUpdateConfidentiality.Command, GetUserDetails.Response>("users/update-confidentiality", command);

            user.ConfidentialityConfirmed.Should().Be(true);
        }


        [Test]
        public async Task TestUserRolesUpdate()
        {
            var command = new UpdateUserRoles.Command { Id = _user.Id, Roles = new[] { _role2Id } };
            GetUserDetails.Response user =
                await Client.PutAsync<UpdateUserRoles.Command, GetUserDetails.Response>("users/user-roles", command);

            user.Roles.Length.Should().Be(1);
            user.Roles.FirstOrDefault(r => r.Id == _role1Id).Should().BeNull();
            user.Roles.FirstOrDefault(r => r.Id == _role2Id).Should().NotBeNull();
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
                .Organization(_organization.Id)
                .WithRoles(_role1Id, _role2Id);

            AddAndSaveChanges(_user);
        }

        private void CreateAndSaveRoles()
        {
            _role1 = new RoleBuilder()
                .With(_role1Id, "Role1")
                .WithPermissions(PermissionId.ApiPrivate, PermissionId.TimeRegistrationPersonalReadWrite);

            _role2 = new RoleBuilder()
                .With(_role2Id, "Role2")
                .WithPermissions(PermissionId.MapRead, PermissionId.Mobile);

            AddAndSaveChanges(_role1, _role2);
        }

        private void CreateOrganization()
        {
            _organization = new OrganizationBuilder().WithName("Nultien").WithShortName("NT")
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(575000, 140000));
            AddAndSaveChanges(_organization);
        }
    }
}
