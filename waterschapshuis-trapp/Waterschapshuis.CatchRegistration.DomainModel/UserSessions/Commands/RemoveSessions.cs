using System;
using JetBrains.Annotations;
using MediatR;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands
{
    public static class RemoveSessions
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            public DateTimeOffset CreatedBeforeDate { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public int UserSessionsDeleted { get; set; }
        }
    }
}
