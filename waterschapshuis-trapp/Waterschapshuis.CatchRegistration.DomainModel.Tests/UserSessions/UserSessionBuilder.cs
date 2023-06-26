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
    public class UserSessionBuilder
    {
        private Guid _id;
        private DateTimeOffset _expiresOn;
        private UserSessionOrigin _origin = UserSessionOrigin.BackOfficeApi;
        private User _createdBy = new UserBuilder();
        private DateTimeOffset _createdOn = DateTimeOffset.UtcNow;
        private List<SessionAccessToken> _accessTokens = new List<SessionAccessToken>();

        public static implicit operator UserSession(UserSessionBuilder builder)
        {
            return builder.Build();
        }

        public UserSession Build()
        {
            var session = UserSession.Create(AccessToken.Create(""), _expiresOn, _origin, _createdBy.Id);
            session.ClearAndAddAccessTokens(_accessTokens.ToArray());
            if (_id != Guid.Empty)
            {
                session.WithId(_id);
            }

            session.SetCreated(_createdOn, _createdBy.Id);

            _accessTokens.ForEach(at => at.WithUserSession(session));
            return session;
        }

        public UserSessionBuilder WithExpiresOn(DateTimeOffset value)
        {
            _expiresOn = value;
            return this;
        }

        public UserSessionBuilder WithOrigin(UserSessionOrigin value)
        {
            _origin = value;
            return this;
        }

        public UserSessionBuilder WithCreatedBy(User value)
        {
            _createdBy = value;
            return this;
        }

        public UserSessionBuilder WithAccessTokens(params SessionAccessToken[] value)
        {
            _accessTokens = value.ToList();
            return this;
        }

        public UserSessionBuilder WithId(Guid value)
        {
            _id = value;
            return this;
        }

        public UserSessionBuilder WithCreatedOn(DateTimeOffset value)
        {
            _createdOn = value;
            return this;
        }
    }
}
