using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts;

namespace Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands
{
    public static class ObservationSync
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [DisplayName("Observations")] public IEnumerable<ObservationItem> Observations { get; set; } = null!;

            public class ObservationItem
            {
                /// <summary>
                /// GUID of the observation
                /// </summary>
                [DisplayName("Id")]
                [Required]
                public Guid Id { get; set; }

                /// <summary>
                /// Longitude where observation is registered
                /// </summary>
                [DisplayName("Longitude")] public double Longitude { get; set; }

                /// <summary>
                /// Latitude where observation is registered
                /// </summary>
                [DisplayName("Latitude")] public double Latitude { get; set; }

                /// <summary>
                /// Type of observation (schade, other)
                /// </summary>
                [DisplayName("Type")] public byte Type { get; set; }

                /// <summary>
                /// Remarks entered by the trapper
                /// </summary>
                [DisplayName("Remarks")] public string Remarks { get; set; } = String.Empty;

                /// <summary>
                /// Creation date of the observation
                /// </summary>
                [DisplayName("RecordedOn")] public DateTimeOffset RecordedOn { get; set; }

                /// <summary>
                /// Indicator whether a photo has been made
                /// </summary>
                [DisplayName("HasPhoto")] public bool HasPhoto { get; set; }

                [UsedImplicitly]
                public class ObservationItemValidator : AbstractValidator<ObservationItem>
                {
                    public ObservationItemValidator(ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
                    {
                        RuleFor(x => x.Id).NotEmpty();
                        RuleFor(x => x.Latitude).NotEmpty();
                        RuleFor(x => x.Longitude).NotEmpty();
                        RuleFor(x => (int)x.Type).GreaterThanOrEqualTo(1).LessThanOrEqualTo(2);
                        RuleFor(x => x.Remarks).MaximumLength(Observation.RemarkFieldMaxLength);
                        RuleFor(x => new { x.Longitude, x.Latitude })
                            .Must(x => HaveSubAreaHourSquareAtLocation(x.Longitude, x.Latitude, currentVersionRegionalLayoutService))
                            .WithMessage("Er kan geen uurhok gevonden worden voor de betreffende coördinaten");
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

        [PublicAPI]
        public class Result
        {
            public IEnumerable<ResultItem> SavedItems { get; set; } = null!;
            public string StorageAccessKey { get; set; } = String.Empty;

            public static Result CreateResult(IEnumerable<ResultItem> savedItems, string storageAccessKey)
            {
                return new Result { SavedItems = savedItems, StorageAccessKey = storageAccessKey};
            }

            public class ResultItem
            {
                public Guid Id { get; set; }
                public bool IsNew { get; set; }
            }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator(ApiConfigurationSettings apiConfigurationSettings, ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                RuleFor(x => x.Observations)
                    .Must(x => x.Count() <= apiConfigurationSettings.MaxItemsPerBatch)
                    .WithMessage("Te veel elementen in de batch");
                RuleForEach(x => x.Observations)
                    .SetValidator(new Command.ObservationItem.ObservationItemValidator(currentVersionRegionalLayoutService));
            }
        }
    }
}
