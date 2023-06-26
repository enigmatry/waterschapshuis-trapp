using FluentValidation;
using FluentValidation.Validators;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts.Commands
{
    public static partial class VersionRegionalLayoutCreate
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            [Required] public string Name { get; set; } = String.Empty;
            public List<SubAreaCsvRecord> SubAreaCsvRecords { get; set; } = new List<SubAreaCsvRecord>();

            public static Command Create(string name, List<SubAreaCsvRecord> subAreaCsvRecords) =>
                new Command { Name = name, SubAreaCsvRecords = subAreaCsvRecords };
        }

        [PublicAPI]
        public class Result
        {
            public bool Succeed { get; set; }

            public bool Failed => !Succeed;
            public static Result Create(bool succeed) => new Result { Succeed = succeed };
        }

        [UsedImplicitly]

        public class Validator : AbstractValidator<Command>
        {
            private readonly ICurrentVersionRegionalLayoutService _currentVersionRegionalLayoutService;

            public Validator(ICurrentVersionRegionalLayoutService currentVersionRegionalLayoutService)
            {
                _currentVersionRegionalLayoutService = currentVersionRegionalLayoutService;

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                    $"VersionRegionalLayout Name is required");
                RuleFor(x => x.Name)
                    .MaximumLength(VersionRegionalLayout.NameMaxLength)
                    .WithMessage($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                    $"VersionRegionalLayout Name can have maximal {VersionRegionalLayout.NameMaxLength} characters");
                RuleFor(x => x.Name)
                    .Must(name => _currentVersionRegionalLayoutService.All.NameIsUnique(name))
                    .WithMessage($"{VersionRegionalLayoutImport.ErrorPrefix}: VersionRegionalLayout Name must be unique");
                RuleFor(x => x.SubAreaCsvRecords)
                    .Must(x => x.Count > 0)
                    .WithMessage($"{VersionRegionalLayoutImport.ErrorPrefix}: File must contain records");
                RuleFor(x => x.SubAreaCsvRecords)
                    .Must(HaveValidSubAreas)
                    .WithMessage("{ValidationMessages}");
                RuleFor(x => x.SubAreaCsvRecords)
                    .Must(HaveValidWaterAuthoritiesAndRayons)
                    .WithMessage("{ValidationMessages}");
                RuleFor(x => x.SubAreaCsvRecords)
                    .Must(HaveValidCatchAreas)
                    .WithMessage("{ValidationMessages}");
            }

            private bool HaveValidSubAreas(Command root, List<SubAreaCsvRecord> records, PropertyValidatorContext context)
            {
                var validationMessages = records
                    .Where(x => !String.IsNullOrWhiteSpace(x.ValidationMessage))
                    .Select(x => x.ValidationMessage).ToList();
                var invalidNames = records
                    .Where(x => x.Name.Length > SubArea.NameMaxLength)
                    .Select(x => x.Name).ToList();
                var doubleSubAreaNames = records
                    .GroupBy(x => x.Name)
                    .Where(x => x.Count() > 1)
                    .Select(x => x.Key).ToList();
                var doubleSubAreaIds = records
                    .Where(x => x.HasId())
                    .GroupBy(x => x.Id)
                    .Where(x => x.Count() > 1)
                    .Select(x => x.Key).ToList();

                if (invalidNames.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Following SubArea Names are longer then {SubArea.NameMaxLength}: {String.Join(", ", invalidNames)}");

                if (doubleSubAreaNames.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Following SubArea Names are not unique: {String.Join(", ", doubleSubAreaNames)}");

                if (doubleSubAreaIds.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Following SubArea Ids are not unique: {String.Join(", ", doubleSubAreaIds)}");

                if (validationMessages.Any())
                    context.MessageFormatter.AppendArgument("ValidationMessages", String.Join(Environment.NewLine, validationMessages));

                return !validationMessages.Any();
            }

            private bool HaveValidWaterAuthoritiesAndRayons(Command root, List<SubAreaCsvRecord> records, PropertyValidatorContext context)
            {
                var validationMessages = new List<string>();

                var waterAuthorityNamesImported = records.Select(x => x.WaterAuthorityName).Distinct().ToList();
                var waterAuthorityNamesFromDb = _currentVersionRegionalLayoutService
                    .QueryWaterAuthoritiesNoTracking().Select(x => x.Name).Distinct().ToList();
                var missingWaterAuthorities = waterAuthorityNamesFromDb.Where(waInDb => !waterAuthorityNamesImported.Contains(waInDb));
                var addedWaterAuthorities = waterAuthorityNamesImported.Where(waImport => !waterAuthorityNamesFromDb.Contains(waImport));

                if (missingWaterAuthorities.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Missing WaterAuthorities found (every water authority must be referenced at least once): " +
                        $"{String.Join(", ", missingWaterAuthorities)}");

                if (addedWaterAuthorities.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Adding new WaterAuthorities is not possible (use only existing water authorities): " +
                        $"{String.Join(", ", addedWaterAuthorities)}");

                var rayonNamesImported = records.Select(x => x.RayonName).Distinct().ToList();
                var rayonNamesFromDb = _currentVersionRegionalLayoutService
                    .QueryRayonsNoTracking().Select(x => x.Name).Distinct().ToList();
                var missingRayons = rayonNamesFromDb.Where(rInDb => !rayonNamesImported.Contains(rInDb));
                var addedRayons = rayonNamesImported.Where(rImport => !rayonNamesFromDb.Contains(rImport));

                if (missingRayons.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Missing Rayons found (every rayon must be referenced at least once): " +
                        $"{String.Join(", ", missingRayons)}");

                if (addedRayons.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Adding new Rayons is not possible (use only existing rayons): " +
                        $"{String.Join(", ", addedRayons)}");

                if (validationMessages.Any())
                    context.MessageFormatter.AppendArgument("ValidationMessages", String.Join(Environment.NewLine, validationMessages));

                return !validationMessages.Any();
            }

            private bool HaveValidCatchAreas(Command root, List<SubAreaCsvRecord> records, PropertyValidatorContext context)
            {
                var validationMessages = new List<string>();
                var invalidNames = records
                    .Where(x => x.CatchAreaName.Length > CatchArea.NameMaxLength)
                    .Select(x => x.CatchAreaName).ToList();
                var multipleRayons = records
                    .GroupBy(x => x.CatchAreaName)
                    .Where(group => group.Select(record => record.RayonName).Distinct().Count() > 1)
                    .Select(group => group.Key).ToList();

                if (invalidNames.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Following CatchArea Names are longer then {CatchArea.NameMaxLength}: {String.Join(", ", invalidNames)}");

                if (multipleRayons.Any())
                    validationMessages.Add($"{VersionRegionalLayoutImport.ErrorPrefix}: " +
                        $"Following CatchAreas belong to multiple rayons, which is not allowed: {String.Join(", ", multipleRayons)}");

                if (validationMessages.Any())
                    context.MessageFormatter.AppendArgument("ValidationMessages", String.Join(Environment.NewLine, validationMessages));

                return !validationMessages.Any();
            }
        }
    }
}
