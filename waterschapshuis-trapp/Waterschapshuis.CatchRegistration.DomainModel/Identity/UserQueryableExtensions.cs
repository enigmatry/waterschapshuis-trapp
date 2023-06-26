using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity
{
    public static class UserQueryableExtensions
    {
        public static IQueryable<User> QueryByEmail(this IQueryable<User> query, string email) =>
            query.Where(e => e.Email == email);

        public static IQueryable<User> QueryByKeyword(this IQueryable<User> query, string keyword) =>
            keyword.IsNotNullOrEmpty() ? query.Where(e => e.UserRoles.Any(x=> x.Role.Name.Contains(keyword)) 
                                        || e.Email.Contains(keyword)) : query;

        public static IQueryable<User> QueryByRoleIds(this IQueryable<User> query, params Guid[] roleIds) =>
            query.Where(e => e.UserRoles.Any(r => roleIds.Contains(r.RoleId)));

        public static IQueryable<User> QueryByOrganizationId(this IQueryable<User> query, Guid organizationId) =>
            query.Where(e => e.OrganizationId == organizationId);

        public static IQueryable<User> QueryByAuthorizedOnly(this IQueryable<User> query) =>
            query.Where(e => e.Authorized);

        public static IQueryable<User> QueryUnauthorizedOnly(this IQueryable<User> query) =>
            query.Where(e => !e.Authorized);

        public static IQueryable<User> QueryInactiveBeforeDate(this IQueryable<User> query, DateTimeOffset date) =>
            query.Where(e => e.InactiveOn <= date);

        public static async Task<User?> GetByEmailWithSessions(this IQueryable<User> query, string email, CancellationToken cancelationToken) =>
            await query
                .Include(u => u.UserSessions).ThenInclude(us => us.AccessTokens)
                .QueryByEmail(email)
                .SingleOrDefaultAsync(cancelationToken);
    }
}
