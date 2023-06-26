using System;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches
{
    public class CatchBuilder
    {
        private CatchType _catchType = new CatchTypeBuilder()
            .WithId(CatchType.BeverratMoerOudId)
            .WithIsByCatch(true)
            .WithName("Beverrat moer oud (>1jr)");
        private int _numberOfCatches;
        private Guid _id = Guid.NewGuid();
        private Guid _createdById = Guid.NewGuid();
        private CatchStatus _status = CatchStatus.Written;
        private DateTimeOffset _recordedOn;
        private DateTimeOffset _createdOn = DateTimeOffset.Now;

        private string _organizationName = String.Empty;
        private string _rayonName = String.Empty;
        private string _catchAreaName = String.Empty;
        private string _subAreaName = String.Empty;
        private string _hourSquareName = String.Empty;
        private string _waterAuthorityName = String.Empty;

        private Trap? _trap = null;

        public CatchBuilder WithGeoHierarchy(
            string organizationName,
            string rayonName,
            string catchAreaName,
            string subAreaName,
            string waterAuthorityName,
            string hourSquareName)
        {
            _organizationName = organizationName;
            _rayonName = rayonName;
            _catchAreaName = catchAreaName;
            _subAreaName = subAreaName;
            _waterAuthorityName = waterAuthorityName;
            _hourSquareName = hourSquareName;
            return this;
        }

        public CatchBuilder WithCatchType(CatchType value)
        {
            _catchType = value;
            return this;
        }

        public CatchBuilder WithCreatedById(Guid value)
        {
            _createdById = value;
            return this;
        }

        public CatchBuilder WithNumberOfCatches(int value)
        {
            _numberOfCatches = value;
            return this;
        }

        public CatchBuilder WithTrap(Trap value)
        {
            _trap = value;
            return this;
        }

        public CatchBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public CatchBuilder WithStatus(CatchStatus value)
        {
            _status = value;
            return this;
        }

        public CatchBuilder WithRecordedOn(DateTimeOffset value)
        {
            _recordedOn = value;
            return this;
        }

        public CatchBuilder WithCreatedOn(DateTimeOffset value)
        {
            _createdOn = value;
            return this;
        }

        public static implicit operator Catch(CatchBuilder builder)
        {
            return builder.Build();
        }

        private Catch Build()
        {
            Trap trap = _trap ?? new TrapBuilder()
                .WithGeoHierarchy(_organizationName, _rayonName, _catchAreaName, _subAreaName, _waterAuthorityName, _hourSquareName);

            var result = Catch
                .Create(
                    new CatchCreateOrUpdate.Command
                    {
                        Id = _id,
                        TrapId = trap.Id,
                        CatchTypeId = _catchType.Id,
                        Number = _numberOfCatches,
                        RecordedOn = _recordedOn,
                        Status = _status
                    },
                    _catchType
                )
                .WithTrap(trap);
            result.SetCreated(_createdOn, _createdById);

            return result;
        }
    }
}
