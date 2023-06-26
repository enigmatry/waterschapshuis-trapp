using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class CatchAreaConfiguration : IEntityTypeConfiguration<CatchArea>
    {
        public void Configure(EntityTypeBuilder<CatchArea> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CatchArea.NameMaxLength);

            builder.Property(x => x.Geometry)
                .HasColumnType("geometry");
        }
    }
}
