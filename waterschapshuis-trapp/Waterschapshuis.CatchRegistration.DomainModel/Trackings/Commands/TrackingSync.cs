using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.Core.Settings;

namespace Waterschapshuis.CatchRegistration.DomainModel.Trackings.Commands
{
    public static partial class TrackingSync
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [DisplayName("TrackingLocations")]
            public IEnumerable<TrackingItem> TrackingLocations { get; set; } = null!;

            public class TrackingItem
            {
                /// <summary>
                /// GUID of the tracking record
                /// </summary>
                [DisplayName("Id")]

                //TODO API Patch: change from Guid to Id
                public string Id { get; set; } = String.Empty;

                /// <summary>
                /// Longitude of registered position
                /// </summary>
                [DisplayName("Longitude")]
                public double Longitude { get; set; }

                /// <summary>
                /// Latitude of registered positions
                /// </summary>
                [DisplayName("Latitude")]
                public double Latitude { get; set; }

                /// <summary>
                /// Date when the position is registered
                /// </summary>
                [DisplayName("RecordedOn")]
                public DateTimeOffset RecordedOn { get; set; }

                /// <summary>
                /// trapping type while tracking
                /// </summary>
                [DisplayName("TrappingTypeId")]
                public Guid TrappingTypeId { get; set; }

                /// <summary>
                /// session id for tracking
                /// </summary>
                [DisplayName("SessionId")]
                public Guid SessionId { get; set; }

                /// <summary>
                /// Indicator whether tracking could be used for timewriting
                /// </summary>
                [DisplayName("IsTimewriting")]
                public bool IsTimewriting { get; set; }

                /// <summary>
                /// Indicator whether tracking could be used for tracking lines
                /// </summary>
                [DisplayName("IsTrackingMap")]
                public bool IsTrackingMap { get; set; }

                /// <summary>
                /// Indicator whether other traps can see your position
                /// </summary>
                [DisplayName("IsTrackingPrivate")]
                public bool IsTrackingPrivate { get; set; }
            }
        }

        [PublicAPI]
        public class Result
        {
            public static Result CreateResult()
            {
                return new Result();
            }
        }

        [UsedImplicitly]
        public class TrackingItemValidator : AbstractValidator<Command.TrackingItem>
        {
            public TrackingItemValidator()
            {
                //TODO API Patch: Temporarily comment out until invalid data is cleansed on the Mobiles
                //RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.Latitude).NotEmpty();
                RuleFor(x => x.Longitude).NotEmpty();
                RuleFor(x => x.RecordedOn).NotEmpty();
                RuleFor(x => x.SessionId).NotEmpty();
                RuleFor(x => x.IsTrackingMap).Must(x => x.Equals(true)).When(x => !x.IsTimewriting)
                    .WithMessage("Kies tenminste één GPS-volg optie.");
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator(ApiConfigurationSettings apiConfigurationSettings)
            {
                RuleFor(x => x.TrackingLocations)
                    .Must(x => x.Count() <= apiConfigurationSettings.MaxItemsPerBatch)
                    .WithMessage("Te veel elementen in de batch");
                RuleForEach(x => x.TrackingLocations).SetValidator(new TrackingItemValidator());
            }
        }
    }
}
