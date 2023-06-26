using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates;
using Waterschapshuis.CatchRegistration.DomainModel.Templates;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.Configurations
{
    [UsedImplicitly]
    public class ReportTemplateConfiguration : IEntityTypeConfiguration<ReportTemplate>
    {
        public void Configure(EntityTypeBuilder<ReportTemplate> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Type)
                .HasConversion(x => (int)x, x => (ReportTemplateType)x)
                .HasColumnType("tinyint");

            builder.Property(x => x.Title)
                .HasMaxLength(ReportTemplate.TitleMaxLength);

            builder.Property(x => x.Group)
                .HasMaxLength(ReportTemplate.GroupMaxLength);

            builder.Property(x => x.RouteUri)
                .HasMaxLength(ReportTemplate.RouteUriMaxLength);

            builder.Property(x => x.Key)
                .HasMaxLength(ReportTemplate.KeyMaxLength);

            builder.Property(x => x.ExportFileName)
                .HasMaxLength(ReportTemplate.ExportFileNameMaxLength);
        }
    }
}
