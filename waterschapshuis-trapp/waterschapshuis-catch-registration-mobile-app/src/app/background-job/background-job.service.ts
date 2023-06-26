import { HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, from, Observable, Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { AppSettings } from '../app-configuration/app-settings';
import { Logger } from '../core/logger/logger';
import { BackgroundJobRunnerService } from './background-job-runner.service';
import { QueuedRequestStoreClient } from './clients/queued-request-store-client';

const logger = new Logger('BackgroundJobService');

@Injectable({
  providedIn: 'root'
})
export class BackgroundJobService {

  private scheduler: Subscription;
  private hasUnsyncedItemsSubject = new BehaviorSubject<boolean>(false);
  hasUnsyncedItems$ = this.hasUnsyncedItemsSubject.asObservable();

  constructor(
    private backgroundJobRunner: BackgroundJobRunnerService, private requestStore: QueuedRequestStoreClient) {

    this.scheduleJobRunner();
  }

  scheduleJobRunner() {
    const initialDelay = AppSettings.backgroundJobSettings.initialDelay;
    const interval = AppSettings.backgroundJobSettings.interval;

    if (this.scheduler) {
      this.scheduler.unsubscribe();
    }

    this.scheduler = timer(initialDelay, interval)
      .pipe(switchMap(() => this.backgroundJobRunner.processOfflineItems()))
      .subscribe();

    logger.debug(`Background job scheduled to start in ${initialDelay}ms and runs every ${interval}ms.`);

    this.backgroundJobRunner.hasUnsyncedItems()
      .then(value => this.hasUnsyncedItemsSubject.next(value));

    this.backgroundJobRunner.isRunning$
      .subscribe(running => {
        if (!running) {
          this.hasUnsyncedItemsSubject.next(false);
        }
      });
  }

  queue(req: HttpRequest<any>): Observable<void> {
    const queueItem = {
      method: req.method,
      url: req.urlWithParams,
      payload: req.body,
      timestamp: new Date()
    };
    return from(this.requestStore.add(queueItem));
  }

  deQueue(): Observable<void> {
    return from(this.backgroundJobRunner.processOfflineItems());
  }

  syncItemAdded(): void {
    this.hasUnsyncedItemsSubject.next(true);
  }
}
