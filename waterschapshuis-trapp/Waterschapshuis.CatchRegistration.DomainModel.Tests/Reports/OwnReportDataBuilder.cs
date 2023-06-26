using System;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.ReportData;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.Reports
{
    public class OwnReportDataBuilder
    {
        private DateTimeOffset? _createdOn = DateTimeOffset.Now;
        private int? _recordedOnYear = DateTimeOffset.Now.Year;
        private int? _numberOfCatches = null;
        private int? _numberOfByCatches = null;
        private int? _numberOfCatchesPreviousYear = null;
        private int? _numberOfByCatchesPreviousYear = null;
        private int? _period = null;
        private int? _week = null;
        private Guid? _ownerId = User.SystemUserId;
        private string? _ownerName = "SystemUser";
        private int? _numberOfTraps = null;
        private string? _trapStatus = String.Empty;
        private string? _trapTypeName = String.Empty;
        private string? _trappingTypeName = String.Empty;
        private bool? _isByCatch = null;
        private string? _catchTypeName = String.Empty;
        private Guid _organizationId;
        private string _organizationName = String.Empty;
        private string _waterAuthorityName = String.Empty;
        private string _rayonName = String.Empty;
        private string _catchAreaName = String.Empty;
        private string _subAreaName = String.Empty;
        private string _hourSquareName = String.Empty;
        private string? _provinceName = String.Empty;
        private string? _fieldTestName = String.Empty;
        private double? _hours = null;
        private double? _hoursPreviousYear = null;
        private int? _catchingNights = null;
        private int? _kmWaterways = null;
        private string? _timeRegistrationType = null;
        private string? versionRegionalLayout = null;
        private double? _hoursOther = null;

        public OwnReportDataBuilder WithGeoHierarchy(
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

        public OwnReportDataBuilder WithOwnerId(Guid value)
        {
            _ownerId = value;
            return this;
        }

        public OwnReportDataBuilder WithOrganzietion(Organization value)
        {
            _organizationId = value.Id;
            _organizationName = value.Name;
            return this;
        }


        public static implicit operator OwnReportData(OwnReportDataBuilder builder) => builder.Build();

        private OwnReportData Build()
        {
            return OwnReportData.Create
            (
                _createdOn,
                _recordedOnYear,
                _numberOfCatches,
                _numberOfByCatches,
                _numberOfCatchesPreviousYear,
                _numberOfByCatchesPreviousYear,
                _period,
                _week,
                _ownerId,
                _ownerName,
                _numberOfTraps,
                _trapStatus,
                _trapTypeName,
                _trappingTypeName,
                _isByCatch,
                _catchTypeName,
                _organizationId,
                _organizationName,
                _waterAuthorityName,
                _rayonName,
                _catchAreaName,
                _subAreaName,
                _hourSquareName,
                _provinceName,
                _fieldTestName,
                _hours,
                _hoursPreviousYear,
                _catchingNights,
                _kmWaterways,
                _timeRegistrationType,
                versionRegionalLayout,
                _hoursOther
            );
        }
    }
}
