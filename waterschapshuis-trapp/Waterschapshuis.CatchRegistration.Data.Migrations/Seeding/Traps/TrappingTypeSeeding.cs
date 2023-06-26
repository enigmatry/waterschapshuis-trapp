using System;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Data.Migrations.Seeding.Traps
{
    public class TrappingTypeSeeding : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            createType(TrappingType.MuskusratId, "Muskusrat");
            createType(TrappingType.BeverratId, "Beverrat");

            void createType(Guid id, string name)
            {
                TrappingType trappingType = TrappingType
                    .Create(name)
                    .WithId(id);

                modelBuilder.Entity<TrappingType>().HasData(trappingType);
            }
        }
    }
}
