using System.Xml.XPath;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class HourSquareConfiguration : IEntityTypeConfiguration<HourSquare>
    {
        public void Configure(EntityTypeBuilder<HourSquare> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(HourSquare.NameMaxLength);

            builder.Property(x => x.Geometry)
                .HasColumnType("geometry");

            // Prediction model configuration
            builder.OwnsOne(x => x.PredictionModel, xb =>
            {
                // Replace PredictionModel_* column names.
                xb.Property(xp => xp!.CpeRecent)
                    .HasColumnName(nameof(PredictionModel.CpeRecent));

                xb.Property(xp => xp!.CpeAB)
                    .HasColumnName(nameof(PredictionModel.CpeAB));

                xb.Property(xp => xp!.PopulationPresent)
                    .HasColumnName(nameof(PredictionModel.PopulationPresent));

                xb.Property(xp => xp!.R2)
                    .HasColumnName(nameof(PredictionModel.R2));
            });

            builder.HasMany(x => x.FieldTestHourSquare).WithOne(x => x.HourSquare).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
