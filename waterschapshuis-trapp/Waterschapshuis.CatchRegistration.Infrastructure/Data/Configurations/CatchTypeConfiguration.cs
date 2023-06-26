using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class CatchTypeConfiguration : IEntityTypeConfiguration<CatchType>
    {
        public void Configure(EntityTypeBuilder<CatchType> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CatchType.NameMaxLength);

            builder.Property(x => x.AnimalType)
                .HasConversion(x => (int)x, x => (AnimalType)x)
                .HasColumnType("tinyint");

            builder.Property(x => x.Order)
                .HasColumnType("tinyint");
        }
    }
}
