using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public static class ReportTemplateQueryableExtensions
    {
        public static IQueryable<ReportTemplate> QueryActive(this IQueryable<ReportTemplate> query) 
            => query.Where(e => e.Active);
        public static IQueryable<ReportTemplate> QueryByExported(this IQueryable<ReportTemplate> query, bool exported)
            => query.Where(e => e.Exported == exported);
    }
}
