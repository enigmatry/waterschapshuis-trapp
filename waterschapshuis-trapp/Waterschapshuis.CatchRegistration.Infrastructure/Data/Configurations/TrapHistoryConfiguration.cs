using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TrapHistoryConfiguration : IEntityTypeConfiguration<TrapHistory>
    {
        public void Configure(EntityTypeBuilder<TrapHistory> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Message)
                .HasMaxLength(TrapHistory.MessageMaxLength);

            builder.Property(x => x.RecordedOn)
                .HasDefaultValueSql("getutcdate()");
        }
    }
}
