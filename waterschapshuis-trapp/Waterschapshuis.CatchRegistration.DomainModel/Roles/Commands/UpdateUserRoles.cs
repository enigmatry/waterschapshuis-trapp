using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel;

namespace Waterschapshuis.CatchRegistration.DomainModel.Roles.Commands
{
    public class UpdateUserRoles
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// GUID of the user
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// List of roles user will be assigned to
            /// </summary>
            [DisplayName(nameof(Roles))]
            public Guid[] Roles { get; set; } = new Guid[0];
        }

        [PublicAPI]
        public class Result
        {
            public Guid UserId { get; set; }
        }
    }
}
