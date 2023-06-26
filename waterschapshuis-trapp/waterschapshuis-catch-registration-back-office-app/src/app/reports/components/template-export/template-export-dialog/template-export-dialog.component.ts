import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReportService } from 'src/app/reports/services/report.service';
import { DevExtremeReportTemplate } from 'src/app/reports/models/common/dev-extreme-report-template.model';
import { Validators, FormBuilder, FormGroup, AbstractControl, FormControl } from '@angular/forms';
import { ToastService } from 'src/app/shared/toast/toast.service';
import { DxFunctions } from 'src/app/reports/models/dev-extreme-functions.model';

enum ExportState {
    Init = 0,
    Exporting = 1,
    Exported = 2
}

@Component({
    selector: 'app-template-export-dialog',
    templateUrl: './template-export-dialog.component.html',
    styleUrls: ['./template-export-dialog.component.scss']
})
export class TemplateExportDialogComponent implements OnInit {

    private originReportTemplate: DevExtremeReportTemplate;
    private updatedReportTemplateContent: any[];

    exportState = ExportState.Init;
    exportForm: FormGroup;

    get templateNameControl(): AbstractControl {
        return this.exportForm.get('templateName');
    }
    get exportUriControl(): AbstractControl {
        return this.exportForm.get('exportUri');
    }

    constructor(
        private reportService: ReportService,
        public dialogRef: MatDialogRef<TemplateExportDialogComponent>,
        private fb: FormBuilder,
        private toaster: ToastService,
        @Inject(MAT_DIALOG_DATA) data) {
        this.originReportTemplate = data.originReportTemplate;
        this.updatedReportTemplateContent = data.updatedReportTemplateContent;
        this.exportForm = this.fb.group({
            templateName: new FormControl(null, [Validators.required, Validators.maxLength(100)]),
            exportUri: new FormControl({ value: null, disabled: true })
        });
    }

    ngOnInit() { }

    exportDisabled = (): boolean => !this.exportForm || this.exportForm.invalid || this.exportState !== ExportState.Init;

    exportReport = () => {
        if (this.exportForm.invalid) { return; }
        const newTemplateContentAsString = DxFunctions.clearCalculateSummaryValues(this.updatedReportTemplateContent);
        this.exportState = ExportState.Exporting;
        this.templateNameControl.disable();
        this.reportService
            .createReportTemplateExport(
                this.originReportTemplate.routeUri,
                this.templateNameControl.value,
                newTemplateContentAsString,
                this.originReportTemplate.chartType
            )
            .then(result => {
                this.exportState = ExportState.Exported;
                this.exportUriControl.setValue(`${window.location.protocol}//${window.location.host}/reports/${this.originReportTemplate.routeUri}/${result.id}`);
            })
            .catch(err => {
                this.exportState = ExportState.Init;
                this.templateNameControl.enable();
            });
    }

    showToasterMessage = () => this.toaster.showInfoMessage('Link naar rapportage is gekopieerd naar het klembord!');
}
