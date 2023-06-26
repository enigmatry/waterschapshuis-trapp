using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class RayonConfiguration : IEntityTypeConfiguration<Rayon>
    {
        public void Configure(EntityTypeBuilder<Rayon> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Rayon.NameMaxLength);

            builder.Property(x => x.Geometry)
                .HasColumnType("geometry");
        }
    }
}
