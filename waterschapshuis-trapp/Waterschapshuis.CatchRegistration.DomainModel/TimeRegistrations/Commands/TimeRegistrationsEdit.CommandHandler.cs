using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Commands
{
    public static partial class TimeRegistrationsEdit
    {
        [UsedImplicitly]
        public class TimeRegistrationsEditCommandHandler : IRequestHandler<Command, Result>
        {
            private readonly ICurrentUserIdProvider _currentUserIdProvider;
            private readonly ICurrentUserProvider _currentUserProvider;
            private readonly IRepository<TimeRegistration> _timeRegistrationRepository;
            private readonly IRepository<TimeRegistrations.TimeRegistrationGeneral> _timeRegistrationGeneralRepository;
            private readonly IRepository<Catch> _catchRegistrationRepository;
            private readonly IRepository<User> _userRepository;
            private readonly IRepository<CatchType> _catchTypeRepository;
            private readonly IRepository<SubArea> _subAreaRepository;
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

            public TimeRegistrationsEditCommandHandler(
                ICurrentUserIdProvider currentUserIdProvider,
                IRepository<TimeRegistration> timeRegistrationRepository,
                IRepository<TimeRegistrations.TimeRegistrationGeneral> timeRegistrationGeneralRepository,
                IRepository<Catch> catchRegistrationRepository,
                IRepository<User> userRepository,
                IRepository<CatchType> catchTypeRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService,
                ICurrentUserProvider currentUserProvider,
                IRepository<SubArea> subAreaRepository)
            {
                _currentUserIdProvider = currentUserIdProvider;
                _timeRegistrationRepository = timeRegistrationRepository;
                _catchRegistrationRepository = catchRegistrationRepository;
                _userRepository = userRepository;
                _catchTypeRepository = catchTypeRepository;
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;
                _timeRegistrationGeneralRepository = timeRegistrationGeneralRepository;
                _currentUserProvider = currentUserProvider;
                _subAreaRepository = subAreaRepository;
            }

            public Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = request.GetUserId() ?? _currentUserIdProvider.FindUserId(_userRepository.QueryAll()) ??
                   throw new InvalidOperationException("Cannot find user id.");

                HandleTimeRegistrationItems(request, userId);

                HandleTimeRegistrationGeneralItems(request, userId);

                HandleCatchItems(request);

                return Task.FromResult(new Result
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    UserId = userId,
                    RayonId = request.GetRayonId()
                });
            }

            private void HandleTimeRegistrationItems(Command request, Guid userId)
            {
                var current = _currentVersionRegionalLayoutService
                    .QueryTimeRegistrations(x => x.SubAreaHourSquare.SubArea.CatchArea.Rayon)
                    .QueryByUser(userId)
                    .QueryByDateRangeExclusiveEnd(request.StartDate, request.EndDate)
                    .QueryByOptionalSubAreaHourSquareId(request.SubAreaHourSquareId);

                var isManager = request.IsManagerEnteringDataForUser();
                // filter so other organization data don't get removed
                if (isManager && request.GetRayonId().HasValue)
                {
                    current = current.QueryByRayon(request.GetRayonId()!.Value);
                }


                if (current.Any() && current.AsEnumerable().All(x => !CanBeUpdated(x.Status, isManager)))
                    return;

                var removed = current
                    .AsEnumerable()
                    .RemovedTimeRegistrations(request.DaysOfWeek.SelectMany(x => x.Items), isManager)
                    .ToList();

                removed.ForEach(x => x.AddDeletedEvent());

                _timeRegistrationRepository.DeleteRange(removed);
                var subAreasIds = Enumerable.Empty<Guid>();
                if (isManager && _currentUserProvider.Organization != null)
                {
                    subAreasIds = _subAreaRepository.QueryAll().AsNoTracking()
                        .Where(x => x.CatchArea.Rayon.OrganizationId == _currentUserProvider.Organization.Id)
                        .Select(x => x.Id);
                }
                IQueryable<SubAreaHourSquare> currentVersionSubAreaHourSquares = _currentVersionRegionalLayoutService
                                                .QuerySubAreaHourSquares()
                                                .AsNoTracking();

                foreach (var dayOfWeekItem in request.DaysOfWeek)
                {
                    foreach (var item in dayOfWeekItem.Items)
                    {
                        if (isManager && _currentUserProvider.Organization != null)
                        {
                            if (!subAreasIds.Contains(item.SubAreaId))
                                continue;
                        }

                        var subAreaHourSquare = currentVersionSubAreaHourSquares
                        .FindBySubAreaAndHourSquare(item.SubAreaId, item.HourSquareId)
                            ?? throw new InvalidOperationException($"Cannot find SubAreaHourSquare for SubArea: { item.SubAreaId } and HourSquare: { item.HourSquareId }");

                        var existing = current.GetExisting(item);

                        if (existing != null)
                        {
                            if (item.Status == TimeRegistrationStatus.Written && (existing.Status == TimeRegistrationStatus.Closed || existing.Status == TimeRegistrationStatus.Completed))
                                throw new InvalidOperationException($"Status can not be updated from: { existing.Status } into { item.Status }");

                            if (existing.HasChanges(subAreaHourSquare.Id, item.TrappingTypeId, item.Status, item.GetHours()) && CanBeUpdated(existing.Status, isManager))
                            {
                                existing.Update(
                                    subAreaHourSquare.Id,
                                    item.TrappingTypeId,
                                    item.GetHours(),
                                    item.Status);
                            }
                        }
                        else
                        {
                            _timeRegistrationRepository.Add(TimeRegistration.Create(
                                userId,
                                subAreaHourSquare.Id,
                                item.TrappingTypeId,
                                dayOfWeekItem.Date,
                                item.GetHours(),
                                item.Status,
                                false));
                        }
                    }
                }
            }

            private void HandleTimeRegistrationGeneralItems(Command request, Guid userId)
            {
                var currentTimeRegistrationGeneral =
                    _timeRegistrationGeneralRepository
                        .QueryAll()
                        .QueryByUser(userId)
                        .QueryByDateRangeExclusiveEnd(request.StartDate, request.EndDate)
                        .ToList();
                var isManager = request.IsManagerEnteringDataForUser();
                var user = _userRepository.QueryAll().FirstOrDefault(x => x.Id == userId);
                if (currentTimeRegistrationGeneral.Any() && currentTimeRegistrationGeneral.AsEnumerable().All(x => !CanBeUpdated(x.Status, isManager)) ||
                    (isManager && _currentUserProvider.Organization?.Id != user.OrganizationId))
                    return;

                var removedGeneral = currentTimeRegistrationGeneral
                    .RemovedTimeRegistrationGeneral(request.TimeRegistrationGeneralItems, isManager);

                _timeRegistrationGeneralRepository.DeleteRange(removedGeneral);

                foreach (var item in request.TimeRegistrationGeneralItems)
                {
                    var existing = currentTimeRegistrationGeneral.GetExisting(item);

                    if (existing != null)
                    {
                        if (existing.HasChanges(item.CategoryId, item.Status, item.GetHours()) && CanBeUpdated(existing.Status, isManager))
                        {
                            existing.Update(item.CategoryId, item.GetHours(), item.Status);
                        }
                    }
                    else
                    {
                        _timeRegistrationGeneralRepository.Add(
                            TimeRegistrations.TimeRegistrationGeneral.Create(userId, item.CategoryId, item.Date, item.GetHours(), item.Status));
                    }
                }
            }

            private void HandleCatchItems(Command request)
            {
                var catches = _catchRegistrationRepository
                    .QueryAll()
                    .Where(x => request.Catches.Select(c => c.Id).Contains(x.Id));
                var catchTypes = _catchTypeRepository
                    .QueryAll()
                    .QueryByIds(request.Catches.Select(x => x.CatchTypeId).Distinct().ToArray())
                    .ToList();

                var isManager = request.IsManagerEnteringDataForUser();
                if (catches.Any() && catches.AsEnumerable().All(x => !CatchCanBeUpdated(x.Status, isManager)))
                    return;

                foreach (var catchItem in request.Catches)
                {
                    var existingCatch = catches.Single(x => x.Id == catchItem.Id);
                    if (existingCatch != null && CatchCanBeUpdated(existingCatch.Status, isManager))
                    {
                        var updateCommand = new CatchCreateOrUpdate.Command
                        {
                            Status = catchItem.Status
                        };
                        existingCatch.Update(updateCommand, catchTypes.Single(x => x.Id == catchItem.CatchTypeId));
                    }
                }
            }
        }

        private static IEnumerable<TimeRegistration> RemovedTimeRegistrations(
            this IEnumerable<TimeRegistration> current,
            IEnumerable<Command.Item> commandItems, bool isManagerEnteringDataForUser) =>
                current.Where(x => commandItems.All(e => e.Id != x.Id)
                                                         && CanBeUpdated(x.Status, isManagerEnteringDataForUser));

        private static IEnumerable<TimeRegistrations.TimeRegistrationGeneral> RemovedTimeRegistrationGeneral(
            this IEnumerable<TimeRegistrations.TimeRegistrationGeneral> current,
            IEnumerable<TimeRegistrationGeneral> commandItems, bool isManagerEnteringDataForUser) =>
                current.Where(x => commandItems.All(e => e.Id != x.Id)
                                                         && CanBeUpdated(x.Status, isManagerEnteringDataForUser));


        private static TimeRegistration? GetExisting(
            this IEnumerable<TimeRegistration> current,
            Command.Item commandItem) =>
                current.SingleOrDefault(x => x.Id == commandItem.Id);

        private static TimeRegistrations.TimeRegistrationGeneral? GetExisting(
            this IEnumerable<TimeRegistrations.TimeRegistrationGeneral> current,
            TimeRegistrationGeneral commandItem) =>
                current.SingleOrDefault(x => x.Id == commandItem.Id);

        private static bool HasChanges(
            this TimeRegistration current,
            Guid subAreaHourSquareId,
            Guid trappingTypeId,
            TimeRegistrationStatus status,
            double hours) =>
                current.SubAreaHourSquareId != subAreaHourSquareId ||
                current.TrappingTypeId != trappingTypeId ||
                current.Status != status ||
                !current.Hours.Equals(hours);

        private static bool HasChanges(
            this TimeRegistrations.TimeRegistrationGeneral current,
            Guid categoryId,
            TimeRegistrationStatus status,
            double hours) =>
                current.TimeRegistrationCategoryId != categoryId ||
                current.Status != status ||
                !current.Hours.Equals(hours);

        private static bool CanBeUpdated(
            TimeRegistrationStatus status,
            bool isManagerEnteringDataForUser) =>
                status != TimeRegistrationStatus.Completed && (status != TimeRegistrationStatus.Closed || isManagerEnteringDataForUser);

        private static bool CatchCanBeUpdated(
            CatchStatus status,
            bool isManagerEnteringDataForUser) =>
                status != CatchStatus.Completed && (status != CatchStatus.Closed || isManagerEnteringDataForUser);

    }
}
