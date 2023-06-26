using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.Core.Helpers;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands
{
    [UsedImplicitly]
    public class CatchTypeCreateOrUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// GUID of the catch type
            /// </summary>
            public Guid? Id { get; set; }

            /// <summary>
            /// Name of catch type
            /// </summary>
            public string Name { get;  set; } = String.Empty;

            /// <summary>
            /// Is this catch type used for catch or by-catch
            /// </summary>
            public bool IsByCatch { get;  set; }

            /// <summary>
            /// Animal type (Mammal, Bird, Fish or Other)
            /// </summary>
            public AnimalType AnimalType { get;  set; }

            /// <summary>
            /// Order to be used in listing on mobile
            /// </summary>
            public short Order { get;  set; }
        }

        [PublicAPI]
        public class Result
        {
            public Guid CatchTypeId { get; set; }

            public static Result CreateResult(Guid catchTypeId)
            {
                return new Result { CatchTypeId = catchTypeId };
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator(IRepository<Trap> trapRepository, IRepository<CatchType> catchTypeRepository)
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(CatchType.NameMaxLength);
                RuleFor(x => x.AnimalType).NotEmpty();
                RuleFor(x => x.Order).NotEmpty();
            }
        }
    }
}
