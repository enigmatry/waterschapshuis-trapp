using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.WaterAreas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    public class WaterPlaneConfiguration : IEntityTypeConfiguration<WaterPlane>
    {
        public void Configure(EntityTypeBuilder<WaterPlane> builder)
        {
            builder.ToTable("water_planes");

            builder.Ignore(x => x.Id).HasNoKey();

            builder.Property(x => x.LocalId)
                .HasColumnName("lokaalid");
            builder.Property(x => x.Geometry)
                .HasColumnType("geometry")
                .HasColumnName("ogr_geometry");
            builder.Property(x => x.Type)
                .HasColumnName("typewater");
        }
    }
}
