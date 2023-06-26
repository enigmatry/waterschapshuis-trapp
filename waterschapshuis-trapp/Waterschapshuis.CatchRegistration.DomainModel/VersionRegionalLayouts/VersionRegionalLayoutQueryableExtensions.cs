using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts
{
    public static class VersionRegionalLayoutQueryableExtensions
    {
        public static async Task<VersionRegionalLayout> TryGetCurrentlyActiveAsync(this IQueryable<VersionRegionalLayout> query, CancellationToken cancellationToken) =>
            await query
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync(cancellationToken);

        public static VersionRegionalLayout TryGetCurrentlyActive(this IQueryable<VersionRegionalLayout> query) =>
            query
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefault();

        public static bool NameIsUnique(this IQueryable<VersionRegionalLayout> query, string name) =>
            !query.Any(x => x.Name == name);
    }
}
