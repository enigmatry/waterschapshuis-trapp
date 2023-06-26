using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Anonymization;
using Waterschapshuis.CatchRegistration.DomainModel.Auditing;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;

namespace Waterschapshuis.CatchRegistration.DomainModel.Traps
{
    public class TrapHistory : EntityHasCreatedUpdatedRecorded<Guid>, IAnonymizeCreatedBy
    {
        public const int MessageMaxLength = 250;
        public const string MessageOnTrapLocationUpdate = "Vangmiddel verplaatst";

        private TrapHistory() { }

        public User CreatedBy { get; } = null!;
        public User UpdatedBy { get; } = null!;
        public string Message { get; private set; } = null!;

        public Guid TrapId { get; private set; }
        public Trap Trap { get; private set; } = null!;
        public Guid? CatchId { get; private set; }
        public Catch Catch { get; private set; } = null!;

        [NotMapped]
        public Guid? LocationOrganizationId { get; set; }

        public static List<TrapHistory> Create(TrapHistoryDomainEvent command) =>
            command.Messages
                .Select(message => Create(command.TrackedEntityId, command.CatchId, message, command.RecordedOn))
                .ToList();

        private static TrapHistory Create(Guid trapId, Guid? catchId, string message, DateTimeOffset recordedOn)
        {
            var result = new TrapHistory
            {
                Id = GenerateId(),
                TrapId = trapId,
                CatchId = catchId,
                Message = message
            };
            result.SetRecorded(recordedOn);
            return result;
        }

        public static string GetTrapCreatedMessage(int numberOfTraps)
        {
            return $"Vangmiddel geplaatst: {numberOfTraps}";
        }

        public static string GetTrapNumberChangedMessage(int numberOfTraps)
        {
            return $"Aantal vangmiddelen gewijzigd: {numberOfTraps}";
        }
    }
}
