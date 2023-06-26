using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Utils;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.ReportData;
using Waterschapshuis.CatchRegistration.DomainModel.RoleAwareFiltering;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Reports;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.RoleAwareFiltering
{
    [Category("unit")]
    public class OwnReportDataFilterQueryExpressionFixture
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void GetExpression(bool filterEverything)
        {
            var currentUserProvider = MockCurrentUserProvider(filterEverything);
            var ownReportDataList = new List<OwnReportData>()
                {
                    new OwnReportDataBuilder(),
                    new OwnReportDataBuilder(),
                    new OwnReportDataBuilder(),
                }
                .AsQueryable();

            var result =
                ownReportDataList
                .Where(new OwnReportDataFilterQueryExpression().GetExpression(currentUserProvider))
                .ToList();

            result.Count.Should().Be(filterEverything ? 0 : ownReportDataList.Count());
        }

        #region Helpers
        private ICurrentUserProvider MockCurrentUserProvider(bool shouldFilterData)
        {
            var permissionIds = shouldFilterData
                ? Enum<PermissionId>.GetAll().Where(x => x != PermissionId.ReportReadWrite).ToArray()
                : Enum<PermissionId>.GetAll().ToArray();
            var result = A.Fake<ICurrentUserProvider>();
            A.CallTo(() => result.PermissionIds).Returns(permissionIds);
            return result;
        }
        #endregion Helpers
    }
}
