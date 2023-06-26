import { Injectable } from '@angular/core';
import { EmailComposer } from '@ionic-native/email-composer/ngx';
import { File } from '@ionic-native/file/ngx';
import { from, Observable, of } from 'rxjs';
import { concatMap, map, switchMap, tap } from 'rxjs/operators';
import {
  BlobStorageClient,
  IGetObservationDetailsResponseItem,
  GetObservationDetailsResponseItem
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { BlobDownloadsViewStateService } from 'src/app/azure-blob-storage/services/blob-downloads-view-state.service';
import { BlobSharedViewStateService } from 'src/app/azure-blob-storage/services/blob-shared-view-state.service';
import { SasGeneratorService } from 'src/app/azure-blob-storage/services/sas-generator.service';
import { BlobItemDownload } from 'src/app/azure-blob-storage/types/azure-storage';
import { ObservationsCachedClient } from 'src/app/cache/clients/observation-cached.client';
import { GeoJsonUpdateCommand } from 'src/app/geo-json/models/geo-json-update-command.model';
import {
  ObservationGeoJsonUpdateCommand,
} from 'src/app/geo-json/models/observations/observation-geo-json-update-command.model';
import { PlatformService } from 'src/app/services/platform.service';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { ToastService } from 'src/app/services/toast.service';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';
import { BaseOfflineEntityService, OfflineServiceContext } from 'src/app/shared/offline/base-offline-entity.service';
import { CurrentUserProviderService } from 'src/app/shared/services/current-user-provider.service';
import { environment } from 'src/environments/environment';

import { ObservationResponse } from '../models/observation-response.model';
import { Observation } from '../models/observation.model';
import { ProjectionModel } from '../models/projection.model';
import { Logger } from 'src/app/core/logger/logger';

const logger = new Logger('ObservationService');

@Injectable({
  providedIn: 'root'
})
export class ObservationService extends BaseOfflineEntityService<Observation, IGetObservationDetailsResponseItem> {

  get entityCacheKey(): string {
    return 'observations';
  }

  constructor(
    private sqliteProviderService: SqliteProviderService,
    private toastService: ToastService,
    private sasGeneratorService: SasGeneratorService,
    private blobSharedViewStateService: BlobSharedViewStateService,
    private observationsClient: ObservationsCachedClient,
    private blobDownloadsViewStateService: BlobDownloadsViewStateService,
    private blobClient: BlobStorageClient,
    private file: File,
    private emailComposer: EmailComposer,
    protected offlineServiceContext: OfflineServiceContext,
    private platformService: PlatformService,
    private currentUserService: CurrentUserProviderService
  ) {
    super(offlineServiceContext);
  }

  addObservation(command: Observation): Observable<void> {
    return from(this.sqliteProviderService.insertData(SqliteTableConfig.observations, Object.values(command)))
      .pipe(
        tap(() => this.offlineServiceContext.backgroundJobService.syncItemAdded()),
        concatMap(() => {
          if (this.offlineServiceContext.networkService.isOffline()) {
            return this.updateOfflineData(command);
          } else {
            super.deQueueBackgroungJobs();
          }
          return of(null);
        })
      );
  }

  private updateOfflineData(command: Observation): Observable<IGetObservationDetailsResponseItem> {
    return this.updateGeoJsonLayer(command)
      .pipe(
        switchMap(r => this.updateCacheById(command))
      );
  }

  updateCacheValue(command: Observation, target?: IGetObservationDetailsResponseItem): Observable<IGetObservationDetailsResponseItem> {
    return target ?
      this.commandToModel(command, target) :
      this.commandToModel(command, new GetObservationDetailsResponseItem());
  }

  private commandToModel(command: Observation, model: IGetObservationDetailsResponseItem): Observable<IGetObservationDetailsResponseItem> {
    return of({
      id: command.id,
      type: command.type,
      image: command.image,
      photoUrl: model.photoUrl,
      remarks: command.remarks,
      createdOn: model.createdOn,
      createdBy: this.currentUserService.currentUser.name,
      position: model.position,
      archived: model.archived,
      recordedOn: command.recordedOn,
      longitude: command.longitude,
      latitude: command.latitude
    });
  }

  mapToGeoJsonUpdateCommands(command: Observation): Array<GeoJsonUpdateCommand> {
    return [ObservationGeoJsonUpdateCommand.createCommand(command, ProjectionModel.dutchMatrix, ProjectionModel.mercatorMatrix)];
  }

  get(id: string): Observable<ObservationResponse> {
    return this.observationsClient.get(id)
      .pipe(
        map((response: GetObservationDetailsResponseItem) => ObservationResponse.fromResponse(response))
      );
  }

  getObservationsForGivenIds(observationsIds: string[]): Observable<Array<ObservationResponse>> {
    return this.observationsClient.getMultiple(observationsIds)
      .pipe(
        map((response: Array<IGetObservationDetailsResponseItem>) =>
          response.map(o => ObservationResponse.fromResponse(o)))
      );
  }

  downloadImage(imageName: string): Observable<BlobItemDownload> {
    return this.blobClient.getSasKey().pipe(switchMap(response => {
      this.sasGeneratorService.setSasToken(response.sasKey);
      this.blobSharedViewStateService.getContainerItems(environment.azureStorage.baseObservationBlobContainer);
      return this.blobDownloadsViewStateService.downloadFile(imageName);
    }));
  }

  sendEmail(containerName: string, emailData: any, emailObject: any, img: BlobItemDownload): void {
    this.file.createDir(this.file.dataDirectory, containerName, true).then(dir => {
      const imageData = {
        filePath: this.file.dataDirectory + '/' + containerName,
        imageName: emailData.id + '.jpg'
      };

      this.file.writeFile(imageData.filePath, imageData.imageName, img.blob, { replace: true }).then(fl => {
        emailObject.attachments.push(fl.toURL());
        this.openEmailComposer(emailObject, imageData);
      }).catch(err => logger.error(err));

    }).catch(err => logger.error(err));
  }

  async openEmailComposer(email: any, imageData?: any): Promise<void> {

    const isIos = this.platformService.isIos();

    const hasAccount = await this.emailComposer.hasAccount();
    if (!hasAccount && isIos) {
      this.toastService.error('U moet eerst een e-mailaccount instellen voordat u een melding kunt e-mailen.');
      return;
    }

    // for iOS it is required to send 'mailto' parameter to isAvailable method to be able to work
    // for Andorid we should not send anything
    const emailComposerPromise = isIos ?
      this.emailComposer.isAvailable('mailto') :
      this.emailComposer.isAvailable();

    emailComposerPromise.then((available: boolean) => {
      if (available) {
        this.emailComposer.open(email)
          .catch(err => logger.error(err));
      }
    }).catch(err => logger.error(err));
  }
}
