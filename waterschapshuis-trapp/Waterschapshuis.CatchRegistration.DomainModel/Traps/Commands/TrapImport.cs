using System;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    public class TrapImport
    {
        public int NumberOfTraps { get; private set; }
        public TrapStatus Status { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        public Guid TrapTypeId { get; private set; }
        public string? Remarks { get; private set; }
        public SubAreaHourSquare SubAreaHourSquare { get; private set; } = null!;
        public Province Province { get; private set; } = null!;
        public string? ExternalId { get; private set; }

        public TrapImport(
            int numberOfTraps,
            TrapStatus status,
            double longitude,
            double latitude,
            Guid trapTypeId,
            string? remarks,
            SubAreaHourSquare subAreaHourSquare,
            Province province,
            string? externalId)
        {
            NumberOfTraps = numberOfTraps;
            Status = status;
            Longitude = longitude;
            Latitude = latitude;
            TrapTypeId = trapTypeId;
            Remarks = remarks;
            SubAreaHourSquare = subAreaHourSquare;
            Province = province;
            ExternalId = externalId;
        }
    }
}
