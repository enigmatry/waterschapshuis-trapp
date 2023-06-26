using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Organizations;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Organizations;
using Waterschapshuis.CatchRegistration.Common.Tests.Api;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Infrastructure;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Rayons;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Tests
{
    [Category("integration")]
    public class OrganizationsControllerFixture : BackOfficeApiIntegrationFixtureBase
    {
        private Organization _organization = null!;
        private Organization _organization2 = null!;

        [SetUp]
        public void SetUp()
        {
            _organization = new OrganizationBuilder()
                .WithName("Nultien")
                .WithShortName("NT")
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(575000, 140000));

            Rayon rayon1 = new RayonBuilder()
                .WithName("Test rayon 1");

            Rayon rayon2 = new RayonBuilder()
                .WithName("Test rayon 2");

            _organization.AddRayon(rayon1);
            _organization.AddRayon(rayon2);

            _organization2 = new OrganizationBuilder()
                .WithName("NovaLite")
                .WithShortName("NL")
                .WithGeometry(EntityWithGeometryBuilderBase.CreateRectangle(575000, 140000));

            AddAndSaveChanges(_organization, _organization2);
        }

        [Test]
        public async Task TestGetAll()
        {
            var organizations = (await Client.GetAsync<GetOrganizations.Response>("organizations")).Items.ToList();

            organizations.Count.Should().Be(3);

            GetOrganizations.Response.Item item = organizations.Single(u => u.Name == "Nultien");
            item.Name.Should().Be("Nultien");
        }
    }
}
