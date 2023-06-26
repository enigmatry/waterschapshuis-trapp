using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    public static class TrapDelete
    {
        [PublicAPI]
        public class Command : IRequest<bool>
        {
            [Required] public Guid Id { get; set; }

            public static Command Create(Guid id)
            {
                return new Command {Id = id};
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).Must(x => x.NotEmpty())
                    .WithMessage("Trap Id not provided.");
            }
        }
    }
}
