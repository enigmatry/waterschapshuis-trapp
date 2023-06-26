import { Injectable } from '@angular/core';
import {
  BackgroundGeolocation,
  BackgroundGeolocationConfig,
  BackgroundGeolocationEvents,
  BackgroundGeolocationResponse,
} from '@ionic-native/background-geolocation/ngx';
import { AlertController } from '@ionic/angular';
import { Guid } from 'guid-typescript';
import { Coordinate } from 'ol/coordinate';
import { fromLonLat } from 'ol/proj';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { ListResponseOfGetTrappingTypesResponseItem } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { AppSettings } from 'src/app/app-configuration/app-settings';
import { BackgroundJobService } from 'src/app/background-job/background-job.service';
import { LookupsCachedClient } from 'src/app/cache/clients/lookups-cached.client';
import { Logger } from 'src/app/core/logger/logger';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { ToastService } from 'src/app/services/toast.service';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';

import { ProjectionModel } from '../models/projection.model';
import { TrackingLocation } from '../models/tracking-location.model';
import { TrappingType } from '../models/trapping-type.model';


const logger = new Logger('TrackingService');

@Injectable({
  providedIn: 'root'
})
export class TrackingService {

  trappingTypeId: string;
  private trackingEnabledSubject = new BehaviorSubject<boolean>(false);
  trackingEnabled$ = this.trackingEnabledSubject.asObservable();
  trackingSyncCanceled: boolean;
  syncProgressAlert: HTMLIonAlertElement;
  trackingSessionId: string;
  dutchProjection = ProjectionModel.initDutchProjection();
  private locationInitialized = false;
  private isTimewriting = false;
  private isTrackingMap = false;
  private isTrackingPrivate = false;

  constructor(
    private lookupsClient: LookupsCachedClient,
    private backgroundGeolocation: BackgroundGeolocation,
    private sqliteProvider: SqliteProviderService,
    private toastService: ToastService,
    public alertController: AlertController,
    private backgroundJobService: BackgroundJobService
  ) { }

  public get trackingEnabled(): boolean {
    return this.trackingEnabledSubject.value;
  }

  public set trackingEnabled(value: boolean) {
    this.trackingEnabledSubject.next(value);
  }

  getTrackingStatus(): void {
    this.backgroundGeolocation.checkStatus().then(status => {
      this.trackingEnabled = (status ? status.isRunning : false);
    });
  }

  getAllTrappingTypes(): Observable<Array<TrappingType>> {
    return this.lookupsClient.trappingTypes()
      .pipe(
        map((response: ListResponseOfGetTrappingTypesResponseItem) =>
          response.items.map(item => TrappingType.fromResponse(item)))
      );
  }

  startBackgroundGeolocation(trappingTypeId: string, isTimewriting: boolean, isTrackingMap: boolean, isTrackingPrivate: boolean): void {
    this.trappingTypeId = trappingTypeId;
    this.trackingSessionId = Guid.create().toString();
    this.isTimewriting = isTimewriting;
    this.isTrackingMap = isTrackingMap;
    this.isTrackingPrivate = isTrackingPrivate;

    this.configureTracking();
    this.backgroundGeolocation.start();
    this.trackingEnabled = true;
    this.locationInitialized = false;
  }

  stopBackgroundGeolocation(): void {

    if (!this.trackingEnabled) {
      return;
    }

    this.backgroundGeolocation.getCurrentLocation()
      .then(lastLocation =>
        this.saveLocation({ ...lastLocation, timestamp: lastLocation.time })
      );

    this.backgroundGeolocation.removeAllListeners(BackgroundGeolocationEvents.location);
    this.backgroundGeolocation.stop();

    this.trackingEnabled = false;
  }

  private configureTracking(): void {
    const config: BackgroundGeolocationConfig = {
      desiredAccuracy: AppSettings.trackingSettings.desiredAccuracy,
      stationaryRadius: AppSettings.trackingSettings.stationaryRadius,
      distanceFilter: AppSettings.trackingSettings.distanceFilter,
      // debug: true, //  enable this hear sounds for background-geolocation life-cycle.
      interval: 1000,
      fastestInterval: 500,
      activitiesInterval: 1000,
      activityType: 'fitness'
    };

    this.backgroundGeolocation.configure(config).then(() => {
      this.backgroundGeolocation
        .on(BackgroundGeolocationEvents.location)
        .subscribe((location: BackgroundGeolocationResponse) => {
          logger.debug(`BackgroundGeolocationEvents.location event: ${location}`);

          if (this.locationInitialized) {
            this.saveLocation({ ...location, timestamp: location.time });
          } else {
            this.locationInitialized = true;
          }

          // IMPORTANT:  You must execute the finish method here to inform the native plugin that you're finished,
          // and the background-task may be completed.  You must do this regardless if your operations are successful or not.
          // IF YOU DON'T, ios will CRASH YOUR APP for spending too much time in the background.
          this.backgroundGeolocation.finish();
        },
          error => {
            this.trackingEnabled = false;
            this.toastService.error(error);
          });
    },
      error => {
        this.trackingEnabled = false;
        this.toastService.error(error);
      });
  }

  private saveLocation(location: TrackingLocation): void {
    if (!this.trackingSessionId) {
      logger.warn('Unable to save location. Missing SessionId.');
      return;
    }

    if (!this.trappingTypeId) {
      logger.warn('Unable to save location. Missing TrappingTypeId');
      return;
    }

    const projectedCoordinate: Coordinate = fromLonLat([location.longitude, location.latitude], this.dutchProjection);
    this.sqliteProvider.insertData(
      SqliteTableConfig.tracking,
      [
        Guid.create().toString(),
        location.timestamp,
        projectedCoordinate[0],
        projectedCoordinate[1],
        this.trappingTypeId,
        this.trackingSessionId,
        this.isTimewriting,
        this.isTrackingMap,
        this.isTrackingPrivate
      ]
    ).then(() => this.backgroundJobService.syncItemAdded());
  }
}
