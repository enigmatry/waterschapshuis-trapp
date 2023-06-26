using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    public class VersionRegionalLayoutConfiguration : IEntityTypeConfiguration<VersionRegionalLayout>
    {
        public void Configure(EntityTypeBuilder<VersionRegionalLayout> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(VersionRegionalLayout.NameMaxLength);
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.HasMany(x => x.SubAreaHourSquares)
                .WithOne(x => x.VersionRegionalLayout)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
