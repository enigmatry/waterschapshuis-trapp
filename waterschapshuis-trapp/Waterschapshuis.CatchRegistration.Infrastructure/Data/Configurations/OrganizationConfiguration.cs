using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Organization.NameMaxLength);

            builder.Property(x => x.ShortName)
                .IsRequired()
                .HasMaxLength(Organization.ShortNameMaxLength);

            builder.Property(x => x.Geometry)
                .HasColumnType("geometry");
        }
    }
}
