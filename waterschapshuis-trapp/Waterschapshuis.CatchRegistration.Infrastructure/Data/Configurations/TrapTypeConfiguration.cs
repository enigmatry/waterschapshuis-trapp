using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TrapTypeConfiguration : IEntityTypeConfiguration<TrapType>
    {
        public void Configure(EntityTypeBuilder<TrapType> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(TrapType.NameMaxLength);

            builder.Property(x => x.Order)
                .HasColumnType("tinyint");

            builder.HasMany(x => x.TrapTypeTrapStatusStyles)
                .WithOne(x => x.TrapType)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
