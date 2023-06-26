import { Injectable, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import {
  IObservationSyncResult,
  ObservationSyncCommand,
  ObservationSyncCommandObservationItem,
  SyncsClient
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { BlobStorageService } from 'src/app/azure-blob-storage/services/blob-storage.service';
import { SasGeneratorService } from 'src/app/azure-blob-storage/services/sas-generator.service';
import { BlobFileRequest } from 'src/app/azure-blob-storage/types/azure-storage';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { ToastService } from 'src/app/services/toast.service';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';
import { ISyncable } from 'src/app/sync/syncable.interface';
import { environment } from 'src/environments/environment';
import { Observation } from '../models/observation.model';
import { MapStateService } from './map-state.service';
import { Logger } from 'src/app/core/logger/logger';
import { ObservationsCachedClient } from 'src/app/cache/clients/observation-cached.client';

const logger = new Logger('ObservationSyncService');

@Injectable({
  providedIn: 'root'
})
export class ObservationSyncService implements ISyncable, OnDestroy {

  uploadItemSubscription: Subscription;
  syncObjectName = 'meldingen';

  syncProgressAlert: HTMLIonAlertElement;
  syncCanceled: boolean;

  constructor(
    private sqliteProviderService: SqliteProviderService,
    private observationCacheService: ObservationsCachedClient,
    private syncsClient: SyncsClient,
    private toastService: ToastService,
    private mapStateService: MapStateService,
    private sasGeneratorService: SasGeneratorService,
    private blobStorageService: BlobStorageService
  ) { }

  ngOnDestroy(): void {
    if (this.uploadItemSubscription) {
      this.uploadItemSubscription.unsubscribe();
    }
  }

  async getObjectsToSync(): Promise<Array<Observation>> {
    return await this.sqliteProviderService.getAll(SqliteTableConfig.observations)
      .catch((error: Error) => {
        this.syncProgressAlert.dismiss();
        this.toastService.error('Fout opgetreden');
        return Promise.reject(error.message || error);
      });
  }

  async syncData(observationsToSync: Array<Observation>): Promise<void> {

    const observationsToBeSyncedToApi =
      [...observationsToSync
        .filter(i => !Boolean(i.syncedToApi)) // send to api only observations that are not already sync
        .map(
          item =>
            ObservationSyncCommandObservationItem.fromJS({ ...item, hasPhoto: !!item.image })
        )];

    const response = await this.syncObservationToApi(ObservationSyncCommand.fromJS({ observations: observationsToBeSyncedToApi }));

    await this.markObservationsAsSyncedToApi(response);

    await this.uploadToBlobStorage(observationsToSync, response).then();
  }

  async countDataToSync(): Promise<number> {
    return await this.sqliteProviderService.count(SqliteTableConfig.observations);
  }

  async postSync(batch: Array<any>): Promise<void> {
    batch.forEach(item => this.observationCacheService.deleteCacheItem(item.id));
    this.mapStateService.refreshLayers(OverlayLayerName.Observations);
  }

  async uploadToBlobStorage(observationsToSync: Observation[], response: IObservationSyncResult): Promise<void> {
    this.sasGeneratorService.setSasToken(response.storageAccessKey);

    const list = [];

    for (const item of observationsToSync) {
      try {
        if (response.savedItems.length < 1 || !response.savedItems.find(x => x.id.toUpperCase() === item.id.toUpperCase())?.isNew) {
          this.deleteObservationLocally(item.id);
          return;
        }

        if (item.image) {
          const imageBlob = await this.getBlob(item.image);

          const imageFile = Object.assign(
            imageBlob,
            {
              lastModified: new Date(),
              name: `${item.id}.jpeg`,
              containerName: this.getContainerName(new Date(item.recordedOn)),
              id: item.id
            }
          );

          list.push(imageFile);
        } else {
          this.deleteObservationLocally(item.id);
        }
      } catch (error) {
        // if image data processing fails for some reason, log error and continue to next image
        logger.error(error);
      }
    }

    for (const file of list) {
      this.blobStorageService.uploadToBlobStorage(
        file,
        this.getBlobFileRequest(file.name, `${environment.azureStorage.baseObservationBlobContainer}/${file.containerName}`)
      ).toPromise().then(
        () => this.deleteObservationLocally(file.id),
        e => logger.error(e)
      );
    }

  }

  private getBlobFileRequest(filename: string, containerName: string): BlobFileRequest {
    return {
      ...this.sasGeneratorService.getSasTokenData(),
      containerName,
      filename
    };
  }

  private deleteObservationLocally(id: string): void {
    this.sqliteProviderService.deleteByIds(SqliteTableConfig.observations, [id]);
  }

  private getContainerName(recordedOn: Date): any {
    return `${recordedOn.getFullYear()}/${recordedOn.getMonth() + 1}/${recordedOn.getDate()}`;
  }

  private async markObservationsAsSyncedToApi(response: IObservationSyncResult): Promise<void> {
    for (const savedItem of response.savedItems) {
      await this.sqliteProviderService.updateById(
        SqliteTableConfig.observations,
        {
          syncedToApi: 1,
        },
        savedItem.id
      );
    }
  }

  private syncObservationToApi(command: ObservationSyncCommand): Promise<IObservationSyncResult> {
    return this.syncsClient.observation(command).toPromise();
  }

  private async getBlob(b64Data: string): Promise<Blob> {
    const sliceSize = 512;

    b64Data = b64Data.replace(/data\:image\/(jpeg|jpg|png)\;base64\,/gi, '');

    const byteCharacters = atob(b64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
      const slice = byteCharacters.slice(offset, offset + sliceSize);

      const byteNumbers = new Array(slice.length);
      for (let i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i);
      }

      const byteArray = new Uint8Array(byteNumbers);
      byteArrays.push(byteArray);
    }

    const blob = new Blob(byteArrays, { type: 'image/jpeg' });
    return blob;
  }
}
