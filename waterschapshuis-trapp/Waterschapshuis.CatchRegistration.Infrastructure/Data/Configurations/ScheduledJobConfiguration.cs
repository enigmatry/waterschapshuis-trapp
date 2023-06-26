using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    public class ScheduledJobConfiguration : IEntityTypeConfiguration<ScheduledJob>
    {
        public void Configure(EntityTypeBuilder<ScheduledJob> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<ScheduledJobName>())
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);
            builder.Property(x => x.Output)
                .HasMaxLength(ScheduledJob.OutputMaxLength);
            ;
        }
    }
}
