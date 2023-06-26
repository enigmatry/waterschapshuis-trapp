using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class CatchConfiguration : IEntityTypeConfiguration<Catch>
    {
        public void Configure(EntityTypeBuilder<Catch> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.OwnsOne(x => x.WeekPeriod, cb =>
                {
                    cb.Property(x => x.Year)
                        .IsRequired()
                        .ValueGeneratedNever()
                        .HasColumnName(nameof(YearWeekPeriod.Year))
                        .HasDefaultValueSql(YearPeriod.DefaultValueYear.ToString());

                    cb.Property(x => x.Week)
                        .IsRequired()
                        .ValueGeneratedNever()
                        .HasColumnName(nameof(YearWeekPeriod.Week))
                        .HasDefaultValueSql(YearPeriod.DefaultValueWeek.ToString());

                    cb.Property(x => x.Period)
                        .IsRequired()
                        .ValueGeneratedNever()
                        .HasColumnName(nameof(YearWeekPeriod.Period))
                        .HasDefaultValueSql(YearPeriod.DefaultValuePeriod.ToString());
                });

            builder.Property(x => x.Status)
                .HasConversion(x => (int)x, x => (CatchStatus)x)
                .HasColumnType("tinyint");

            builder.Property(x => x.RecordedOn)
                .HasDefaultValueSql("getutcdate()");

            builder.HasMany(x => x.TrapHistories)
                .WithOne(x => x.Catch)
                .HasForeignKey(x => x.CatchId);

            builder.HasIndex(x => x.RecordedOn)
             .HasName("IX_RecordedOn");
        }
    }
}
