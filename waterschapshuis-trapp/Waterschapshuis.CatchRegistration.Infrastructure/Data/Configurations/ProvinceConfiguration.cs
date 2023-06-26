using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Province.NameMaxLength);

            builder.Property(x => x.Geometry)
                .HasColumnType("geometry");

            builder.HasMany(x => x.Traps)
                .WithOne(x => x.Province)
                .HasForeignKey(x => x.ProvinceId);
        }
    }
}
