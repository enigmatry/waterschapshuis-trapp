using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Identity.Commands
{
    public static class AutoCreateUserAfterLogin
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            public string Name { get; set; } = String.Empty;
            public string Email { get; set; } = String.Empty;
            public string Surname { get; set; } = String.Empty;
            public string GivenName { get; set; } = String.Empty;
        }

        [PublicAPI]
        public class Result
        {
            /// <summary>
            /// New GUID created for the user
            /// </summary>
            public Guid UserId { get; set; }

            /// <summary>
            /// Indication whether creation of user was successfull
            /// </summary>
            public bool Created { get; set; }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty().MaximumLength(User.TextualFieldMaxLength).EmailAddress();
                RuleFor(x => x.Name).NotEmpty().MaximumLength(User.TextualFieldMaxLength);
                RuleFor(x => x.Surname).MaximumLength(User.TextualFieldMaxLength);
                RuleFor(x => x.GivenName).MaximumLength(User.TextualFieldMaxLength);
            }
        }
    }
}
