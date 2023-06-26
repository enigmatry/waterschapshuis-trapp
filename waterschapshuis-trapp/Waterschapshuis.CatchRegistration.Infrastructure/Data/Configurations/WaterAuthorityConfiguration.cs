using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class WaterAuthorityConfiguration : IEntityTypeConfiguration<WaterAuthority>
    {
        public void Configure(EntityTypeBuilder<WaterAuthority> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(WaterAuthority.NameMaxLength);

            builder.Property(x => x.CodeUvw)
                .HasMaxLength(WaterAuthority.CodeUvwMaxLength);

            builder.Property(x => x.Geometry)
                .HasColumnType("geometry");

            builder.HasMany(x => x.SubAreas)
                .WithOne(x => x.WaterAuthority)
                .HasForeignKey(x => x.WaterAuthorityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
