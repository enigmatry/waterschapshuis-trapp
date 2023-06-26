using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.UserSessions;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions;

namespace Waterschapshuis.CatchRegistration.DomainModel.Tests
{
    [Category("unit")]
    public class PredicateBuilderFixture
    {
        [Test]
        public void Not()
        {
            Expression<Func<UserSession, bool>> validSessionExpression = session =>
                session.ExpiresOn != null &&
                DateTimeOffset.Now <= session.ExpiresOn &&
                DateTimeOffset.Now >= session.CreatedOn &&
                session.AccessTokens.Any() &&
                session.AccessTokens.Count(x => !x.Expired) == 1;
            UserSession validSession = new UserSessionBuilder()
                .WithId(Guid.NewGuid())
                .WithExpiresOn(DateTimeOffset.Now.AddMinutes(15))
                .WithAccessTokens(SessionAccessToken.Create(AccessToken.Create("TOKEN"), Guid.Empty));

            var sessions = GetInvalidSessions().Append(validSession).ToList();

            sessions.AsQueryable()
                .Where(validSessionExpression)
                .SingleOrDefault(x => x.Id == validSession.Id)
                .Should().NotBeNull();

            sessions.AsQueryable()
                .Count(validSessionExpression.Not())
                .Should().Be(sessions.Count - 1);
            sessions.AsQueryable()
                .Where(validSessionExpression.Not())
                .All(x => x.Id != validSession.Id)
                .Should().BeTrue();
        }

        #region Helpers
        private List<UserSession> GetInvalidSessions() =>
            new List<UserSession>
            {
                new UserSessionBuilder()
                    .WithExpiresOn(DateTimeOffset.Now.AddDays(-1))
                    .WithAccessTokens(SessionAccessToken.Create(AccessToken.Create("TOKEN"), Guid.Empty)),
                new UserSessionBuilder()
                    .WithExpiresOn(DateTimeOffset.Now.AddMinutes(-15))
                    .WithAccessTokens(SessionAccessToken.Create(AccessToken.Create("TOKEN"), Guid.Empty)),
                new UserSessionBuilder()
                    .WithExpiresOn(DateTimeOffset.Now.AddMinutes(15))
                    .WithCreatedOn(DateTimeOffset.Now.AddMinutes(15))
                    .WithAccessTokens(SessionAccessToken.Create(AccessToken.Create("TOKEN"), Guid.Empty)),
                new UserSessionBuilder()
                    .WithExpiresOn(DateTimeOffset.Now.AddMinutes(15))
                    .WithAccessTokens(SessionAccessToken.Create(AccessToken.Create("TOKEN"), Guid.Empty).WithExpired()),
                new UserSessionBuilder()
                    .WithExpiresOn(DateTimeOffset.Now.AddMinutes(15))
                    .WithAccessTokens(
                        SessionAccessToken.Create(AccessToken.Create("TOKEN1"), Guid.Empty),
                        SessionAccessToken.Create(AccessToken.Create("TOKEN2"), Guid.Empty)
                    ),
                new UserSessionBuilder()
                    .WithExpiresOn(DateTimeOffset.Now.AddMinutes(15))
            };
        #endregion Helpers
    }
}
