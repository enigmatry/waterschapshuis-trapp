using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class SubAreaHourSquareConfiguration : IEntityTypeConfiguration<SubAreaHourSquare>
    {
        public void Configure(EntityTypeBuilder<SubAreaHourSquare> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.PercentageDitch)
                .HasColumnType("tinyint");

            builder.Property(x => x.Geometry)
                .HasColumnType("geometry");

            builder.HasIndex(x => new { x.SubAreaId, x.HourSquareId })
                .IsUnique();
        }
    }
}
