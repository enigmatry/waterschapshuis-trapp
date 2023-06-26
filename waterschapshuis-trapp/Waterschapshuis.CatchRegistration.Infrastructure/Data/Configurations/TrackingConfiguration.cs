using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Trackings;
namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TrackingConfiguration : IEntityTypeConfiguration<Tracking>
    {
        public void Configure(EntityTypeBuilder<Tracking> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Location)
                .HasColumnType("geometry");
        }
    }
}
