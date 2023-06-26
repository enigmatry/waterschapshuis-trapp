using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    public partial class TrapUpdate
    {
        [PublicAPI]
        public class Command : IRequest<TrapCreateOrUpdate.Result>
        {
            /// <summary>
            /// GUID of the trap
            /// </summary>
            [Required] public Guid Id { get; set; }

            /// <summary>
            /// GUID of the (new) trap type
            /// </summary>
            [Required] public Guid TrapTypeId { get; set; }

            /// <summary>
            /// Indicator of trap status: catching, non catching or deleted
            /// </summary>
            [Required] public TrapStatus Status { get; set; }

            /// <summary>
            /// Longitude of trap where it is registered
            /// </summary>
            public double Longitude { get; set; }

            /// <summary>
            /// Latitude of trap where it is registered
            /// </summary>
            public double Latitude { get; set; }

            /// <summary>
            /// Remarks that apply to this trap, entered by a trapper
            /// </summary>
            public string? Remarks { get; set; }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IRepository<TrapType> ttRepository)
            {
                RuleFor(x => x.Id).Must(x => x.NotEmpty()).WithMessage("Trap Id not provided");
                RuleFor(x => x.TrapTypeId).Must(x => x.NotEmpty()).WithMessage("TrapType Id not provided");
                RuleFor(x => x)
                    .Must(x => ttRepository.QueryAll().AllowTrapStatus(x.TrapTypeId, x.Status))
                    .WithMessage("Trap status is not allowed for selected trap type");
                RuleFor(x => x.Remarks).MaximumLength(Trap.RemarksMaxLength);
            }
        }
    }
}
