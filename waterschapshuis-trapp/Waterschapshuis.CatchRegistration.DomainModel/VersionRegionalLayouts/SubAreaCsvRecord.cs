using CsvHelper;
using CsvHelper.Configuration;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts
{
    public class SubAreaCsvRecord
    {
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string CatchAreaName { get; set; } = String.Empty;
        public string RayonName { get; set; } = String.Empty;
        public string WaterAuthorityName { get; set; } = String.Empty;
        public string GeometryAsWKT { get; set; } = String.Empty;
        public string ValidationMessage { get; set; } = String.Empty;

        public bool HasId() => !String.IsNullOrWhiteSpace(Id);
    }

    [UsedImplicitly]
    public sealed class SubAreaCsvRecrodMap : ClassMap<SubAreaCsvRecord>
    {
        public const string GeomatryFieldHeader = "WKT";
        public const string IdFieldHeader = "Id";
        public const string NameFieldHeader = "Name";
        public const string CatchAreaFieldHeader = "CatchArea";
        public const string RayonFieldHeader = "Rayon";
        public const string WaterAuthorityFieldHeader = "WaterAuthority";

        public SubAreaCsvRecrodMap()
        {
            Map(x => x.GeometryAsWKT).Index(0).Name(GeomatryFieldHeader);
            Map(x => x.Id).Index(1).Name(IdFieldHeader);
            Map(x => x.Name).Index(2).Name(NameFieldHeader);
            Map(x => x.CatchAreaName).Index(3).Name(CatchAreaFieldHeader);
            Map(x => x.RayonName).Index(4).Name(RayonFieldHeader);
            Map(x => x.WaterAuthorityName).Index(5).Name(WaterAuthorityFieldHeader);

            Map(x => x.ValidationMessage)
                .ConvertUsing((IReaderRow row) =>
                {
                    var invalidFields = new List<string>();

                    if (HasNoValue(row.GetField(GeomatryFieldHeader))) // TODO Add validation of geometry
                        invalidFields.Add($"{GeomatryFieldHeader} required");
                    if (InvalidNullableGuid(row.GetField(IdFieldHeader)))
                        invalidFields.Add($"{IdFieldHeader} must be empty or valid guid");
                    if (HasNoValue(row.GetField(NameFieldHeader)))
                        invalidFields.Add($"{NameFieldHeader} required");
                    if (HasNoValue(row.GetField(CatchAreaFieldHeader)))
                        invalidFields.Add($"{CatchAreaFieldHeader} required");
                    if (HasNoValue(row.GetField(RayonFieldHeader)))
                        invalidFields.Add($"{RayonFieldHeader} required");
                    if (HasNoValue(row.GetField(WaterAuthorityFieldHeader)))
                        invalidFields.Add($"{WaterAuthorityFieldHeader} required");

                    return invalidFields.Any()
                        ? $"Invalid record at row {row.Context.Row}: {String.Join(", ", invalidFields)}."
                        : "";
                })
                .Ignore();
        }

        private bool HasNoValue(string value) => String.IsNullOrWhiteSpace(value);

        private bool InvalidNullableGuid(string value) =>
            !HasNoValue(value) &&
            (
                Guid.TryParse(value, out var output) == false ||
                value == Guid.Empty.ToString()
            );
    }
}
