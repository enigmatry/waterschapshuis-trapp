import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';

import { ReportService } from '../../services/report.service';
import { ISideBar } from 'src/app/core/side-bar/side-bar-abstract/side-bar.interface';
import { DevExtremeReportTemplate } from '../../models/common/dev-extreme-report-template.model';
import { ReportsRouteUri, standardReportUris } from '../../models/reports-route-uri.enum';
import { ReportTemplateBase } from '../../models/common/report-template-base.model';
import { TrackingLinesReportTemplate } from '../../models/common/tracking-lines-report-template.model';
import { HeatMapReportTemplate } from '../../models/common/heat-map-report-template.model';
import { ReportTemplateType } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

@Component({
    selector: 'app-side-bar-reports',
    templateUrl: './side-bar-reports.component.html'
})
export class SideBarReportsComponent implements ISideBar, OnInit, OnDestroy {
    data: any;
    standardReportTemplates: DevExtremeReportTemplate[];
    ownReportTemplate: DevExtremeReportTemplate;
    trackingLinesTemplate: TrackingLinesReportTemplate;
    heatMapTemplate: HeatMapReportTemplate;

    constructor(private router: Router, private reportService: ReportService) {}

    ngOnInit() {
        forkJoin({
            standardReportTemplates: this.reportService.getReportTemplatesByGroup(ReportTemplateType.StandardReport),
            ownReportTemplate: this.reportService.getReportTemplateByUri(ReportsRouteUri.OwnReport),
            trackingLinesTemplate: this.reportService.getReportTemplateByUri(ReportsRouteUri.TrackingLines),
            heatMapTemplate: this.reportService.getReportTemplateByUri(ReportsRouteUri.HeatMap),
        })
        .subscribe(x => {
            this.standardReportTemplates = x.standardReportTemplates as DevExtremeReportTemplate[];
            this.ownReportTemplate = x.ownReportTemplate as DevExtremeReportTemplate;
            this.trackingLinesTemplate = x.trackingLinesTemplate as TrackingLinesReportTemplate;
            this.heatMapTemplate = x.heatMapTemplate as HeatMapReportTemplate;
        });
    }

    ngOnDestroy(): void { }

    open(): void { }

    close(): void { }

    toggle(): void { }

    isAccordionExpanded = (template: ReportTemplateBase): boolean => this.router.url.includes(template.routeUri);

    isStandardReportAccordionExpanded = (): boolean =>
        standardReportUris.some(substring => this.router.url.includes(substring))

}
