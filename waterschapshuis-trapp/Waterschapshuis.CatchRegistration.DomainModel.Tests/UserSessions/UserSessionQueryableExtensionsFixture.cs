using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests.UserSessions
{
    [Category("unit")]
    public class UserSessionQueryableExtensionsFixture
    {
        private static readonly AccessToken TokenValue1 = AccessToken.Create("Bearer TOKEN_VALUE_1");
        private static readonly AccessToken TokenValue2 = AccessToken.Create("Bearer TOKEN_VALUE_2");
        private const string User1Id = "bb117e65-5452-4068-ad2c-339bc56c49ba";
        private const string User2Id = "e0b9c630-a437-4608-b2f8-b0a19b2becf2";
        private IQueryable<UserSession> _sessions = null!;

        [SetUp]
        public void SetUp()
        {
            User user1 = new UserBuilder().Name("User 1");
            User user2 = new UserBuilder().Name("User 2");
            user1.WithId(new Guid(User1Id));
            user2.WithId(new Guid(User2Id));

            UserSession invalidToken = new UserSessionBuilder()
                .WithCreatedBy(user1)
                .WithExpiresOn(DateTimeOffset.Now.AddDays(1))
                .WithOrigin(UserSessionOrigin.BackOfficeApi)
                .WithAccessTokens(SessionAccessToken.Create(TokenValue1, Guid.Empty).WithExpired());

            UserSession invalidTokens = new UserSessionBuilder()
                .WithCreatedBy(user1)
                .WithExpiresOn(DateTimeOffset.Now.AddDays(-1))
                .WithOrigin(UserSessionOrigin.BackOfficeApi)
                .WithAccessTokens(
                    SessionAccessToken.Create(TokenValue1, Guid.Empty).WithExpired(),
                    SessionAccessToken.Create(TokenValue2, Guid.Empty).WithExpired()
                );

            UserSession invalidExpiresOn = new UserSessionBuilder()
                .WithCreatedBy(user1)
                .WithExpiresOn(DateTimeOffset.Now.AddDays(-1))
                .WithOrigin(UserSessionOrigin.ExternalApi)
                .WithAccessTokens(SessionAccessToken.Create(TokenValue1, Guid.Empty));

            UserSession valid = new UserSessionBuilder()
                .WithCreatedBy(user2)
                .WithExpiresOn(DateTimeOffset.Now.AddDays(1))
                .WithOrigin(UserSessionOrigin.MobileApi)
                .WithAccessTokens(
                    SessionAccessToken.Create(TokenValue1, Guid.Empty).WithExpired(),
                    SessionAccessToken.Create(TokenValue2, Guid.Empty)
                );

            UserSession invalidAndOld = new UserSessionBuilder()
                .WithCreatedBy(user2)
                .WithCreatedOn(DateTimeOffset.Now.AddYears(-10))
                .WithExpiresOn(DateTimeOffset.Now.AddYears(-10))
                .WithOrigin(UserSessionOrigin.ExternalApi)
                .WithAccessTokens(SessionAccessToken.Create(TokenValue1, Guid.Empty));

            _sessions = new List<UserSession> { valid, invalidExpiresOn, invalidToken, invalidTokens, invalidAndOld }
                .AsQueryable();
        }

        [TestCase(User1Id, ExpectedResult = 3)]
        [TestCase(User2Id, ExpectedResult = 2)]
        public int QueryByUserId(string userId) => _sessions.QueryByUserId(new Guid(userId)).Count();

        [TestCase(UserSessionOrigin.BackOfficeApi, ExpectedResult = 2)]
        [TestCase(UserSessionOrigin.MobileApi, ExpectedResult = 1)]
        [TestCase(UserSessionOrigin.ExternalApi, ExpectedResult = 2)]
        public int QueryByOrigin(UserSessionOrigin origin) => _sessions.QueryByOrigin(origin).Count();

        [TestCase(ExpectedResult = 2)]
        public int QueryByValid() => _sessions.QueryValid().Count();

        [TestCase(ExpectedResult = 1)]
        public int QueryByReadyForRemoval() =>
            _sessions.QueryByReadyForRemoval(DateTimeOffset.Now.AddDays(-5)).Count();
    }
}
