import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { parseUrl, stringifyUrl } from 'query-string';

import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { OverlayLayer } from 'src/app/shared/models/overlay-layer.model';
import { MapReportBaseComponent } from '../map-report-base.component';
import { SpinnerService } from 'src/app/shared/services/spinner.service';
import { GeoMapReportService } from 'src/app/reports/services/geo-map-report.service';
import { TrackingLinesReportTemplate } from 'src/app/reports/models/common/tracking-lines-report-template.model';
import { SideBarFilterService } from 'src/app/reports/services/side-bar-filter.service';
import { TrackingLineFilter } from 'src/app/reports/models/tracking-line-filter.model';
import { TrackingLinesService } from 'src/app/reports/services/tracking-lines.service';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { dateAsYearMonthDay } from 'src/app/shared/models/utils';

@Component({
    selector: 'app-tracking-lines-report',
    templateUrl: '../map-report-base.component.html'
})
export class TrackingLinesReportComponent extends MapReportBaseComponent implements OnInit, OnDestroy {


    reportTemplate: TrackingLinesReportTemplate;

    constructor(
        private route: ActivatedRoute,
        private trackingLinesService: TrackingLinesService,
        private filterService: SideBarFilterService,
        sideBarService: SideBarService,
        geoMapReportService: GeoMapReportService,
        spinnerService: SpinnerService) {
        super(sideBarService, geoMapReportService, spinnerService, filterService);
        this.reportTemplate = this.route.snapshot.data.template as TrackingLinesReportTemplate;
    }

    ngOnInit() {
        super.ngOnInit();


        this.filterService.trackingLineFilter
            .pipe(untilComponentDestroyed(this))
            .subscribe(filter => this.filterTrackingLines(filter));
    }

    ngOnDestroy(): void {
        super.ngOnDestroy();
    }

    private async filterTrackingLines(filter: TrackingLineFilter) {
        const layer = await this.trackingLinesService.getOverlayLayer();
        this.appendTrackingLineFilter(layer, filter);
        super.applyLayer(layer);
    }


    private appendTrackingLineFilter(layer: OverlayLayer, filter: TrackingLineFilter) {
        const filed = 'TrackingDate';
        const url = parseUrl(layer.url);
        const cql = `${filed} BETWEEN ${dateAsYearMonthDay(filter.start)} AND ${dateAsYearMonthDay(filter.end)}`;
        url.query.cql_filter = cql;
        layer.url = stringifyUrl(url, { encode: true });
    }
}
