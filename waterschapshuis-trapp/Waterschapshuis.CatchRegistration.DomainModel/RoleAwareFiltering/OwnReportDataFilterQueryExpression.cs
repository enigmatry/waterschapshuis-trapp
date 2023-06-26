using System;
using System.Linq;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.ReportData;
using Waterschapshuis.CatchRegistration.DomainModel.Roles;

namespace Waterschapshuis.CatchRegistration.DomainModel.RoleAwareFiltering
{
    public class OwnReportDataFilterQueryExpression : IFilterEntityByCurrentUserRoleQueryExpression<OwnReportData>
    {
        public Expression<Func<OwnReportData, bool>> GetExpression(ICurrentUserProvider userProvider)
        {
            var noFiltering = userProvider.PermissionIds.Contains(PermissionId.ReportReadWrite);

            if (noFiltering)
            {
                return _ => true;
            }

            // own report data should filter out the results due to security design principles
            return _ => false;
        }
    }
}
