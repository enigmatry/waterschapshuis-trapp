using System;
using System.Linq;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.Core.Settings;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions
{
    public static class UserSessionQueryableExtensions
    {
        public static IQueryable<UserSession> QueryByUserId(this IQueryable<UserSession> query, Guid? userId)
        {
            return query.Where(session => session.CreatedById == userId);
        }

        public static IQueryable<UserSession> QueryByOrigin(this IQueryable<UserSession> query,
            UserSessionOrigin origin)
        {
            return query.Where(session => session.Origin == origin);
        }

        public static IQueryable<UserSession> QueryValid(this IQueryable<UserSession> query)
        {
            return query.Where(IsValid());
        }

        public static IQueryable<UserSession> QueryValidOrWithValidTokens(this IQueryable<UserSession> query)
        {
            return query.Where(IsValid().Or(HasValidTokens()));
        }

        public static IQueryable<SessionAccessToken> QueryValidUserSessions(this IQueryable<SessionAccessToken> query)
        {
            return query.Where(IsSessionForSessionAccessTokenValid());
        }

        public static IQueryable<UserSession> QueryByReadyForRemoval(this IQueryable<UserSession> query,
            DateTimeOffset createdBeforeDate)
        {
            return query
                .Where(IsValid().Not().Or(AllTokensExpired()))
                .Where(session => session.CreatedOn < createdBeforeDate);
        }

        private static Expression<Func<UserSession, bool>> IsValid()
        {
            return session =>
                session.ExpiresOn != null &&
                DateTimeOffset.Now <= session.ExpiresOn &&
                DateTimeOffset.Now >= session.CreatedOn;
        }

        private static Expression<Func<SessionAccessToken, bool>> IsSessionForSessionAccessTokenValid()
        {
            return sessionAccessToken =>
                sessionAccessToken.UserSession.ExpiresOn != null &&
                DateTimeOffset.Now <= sessionAccessToken.UserSession.ExpiresOn &&
                DateTimeOffset.Now >= sessionAccessToken.UserSession.CreatedOn;
        }

        private static Expression<Func<UserSession, bool>> HasValidTokens()
        {
            return session => session.AccessTokens.Any(at => !at.Expired);
        }

        private static Expression<Func<UserSession, bool>> AllTokensExpired()
        {
            return session => session.AccessTokens.All(at => at.Expired);
        }
    }
}
