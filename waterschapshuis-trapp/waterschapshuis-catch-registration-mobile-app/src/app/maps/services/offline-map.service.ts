import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BlobItem } from '@azure/storage-blob';
import { DirectoryEntry } from '@ionic-native/file';
import { FileTransfer, FileTransferObject } from '@ionic-native/file-transfer/ngx';
import { File } from '@ionic-native/file/ngx';
import { Zip } from '@ionic-native/zip/ngx';
import { BehaviorSubject, from, Observable, of, Subject } from 'rxjs';
import { catchError, concatMap, finalize, last, map, switchMap } from 'rxjs/operators';
import { BlobStorageClient } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { AppSettings } from 'src/app/app-configuration/app-settings';
import { BlobSharedViewStateService } from 'src/app/azure-blob-storage/services/blob-shared-view-state.service';
import { SasGeneratorService } from 'src/app/azure-blob-storage/services/sas-generator.service';
import { OfflineMapDetails, OfflineMapDetailsService } from 'src/app/maps/services/offline-map-details.service';
import { ProgressService } from 'src/app/shared/services/progress.service';
import { ToastService } from 'src/app/services/toast.service';
import { ProgressEmitter } from 'src/app/shared/models/progressEmitter';
import { Logger } from 'src/app/core/logger/logger';

const logger = new Logger('OfflineMapService');

@Injectable({
  providedIn: 'root'
})
export class OfflineMapService {
  constructor(
    private httpClient: HttpClient,
    private transfer: FileTransfer,
    private progressService: ProgressService,
    private blobSharedViewStateService: BlobSharedViewStateService,
    private blobStorageClient: BlobStorageClient,
    private sasGeneratorService: SasGeneratorService,
    private offlineMapDetailsService: OfflineMapDetailsService,
    private toast: ToastService,
    private ionicZip: Zip,
    private file: File
  ) {
    this.checkOfflineMapInitialAvailability().then(async isAvailable => {
      const lastDownloadDate = await this.offlineMapDetailsService.getOfflineMapDownloadDate();
      this.isOfflineMapAvailable.next({ available: isAvailable, downloadDate: lastDownloadDate });
    });
  }

  operationIsCancelled: boolean;
  fileTransfer: FileTransferObject = this.transfer.create();

  private isOfflineMapAvailable: Subject<OfflineMapDetails> =
    new BehaviorSubject<OfflineMapDetails>({ available: false, downloadDate: '' });

  // Remove following variables once offline map download performance is analyzed
  unzipTimeElapsed: number;
  downloadMapZipFromBlobStorageTimeElapsed: number;


  public offlineMapDownload(): Observable<boolean> {
    const isStoredMapLatest$ = this.fetchLatestModifiedOfflineMapZipFileInfo().pipe(
      concatMap(zipFileInfo => this.isStoredMapVersionLatest(zipFileInfo.properties.lastModified)
        .pipe(map(isStoredLatest => ({ zipFileInfo, isStoredLatest })), catchError(err => of(err))))
    );

    return isStoredMapLatest$.pipe(
      switchMap(({ zipFileInfo, isStoredLatest }) => {
        if (isStoredLatest) {
          logger.debug('Latest offline map already downloaded.');
          this.toast.success('De meest recente versie van de offline kaart is al gedownload');
          return of(false);
        } else {
          return this.offlineMapDownloadInternal(zipFileInfo.properties.lastModified);
        }
      }));
  }

  public offlineMapDownloadInternal(lastModified: Date): Observable<boolean> {
    this.toast.success('Het downloaden van de offline kaart is gestart');

    return this.createTempDirectory().pipe(
      concatMap(_ => this.sasGeneratorService.getSasToken()),
      concatMap(data => this.downloadFile(data.storageUri, data.storageAccessToken)),
      concatMap(_ => this.unzipTiles()),
      concatMap(mapDownloadedSucesfully => this.updateStoredOfflineMapDetails(mapDownloadedSucesfully, lastModified)),
      catchError((err) => {
        this.toast.error('Er is iets fout gegaan. Probeer het alstublieft opnieuw.');
        logger.error(new Error(`Offline map download error: ${err}`));
        return of(false);
      }),
      finalize(() => this.removeTempDirAndFile()));
  }

