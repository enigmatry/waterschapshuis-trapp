import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../../services/report.service';
import { ReportsRouteUri } from '../../../models/reports-route-uri.enum';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CatchesByGeoRegionReportTemplate, CatchesByGeoRegionReportFilter } from '../../../models/common/catches-by-geo-region-report-template.model';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { Router } from '@angular/router';
import { CatchesByGeoRegionService } from 'src/app/reports/services/catches-by-geo-region.service';
import { catchNumberColorScale, catchPerKmColorScale, hoursPerKmColorScale, Fill } from 'src/app/api/map/map-report-color-scales';
import { IListItem } from 'src/app/shared/models/list-item';
import { SideBarFilterService } from 'src/app/reports/services/side-bar-filter.service';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';


@Component({
    selector: 'app-catches-report-filter',
    templateUrl: './catches-report-filter.component.html',
    styleUrls: ['./catches-report-filter.component.scss']
})
export class CatchesReportFilterComponent extends OnDestroyMixin implements OnInit {
    reportTemplate: CatchesByGeoRegionReportTemplate;
    form: FormGroup;
    startYear = 2012;
    buttonDisabled = false;
    catchNumberColorScale = catchNumberColorScale;
    catchPerKmColorScale = catchPerKmColorScale;
    hoursPerKmColorScale = hoursPerKmColorScale;

    measurements = [
        { id: 0, name: 'Vangsten', colorScale: catchNumberColorScale },
        { id: 1, name: 'Bijvangsten', colorScale: catchNumberColorScale },
        { id: 2, name: 'Vangsten/km', colorScale: catchPerKmColorScale },
        { id: 3, name: 'Bijvangsten/km', colorScale: catchPerKmColorScale },
        { id: 4, name: 'Uren/km', colorScale: hoursPerKmColorScale }
    ];

    selectedMeasurement: { id: number, name: string, colorScale: Fill[] };

    years = Array.from(Array((new Date()).getFullYear() - this.startYear + 1), (_, i) => i + this.startYear);
    periods = Array.from(Array(13), (_, i) => i + 1);
    trappingTypes = [];
    overlayLayers: IListItem[];
    versionRegionalLayouts = [];

    constructor(
        private reportService: ReportService,
        private fb: FormBuilder,
        private catchesByGeoRegionService: CatchesByGeoRegionService,
        private lookupsService: LookupsService,
        private sideBarFilterService: SideBarFilterService,
        private router: Router) {
        super();
        this.createForm();
    }

    ngOnInit(): void {
        Promise.all([
            this.loadOverlayLayers(),
            this.loadReportTemplate(),
            this.loadTrappingTypes(),
            this.loadVersionRegionalLayouts()
        ])
            .then(() => this.applyFilter());

        this.sideBarFilterService.mapRendered
            .pipe(untilComponentDestroyed(this))
            .subscribe(x => this.buttonDisabled = !x);
    }

    applyFilter(): void {
        this.buttonDisabled = true;
        const filter = this.toReportFilterTemplate();
        this.catchesByGeoRegionService.updateFilter(filter);
    }

    isExpanded = (): boolean => this.router.url.includes(this.reportTemplate.routeUri);

    private createForm(): void {
        this.form = this.fb.group({
            layer: [null],
            versionRegionalLayout: [null],
            measurement: [null],
            trappingType: [null],
            yearFrom: [null],
            periodFrom: [null],
            yearTo: [null],
            periodTo: [null]
        });

        this.form.get('measurement').valueChanges
            .subscribe(measurementId => this.selectedMeasurement = this.measurements[measurementId]);
    }

    private async loadOverlayLayers(): Promise<void> {
        this.overlayLayers = await this.catchesByGeoRegionService
            .getOverlayLayersList();
    }

    private async loadReportTemplate(): Promise<void> {
        await this.reportService
            .getReportTemplateByUri(ReportsRouteUri.CatchesByGeoRegion)
            .then(report => {
                this.reportTemplate = report as CatchesByGeoRegionReportTemplate;
                this.form.patchValue({ ...this.reportTemplate.filter });
            });
    }

    private async loadTrappingTypes(): Promise<void> {
        await this.lookupsService
            .getTrappingTypes().toPromise()
            .then(response => this.trappingTypes = response);
    }

    private async loadVersionRegionalLayouts(): Promise<void> {
        await this.lookupsService
            .getVersionRegionalLayouts().toPromise()
            .then(response => this.versionRegionalLayouts = response);
    }

    private toReportFilterTemplate(): CatchesByGeoRegionReportFilter {
        return {
            ...this.reportTemplate.filter,
            layer: this.form.value.layer,
            measurement: this.form.value.measurement,
            versionRegionalLayout: this.form.value.versionRegionalLayout,
            trappingType: this.form.value.trappingType,
            yearFrom: this.form.value.yearFrom,
            periodFrom: this.form.value.periodFrom,
            yearTo: this.form.value.yearTo,
            periodTo: this.form.value.periodTo
        };
    }

}
