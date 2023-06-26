using JetBrains.Annotations;
using MediatR;
using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.UserSessions.Commands
{
    public static class UserSessionTerminate
    {
        public class Command : IRequest<Result>
        {
            public AccessToken AccessToken { get;  }

            private Command(AccessToken accessToken)
            {
                AccessToken = accessToken;
            }

            public static Command Create(AccessToken accessToken) => new Command(accessToken);
        }

        [PublicAPI]
        public class Result
        {
            public string UserEmail { get; set; } = String.Empty;

            public static Result Create(string userEmail) => new Result { UserEmail = userEmail };
        }
    }
}
