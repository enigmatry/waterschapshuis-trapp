using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TrapConfiguration : IEntityTypeConfiguration<Trap>
    {
        public void Configure(EntityTypeBuilder<Trap> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Remarks)
                .HasMaxLength(Trap.RemarksMaxLength);

            builder.Property(x => x.Status)
                .HasConversion(x => (int)x, x => (TrapStatus)x)
                .HasColumnType("tinyint");

            builder.Property(x => x.ExternalId)
                .HasMaxLength(Trap.ExternalIdMaxLength);

            builder.Property(x => x.Location)
                .HasColumnType("geometry");

            builder.Property(x => x.RecordedOn)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}

