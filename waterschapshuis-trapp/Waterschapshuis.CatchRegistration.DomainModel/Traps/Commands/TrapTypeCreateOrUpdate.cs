using System;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands
{
    public static class TrapTypeCreateOrUpdate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// Indicator whether trap type is active
            /// </summary>
            public bool Active { get; set; }

            /// <summary>
            /// GUID of the trap type
            /// </summary>
            public Guid? Id { get; set; }

            /// <summary>
            /// Name of the trap type
            /// </summary>
            public string Name { get; set; } = String.Empty;

            /// <summary>
            /// Order number used for showing trap type in the list on mobile app
            /// </summary>
            public short Order { get; set; }
            
            /// <summary>
            /// GUID of the trapping type this trap type is used for
            /// </summary>            
            public Guid TrappingTypeId { get; set; } = Guid.Empty;

            /// <summary>
            /// Indicator whether Trap of this type can be in status Not-catching
            /// </summary>  
            public bool AllowNotCatching { get; set; }
        }

        [PublicAPI]
        public class Result
        {
            public Guid TrapTypeId { get; set; }

            public static Result CreateResult(Guid trapTypeId)
            {
                return new Result {TrapTypeId = trapTypeId};
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            private readonly IRepository<TrappingType> _repository;

            public Validator(IRepository<TrappingType> repository)
            {
                _repository = repository;
                RuleFor(x => x.Name).NotEmpty().MaximumLength(TrapType.NameMaxLength);
                RuleFor(x => x.Order).NotEmpty();
                RuleFor(x => x.TrappingTypeId).Must(EntityExists);
            }

            private bool EntityExists(Guid id)
            {
                return _repository.EntityExists(id);
            }
        }
    }
}
