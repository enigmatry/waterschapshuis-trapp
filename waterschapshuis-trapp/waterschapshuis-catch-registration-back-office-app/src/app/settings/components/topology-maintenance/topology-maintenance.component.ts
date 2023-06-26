import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { saveAs } from 'file-saver';

import {
    FileResponse,
    ImportVersionRegionalLayout,
    VersionRegionalLayoutImportState
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { dateAsDayMontYearString } from 'src/app/shared/models/utils';
import { ToastService } from 'src/app/shared/toast/toast.service';
import { TopologyMaintenanceService } from '../../services/topology-maintenance.service';
import { SettingsBaseComponent } from '../settings-base.component';
import { ImportTopologyDialogComponent, ImportTopologyDialogResult } from './import-topology-dialog/import-topology-dialog.component';


@Component({
    selector: 'app-topology-maintenance',
    templateUrl: './topology-maintenance.component.html',
    styleUrls: ['./topology-maintenance.component.scss']
})
export class TopologyMaintenanceComponent extends SettingsBaseComponent implements OnInit, OnDestroy {

    policyName = PolicyName;

    constructor(
        sideBarService: SideBarService,
        private toastService: ToastService,
        public topologyService: TopologyMaintenanceService,
        private dialog: MatDialog) {
        super(sideBarService);
    }

    ngOnInit() {
        super.ngOnInit();
        this.topologyService.loadCalculatingKmWaterwaysInfo();
    }

    ngOnDestroy(): void {
        super.ngOnDestroy();
    }

    onClickExport = () => {
        this.topologyService
            .exportTopologies()
            .subscribe(
                (response: FileResponse) => {
                    this.toastService.showSuccessMessage(TopologyMaintenanceService.ToastMessages.exportSucceed);
                    saveAs(response.data, response.fileName ?? `Deelgebieden_${dateAsDayMontYearString()}.csv`);
                },
                error => {
                    this.toastService.showErrorMessage(TopologyMaintenanceService.ToastMessages.exportFailed);
                    throw new Error(error);
                }
            );
    }

    onClickImport = () => {
        const dialogRef = this.dialog.open(ImportTopologyDialogComponent);
        dialogRef.afterClosed()
            .subscribe((response: ImportTopologyDialogResult) => {
                if (response && response.name && response.file) {
                    this.readAndUploadFile(response);
                }
            });
    }

    onClickCalculateKmWaterWays = () =>
        this.topologyService
            .recalculateWaterAreas()

    onClickReloadImportStatus = () =>
        this.topologyService.loadVersionRegionalLayoutImport()


    // https://www.codemag.com/article/1901061/Upload-Small-Files-to-a-Web-API-Using-Angular
    private readAndUploadFile = (result: ImportTopologyDialogResult) => {
        const request = new ImportVersionRegionalLayout();
        request.name = result.name;
        request.file = result.asFileUpload();
        const fileReader = new FileReader();
        fileReader.onload = () => {
            request.file.dataAsBase64 = fileReader.result.toString();
            this.topologyService.importTopologies(request);
            setTimeout(this.topologyService.loadVersionRegionalLayoutImport, 900000); // 15min
        };
        fileReader.readAsDataURL(result.file);
    }
}
