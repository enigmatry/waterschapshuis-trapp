using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class TimeRegistrationGeneralConfiguration : IEntityTypeConfiguration<TimeRegistrationGeneral>
    {
        public void Configure(EntityTypeBuilder<TimeRegistrationGeneral> builder)
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

            builder.Property(x => x.Date)
                .HasConversion(x => x.Date, x=> new DateTimeOffset(x))
                .HasColumnType("date");

            builder.Property(x => x.CreatedById)
                .HasDefaultValue(User.SystemUserId);

            builder.Property(x => x.UpdatedById)
                .HasDefaultValue(User.SystemUserId);

            builder.Property(x => x.Status)
                .HasConversion(x => (int)x, x => (TimeRegistrationStatus)x)
                .HasColumnType("tinyint");

            builder.HasIndex(x => x.Date)
                .HasName("IX_Date");
        }
    }
}
