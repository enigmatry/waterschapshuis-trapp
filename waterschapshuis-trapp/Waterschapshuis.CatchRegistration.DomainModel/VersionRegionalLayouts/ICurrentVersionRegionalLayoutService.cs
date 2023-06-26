using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts
{
    public interface ICurrentVersionRegionalLayoutService
    {
        VersionRegionalLayout Current { get; }
        IQueryable<VersionRegionalLayout> All { get; }

        IQueryable<SubAreaHourSquare> QuerySubAreaHourSquares(params Expression<Func<SubAreaHourSquare, object>>[] paths);
        IQueryable<SubArea> QuerySubAreas(params Expression<Func<SubArea, object>>[] paths);
        IQueryable<CatchArea> QueryCatchAreas(params Expression<Func<CatchArea, object>>[] paths);
        IQueryable<Rayon> QueryRayons(params Expression<Func<Rayon, object>>[] paths);
        IQueryable<WaterAuthority> QueryWaterAuthorities(params Expression<Func<WaterAuthority, object>>[] paths);
        IQueryable<TimeRegistration> QueryTimeRegistrations(params Expression<Func<TimeRegistration, object>>[] paths);


        public IQueryable<SubAreaHourSquare> QuerySubAreaHourSquaresNoTracking(params Expression<Func<SubAreaHourSquare, object>>[] paths) =>
            QuerySubAreaHourSquares(paths).AsNoTracking();

        public IQueryable<SubArea> QuerySubAreasNoTracking(params Expression<Func<SubArea, object>>[] paths) =>
            QuerySubAreas(paths).AsNoTracking();

        public IQueryable<CatchArea> QueryCatchAreasNoTracking(params Expression<Func<CatchArea, object>>[] paths) =>
            QueryCatchAreas(paths).AsNoTracking();

        public IQueryable<Rayon> QueryRayonsNoTracking(params Expression<Func<Rayon, object>>[] paths) =>
            QueryRayons(paths).AsNoTracking();

        public IQueryable<WaterAuthority> QueryWaterAuthoritiesNoTracking(params Expression<Func<WaterAuthority, object>>[] paths) =>
            QueryWaterAuthorities(paths).AsNoTracking();

        public IQueryable<TimeRegistration> QueryTimeRegistrationsNoTracking(params Expression<Func<TimeRegistration, object>>[] paths) =>
            QueryTimeRegistrations(paths).AsNoTracking();
        public SubAreaHourSquare? QuerySingleSubAreaHourSquareAsNoTracking(Guid id);
    }
}
