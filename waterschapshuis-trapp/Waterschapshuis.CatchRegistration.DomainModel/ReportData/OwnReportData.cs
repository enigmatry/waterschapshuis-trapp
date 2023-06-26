using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;

namespace Waterschapshuis.CatchRegistration.DomainModel.ReportData
{
    public class OwnReportData : Entity<Guid>, ICurrentUserRoleAwareEntity
    {
        private OwnReportData() { }

        public DateTimeOffset? CreatedOn { get; private set; }
        public int? RecordedOnYear { get; private set; }
        public int? NumberOfCatches { get; private set; }
        public int? NumberOfByCatches { get; private set; }
        public int? NumberOfCatchesPreviousYear { get; private set; }
        public int? NumberOfByCatchesPreviousYear { get; private set; }
        public int? Period { get; private set; }
        public int? Week { get; private set; }
        public Guid? OwnerId { get; private set; }
        public string? OwnerName { get; private set; } = String.Empty;
        public int? NumberOfTraps { get; private set; }
        public string? TrapStatus { get; private set; } = String.Empty;
        public string? TrapTypeName { get; private set; } = String.Empty;
        public string? TrappingTypeName { get; private set; } = String.Empty;
        public bool? IsByCatch { get; private set; }
        public string? CatchTypeName { get; private set; } = String.Empty;
        public Guid? OrganizationId { get; private set; }
        public string? OrganizationName { get; private set; } = String.Empty;
        public string? WaterAuthorityName { get; private set; } = String.Empty;
        public string? RayonName { get; private set; } = String.Empty;
        public string? CatchAreaName { get; private set; } = String.Empty;
        public string? SubAreaName { get; private set; } = String.Empty;
        public string? HourSquareName { get; private set; } = String.Empty;
        public string? ProvinceName { get; private set; } = String.Empty;
        public string? FieldTestName { get; private set; } = String.Empty;
        public double? Hours { get; private set; }
        public double? HoursPreviousYear { get; private set; }
        public int? CatchingNights { get; private set; }
        public int? KmWaterway { get; private set; }
        public string? TimeRegistrationType { get; set; } = String.Empty;
        public string? VersionRegionalLayout { get; set; } =String.Empty;
        public double? HoursOther { get; private set; }

        public static OwnReportData Create(
            DateTimeOffset? createdOn,
            int? recordedOnYear,
            int? numberOfCatches,
            int? numberOfByCatches,
            int? numberOfCatchesPreviousYear,
            int? numberOfByCatchesPreviousYear,
            int? period,
            int? week,
            Guid? ownerId,
            string? ownerName,
            int? numberOfTraps,
            string? trapStatus,
            string? trapTypeName,
            string? trappingTypeName,
            bool? isByCatch,
            string? catchTypeName,
            Guid organizationId,
            string organizationName,
            string waterAuthorityName,
            string rayonName,
            string catchAreaName,
            string subAreaName,
            string hourSquareName,
            string? provinceName,
            string? fieldTestName,
            double? hours,
            double? hoursPreviousYear,
            int? catchingNights,
            int? kmWaterway,
            string? timeRegistrationType,
            string? versionRegionalLayout,
            double? hoursOther)
        {
            return new OwnReportData
            {
                CreatedOn = createdOn,
                RecordedOnYear = recordedOnYear,
                NumberOfCatches = numberOfCatches,
                NumberOfByCatches = numberOfByCatches,
                NumberOfCatchesPreviousYear = numberOfCatchesPreviousYear,
                NumberOfByCatchesPreviousYear = numberOfByCatchesPreviousYear,
                Period = period,
                Week = week,
                OwnerId = ownerId,
                OwnerName = ownerName,
                NumberOfTraps = numberOfTraps,
                TrapStatus = trapStatus,
                TrapTypeName = trapTypeName,
                TrappingTypeName = trappingTypeName,
                IsByCatch = isByCatch,
                CatchTypeName = catchTypeName,
                OrganizationId = organizationId,
                OrganizationName = organizationName,
                WaterAuthorityName = waterAuthorityName,
                RayonName = rayonName,
                CatchAreaName = catchAreaName,
                SubAreaName = subAreaName,
                HourSquareName = hourSquareName,
                ProvinceName = provinceName,
                FieldTestName = fieldTestName,
                Hours = hours,
                HoursPreviousYear = hoursPreviousYear,
                CatchingNights = catchingNights,
                KmWaterway = kmWaterway,
                TimeRegistrationType = timeRegistrationType,
                VersionRegionalLayout = versionRegionalLayout,
                HoursOther = hoursOther
            };
        }

