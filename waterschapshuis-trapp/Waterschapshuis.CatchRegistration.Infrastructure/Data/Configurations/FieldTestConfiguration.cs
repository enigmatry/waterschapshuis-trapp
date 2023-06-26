using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class FieldTestConfiguration : IEntityTypeConfiguration<FieldTest>
    {
        public void Configure(EntityTypeBuilder<FieldTest> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name).HasMaxLength(FieldTest.NameMaxLength);

            builder.OwnsOne(x => x.StartPeriod, cb =>
            {
                cb.Property(x => x.Year)
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasColumnName($"Start{nameof(YearWeekPeriod.Year)}")
                    .HasDefaultValueSql(YearPeriod.DefaultValueYear.ToString());

                cb.Property(x => x.Period)
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasColumnName($"Start{nameof(YearWeekPeriod.Period)}")
                    .HasDefaultValueSql(YearPeriod.DefaultValuePeriod.ToString());
            });

            builder.OwnsOne(x => x.EndPeriod, cb =>
            {
                cb.Property(x => x.Year)
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasColumnName($"End{ nameof(YearWeekPeriod.Year) }")
                    .HasDefaultValueSql(YearPeriod.DefaultValueYear.ToString());

                cb.Property(x => x.Period)
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasColumnName($"End{ nameof(YearWeekPeriod.Period) }")
                    .HasDefaultValueSql(YearPeriod.DefaultValuePeriod.ToString());
            });
        }
    }
}
