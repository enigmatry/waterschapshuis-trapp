using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    public class VersionRegionalLayoutImportConfiguration : IEntityTypeConfiguration<VersionRegionalLayoutImport>
    {
        public void Configure(EntityTypeBuilder<VersionRegionalLayoutImport> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.StartedBy)
                .IsRequired()
                .HasMaxLength(User.TextualFieldMaxLength);
            builder.Property(x => x.NextVersionRegionalLayoutName)
                .IsRequired()
                .HasMaxLength(VersionRegionalLayout.NameMaxLength);
            builder.Property(x => x.Output)
                .HasMaxLength(VersionRegionalLayoutImport.OutputMaxMaxLength);;
        }
    }
}
