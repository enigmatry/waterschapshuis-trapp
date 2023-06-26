using System;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.RoleAwareFiltering
{
    public interface IFilterEntityByCurrentUserRoleQueryExpression<T> where T : ICurrentUserRoleAwareEntity
    {
        Expression<Func<T, bool>> GetExpression(ICurrentUserProvider userProvider);
    }
}
