using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Data.Migrations.Seeding;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Catches;
using Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.ReportTemplates;
using Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.TimeRegistrations;
using Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Traps;
using Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Users;

namespace Waterschapshuis.CatchRegistration.Data.Migrations
{
    public static class DbInitializer
    {
        private static readonly IList<ISeeding> Seedings = new List<ISeeding>(10);

        static DbInitializer()
        {
            Seedings.Add(new UserSeeding());
            Seedings.Add(new TrappingTypeSeeding());
            Seedings.Add(new TrapTypeSeeding());
            Seedings.Add(new CatchTypeSeeding());
            Seedings.Add(new RolesAndPermissionsSeeding());
            Seedings.Add(new UserRolesSeeding());
            Seedings.Add(new ReportTemplateTypeSeeding());
            Seedings.Add(new TimeRegistrationCategorySeeding());
        }

        // EF Core way of seeding data: https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
        public static void SeedData(ModelBuilder modelBuilder)
        {
            foreach (ISeeding seeding in Seedings)
            {
                seeding.Seed(modelBuilder);
            }
        }
    }
}
