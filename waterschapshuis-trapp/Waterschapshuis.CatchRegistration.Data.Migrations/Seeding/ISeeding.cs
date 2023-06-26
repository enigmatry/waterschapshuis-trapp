using Microsoft.EntityFrameworkCore;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding
{
    internal interface ISeeding
    {
        void Seed(ModelBuilder modelBuilder);
    }
}