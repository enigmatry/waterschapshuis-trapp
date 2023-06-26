import { Observable, of, from, Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { QueuedRequest } from './models/queued-request.model';
import { catchError, concatMap, finalize, switchMap, flatMap } from 'rxjs/operators';
import { NetworkService } from '../network/network.service';
import { QueuedRequestStoreClient } from './clients/queued-request-store-client';
import { BackgroundJobHttpClient } from './clients/background-job-http-client';
import { RequestRetryPolicy } from './models/request-retry-policy.enum';
import { AppSettings } from '../app-configuration/app-settings';
import { ISyncable } from '../sync/syncable.interface';
import { SyncService } from '../sync/sync.service';
import { ObservationSyncService } from '../maps/services/observation-sync.service';
import { ToastService } from '../services/toast.service';
import { SyncConfig } from '../sync/sync-config';
import { Logger } from '../core/logger/logger';
import { TrackingSyncService } from '../maps/services/tracking-sync.service';
import { environment } from 'src/environments/environment';
import { MapStateService } from '../maps/services/map-state.service';

const logger = new Logger('BackgroundJobRunnerService');

@Injectable({
  providedIn: 'root'
})
export class BackgroundJobRunnerService {

  constructor(
    private httpClient: BackgroundJobHttpClient,
    private requestStore: QueuedRequestStoreClient,
    private networkService: NetworkService,
    private mapService: MapStateService,
    private trackingSyncService: TrackingSyncService,
    private observationSyncService: ObservationSyncService,
    private syncService: SyncService,
    private toast: ToastService
  ) { }

  private isRunning: boolean;
  private completedRequests: QueuedRequest[] = [];
  private failedRequests: QueuedRequest[] = [];
  private monitorCompletedRequestsSubject = new Subject<QueuedRequest[]>();
  private monitorFailedRequestsSubject = new Subject<QueuedRequest[]>();
  private isRunningSubject = new Subject<boolean>();

  private queuedObservationsCount = 0;
  private queuedTrackingCount = 0;

  monitorCompletedRequests$ = this.monitorCompletedRequestsSubject.asObservable();
  monitorFailedRequests$ = this.monitorCompletedRequestsSubject.asObservable();
  isRunning$ = this.isRunningSubject.asObservable();

  async processOfflineItems(): Promise<void> {
    if (this.networkService.isOffline()) {
      logger.debug('Background job not started: No conection.');
      return;
    }
    if (this.isRunning) {
      logger.debug('Background job not started: Already running.');
      return;
    }

    const queuedRequests = await this.requestStore.getAll();
    this.queuedObservationsCount = await this.observationSyncService.countDataToSync();
    this.queuedTrackingCount = await this.trackingSyncService.countDataToSync();

    const totalRequests = queuedRequests.length + this.queuedObservationsCount + this.queuedTrackingCount;

    if (totalRequests === 0) {
      logger.debug('Background job not started: No queued requests.');
      return;
    }

    this.onStarted();

    logger.debug(`Background job started: ${totalRequests} queued requests.`);

    if (this.queuedObservationsCount + this.queuedTrackingCount > 0) {
      await this.syncRecordedItems()
        .toPromise();
    }

    if (queuedRequests.length > 0) {

      await from(queuedRequests.map(request => this.process(request)))
        .pipe(concatMap(processRequest => processRequest))
        .toPromise();
    }

    this.onFinished();
  }

  async hasUnsyncedItems(): Promise<boolean> {
    const queuedRequests = await this.requestStore.getAll();
    const queuedObservationsCount = await this.observationSyncService.countDataToSync();
    const queuedTrackingCount = await this.trackingSyncService.countDataToSync();

    const totalRequests = queuedRequests.length + queuedObservationsCount + queuedTrackingCount;

    return totalRequests > 0;
  }

  private syncRecordedItems(): Observable<void> {
    const syncItems: Array<ISyncable> = [
      this.observationSyncService,
      this.trackingSyncService
    ];
    const config: SyncConfig = { silentSync: true, disableSyncCancel: true };
    logger.debug('Background job running SyncService.');
    return from(this.syncService.sync(syncItems, config));
  }

  private onStarted() {
    this.isRunning = true;
    this.isRunningSubject.next(true);

    this.completedRequests = [];
    this.failedRequests = [];

    if (!environment.production) {
      this.toast.success('Start data synchronisatie.');
    }
  }

  private onFinished() {
    this.isRunning = false;
    this.isRunningSubject.next(false);

    this.monitorCompletedRequestsSubject.next([...this.completedRequests]);
    this.monitorFailedRequestsSubject.next([...this.failedRequests]);

    if (!environment.production) {
      this.toast.success('Alle data gesynchroniseerd.');
    }

    this.mapService.refreshMapContentLayers();

    const total = this.completedRequests.length + this.queuedObservationsCount + this.queuedTrackingCount;
    logger.debug(`Background job finished. Successful: ${total}, Failed: ${this.failedRequests.length}`);
  }

  private process(queuedRequest: QueuedRequest): Observable<any> {
    if (this.networkService.isOffline()) {
      return of();
    }
    return this.httpClient.sendRequest(queuedRequest)
      .pipe(
        switchMap(response => this.onSuccessResponse(queuedRequest)),
        catchError(error => this.onErrorResponse(queuedRequest, error))
      )
      .pipe(catchError(_ => of()));
  }

  private onSuccessResponse(item: QueuedRequest): Observable<void> {
    logger.debug(`Request completed successfully: ${item.id}`);
    this.completedRequests.push(item);
    return from(this.requestStore.remove(item.id));
  }

  private onErrorResponse(item: QueuedRequest, error: any): Observable<void> {
    logger.debug(`Request failed, id: ${item.id}, error: ${error}`);
    this.failedRequests.push(item);

    const retryPolicy = this.getRetryPolicy();
    if (retryPolicy === RequestRetryPolicy.NoRetry) {
      return from(this.requestStore.remove(item.id));
    }

    if (retryPolicy === RequestRetryPolicy.RetryWithFixedLimit) {
      if (item.retryAttempts >= this.getMaxRetryAttempts()) {
        return from(this.requestStore.remove(item.id));
      } else {
        return from(this.requestStore.updateRetryAttempts(item.id, ++item.retryAttempts));
      }
    }
    return of();
  }

  private getRetryPolicy(): RequestRetryPolicy {
    return AppSettings.backgroundJobSettings.retryPolicy;
  }

  private getMaxRetryAttempts(): number {
    return AppSettings.backgroundJobSettings.maxRetryAttempts;
  }
}
