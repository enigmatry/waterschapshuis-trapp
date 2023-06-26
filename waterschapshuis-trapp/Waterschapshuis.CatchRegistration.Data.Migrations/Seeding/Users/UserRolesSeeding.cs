using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Users
{
    public class UserRolesSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            var developerUserRole = UserRole.Create(Role.MaintainerRoleId, UserSeeding.DeveloperUserId);
            var systemUserRole = UserRole.Create(Role.MaintainerRoleId, UserSeeding.SystemUserId);

            modelBuilder.Entity<UserRole>().HasData(developerUserRole, systemUserRole);
        }
    }
}
