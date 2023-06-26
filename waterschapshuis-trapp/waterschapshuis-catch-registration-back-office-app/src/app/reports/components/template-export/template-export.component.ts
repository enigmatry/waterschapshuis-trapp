import { Component, OnInit, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { NgxPermissionsService } from 'ngx-permissions';

import { ReportTemplateBase } from '../../models/common/report-template-base.model';
import { TemplateExportDialogComponent } from './template-export-dialog/template-export-dialog.component';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';

@Component({
    selector: 'app-template-export',
    templateUrl: './template-export.component.html',
    styleUrls: ['./template-export.component.scss']
})
export class TemplateExportComponent implements OnInit {

    @Input() template: ReportTemplateBase;
    @Input() updatedTemplateContent: any;

    constructor(
        private dialog: MatDialog,
        private permissionService: NgxPermissionsService) { }

    ngOnInit() { }

    openExportTemplateDialog = () => {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.width = '600px';
        dialogConfig.data = {
            originReportTemplate: this.template,
            updatedReportTemplateContent: this.updatedTemplateContent
        };

        this.dialog.open(TemplateExportDialogComponent, dialogConfig);
    }

    canExportReport = (): boolean =>
        this.permissionService.getPermission(PolicyName.ReportReadWrite) !== undefined
}
