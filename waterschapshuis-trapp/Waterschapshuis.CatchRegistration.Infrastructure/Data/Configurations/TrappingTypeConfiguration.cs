using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TrappingTypeConfiguration : IEntityTypeConfiguration<TrappingType>
    {
        public void Configure(EntityTypeBuilder<TrappingType> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(TrappingType.NameMaxLength);
        }
    }
}
