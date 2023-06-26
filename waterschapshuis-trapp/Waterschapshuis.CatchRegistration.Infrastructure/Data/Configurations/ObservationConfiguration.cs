using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Observations;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class ObservationConfiguration : IEntityTypeConfiguration<Observation>
    {
        public void Configure(EntityTypeBuilder<Observation> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.PhotoUrl)
                .HasMaxLength(Observation.PhotoUrlFieldMaxLength);

            builder.Property(x => x.Remarks)
                .IsRequired()
                .HasMaxLength(Observation.RemarkFieldMaxLength);

            builder.Property(x => x.Type)
                .HasConversion(x => (int)x, x => (ObservationType)x)
                .HasColumnType("tinyint");

            builder.Property(x => x.Location)
                .HasColumnType("geometry");
        }
    }
}
