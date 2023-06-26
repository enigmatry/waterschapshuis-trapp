import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
    FileUpload,
    GetVersionRegionalLayoutsVersionRegionalLayoutResponse
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { TopologyMaintenanceService } from 'src/app/settings/services/topology-maintenance.service';
import { LookupsService } from 'src/app/shared/services/lookups.service';

export class ImportTopologyDialogResult {
    name: string;
    file: any;

    asFileUpload = (): FileUpload => {
        const fileToUpload = new FileUpload();
        fileToUpload.name = this.file.name;
        fileToUpload.size = this.file.size;
        fileToUpload.type = this.file.type;
        fileToUpload.lastModifiedTime = this.file.lastModified;
        fileToUpload.lastModifiedDate = this.file.lastModifiedDate;
        return fileToUpload;
    }
}

@Component({
    selector: 'app-import-topology-dialog',
    templateUrl: './import-topology-dialog.component.html',
    styleUrls: ['./import-topology-dialog.component.scss']
})
export class ImportTopologyDialogComponent implements OnInit {

    @ViewChild('fileInput', { static: false }) fileInput: ElementRef;

    form: FormGroup;

    get nameControl(): AbstractControl {
        return this.form.get('name');
    }

    private allVersionRegionalLayouts: GetVersionRegionalLayoutsVersionRegionalLayoutResponse[];

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: ImportTopologyDialogResult,
        public dialogRef: MatDialogRef<ImportTopologyDialogComponent>,
        public topologyService: TopologyMaintenanceService,
        private lookupsService: LookupsService,
        private fb: FormBuilder) { }

    ngOnInit() {
        this.lookupsService.getVersionRegionalLayouts()
            .subscribe(response => {
                this.allVersionRegionalLayouts = response;
                this.data = new ImportTopologyDialogResult();
                this.data.file = null;
                this.data.name = '';
                this.form = this.fb.group({
                    name: new FormControl(
                        this.data.name,
                        [
                            Validators.required,
                            Validators.maxLength(30),
                            this.nameUniquenessValidator
                        ]
                    )
                });
            });
    }

    canImport = (): boolean => this.form.valid && this.data?.file !== null;

    onClickUpload = () => {
        const fileUpload = this.fileInput.nativeElement;
        fileUpload.value = null;
        fileUpload.onchange = (event: any) => {
            this.topologyService.clearValidationMessages();
            if (event.target && event.target.files && event.target.files.length > 0) {
                this.data.file = event.target.files[0];
            }
        };
        fileUpload.click();
    }

    onClickImport = () => {
        this.data.name = this.nameControl.value;
        this.dialogRef.close(this.data);
    }

    private nameUniquenessValidator = (control: AbstractControl): any | null =>
        (
            control &&
            control.value &&
            this.allVersionRegionalLayouts.some(x => x.name.toLowerCase() === (control.value as string).toLowerCase())
        )
            ? { nameNotUnique: true }
            : null
}
