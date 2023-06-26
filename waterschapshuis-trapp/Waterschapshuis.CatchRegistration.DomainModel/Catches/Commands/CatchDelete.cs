using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands
{
    public static partial class CatchDelete
    {
        [PublicAPI]
        public class Command : IRequest<Unit>
        {
            [Required] public Guid Id { get; set; }

            public static Command Create(Guid id)
            {
                return new Command { Id = id };
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IRepository<Catch> catchRepository,
                ITimeRegistrationHelperService timeRegistrationHelperServiceService)
            {
                RuleFor(x => x.Id)
                    .Must(x => x.NotEmpty())
                    .WithMessage("Catch Id not provided.");

                RuleFor(catchItem => catchRepository.FindById(catchItem.Id).RecordedOn)
                    .Must(timeRegistrationHelperServiceService.WeekClosedOrApprovedForDate)
                    .WithMessage("Vangst kan niet verwijderd worden in afgesloten week");
            }
        }

        private static IQueryable<Trap> BuildInclude(this IQueryable<Trap> query) =>
            query
                .Include(x => x.TrapHistories) // required when removing caches
                .Include(x => x.Catches)
                    .ThenInclude(x => x.CatchType);
    }
}
