using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Waterschapshuis.CatchRegistration.DomainModel.Anonymization;
using Waterschapshuis.CatchRegistration.DomainModel.Areas;
using Waterschapshuis.CatchRegistration.DomainModel.Common;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.Observations.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Observations
{
    public class Observation : EntityHasCreatedUpdatedRecorded<Guid>, IHasLocation, IAnonymizeCreatedUpdatedBy
    {
        public const int RemarkFieldMaxLength = 255;
        public const int PhotoUrlFieldMaxLength = 1024;

        private Observation() { }

        public Guid SubAreaHourSquareId { get; private set; }
        public SubAreaHourSquare SubAreaHourSquare { get; private set; } = null!;
        public ObservationType Type { get; private set; }
        public string? PhotoUrl { get; private set; }
        public string Remarks { get; private set; } = String.Empty;
        public User CreatedBy { get; } = null!;
        public User UpdatedBy { get; } = null!;
        public Point Location { get; private set; } = Point.Empty;
        public bool Archived { get; private set; }

        [NotMapped]
        public Guid? LocationOrganizationId { get; set; }

        public static Observation Create(
            Guid id,
            Guid subAreaHourSquareId,
            ObservationType type,
            string remarks,
            double longitude,
            double latitude,
            DateTimeOffset recordedOn,
            bool hasPhoto,
            AzureBlobSettings? azureBlobSettings = null)
        {
            var observation = new Observation
            {
                Id = id,
                SubAreaHourSquareId = subAreaHourSquareId,
                Type = type,
                PhotoUrl = hasPhoto ? CreatePhotoUrl(id, recordedOn, azureBlobSettings) : null,
                Remarks = remarks,
                Location = GeometryUtil.Factory.CreatePoint(longitude, latitude)
            };
            observation.SetRecorded(recordedOn);
            return observation;
        }

        private static string? CreatePhotoUrl(Guid id,
            in DateTimeOffset recordedOn,
            AzureBlobSettings? azureBlobSettings)
        {
            return azureBlobSettings == null ? null : $"{azureBlobSettings.Url}/{azureBlobSettings.BaseObservationBlobContainer}/{recordedOn.Year}%2F{recordedOn.Month}%2F{recordedOn.Day}%2F{id}.jpeg";
        }

        public static Observation Create(
            Guid id,
            SubAreaHourSquare subAreaHourSquare,
            ObservationType type,
            string remarks,
            double longitude,
            double latitude,
            DateTimeOffset recordedOn,
            bool hasPhoto,
            AzureBlobSettings? azureBlobSettings = null)
        {
            return Create(id, subAreaHourSquare.Id, type, remarks, longitude, latitude, recordedOn, hasPhoto, azureBlobSettings)
                .WithSubAreaHourSquare(subAreaHourSquare);
        }

        private Observation WithSubAreaHourSquare(SubAreaHourSquare subAreaHourSquare)
        {
            SubAreaHourSquare = subAreaHourSquare;
            return this;
        }
        
        public void Update(ObservationUpdate.Command command)
        {
            Type = command.Type;
            Archived = command.Archived;
            if (command.Remarks != null)
            {
                Remarks = command.Remarks;
            }
        }

        public void UpdateStatusAndRemarks(UpdateStatusAndRemarks.Command command)
        {
            Archived = command.Archived;
            if (command.Remarks != null)
            {
                Remarks = command.Remarks;
            }
        }

        public void InsertNewVersionOfSubAreaHourSquareId(Guid id)
        {
            SubAreaHourSquareId = id;
        }
    }
}
