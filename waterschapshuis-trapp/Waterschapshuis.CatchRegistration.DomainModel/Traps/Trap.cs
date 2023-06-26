using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Anonymization;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.DomainEvents;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public class Trap : EntityHasCreatedUpdatedRecorded<Guid>, IAnonymizeCreatedUpdatedBy, IHasLocation
    {
        public const int ExternalIdMaxLength = 50;
        public const int RemarksMaxLength = 250;
        private readonly List<Catch> _catches = new List<Catch>();
        private readonly List<TrapHistory> _trapHistories = new List<TrapHistory>();
        private readonly List<TrapSubAreaHourSquare> _previousVersionSubAreaHourSquares = new List<TrapSubAreaHourSquare>();

        private Trap() { }

        public User CreatedBy { get; } = null!;
        public User UpdatedBy { get; } = null!;
        public string? ExternalId { get; private set; }
        public Point Location { get; private set; } = Point.Empty;
        public int NumberOfTraps { get; private set; }
        public string? Remarks { get; private set; }
        public DateTimeOffset? RemovedOn { get; private set; }
        public TrapStatus Status { get; private set; }
        public Province Province { get; private set; } = null!;
        public Guid? ProvinceId { get; private set; }
        public SubAreaHourSquare SubAreaHourSquare { get; private set; } = null!;
        public Guid SubAreaHourSquareId { get; private set; } = Guid.Empty;
        public TrapType TrapType { get; private set; } = null!;
        public Guid TrapTypeId { get; private set; } = Guid.Empty;
        public IReadOnlyCollection<Catch> Catches => _catches.AsReadOnly();
        public IReadOnlyCollection<TrapHistory> TrapHistories => _trapHistories.AsReadOnly();
        public IReadOnlyCollection<TrapSubAreaHourSquare> PreviousVersionSubAreaHourSquares => _previousVersionSubAreaHourSquares.AsReadOnly();

        public int CatchingNights { get; private set; }

        public static Trap Create(
            TrapCreateOrUpdate.Command command,
            SubAreaHourSquare subAreaHourSquare,
            Province province,
            List<CatchType> catchTypes)
        {
            var result = new Trap
            {
                Id = command.Id == Guid.Empty ? GenerateId() : command.Id,
                Remarks = command.Remarks,
                NumberOfTraps = command.NumberOfTraps,
                Status = command.Status,
                TrapTypeId = command.TrapTypeId,
            }
            .WithLocation(command.Longitude, command.Latitude)
            .WithSubAreaHourSquare(subAreaHourSquare)
            .WithProvince(province);
            result.SetRecordedWithTimeReset(command.RecordedOn);

            result.AddDomainEvent(TrapHistoryDomainEvent.OnTrapCreate(result)); // Must go before WithCatches()!
            result.WithCatches(command.Catches, catchTypes);
            result.AddDomainEvent(new TrapCreatedDomainEvent(result));

            return result;
        }

        public static Trap Create(TrapImport import)
        {
            var result = new Trap
            {
                Id = GenerateId(),
                Remarks = import.Remarks,
                NumberOfTraps = import.NumberOfTraps,
                Status = import.Status,
                TrapTypeId = import.TrapTypeId,
                ExternalId = import.ExternalId
            }
            .WithLocation(import.Longitude, import.Latitude)
            .WithSubAreaHourSquare(import.SubAreaHourSquare)
            .WithProvince(import.Province);

            result.AddDomainEvent(new TrapCreatedDomainEvent(result));

            return result;
        }

        public void Update(
            TrapCreateOrUpdate.Command command,
            SubAreaHourSquare subAreaHourSquare,
            Province province,
            List<CatchType> catchTypes)
        {
            AddDomainEvent(TrapHistoryDomainEvent.OnTrapUpdate(this, command)); // Must go first, before state change!

            if (Catching)
            {
                if (NoCaches && CreatedToday)
                {
                    TrapTypeId = command.TrapTypeId;
                }
                NumberOfTraps = command.NumberOfTraps;
                WithLocation(command.Longitude, command.Latitude);
                WithProvince(province);
                WithSubAreaHourSquare(subAreaHourSquare);
                WithCatches(command.Catches, catchTypes);
            }
            if (NotRemoved)
            {
                Status = command.Status;
            }
            Remarks = command.Remarks;

            AddDomainEvent(new TrapUpdatedDomainEvent(this));
        }

        public void Update(TrapUpdate.Command command)
        {
            AddDomainEvent(TrapHistoryDomainEvent.OnTrapUpdate(this, command)); // Must go first, before state change!

            if (Catching && NoCaches && CreatedToday)
            {
                TrapTypeId = command.TrapTypeId;
            }
            if (NoCaches)
            {
                WithLocation(command.Longitude, command.Latitude);
            }
            Remarks = command.Remarks;
            Status = command.Status;

            AddDomainEvent(new TrapUpdatedDomainEvent(this));
        }

        public bool Catching => Status == TrapStatus.Catching;
        public bool NotRemoved => Status != TrapStatus.Removed;
        public bool NoCaches => !Catches.Any();
        public bool CreatedToday => DateTimeOffset.Now.Date == CreatedOn.Date;

        [NotMapped]
        public Guid? LocationOrganizationId => SubAreaHourSquare?.SubArea?.CatchArea?.Rayon?.OrganizationId;


        private Trap WithSubAreaHourSquare(SubAreaHourSquare subAreaHourSquare)
        {
            SubAreaHourSquare = subAreaHourSquare;
            SubAreaHourSquareId = subAreaHourSquare.Id;
            return this;
        }

        private Trap WithProvince(Province province)
        {
            Province = province;
            ProvinceId = province.Id;
            return this;
        }

        private Trap WithLocation(double longitude, double latitude)
        {
            Location = GeometryUtil.Factory.CreatePoint(longitude, latitude);
            return this;
        }

        private Trap WithCatches(IEnumerable<CatchCreateOrUpdate.Command> commands, List<CatchType> catchTypes)
        {
            commands
                .Where(c => !c.MarkedForRemoval)
                .Select(c => Catch.Create(c, catchTypes.Single(ct => ct.Id == c.CatchTypeId)))
                .ToList()
                .ForEach(newCatch => _catches.Add(newCatch));

            commands
                .Where(c => c.MarkedForRemoval)
                .Select(c => c.Id)
                .ToList()
                .ForEach(catchToRemoveId => RemoveCatch(catchToRemoveId));

            return this;
        }

        public void RemoveCatch(Guid catchToRemoveId)
        {
            var catchForRemoval = _catches.Single(c => c.Id == catchToRemoveId);
            AddDomainEvent(TrapHistoryDomainEvent.OnCatchRemove(catchForRemoval));
            AddDomainEvent(new CatchDeletedDomainEvent(catchForRemoval));
            _catches.Remove(catchForRemoval);
        }

        public void IncreaseCatchingNights()
        {
            if (Catching)
            {
                CatchingNights += NumberOfTraps;
            }
        }

        public override void SetRecorded(DateTimeOffset recordedOn)
        {
            base.SetRecorded(recordedOn);
            SetCatchingNightsWhenRecordedOnChanged();
        }

        private void SetCatchingNightsWhenRecordedOnChanged()
        {
            if (!Catching)
            {
                return;
            }

            CatchingNights = (int)(DateTimeOffset.Now - RecordedOn).TotalDays * NumberOfTraps;
        }

        public void InsertNewVersionOfSubAreaHourSquareId(Guid id)
        {
            _previousVersionSubAreaHourSquares.Add(TrapSubAreaHourSquare.Create(Id, SubAreaHourSquareId));
            SubAreaHourSquareId = id;
        }
    }
}
