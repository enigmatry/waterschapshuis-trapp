using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    public class LocationAreaDataSummaryConfiguration : IEntityTypeConfiguration<LocationAreaDataSummary>
    {
        public void Configure(EntityTypeBuilder<LocationAreaDataSummary> builder)
        {
            builder.HasNoKey().ToView(null);
        }
    }
}
