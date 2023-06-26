using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Provinces;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.SubAreaHourSquares;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Traps
{
    public class TrapBuilder
    {
        private string _organizationName = String.Empty;
        private string _rayonName = String.Empty;
        private string _catchAreaName = String.Empty;
        private string _subAreaName = String.Empty;
        private string _hourSquareName = String.Empty;
        private string _waterAuthorityName = String.Empty;
        private SubAreaHourSquare _subAreaHourSquare = null!;
        private Province _province = null!;

        private Guid _id = Guid.NewGuid();
        private Guid _trapTypeId = TrapType.ConibearBeverratId;
        private int _numberOfTraps = 0;
        private TrapStatus _status = TrapStatus.Catching;
        private string _remarks = String.Empty;
        private double _longitude = 4.899431;
        private double _latitude = 52.379189;
        private Guid _createdById = Guid.NewGuid();
        private DateTimeOffset _recordedOn;
        private List<Catch> _catches = new List<Catch>();

        public TrapBuilder WithGeoHierarchy(
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

        public TrapBuilder WithGeoHierarchy(SubAreaHourSquare subAreaHourSquare, Province province)
        {
            _subAreaHourSquare = subAreaHourSquare;
            _province = province;
            return this;
        }

        public TrapBuilder WithSubAreaHourSquare(SubAreaHourSquare subAreaHourSquare)
        {
            _subAreaHourSquare = subAreaHourSquare;
            return this;
        }

        public TrapBuilder WithTrapTypeId(Guid value)
        {
            _trapTypeId = value;
            return this;
        }

        public TrapBuilder WithNumberOfTraps(int value)
        {
            _numberOfTraps = value;
            return this;
        }

        public TrapBuilder WithStatus(TrapStatus value)
        {
            _status = value;
            return this;
        }

        public TrapBuilder WithRemarks(string value)
        {
            _remarks = value;
            return this;
        }

        public TrapBuilder WithCoordinates(double longitude, double latitude)
        {
            _longitude = longitude;
            _latitude = latitude;
            return this;
        }

        public TrapBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TrapBuilder WithCreatedById(Guid value)
        {
            _createdById = value;
            return this;
        }

        public TrapBuilder WithRecordedOn(DateTimeOffset value)
        {
            _recordedOn = value;
            return this;
        }

        public TrapBuilder WithCatches(params Catch[] value)
        {
            value
                .Where(c => c.CatchType == null).ToList()
                .ForEach(c =>
                {
                    var catchType = new CatchTypeBuilder()
                        .WithId(CatchType.BeverratMoerOudId)
                        .WithIsByCatch(true)
                        .WithName("Beverrat moer oud (>1jr)");
                    c.WithCatchType(catchType);
                });
            _catches = value.ToList();
            return this;
        }

        public static implicit operator Trap(TrapBuilder builder)
        {
            return builder.Build();
        }

        private Trap Build()
        {
            SubAreaHourSquare subAreaHourSquare = _subAreaHourSquare ??
                new SubAreaHourSquareBuilder()
                    .WithGeoHierarchy(_organizationName, _rayonName, _catchAreaName, _subAreaName, _waterAuthorityName, _hourSquareName)
                    .WithStartingCoordinate(_longitude, _latitude)
                    .WithWaterwayValues(120, 85, 100, 65);

            Province province = _province ??
                new ProvinceBuilder()
                    .WithName("trap_builder")
                    .WithRectangleGeometry(_longitude, _latitude, 20);

            var result = Trap.Create(
                new TrapCreateOrUpdate.Command
                {
                    Id = _id,
                    TrapTypeId = _trapTypeId,
                    Status = _status,
                    NumberOfTraps = _numberOfTraps,
                    Remarks = _remarks,
                    Longitude = _longitude,
                    Latitude = _latitude,
                    RecordedOn = _recordedOn,
                    Catches = _catches.Select(c =>
                    {
                        var result = CatchCreateOrUpdate.Command.CreateFrom(c);
                        result.TrapId = _id;
                        return result;
                    })
                },
                subAreaHourSquare,
                province,
                _catches.Select(c => c.CatchType).ToList());
            result.SetCreated(DateTimeOffset.Now, _createdById);
            return result;
        }
    }
}
