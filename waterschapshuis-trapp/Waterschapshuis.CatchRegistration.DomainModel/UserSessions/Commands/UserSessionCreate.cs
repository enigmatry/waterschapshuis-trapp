using JetBrains.Annotations;
using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands
{
    public static class UserSessionCreate
    {
        [PublicAPI]
        public class Result
        {
            public Guid SessionId { get; set; }

            public static Result Create(Guid sessionId) => new Result { SessionId = sessionId };
        }
    }
}
