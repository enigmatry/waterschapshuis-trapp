using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TrapSubAreaHourSquareConfiguration : IEntityTypeConfiguration<TrapSubAreaHourSquare>
    {
        public void Configure(EntityTypeBuilder<TrapSubAreaHourSquare> builder)
        {
            builder.HasKey(x => new { x.TrapId, x.SubAreaHourSquareId });

            builder
                .HasOne(x => x.Trap)
                .WithMany(x => x.PreviousVersionSubAreaHourSquares)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(x => x.SubAreaHourSquare)
                .WithMany(x => x.PreviousVersionTraps)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
