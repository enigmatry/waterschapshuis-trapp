using System;
using System.Collections.Generic;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.UserSessions.DomainEvents;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions
{
    public class UserSession : EntityHasCreatedUpdated<Guid>
    {
        private readonly List<SessionAccessToken> _accessTokens = new List<SessionAccessToken>();

        public DateTimeOffset? ExpiresOn { get; private set; }
        public UserSessionOrigin Origin { get; private set; } = UserSessionOrigin.BackOfficeApi;
        public User CreatedBy { get; } = null!;
        public IReadOnlyCollection<SessionAccessToken> AccessTokens => _accessTokens.AsReadOnly();

        private UserSession() { }

        public static UserSession Create(AccessToken accessToken, DateTimeOffset expirationDate, UserSessionOrigin origin, Guid userId)
        {
            UserSession session = new UserSession
            {
                Id = GenerateId(),
                ExpiresOn = expirationDate,
                Origin = origin
            };
            session.WithAccessToken(accessToken);
            session.SetCreated(DateTimeOffset.Now, userId);
            session.AddDomainEvent(new UserSessionCreatedDomainEvent(userId, session.Id, session.Origin));
            return session;
        }

        public void Update(AccessToken newAccessToken)
        {
            WithAccessToken(newAccessToken);
            AddDomainEvent(new UserSessionUpdatedDomainEvent(CreatedById, Id, Origin));
        }

        public void Terminate()
        {
            ExpiresOn = null;
            _accessTokens.ForEach(x => x.WithExpired());
            AddDomainEvent(new UserSessionTerminatedDomainEvent(CreatedById, Id, Origin));
        }

        public bool IsValid() => new[] { this }.AsQueryable().QueryValid().Any();

        private UserSession WithAccessToken(AccessToken value)
        {
            _accessTokens.ForEach(x => x.WithExpired());
            _accessTokens.Add(SessionAccessToken.Create(value, Id));
            return this;
        }

        public bool HasValidToken(AccessToken accessToken) =>
            AccessTokens.Any(token => token.Matches(accessToken) && !token.Expired);

        //used for testing purposes
        internal void ClearAndAddAccessTokens(params SessionAccessToken[] accessTokens)
        {
            _accessTokens.Clear();
            accessTokens.ToList().ForEach(token => _accessTokens.Add(token));
        }
    }
}
