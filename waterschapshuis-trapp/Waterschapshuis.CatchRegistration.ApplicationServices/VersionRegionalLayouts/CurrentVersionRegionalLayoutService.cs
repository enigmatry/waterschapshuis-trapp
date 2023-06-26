using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.VersionRegionalLayouts
{
    public class CurrentVersionRegionalLayoutService : ICurrentVersionRegionalLayoutService
    {
        private readonly VersionRegionalLayout _current = null!;

        private readonly IRepository<Rayon> _rayonRepository;
        private readonly IRepository<SubArea> _subAreaRepository;
        private readonly IRepository<CatchArea> _catchAreaRepository;
        private readonly IRepository<WaterAuthority> _waterAuthorityRepository;
        private readonly IRepository<DomainModel.TimeRegistrations.TimeRegistration> _timeRegistrationRepository;
        private readonly IRepository<SubAreaHourSquare> _subAreaHourSquareRepository;
        private readonly IRepository<VersionRegionalLayout> _versionRegionalLayoutRepository;

        public CurrentVersionRegionalLayoutService(
            IRepository<Rayon> rayonRepository,
            IRepository<SubArea> subAreaRepository,
            IRepository<CatchArea> catchAreaRepository,
            IRepository<WaterAuthority> waterAuthorityRepository,
            IRepository<DomainModel.TimeRegistrations.TimeRegistration> timeRegistrationRepository,
            IRepository<SubAreaHourSquare> subAreaHourSquareRepository,
            IRepository<VersionRegionalLayout> versionRegionalLayoutRepository)
        {
            _rayonRepository = rayonRepository;
            _subAreaRepository = subAreaRepository;
            _catchAreaRepository = catchAreaRepository;
            _waterAuthorityRepository = waterAuthorityRepository;
            _timeRegistrationRepository = timeRegistrationRepository;
            _subAreaHourSquareRepository = subAreaHourSquareRepository;
            _versionRegionalLayoutRepository = versionRegionalLayoutRepository;

            _current = TryGetCurrentVersionRegionalLayout();
        }

        public VersionRegionalLayout Current => _current;
        public IQueryable<VersionRegionalLayout> All => _versionRegionalLayoutRepository.QueryAll();

        public IQueryable<SubAreaHourSquare> QuerySubAreaHourSquares(params Expression<Func<SubAreaHourSquare, object>>[] paths) =>
            paths.Any()
                ? _subAreaHourSquareRepository.QueryAllIncluding(paths)
                    .QueryByVersionRegionalLayoutId(Current.Id)
                : _subAreaHourSquareRepository.QueryAll()
                    .QueryByVersionRegionalLayoutId(Current.Id);

        public IQueryable<SubArea> QuerySubAreas(params Expression<Func<SubArea, object>>[] paths) =>
            paths.Any()
                ? _subAreaRepository.QueryAllIncluding(paths)
                    .QueryByVersionRegionalLayoutId(Current.Id)
                : _subAreaRepository.QueryAll()
                    .QueryByVersionRegionalLayoutId(Current.Id);

        public IQueryable<CatchArea> QueryCatchAreas(params Expression<Func<CatchArea, object>>[] paths) =>
            paths.Any()
                ? _catchAreaRepository.QueryAllIncluding(paths)
                    .QueryByVersionRegionalLayoutId(Current.Id)
                : _catchAreaRepository.QueryAll()
                    .QueryByVersionRegionalLayoutId(Current.Id);

        public IQueryable<Rayon> QueryRayons(params Expression<Func<Rayon, object>>[] paths) =>
            paths.Any()
                ? _rayonRepository.QueryAllIncluding(paths)
                    .QueryByVersionRegionalLayoutId(Current.Id)
                : _rayonRepository.QueryAll()
                    .QueryByVersionRegionalLayoutId(Current.Id);

        public IQueryable<WaterAuthority> QueryWaterAuthorities(params Expression<Func<WaterAuthority, object>>[] paths) =>
            paths.Any()
                ? _waterAuthorityRepository.QueryAllIncluding(paths)
                    .QueryByVersionRegionalLayoutId(Current.Id)
                : _waterAuthorityRepository.QueryAll()
                    .QueryByVersionRegionalLayoutId(Current.Id);

        public IQueryable<DomainModel.TimeRegistrations.TimeRegistration> QueryTimeRegistrations(
            params Expression<Func<DomainModel.TimeRegistrations.TimeRegistration, object>>[] paths) =>
            paths.Any()
                ? _timeRegistrationRepository.QueryAllIncluding(paths)
                    .QueryByVersionRegionalLayoutId(Current.Id)
                : _timeRegistrationRepository.QueryAll()
                    .QueryByVersionRegionalLayoutId(Current.Id);

        public SubAreaHourSquare? QuerySingleSubAreaHourSquareAsNoTracking(Guid sahsId) =>
            _subAreaHourSquareRepository.QueryAllIncluding(x => x.HourSquare, x => x.SubArea)
            .AsNoTracking()
            .QueryById(sahsId);

        private VersionRegionalLayout TryGetCurrentVersionRegionalLayout() =>
            _versionRegionalLayoutRepository
                .QueryAll().AsNoTracking()
                .TryGetCurrentlyActive()
                    ?? throw new InvalidOperationException($"Could not find currently active {nameof(VersionRegionalLayout)}");

    }
}
