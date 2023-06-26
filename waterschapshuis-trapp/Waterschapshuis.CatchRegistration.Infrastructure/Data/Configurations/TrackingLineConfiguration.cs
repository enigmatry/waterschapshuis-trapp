using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TrackingLineConfiguration : IEntityTypeConfiguration<TrackingLine>
    {
        public void Configure(EntityTypeBuilder<TrackingLine> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Date)
                .HasConversion(x => x.Date, x=> new DateTimeOffset(x))
                .HasColumnType("date");

            builder.Property(x => x.Polyline)
                .HasColumnType("geometry");
        }
    }
}
