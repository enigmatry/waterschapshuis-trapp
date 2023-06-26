using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class SubAreaConfiguration : IEntityTypeConfiguration<SubArea>
    {
        public void Configure(EntityTypeBuilder<SubArea> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SubArea.NameMaxLength);

            builder.Property(x => x.Geometry)
                .HasColumnType("geometry");
        }
    }
}
