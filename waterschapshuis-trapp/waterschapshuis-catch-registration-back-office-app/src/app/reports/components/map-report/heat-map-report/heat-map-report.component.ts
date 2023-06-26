import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { MapReportBaseComponent } from '../map-report-base.component';
import { SpinnerService } from 'src/app/shared/services/spinner.service';
import { SideBarFilterService } from 'src/app/reports/services/side-bar-filter.service';
import { generateUrlWithFilter } from 'src/app/shared/models/map-filter.model';
import { HeatMapService } from 'src/app/reports/services/heat-map.service';
import { HeatMapReportTemplate, HeatMapReportFilter } from 'src/app/reports/models/common/heat-map-report-template.model';
import { GeoMapReportService } from 'src/app/reports/services/geo-map-report.service';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-heat-map-report',
  templateUrl: '../map-report-base.component.html'
})
export class HeatMapReportComponent extends MapReportBaseComponent implements OnInit, OnDestroy {

  reportTemplate: HeatMapReportTemplate;

  constructor(
    private route: ActivatedRoute,
    private heatMapService: HeatMapService,
    sideBarService: SideBarService,
    geoMapReportService: GeoMapReportService,
    spinnerService: SpinnerService,
    sideBarFilterService: SideBarFilterService) {
    super(sideBarService, geoMapReportService, spinnerService, sideBarFilterService);
    this.reportTemplate = this.route.snapshot.data.template as HeatMapReportTemplate;
  }

  ngOnInit() {
    super.ngOnInit();

    this.heatMapService.heatMapReportFilter
      .pipe(untilComponentDestroyed(this))
      .subscribe(filterData => this.handleFilterChange(filterData));
  }

  private async handleFilterChange(filter: HeatMapReportFilter): Promise<void> {
    const layer = await this.heatMapService.getOverlayLayer();
    layer.url = generateUrlWithFilter(layer.url, filter);
    super.applyLayer(layer);
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

}
