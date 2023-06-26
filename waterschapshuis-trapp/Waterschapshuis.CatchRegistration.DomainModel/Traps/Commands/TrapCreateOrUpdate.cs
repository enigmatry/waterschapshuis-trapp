using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.TimeRegistrations.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    public static partial class TrapCreateOrUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// GUID of trap
            /// </summary>
            [Required] public Guid Id { get; set; }

            /// <summary>
            /// Date when the trap has been created on mobile by trapper
            /// </summary>
            [Required] public DateTimeOffset RecordedOn { get; set; }

            /// <summary>
            /// Number of traps
            /// </summary>
            [Required] public int NumberOfTraps { get; set; }

            /// <summary>
            /// Trap status: catching, non catching, deleted
            /// </summary>
            [Required] public TrapStatus Status { get; set; }

            /// <summary>
            /// Longitude of trap where it is registered
            /// </summary>
            [Required] public double Longitude { get; set; }

            /// <summary>
            /// Latitude of trap where it is registered
            /// </summary>
            [Required] public double Latitude { get; set; }

            /// <summary>
            /// GUID of the trap type
            /// </summary>
            [Required] public Guid TrapTypeId { get; set; }

            /// <summary>
            /// Remarks entered by a trapper
            /// </summary>
            public string? Remarks { get; set; }

            /// <summary>
            /// List of catches registered on this trap
            /// </summary>
            public IEnumerable<CatchCreateOrUpdate.Command> Catches { get; set; } = new List<CatchCreateOrUpdate.Command>();

            /// <summary>
            /// Hint for creation of trap
            /// </summary>
            public bool ShouldCreate { get; set; }


            public IEnumerable<CatchCreateOrUpdate.Command> CatchesToCreate => Catches.Where(x => !x.MarkedForRemoval);
            public IEnumerable<CatchCreateOrUpdate.Command> CatchesToRemove => Catches.Where(x => x.MarkedForRemoval);


            public static Command CreateFrom(Trap value, bool shouldCreate) =>
                new Command
                {
                    Id = value.Id,
                    Longitude = value.Location.X,
                    Latitude = value.Location.Y,
                    NumberOfTraps = value.NumberOfTraps,
                    RecordedOn = value.RecordedOn,
                    Remarks = value.Remarks,
                    Status = value.Status,
                    TrapTypeId = value.TrapTypeId,
                    ShouldCreate = shouldCreate
                };
        }

        [PublicAPI]
        public class Result
        {
            public Guid TrapId { get; set; }

            public static Result CreateResult(Guid trapId)
            {
                return new Result { TrapId = trapId };
            }
        }

        private static IQueryable<Trap> BuildInclude(this IQueryable<Trap> query) =>
            query
                .Include(x => x.TrapHistories) // required when removing caches
                .Include(x => x.Catches)
                    .ThenInclude(x => x.CatchType);

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator(
                ITimeRegistrationHelperService timeRegistrationHelperServiceService,
                IRepository<Catch> catchRepository,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService,
                IRepository<TrapType> ttRepository)
            {
                RuleFor(x => x.Id).Must(x => x.NotEmpty())
                    .WithMessage("Vangmiddel Id niet voorzien.");
                RuleFor(x => x.TrapTypeId).Must(x => x.NotEmpty())
                    .WithMessage("VangmiddelType Id niet voorzien.");
                RuleFor(x => x.Status).IsInEnum();
                RuleFor(x => x)
                    .Must(x => ttRepository.QueryAll().AllowTrapStatus(x.TrapTypeId, x.Status))
                    .WithMessage("Vangmiddelstatus is niet toegestaan voor dit vangmiddel type");
                RuleFor(x => x.NumberOfTraps).GreaterThan(0);
                RuleFor(x => x.Remarks).MaximumLength(Trap.RemarksMaxLength);
                RuleFor(x => x.Latitude).NotEmpty();
                RuleFor(x => x.Longitude).NotEmpty();
                RuleFor(x => new { x.Longitude, x.Latitude })
                    .Must(x => HaveSubAreaHourSquareAtLocation(x.Longitude, x.Latitude, currentVersionRegionalLayoutService))
                    .WithMessage("Er kan geen uurhok gevonden worden voor de betreffende coördinaten.");
                RuleFor(x => x)
                    .Must(x => x.Catches.All(c => c.TrapId == x.Id))
                    .WithMessage("Vangsten moeten bij dezelfde vangmiddel horen.");
                RuleFor(x => x.Catches)
                    .Must(catches => catches.All(c => c.Status == CatchStatus.Written))
                    .WithMessage("Vangsten moeten status opgeslagen hebben.");

                RuleFor(x => x.CatchesToCreate)
                    .Must(catches => catches.All(x => !x.RecordedOn.OlderThanNumberOfWeeks(6)))
                    .WithMessage("Vangsten kunnen niet in afgesloten week vallen.");

                When(x => x.ShouldCreate, () =>
                {
                    RuleFor(trap => trap.Catches)
                        .Must(catches => catches.All(c => !c.MarkedForRemoval))
                        .WithMessage("Vangsten kunnen niet verwijderd worden terwijl vangmiddel aangemaakt wordt.");
                });
                When(x => x.Catches.Any(), () =>
                {
                    RuleFor(trap => trap.CatchesToCreate)
                        .Must(catches => catches.All(
                            catchItem => timeRegistrationHelperServiceService.WeekClosedOrApprovedForDate(catchItem.RecordedOn)))
                        .WithMessage("Vangsten kunnen niet in afgesloten week vallen.");
                    RuleFor(trap => trap.CatchesToCreate.Select(c => c.Id).ToArray())
                        .Must(catchIds => !catchIds.Any() || !catchRepository.QueryAll().Exists(catchIds))
                        .WithMessage("Nieuwe vangsten moeten een uniek id hebben.");
                    RuleFor(trap => trap.CatchesToRemove.Select(c => c.Id).ToArray())
                        .Must(catchIds => !catchIds.Any() || catchRepository.QueryAll().Exists(catchIds))
                        .WithMessage("Te verwijderen vangsten moeten bestaande id's hebben.");
                    RuleFor(trap => trap.CatchesToRemove)
                        .Must(catchItems => catchItems.All(catchItem => timeRegistrationHelperServiceService.WeekClosedOrApprovedForDate(catchItem.RecordedOn)))
                        .WithMessage("Vangst kan niet verwijderd worden in afgesloten week");
                });
            }
        
            private bool HaveSubAreaHourSquareAtLocation(
                double longitude,
                double latitude,
                ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService) =>
                currentVersionRegionalLayoutService
                    .QuerySubAreaHourSquaresNoTracking()
                    .ExistAtLocation(longitude, latitude);
        }
    }
}
