using Microsoft.EntityFrameworkCore;
using System;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Users
{
    public class UserSeeding : ISeeding
    {
        public static readonly Guid DeveloperUserId = new Guid("16B691D5-DA79-49EF-8F67-514B15754071");
        public static readonly Guid SystemUserId = new Guid("8207DB25-94D1-4F3D-BF18-90DA283221F7");

        public void Seed(ModelBuilder modelBuilder)
        {
            User systemUser = SeedSystemUser();
            User developerUser = SeedDeveloperUser();
            modelBuilder.Entity<User>().HasData(systemUser, developerUser);
        }

        private static User SeedSystemUser()
        {
            var createdOn = new DateTimeOffset(2019, 5, 6, 14, 31, 0, TimeSpan.FromHours(0));
            User user = User
                .Create("System", "trap.system@domain.com", null)
                .WithId(SystemUserId)
                .WithCreated(createdOn, SystemUserId)
                .WithUpdated(createdOn, SystemUserId);
            return user;
        }

        private static User SeedDeveloperUser()
        {
            var createdOn = new DateTimeOffset(2020, 3, 21, 17, 11, 0, TimeSpan.FromHours(0));
            User user = User.Create("Developer", "developer@domain.com", null)
                .WithAuthorized(true)
                .WithId(DeveloperUserId)
                .WithCreated(createdOn, DeveloperUserId)
                .WithUpdated(createdOn, DeveloperUserId);
            return user;
        }
    }
}
