using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Settings;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions
{
    public static class SessionAccessTokenQueryableExtensions
    {
        public static IQueryable<SessionAccessToken> QueryValid(this IQueryable<SessionAccessToken> query) =>
            query.Where(x => !x.Expired);

        public static IQueryable<SessionAccessToken> QueryByOrigin(this IQueryable<SessionAccessToken> query, UserSessionOrigin origin) =>
            query.Where(x => x.UserSession.Origin == origin);

        public static IQueryable<SessionAccessToken> QueryByValue(this IQueryable<SessionAccessToken> query, AccessToken token)
        {
            var value = token.GetValueWithAuthorizationHeaderPrefix();
            return query.Where(x => x.Value == value);
        }
    }
}
