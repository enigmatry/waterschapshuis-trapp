using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TrapTypeTrapStatusStyleConfiguration : IEntityTypeConfiguration<TrapTypeTrapStatusStyle>
    {
        public void Configure(EntityTypeBuilder<TrapTypeTrapStatusStyle> builder)
        {
            builder.HasKey(x => new { x.TrapTypeId, x.TrapStatus });

            builder.Property(x => x.IconName).HasMaxLength(50);

            builder.Property(x => x.TrapStatus)
                .HasConversion(x => (int)x, x => (TrapStatus)x)
                .HasColumnType("tinyint");
        }
    }
}
