using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;
using Waterschapshuis.CatchRegistration.DomainModel.Traps.Commands;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Catches.Commands;

namespace Waterschapshuis.CatchRegistration.DomainModel.Auditing
{
    public class TrapHistoryDomainEvent : HistoryDomainEvent
    {
        public Guid? CatchId { get; private set; } = null;

        private TrapHistoryDomainEvent(Guid trackedEntityId, params string[] messages) 
            : base(trackedEntityId, messages) { }

        private TrapHistoryDomainEvent(Guid trackedEntityId, Guid catchId, params string[] messages) : base(trackedEntityId, messages)
        {
            CatchId = catchId;
        }

        public static TrapHistoryDomainEvent OnTrapCreate(Trap trackedEntity)
        {
            return new TrapHistoryDomainEvent(
                trackedEntity.Id, TrapHistory.GetTrapCreatedMessage(trackedEntity.NumberOfTraps));
        }

        public static TrapHistoryDomainEvent? OnTrapUpdate(Trap trackedEntity, TrapCreateOrUpdate.Command updateCommand)
        {
            var messages = new List<string>();

            if (updateCommand.NumberOfTraps != trackedEntity.NumberOfTraps)
            {
                messages.Add(TrapHistory.GetTrapNumberChangedMessage(updateCommand.NumberOfTraps));
            }

            if (updateCommand.Status != trackedEntity.Status)
            {
                messages.Add(updateCommand.Status == TrapStatus.Removed
                    ? $"Vangmiddel {updateCommand.Status.GetDescription().ToLower()}"
                    : $"Vangmiddel staat {updateCommand.Status.GetDescription().ToLower()}");
            }

            if (updateCommand.Longitude != trackedEntity.Location.X || updateCommand.Latitude != trackedEntity.Location.Y)
            {
                messages.Add(TrapHistory.MessageOnTrapLocationUpdate);
            }

            return messages.Any()
                ? new TrapHistoryDomainEvent(trackedEntity.Id, messages.ToArray())
                : null;
        }

        public static TrapHistoryDomainEvent? OnTrapUpdate(Trap trackedEntity, TrapUpdate.Command updateCommand)
        {
            var createOrUpdateCommand = TrapCreateOrUpdate.Command.CreateFrom(trackedEntity, false);
            createOrUpdateCommand.TrapTypeId = updateCommand.TrapTypeId;
            createOrUpdateCommand.Remarks = updateCommand.Remarks;
            return OnTrapUpdate(trackedEntity, createOrUpdateCommand);
        }

        public static TrapHistoryDomainEvent OnCatchCreate(Catch trackedEntity) =>
            new TrapHistoryDomainEvent(
                trackedEntity.TrapId,
                trackedEntity.Id,
                $"{(trackedEntity.CatchType.IsByCatch ? "Bijvangst" : "Vangst")} {trackedEntity.Number}x {trackedEntity.CatchType.Name}"
            );

        public static TrapHistoryDomainEvent? OnCatchUpdate(Catch trackedEntity, CatchCreateOrUpdate.Command updateCommand, CatchType updatedCatchType)
        {
            var messages = new List<string>();
            if (updateCommand.Number != trackedEntity.Number || updateCommand.CatchTypeId != trackedEntity.CatchTypeId)
            {
                messages.Add(
                    $"{(updatedCatchType.IsByCatch ? "Bijvangst" : "Vangst")} gewijzigd {updateCommand.Number}x {updatedCatchType.Name}");
            }

            return messages.Any()
                ? new TrapHistoryDomainEvent(trackedEntity.TrapId, trackedEntity.Id, messages.ToArray())
                : null;
        }

        public static TrapHistoryDomainEvent OnCatchRemove(Catch trackedEntity) =>
            new TrapHistoryDomainEvent(
                trackedEntity.TrapId,
                trackedEntity.Id,
                $"{(trackedEntity.CatchType.IsByCatch ? "Bijvangst" : "Vangst")} {trackedEntity.Number}x {trackedEntity.CatchType.Name} is permanent verwijderd"
            );
    }
}
