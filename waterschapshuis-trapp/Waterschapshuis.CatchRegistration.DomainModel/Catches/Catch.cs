using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Anonymization;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.DomainEvents;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches
{
    public class Catch : EntityHasCreatedUpdatedRecorded<Guid>, ICurrentUserRoleAwareEntity, IAnonymizeCreatedUpdatedBy
    {
        private readonly List<TrapHistory> _trapHistories = new List<TrapHistory>();

        private Catch() { }

        public User CreatedBy { get; private set; } = null!;
        public User UpdatedBy { get; private set; } = null!;
        public Guid TrapId { get; private set; } = Guid.Empty;
        public Trap Trap { get; private set; } = null!;
        public Guid CatchTypeId { get; private set; } = Guid.Empty;
        public CatchType CatchType { get; private set; } = null!;
        public int Number { get; private set; }
        public YearWeekPeriod WeekPeriod { get; private set; } = YearWeekPeriod.Default();
        public CatchStatus Status { get; private set; }
        public IReadOnlyCollection<TrapHistory> TrapHistories => _trapHistories.AsReadOnly();

        public bool CreatedToday(ITimeProvider timeProvider) => timeProvider.Now.Date == CreatedOn.Date;

        public static Catch Create(CatchCreateOrUpdate.Command command, CatchType catchType)
        {
            var result = new Catch
            {
                Id = command.Id == Guid.Empty ? GenerateId() : command.Id,
                TrapId = command.TrapId,
                CatchTypeId = catchType.Id,
                CatchType = catchType,
                Number = command.Number,
                Status = command.Status
            };
            result.SetRecordedWithTimeReset(command.RecordedOn);

            result.AddDomainEvent(TrapHistoryDomainEvent.OnCatchCreate(result));
            result.AddDomainEvent(new CatchCreatedDomainEvent(result));

            return result;
        }

        public static Catch Create(CatchImport import)
        {
            var result = new Catch
            {
                Id = GenerateId(),
                TrapId = import.TrapId,
                CatchTypeId = import.CatchTypeId,
                Number = import.Number,
                Status = import.Status
            };

            result.AddDomainEvent(new CatchCreatedDomainEvent(result));

            return result;
        }

        public void Update(CatchCreateOrUpdate.Command command, CatchType catchType, CatchStatus? status = null)
        {
            if(command.Status != CatchStatus.Closed && command.Status != CatchStatus.Completed)
                AddDomainEvent(TrapHistoryDomainEvent.OnCatchUpdate(this, command, catchType)); // Must go first, before state change!

            Status = status ?? command.Status;

            AddDomainEvent(new CatchUpdatedDomainEvent(this));
        }

        public Catch WithTrap(Trap trap)
        {
            Trap = trap;
            TrapId = trap.Id;
            return this;
        }

        public Catch WithCatchType(CatchType catchType)
        {
            CatchType = catchType;
            CatchTypeId = catchType.Id;
            return this;
        }

        public Catch UpdateStatus(CatchStatus status)
        {
            Status = status;
            return this;
        }

        public override void SetRecorded(DateTimeOffset recordedOn)
        {
            base.SetRecorded(recordedOn);
            WeekPeriod = YearWeekPeriod.Create(recordedOn);
        }

        public int NumberOfCatches => CatchType.IsByCatch ? 0 : Number;
        public int NumberOfByCatches => CatchType.IsByCatch ? Number : 0;

        public Guid? LocationOrganizationId => Trap?.LocationOrganizationId;

        public bool CanUpdateFromBackOffice(List<Guid> userRoleIds, List<PermissionId> userPermissionIds) =>
            userPermissionIds.Contains(PermissionId.MapContentWrite) &&
            (
                (Status == CatchStatus.Written && userRoleIds.Contains(Role.TrapperRoleId)) ||
                (Status == CatchStatus.Closed && !userRoleIds.Contains(Role.TrapperRoleId))
            );

        public bool CanBeRemovedFromMobile(Guid? currentUserId, ITimeProvider timeProvider) =>
            CreatedById == currentUserId && CreatedToday(timeProvider);
    }
}
