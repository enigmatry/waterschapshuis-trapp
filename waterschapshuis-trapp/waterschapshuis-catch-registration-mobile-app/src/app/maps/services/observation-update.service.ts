import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import {
  ObservationUpdateCommand,
  GetObservationDetailsResponseItem
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { GeoJsonUpdateCommand } from 'src/app/geo-json/models/geo-json-update-command.model';
import { BaseOfflineEntityService, OfflineServiceContext } from 'src/app/shared/offline/base-offline-entity.service';
import { ObservationsCachedClient } from 'src/app/cache/clients/observation-cached.client';
import { ObservationGeoJsonUpdateCommand } from 'src/app/geo-json/models/observations/observation-geo-json-update-command.model';
import { ProjectionModel } from '../models/projection.model';

export class ObservationUpdateCommandVm extends ObservationUpdateCommand {
  remarks: string;
  longitude?: number;
  latitude?: number;

  static fromJS(data: any): ObservationUpdateCommandVm {
    data = typeof data === 'object' ? data : {};
    const result = new ObservationUpdateCommandVm();
    result.init(data);
    return result;
  }

  init(data?: any) {
    super.init(data);
    if (data) {
      this.type = data.type;
      this.remarks = data.remarks;
      this.longitude = data.longitude;
      this.latitude = data.latitude;
    }
  }

  toJSON(data?: any) {
    data = super.toJSON(data);
    data.type = this.type;
    data.remarks = this.remarks;
    data.longitude = this.longitude;
    data.latitude = this.latitude;
    return data;
  }
}

@Injectable({
  providedIn: 'root'
})
export class ObservationUpdateService extends BaseOfflineEntityService<ObservationUpdateCommandVm, GetObservationDetailsResponseItem> {

  get entityCacheKey(): string {
    return 'observations';
  }

  constructor(
    private observationsClient: ObservationsCachedClient,
    protected offlineServiceContext: OfflineServiceContext
  ) {
    super(offlineServiceContext);
  }

  updateObservation(value: any): Observable<GetObservationDetailsResponseItem> {
    const command = ObservationUpdateCommandVm.fromJS(value);
    return super.executeSave(command, this.observationsClient.update.bind(this.observationsClient));
  }

  mapToGeoJsonUpdateCommands(command: ObservationUpdateCommandVm): Array<GeoJsonUpdateCommand> {
    return [
      ObservationGeoJsonUpdateCommand.updateCommand(command, 'Inactive', ProjectionModel.dutchMatrix, ProjectionModel.mercatorMatrix),
      ObservationGeoJsonUpdateCommand.updateCommand(command, 'Active', ProjectionModel.dutchMatrix, ProjectionModel.mercatorMatrix)
    ];
  }

  updateCacheValue(command: ObservationUpdateCommandVm, target: GetObservationDetailsResponseItem): Observable<any> {
    return this.commandToModel(command, target);
  }

  private commandToModel(
    command: ObservationUpdateCommandVm,
    model: GetObservationDetailsResponseItem
  ): Observable<GetObservationDetailsResponseItem> {
    model.archived = true;
    return of(model);
  }

}
