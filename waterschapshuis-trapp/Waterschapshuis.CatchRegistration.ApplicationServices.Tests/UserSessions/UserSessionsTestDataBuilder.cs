using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.UserSessions;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Tests.UserSessions
{
    public class UserSessionsTestDataBuilder
    {
        public (IEnumerable<SessionAccessToken> sessionAccessTokens, IEnumerable<UserSession> userSessions) Build()
        {
            var validSessionId = new Guid("3ba435f5-7979-4998-ac1e-42188c68d0ec");

            var validAccessToken =
                SessionAccessToken.Create(AccessToken.Create("valid_access_token_from_valid_session"), validSessionId);

            SessionAccessToken expiredAccessToken =
                SessionAccessToken.Create(AccessToken.Create("expired_access_token_from_valid_session"), validSessionId)
                    .WithExpired();

            UserSession nonExpiredUserSession = new UserSessionBuilder()
                .WithId(validSessionId)
                .WithOrigin(UserSessionOrigin.BackOfficeApi)
                .WithExpiresOn(DateTimeOffset.Now.AddDays(1))
                .WithAccessTokens(validAccessToken, expiredAccessToken)
                .Build();

            var invalidSessionId = new Guid("e7d160bd-2418-416f-8ee6-733bb0e6399d");

            var validAccessTokenFromExpiredSession =
                SessionAccessToken.Create(AccessToken.Create("valid_access_token_from_invalid_session"),
                    invalidSessionId);

            UserSession expiredUserSession = new UserSessionBuilder()
                .WithId(invalidSessionId)
                .WithOrigin(UserSessionOrigin.BackOfficeApi)
                .WithExpiresOn(DateTimeOffset.Now.AddDays(-1))
                .WithAccessTokens(validAccessTokenFromExpiredSession)
                .Build();

            var sessionAccessTokens = new List<SessionAccessToken>
            {
                validAccessToken, expiredAccessToken, validAccessTokenFromExpiredSession
            };

            var userSessions = new List<UserSession> {nonExpiredUserSession, expiredUserSession};

            return (sessionAccessTokens, userSessions);
        }
    }
}
