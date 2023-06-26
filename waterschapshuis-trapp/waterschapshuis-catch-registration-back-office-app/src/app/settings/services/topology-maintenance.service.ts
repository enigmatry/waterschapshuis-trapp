import { ScheduledJobState, GetScheduledJobsResponse } from './../../api/waterschapshuis-catch-registration-backoffice-api';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import {
    FileResponse,
    GetVersionRegionalLayoutImportResponse,
    ImportVersionRegionalLayout,
    TopologiesClient,
    VersionRegionalLayoutCreateResult,
    VersionRegionalLayoutImportState
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { ToastService } from 'src/app/shared/toast/toast.service';

@Injectable({
    providedIn: 'root'
})
export class TopologyMaintenanceService {

    public static ToastMessages = {
        exportSucceed: 'Deelgebieden zijn geëxporteerd',
        exportFailed: 'Export deelgebieden is mislukt',
        importSucceed: 'Deelgebieden zijn geimporteerd',
        importFailed: 'Import deelgebieden is mislukt',
        recalculateWaterWaysSucceed: '?',
        recalculateWaterWaysFailed: '?',
    };

    private inProgressSubject = new BehaviorSubject<boolean>(false);
    private validationMessagesSubject = new BehaviorSubject<string>('');

    inProgress = this.inProgressSubject.asObservable();
    validationMessages = this.validationMessagesSubject.asObservable();

    currentImport: GetVersionRegionalLayoutImportResponse;

    constructor(
        private topologyClient: TopologiesClient,
        private toastService: ToastService) { }

    loadVersionRegionalLayoutImport = () => {
        this.inProgressSubject.next(true);
        this.clearValidationMessages();
        this.topologyClient
            .getVersionRegionalLayoutImport()
            .subscribe(
                response => {
                    if (!response.id) {
                        this.clearValidationMessages();
                    } else if (response.state === VersionRegionalLayoutImportState.Failed) {
                        this.addValidationMessages(response.outputMessages.concat([ '', 'IMPORT FAILED!' ]));
                    } else if (response.state === VersionRegionalLayoutImportState.Succeed) {
                        this.addValidationMessages(response.outputMessages.concat([ '', 'IMPORT SUCCEED!' ]));
                    } else if (response.state === VersionRegionalLayoutImportState.Started) {
                        this.addValidationMessages(response.outputMessages);
                    }

                    this.currentImport = response;
                    this.inProgressSubject.next(response.id && response.state === VersionRegionalLayoutImportState.Started);
                },
                fail => this.inProgressSubject.next(false)
            );
    }

    exportTopologies = (): Observable<FileResponse> => {
        this.inProgressSubject.next(true);
        this.clearValidationMessages();
        return this.topologyClient
            .exportTopologies()
            .pipe(finalize(() => this.inProgressSubject.next(false)));
    }

    importTopologies = (request: ImportVersionRegionalLayout) => {
        this.inProgressSubject.next(true);
        this.clearValidationMessages();
        this.topologyClient
            .importTopologies(request)
            .subscribe(
                (response: VersionRegionalLayoutCreateResult) => {
                    this.toastService.showSuccessMessage(response.succeed
                        ? TopologyMaintenanceService.ToastMessages.importSucceed
                        : TopologyMaintenanceService.ToastMessages.importFailed);
                    this.loadVersionRegionalLayoutImport();
                },
                fail => {
                    this.inProgressSubject.next(false);

                    if (fail.errors && fail.errors.subAreaCsvRecords) { // TODO - do it to be more generic
                        this.addValidationMessages(fail.errors.subAreaCsvRecords);
                    }
                    this.toastService.showErrorMessage(TopologyMaintenanceService.ToastMessages.importFailed);
                    throw new Error(fail);
                }
            );
    }

    loadCalculatingKmWaterwaysInfo = () => {
        this.inProgressSubject.next(true);
        this.clearValidationMessages();
        this.topologyClient
            .getScheduledJobInfo()
            .subscribe(
                (response: GetScheduledJobsResponse) => this.manageScheduledJobresponse(response),
                fail => this.inProgressSubject.next(false)
            );
    }

    recalculateWaterAreas() {
        this.inProgressSubject.next(true);
        this.clearValidationMessages();
        return this.topologyClient
            .calculateKmWaterways()
            .subscribe(
                response => this.manageScheduledJobresponse(response),
                fail => this.inProgressSubject.next(false)
            );
    }

    manageScheduledJobresponse = (response: GetScheduledJobsResponse) => {
        if (!response.id) {
            this.clearValidationMessages();
        }
        this.addValidationMessages(response.outputMessages);
        this.inProgressSubject.next(response.id && (response.state === ScheduledJobState.Started ||
            response.state === ScheduledJobState.Scheduled));
    }

    addValidationMessages = (messages: string[]) => {
        if (messages && messages.length) {
            this.validationMessagesSubject.next(messages.join('\n'));
        }
    }

    clearValidationMessages = () => this.validationMessagesSubject.next('');
}
