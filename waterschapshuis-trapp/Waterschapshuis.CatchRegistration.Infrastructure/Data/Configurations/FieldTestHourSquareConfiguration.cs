using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest;


namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class FieldTestHourSquareConfiguration : IEntityTypeConfiguration<FieldTestHourSquare>
    {
        public void Configure(EntityTypeBuilder<FieldTestHourSquare> builder)
        {
            builder.HasKey(rp => new { rp.FieldTestId, rp.HourSquareId });
        }
    }
}