        public bool IsFromCatchData() =>
            (
                NumberOfCatches != null ||
                NumberOfByCatches != null ||
                NumberOfCatchesPreviousYear != null ||
                NumberOfByCatchesPreviousYear != null
            ) &&
            Hours == null &&
            HoursPreviousYear == null;

        public bool IsFromTimeRegistrationhData() => !IsFromCatchData();

        public bool CreatedFromCatch(Catch value, bool forCurrentYear)
        {
            var result = new bool[] {
                CreatedOn == value.CreatedOn,
                RecordedOnYear == (forCurrentYear ? value.RecordedOn.Year : value.RecordedOn.Year + 1),
                NumberOfCatches == (forCurrentYear ? value.NumberOfCatches : (int?)null),
                NumberOfByCatches == (forCurrentYear ? value.NumberOfByCatches : (int?)null),
                NumberOfCatchesPreviousYear == (forCurrentYear ? (int?)null : value.NumberOfCatches),
                NumberOfByCatchesPreviousYear == (forCurrentYear ? (int?)null : value.NumberOfByCatches),
                Period == value.WeekPeriod.Period,
                Week == value.WeekPeriod.Week,
                OwnerId == value.CreatedById,
                NumberOfTraps == value.Trap.NumberOfTraps,
                TrapStatus == value.Trap.Status.GetDescription(),
                TrappingTypeName == value.Trap.TrapType.TrappingType.Name,
                IsByCatch == value.CatchType.IsByCatch,
                CatchTypeName == value.CatchType.Name,
                OrganizationId == value.Trap.SubAreaHourSquare.SubArea.WaterAuthority.OrganizationId,
                WaterAuthorityName == value.Trap.SubAreaHourSquare.SubArea.WaterAuthority.Name,
                RayonName == value.Trap.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name,
                CatchAreaName == value.Trap.SubAreaHourSquare.SubArea.CatchArea.Name,
                SubAreaName == value.Trap.SubAreaHourSquare.SubArea.Name,
                HourSquareName == value.Trap.SubAreaHourSquare.HourSquare.Name,
                ProvinceName == value.Trap.Province?.Name,
                FieldTestName == null,
                Hours == null,
                HoursPreviousYear == null,
                CatchingNights == value.Trap.CatchingNights,
                KmWaterway == null,
                TimeRegistrationType == null,
                VersionRegionalLayout == null,
                HoursOther == null
            };
            return result.All(x => x);
        }

        public bool CreatedFromTimeRegistration(TimeRegistration value, bool forCurrentYear)
        {
            var result = new bool[] {
                CreatedOn == value.CreatedOn,
                RecordedOnYear == (forCurrentYear ? value.Date.Year : value.Date.Year + 1),
                NumberOfCatches == null,
                NumberOfByCatches == null,
                NumberOfCatchesPreviousYear == null,
                NumberOfByCatchesPreviousYear == null,
                Period == value.WeekPeriod.Period,
                Week == value.WeekPeriod.Week,
                OwnerId == value.UserId,
                NumberOfTraps == null,
                TrapStatus == null,
                TrappingTypeName == value.TrappingType.Name,
                IsByCatch == null,
                CatchTypeName == null,
                OrganizationId == value.SubAreaHourSquare.SubArea.WaterAuthority.OrganizationId,
                WaterAuthorityName == value.SubAreaHourSquare.SubArea.WaterAuthority.Name,
                RayonName == value.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name,
                CatchAreaName == value.SubAreaHourSquare.SubArea.CatchArea.Name,
                SubAreaName == value.SubAreaHourSquare.SubArea.Name,
                HourSquareName == value.SubAreaHourSquare.HourSquare.Name,
                ProvinceName == null,
                FieldTestName == null,
                Hours == (forCurrentYear ? value.Hours : (double?)null),
                HoursPreviousYear == (forCurrentYear ? (double?)null : value.Hours),
                CatchingNights == null,
                KmWaterway == null,
                TimeRegistrationType == null,
                VersionRegionalLayout == null,
                HoursOther == null
            };
            return result.All(x => x);
        }
    }
}
