import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { AppSettings } from '../app-configuration/app-settings';
import { ToastService } from '../services/toast.service';
import { SyncConfig } from './sync-config';
import { ISyncable } from './syncable.interface';
import { Logger } from '../core/logger/logger';
import { BehaviorSubject, Subject } from 'rxjs';

const logger = new Logger('SyncService');

@Injectable({
  providedIn: 'root'
})
export class SyncService {

  syncProgressAlert: HTMLIonAlertElement;
  syncCanceled: boolean;
  private isRunningSubject = new BehaviorSubject<boolean>(false);
  isRunning$ = this.isRunningSubject.asObservable();

  public get isRunning(): boolean {
    return this.isRunningSubject.getValue();
  }

  constructor(
    private alertController: AlertController,
    private toast: ToastService
  ) { }

  async sync(syncItems: Array<ISyncable>, config?: SyncConfig): Promise<void> {

    if (this.isRunning) {
      logger.debug('Sync not started: Already running.');
      return;
    }

    this.isRunningSubject.next(true);

    const silentSync = config && config.silentSync;

    if (!silentSync) {
      await this.showSyncProgressAlert(config && config.disableSyncCancel);
    }

    for (const syncItem of syncItems) {

      const itemsToSync = await syncItem.getObjectsToSync();

      if (itemsToSync.length === 0) {
        continue;
      }

      logger.debug(`SyncService synchronization ${itemsToSync.length} ${syncItem.syncObjectName} started.`);

      if (!silentSync) {
        this.updateSyncProgressMessage(`${itemsToSync.length} records synchroniseren`, `Synchroniseren ${syncItem.syncObjectName}`);
      }

      const batchSize = AppSettings.synchronizationSettings.syncBatchSize;
      const itemBatches = this.chunkArray([...itemsToSync], batchSize);

      for (const itemBatch of itemBatches) {

        if (this.syncCanceled) {
          this.syncCanceled = false;
          return;
        }

        await syncItem.syncData(itemBatch).then(async () => {
          if (!silentSync) {
            this.updateSyncProgressMessage(
              `${this.getNumberOfSyncedItems(batchSize, itemBatches.indexOf(itemBatch), itemsToSync.length)}
           van ${itemsToSync.length} records synchroniseren.`);
          }

          if (syncItem.postSync) {
            await syncItem.postSync(itemBatch);
          }
        },
          (error) => {
            this.handleError(error, silentSync);
          });
      }
    }

    if (!silentSync) {
      this.syncProgressAlert.dismiss();
    }

    this.isRunningSubject.next(false);
  }

  private handleError(error: any, silentSync: boolean): void {
    logger.error(error);
    if (!silentSync) {
      this.syncProgressAlert.dismiss();
      this.toast.error('Fout opgetreden');
    }
  }

  private async showSyncProgressAlert(disableCancel: boolean): Promise<void> {
    this.syncProgressAlert = await this.alertController.create({
      header: 'Synchroniseer',
      message: 'Synchroniseren...',
      buttons: disableCancel ? [] : [
        {
          text: 'Annuleer',
          cssClass: 'primary',
          handler: () => {
            this.syncCanceled = true;
          }
        }
      ]
    });

    await this.syncProgressAlert.present();
  }

  private updateSyncProgressMessage(message: string, header?: string): void {
    if (this.syncProgressAlert) {
      if (header) {
        this.syncProgressAlert.header = header;
      }
      this.syncProgressAlert.message = message;
      this.syncProgressAlert.forceUpdate();
    }
  }

  private chunkArray(items: Array<any>, chunkSize: number): Array<Array<any>> {
    const results = [];

    while (items.length) {
      results.push(items.splice(0, chunkSize));
    }

    return results;
  }

  private getNumberOfSyncedItems(batchSize: number, indexOfCurrentItem: number, noOfAllItems: number): number {
    return batchSize * (indexOfCurrentItem + 1) > noOfAllItems ? noOfAllItems : batchSize * (indexOfCurrentItem + 1);
  }
}
