using System;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions
{
    public class SessionAccessToken : Entity<Guid>
    {
        public string Value { get; private set; } = String.Empty;
        public bool Expired { get; private set; }
        public Guid UserSessionId { get; private set; }
        public UserSession UserSession { get; private set; } = null!;

        private SessionAccessToken() { }

        public static SessionAccessToken Create(AccessToken value, Guid userSessionId) =>
            new SessionAccessToken
            {
                Id = GenerateId(),
                Value = value.GetValueWithAuthorizationHeaderPrefix(),
                Expired = false,
                UserSessionId = userSessionId
            };

        public bool Matches(AccessToken accessToken)
        {
            return Value == accessToken.GetValueWithAuthorizationHeaderPrefix();
        }

        public SessionAccessToken WithExpired()
        {
            Expired = true;
            return this;
        }

        internal SessionAccessToken WithUserSession(UserSession userSession)
        {
            UserSession = userSession;
            UserSessionId = userSession.Id;

            return this;
        }


    }
}