  public downloadFile(storageUri: string, storageAccessToken: string): Promise<any> {
    const url = `${storageUri}/${AppSettings.offlineMapSettings.blobStorage.containerName}/` +
      `${AppSettings.offlineMapSettings.blobStorage.zipFileName}?${storageAccessToken}`;

    return this.fileTransfer.download(url, `${this.file.dataDirectory}/` +
      `${AppSettings.offlineMapSettings.fileSystemLocation.tempDirectoryName}` +
      `/${AppSettings.offlineMapSettings.fileSystemLocation.tempZipFilename}`).then(() => {
        this.progressService.resetProgress();
        logger.debug(`Offline map archive ${AppSettings.offlineMapSettings.blobStorage.zipFileName} successfully downloaded.`);
      }).catch(err => {
        this.progressService.resetProgress();
        logger.error(err);
      });
  }

  public fetchLatestModifiedOfflineMapZipFileInfo(): Observable<BlobItem> {
    return this.blobStorageClient.getSasKey().pipe(switchMap(response => {
      this.sasGeneratorService.setSasToken(response.sasKey);
      return this.blobSharedViewStateService.getLastModifiedContainerItem(AppSettings.offlineMapSettings.blobStorage.containerName)
        .pipe(last(), map(res => res.find(blobItem => blobItem.name === AppSettings.offlineMapSettings.blobStorage.zipFileName)));
    }));
  }

  public isStoredMapVersionLatest(blobStorageMapFileLastModifiedDate: Date): Observable<boolean> {
    return from(this.offlineMapDetailsService.getStoredOfflineMapLastModifiedDate()
      .then(storedOfflineMapModifiedDate => blobStorageMapFileLastModifiedDate.getTime() === storedOfflineMapModifiedDate.getTime()));
  }

  public getOfflineMapStyle(): Observable<any> {
    return this.httpClient.get('/assets/offline-map-styles/osm-bright-style.json');
  }

  public offlineMapAvailabilityStatus(): Observable<OfflineMapDetails> {
    return this.isOfflineMapAvailable.asObservable();
  }

  public cancelDownloadingOperation() {
    this.operationIsCancelled = true;
  }

  private updateStoredOfflineMapDetails(mapDownloadedSucesfully: boolean, downloadedZipFileLastModifiedDate: Date): Observable<boolean> {
    if (!mapDownloadedSucesfully) {
      return of(false);
    }

    return from(this.offlineMapDetailsService.updateOfflineMapDownloadDate(downloadedZipFileLastModifiedDate).then(_ => {
      this.isOfflineMapAvailable.next({ available: true, downloadDate: new Date().toString() });
      return true;
    }).catch(err => {
      logger.error(err, 'Offline map download date sql lite insert failed.');
      return false;
    }));
  }

  private checkOfflineMapInitialAvailability(): Promise<boolean> {
    return this.file.checkDir(
      this.file.dataDirectory, AppSettings.offlineMapSettings.
        fileSystemLocation.pbfDirectoryName)
      .catch(_ => false);
  }

  private createTempDirectory(): Observable<DirectoryEntry> {
    return from(this.file.createDir(this.file.dataDirectory, AppSettings.offlineMapSettings.fileSystemLocation.tempDirectoryName, true));
  }

  private async unzipTiles(): Promise<boolean> {
    const result = await this.ionicZip.unzip(`${this.file.dataDirectory}` +
      `/${AppSettings.offlineMapSettings.fileSystemLocation.tempDirectoryName}` +
      `/${AppSettings.offlineMapSettings.fileSystemLocation.tempZipFilename}`,
      `${this.file.dataDirectory}/${AppSettings.offlineMapSettings.fileSystemLocation.pbfDirectoryName}`,
      (progress) => this.progressService.emitProgressPercentage(progress, ProgressEmitter.OfflineMapUnzipping));

    this.progressService.resetProgress();

    if (result === 0) {
      logger.debug('Offline map archive successfully extracted.');
      this.toast.success('Offline kaart succesvol gedownload');
      return true;
    } else if (this.operationIsCancelled) {
      logger.error(new Error('Offline map archive extraction was cancalled.'));
      this.toast.error('Offline downloaden van kaart werd geannuleerd');
      await this.removeUnzipedFiles();
      return false;
    } else {
      logger.error(new Error('Offline map archive extraction failed.'));
      this.toast.error('Offline downloaden van kaart mislukt');
      await this.removeUnzipedFiles();
      return false;
    }
  }

  private removeUnzipedFiles() {
    return this.file.removeRecursively(this.file.dataDirectory, AppSettings.offlineMapSettings.fileSystemLocation.pbfDirectoryName)
      .catch(err => logger.error(err, 'Offline map pbf files removal failed.'));
  }

  private removeTempDirAndFile() {
    this.file.removeRecursively(this.file.dataDirectory, AppSettings.offlineMapSettings.fileSystemLocation.tempDirectoryName);
    logger.debug('Temporary offline map files successfully removed.');
  }
}
