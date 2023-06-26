import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { Guid } from 'guid-typescript';
import {
  SyncsClient,
  TrackingSyncCommand,
  TrackingSyncCommandTrackingItem,
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Logger } from 'src/app/core/logger/logger';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { ToastService } from 'src/app/services/toast.service';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';
import { ISyncable } from 'src/app/sync/syncable.interface';

import { TrackingLocation } from '../models/tracking-location.model';
import { MapStateService } from './map-state.service';

const logger = new Logger('TrackingSyncService');

@Injectable({
  providedIn: 'root'
})
export class TrackingSyncService implements ISyncable {

  syncProgressAlert: HTMLIonAlertElement;

  constructor(
    private syncsClient: SyncsClient,
    private sqliteProvider: SqliteProviderService,
    private mapStateService: MapStateService,
    private toastService: ToastService,
    public alertController: AlertController
  ) { }

  syncObjectName = 'speurkaart';

  async getObjectsToSync(): Promise<Array<TrackingLocation>> {
    return await this.sqliteProvider.getAll(SqliteTableConfig.tracking, 'timestamp')
      .catch((error: Error) => {
        this.syncProgressAlert.dismiss();
        this.toastService.error('Fout opgetreden');
        logger.error(error);
        return Promise.reject(error.message || error);
      });
  }

  async syncData(items: any[]): Promise<void> {
    const trackingLocations =
      [...items.map(
        item =>
          TrackingSyncCommandTrackingItem.fromJS(
            {
              ...item,
              recordedOn: new Date(item.timestamp)
            })
      )];

    await this.syncsClient.tracking(TrackingSyncCommand.fromJS({ trackingLocations })).toPromise();
  }

  async postSync(batch: Array<any>): Promise<void> {
    this.sqliteProvider.deleteByIds(SqliteTableConfig.tracking, batch.map(b => b.id));
    this.mapStateService.refreshLayers(OverlayLayerName.TrackingLines);
    this.mapStateService.refreshLayers(OverlayLayerName.TrackingsByUser);
    this.mapStateService.refreshLayers(OverlayLayerName.TrackingsByTrappers);
  }

  async countDataToSync(): Promise<number> {
    return await this.sqliteProvider.count(SqliteTableConfig.tracking);
  }
}
