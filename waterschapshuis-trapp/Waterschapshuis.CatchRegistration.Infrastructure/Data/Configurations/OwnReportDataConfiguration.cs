using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.FieldTest;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.ReportData;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    public class OwnReportDataConfiguration : IEntityTypeConfiguration<OwnReportData>
    {
        public void Configure(EntityTypeBuilder<OwnReportData> builder)
        {
            builder.ToTable(nameof(OwnReportData), "report");
            builder.Ignore(x => x.Id).HasNoKey();
            builder.Property(x => x.CatchTypeName).HasMaxLength(CatchType.NameMaxLength);
            builder.Property(x => x.TrapTypeName).HasMaxLength(TrapType.NameMaxLength);
            builder.Property(x => x.TrappingTypeName).HasMaxLength(TrappingType.NameMaxLength);
            builder.Property(x => x.SubAreaName).HasMaxLength(SubArea.NameMaxLength);
            builder.Property(x => x.CatchAreaName).HasMaxLength(CatchArea.NameMaxLength);
            builder.Property(x => x.RayonName).HasMaxLength(Rayon.NameMaxLength);
            builder.Property(x => x.OrganizationName).HasMaxLength(Organization.NameMaxLength);
            builder.Property(x => x.WaterAuthorityName).HasMaxLength(WaterAuthority.NameMaxLength);
            builder.Property(x => x.HourSquareName).HasMaxLength(HourSquare.NameMaxLength);
            builder.Property(x => x.ProvinceName).HasMaxLength(Province.NameMaxLength);
            builder.Property(x => x.FieldTestName).HasMaxLength(FieldTest.NameMaxLength);
            builder.Property(x => x.OwnerName).HasMaxLength(User.TextualFieldMaxLength);
            builder.Property(x => x.TrapStatus).HasMaxLength(20);
        }
    }
}
