import { Component, OnInit } from '@angular/core';
import { ToastService } from 'src/app/services/toast.service';
import { NetworkService } from 'src/app/network/network.service';
import { Insomnia } from '@ionic-native/insomnia/ngx';
import { ProgressService } from 'src/app/shared/services/progress.service';
import { OfflineMapService } from 'src/app/maps/services/offline-map.service';
import { PreFetchSyncService } from 'src/app/sync/prefetch-sync.service';
import { LocalGeoJsonService } from 'src/app/geo-json/services/local-geo-json.service';
import { forkJoin, Subscription } from 'rxjs';
import { OfflineMapDownloadModalComponent } from '../offline-map-download-modal/offline-map-download-modal.component';
import { ModalController } from '@ionic/angular';
import { ProgressEmitter } from 'src/app/shared/models/progressEmitter';
import { Logger } from 'src/app/core/logger/logger';

const logger = new Logger('OfflineDataComponent');

@Component({
  selector: 'app-offline-data',
  templateUrl: './offline-data.component.html',
  styleUrls: ['./offline-data.component.scss'],
})
export class OfflineDataComponent implements OnInit {
  offlineMapAvailable: boolean;
  offlineMapLastDownloadDate: string;
  downloadProcessSubscription: Subscription;

  constructor(
    public progressService: ProgressService,
    public offlineService: OfflineMapService,
    private modal: ModalController,
    private preFetchSyncService: PreFetchSyncService,
    private localGeoJsonService: LocalGeoJsonService,
    private networkService: NetworkService,
    private insomnia: Insomnia,
    private toastService: ToastService) { }

  ngOnInit(): void {
    this.offlineService.offlineMapAvailabilityStatus().subscribe(offlineMapData => {
      this.offlineMapLastDownloadDate = offlineMapData.downloadDate;
      this.offlineMapAvailable = offlineMapData.available;
    });
  }

  downloadOfflineMap() {

    if (this.networkService.isOffline()) {
      this.showNetworkOfflineNotification();
      return;
    }

    this.generateOfflineMapDownloadModal();

    this.insomnia.keepAwake().then(_ =>
      logger.debug('Device keep awake mode activated'));

    this.offlineService.fileTransfer.onProgress((progress) =>
      this.progressService.emitProgressPercentage(progress, ProgressEmitter.OfflineMapDownloading));

    const downloadMap = this.offlineService.offlineMapDownload();
    const prefetchData = this.preFetchSyncService.prefetchAllForCurrentLocation();
    const prefetchJson = this.localGeoJsonService.prefetchAll();

    this.downloadProcessSubscription = forkJoin([downloadMap, prefetchData, prefetchJson])
      .subscribe(() => {
        logger.debug('Offline data successfully downloaded.');
        this.insomnia.allowSleepAgain().then(_ =>
          logger.debug('Device keep awake mode deactivated'));
        this.modal.dismiss();
        this.progressService.resetProgress();
      },
        err => {
          logger.error(err, 'Offline data download error');
          this.insomnia.allowSleepAgain().then(_ =>
            logger.debug('Device keep awake mode deactivated'));
          this.modal.dismiss();
          this.progressService.resetProgress();
        },
        () => logger.debug('Offline data prefetch and download operation completed.'));
  }

  async generateOfflineMapDownloadModal() {
    const offlineMapDownloadModal = await this.modal.create({
      component: OfflineMapDownloadModalComponent,
      id: 'OfflineMapDownloadModal',
      cssClass: 'offline-map-download-modal',
      backdropDismiss: false
    });

    offlineMapDownloadModal.present();

    offlineMapDownloadModal.onDidDismiss().then(result => {
      if (result.data && result.data.cancelled) {
        logger.debug('offline components download cancelled!');
        this.offlineService.cancelDownloadingOperation();
        this.progressService.resetProgress();
        this.offlineService.fileTransfer.abort();
        this.downloadProcessSubscription.unsubscribe();
      }
    });
  }

  private showNetworkOfflineNotification() {
    this.toastService.error('Controleer uw internetverbinding of probeer het later opnieuw');
  }
}
