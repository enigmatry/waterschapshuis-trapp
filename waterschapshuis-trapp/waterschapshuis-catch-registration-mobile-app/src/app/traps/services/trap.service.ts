
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TrapsCachedClient } from 'src/app/cache/clients/traps-cached.client';
import { CatchCreateOrUpdateCommandVm } from 'src/app/traps/model/catch-create-or-update-command-vm';
import { GeoJsonUpdateCommand } from 'src/app/geo-json/models/geo-json-update-command.model';
import { TrapGeoJsonUpdateCommand } from 'src/app/geo-json/models/traps/trap-geo-json-update-command.model';
import { ProjectionModel } from 'src/app/maps/models/projection.model';
import { TrapDetails } from 'src/app/maps/models/trap-details.model';
import { CurrentUserProviderService } from 'src/app/shared/services/current-user-provider.service';
import { LookupsService } from 'src/app/shared/services/lookups.service';

import {
  GetCatchDetailsCatchItem,
  GetTrapDetailsTrapItem,
  IGetMySummaryResponse,
  IGetTrapDetailsTrapItem,
  TrapCreateOrUpdateCommand,
  TrapStatus,
} from '../../api/waterschapshuis-catch-registration-mobile-api';
import { BaseOfflineEntityService, OfflineServiceContext } from '../../shared/offline/base-offline-entity.service';
import { TrapCreateOrUpdateCommandVm } from '../model/trap-create-or-update-command-vm';
import { UserSummary } from '../model/user-summary';

@Injectable({
  providedIn: 'root'
})
export class TrapService extends BaseOfflineEntityService<TrapCreateOrUpdateCommand, IGetTrapDetailsTrapItem> {
  get entityCacheKey(): string {
    return 'traps';
  }

  constructor(
    private lookupsService: LookupsService,
    private trapClient: TrapsCachedClient,
    offlineServiceContext: OfflineServiceContext,
    private currentUserService: CurrentUserProviderService
  ) {
    super(offlineServiceContext);
  }


  saveTrap(command: TrapCreateOrUpdateCommandVm): Observable<IGetTrapDetailsTrapItem> {
    return super.executeSave(command, this.trapClient.post.bind(this.trapClient));
  }

  deleteTrap(command: TrapCreateOrUpdateCommandVm): Observable<any> {
    return super.executeDelete(command, this.trapClient.delete.bind(this.trapClient));
  }

  mapToGeoJsonUpdateCommands(command: TrapCreateOrUpdateCommandVm): Array<GeoJsonUpdateCommand> {
    const commands = [new TrapGeoJsonUpdateCommand(command, command.status, ProjectionModel.dutchMatrix, ProjectionModel.mercatorMatrix)];

    if (command.previousStatus) {
      // additionally add command to remove trap from previous layer
      commands.push(
        new TrapGeoJsonUpdateCommand(command, command.previousStatus, ProjectionModel.dutchMatrix, ProjectionModel.mercatorMatrix)
      );
    }
    return commands;
  }

  updateCacheValue(command: TrapCreateOrUpdateCommand, target?: IGetTrapDetailsTrapItem): Observable<IGetTrapDetailsTrapItem> {
    return target ?
      this.commandToModel(command, target) :
      this.commandToModel(command, new GetTrapDetailsTrapItem());
  }

  get(id: string): Observable<TrapDetails> {
    return this.trapClient.get(id)
      .pipe(
        map((response: IGetTrapDetailsTrapItem) => TrapDetails.fromResponse(response))
      );
  }

  getTrapsForGivenTrapIds(trapIds: string[]): Observable<Array<TrapDetails>> {
    return this.trapClient.getMultiple(trapIds)
      .pipe(
        map((response: Array<IGetTrapDetailsTrapItem>) => response.map(tl => TrapDetails.fromResponse(tl)))
      );
  }

  getCurrentUserSummary(includeDetails: boolean): Observable<UserSummary> {
    return this.trapClient.getCurrentUserSummary(includeDetails)
      .pipe(
        map((response: IGetMySummaryResponse) => UserSummary.fromResponse(response))
      );
  }

  private commandToModel(command: TrapCreateOrUpdateCommand, model: IGetTrapDetailsTrapItem): Observable<IGetTrapDetailsTrapItem> {
    const catches = model?.catches ? model.catches : [];
    if (command.catches) {
      command.catches.forEach((cmdCatch: CatchCreateOrUpdateCommandVm) => catches.push(this.catchCommandToModel(cmdCatch)));
    }
    return this.lookupsService.getTrapType(command.trapTypeId)
      .pipe(
        map(data => {
          return {
            id: command.id,
            type: data?.name,
            status: command.status as TrapStatus,
            remarks: command.remarks,
            numberOfTraps: command.numberOfTraps,
            trapTypeId: data?.id,
            trappingTypeId: data?.trappingTypeId,
            longitude: command.longitude,
            latitude: command.latitude,
            createdBy: model.createdBy ?? this.currentUserService?.currentUser?.name,
            recordedOn: model.recordedOn ?? new Date(),
            catches,
            createdOn: model.recordedOn ?? new Date(),
            createdById: model.createdById ?? this.currentUserService?.currentUser?.id
          };
        })
      );
  }

  private catchCommandToModel(cmdCatch: CatchCreateOrUpdateCommandVm): GetCatchDetailsCatchItem {
    const item = new GetCatchDetailsCatchItem();

    item.id = cmdCatch.id;
    item.type = cmdCatch.type;
    item.number = cmdCatch.number;
    item.isByCatch = cmdCatch.isByCatch;
    item.createdBy = this.currentUserService?.currentUser?.name;
    item.recordedOn = cmdCatch.recordedOn;
    item.catchTypeId = cmdCatch.catchTypeId;

    return item;
  }
}

