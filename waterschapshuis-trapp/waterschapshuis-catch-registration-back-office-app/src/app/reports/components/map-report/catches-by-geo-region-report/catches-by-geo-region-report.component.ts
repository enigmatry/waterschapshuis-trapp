import { Component, OnInit, OnDestroy } from '@angular/core';
import { take } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { MapReportBaseComponent } from '../map-report-base.component';
import { SpinnerService } from 'src/app/shared/services/spinner.service';
import { GeoMapReportService } from 'src/app/reports/services/geo-map-report.service';
import { CatchesByGeoRegionReportTemplate, CatchesByGeoRegionReportFilter } from 'src/app/reports/models/common/catches-by-geo-region-report-template.model';
import { CatchesByGeoRegionService } from 'src/app/reports/services/catches-by-geo-region.service';
import { generateUrlWithFilter } from 'src/app/shared/models/map-filter.model';
import { SideBarFilterService } from 'src/app/reports/services/side-bar-filter.service';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-catches-by-geo-region-report',
  templateUrl: '../map-report-base.component.html'
})
export class CatchesByGeoRegionReportComponent extends MapReportBaseComponent implements OnInit, OnDestroy {

  reportTemplate: CatchesByGeoRegionReportTemplate;

  constructor(
    private route: ActivatedRoute,
    private catchesByGeoRegionService: CatchesByGeoRegionService,
    geoMapReportService: GeoMapReportService,
    spinnerService: SpinnerService,
    sideBarFilterService: SideBarFilterService,
    sideBarService: SideBarService) {
    super(sideBarService, geoMapReportService, spinnerService, sideBarFilterService);
    this.reportTemplate = this.route.snapshot.data.template as CatchesByGeoRegionReportTemplate;
  }

  ngOnInit() {
    super.ngOnInit();
    this.catchesByGeoRegionService.loadOverlayLayers();

    this.catchesByGeoRegionService.catchesByGeoRegionReportFilter
      .pipe(untilComponentDestroyed(this))
      .subscribe(filterData => this.handleFilterChange(filterData));
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  private async handleFilterChange(filter: CatchesByGeoRegionReportFilter): Promise<void> {
    const layers = await this.catchesByGeoRegionService.overlayLayers.pipe(take(1)).toPromise();
    const selectedLayer = layers.find(x => x.fullName === filter.layer);
    selectedLayer.selected = true;
    selectedLayer.url = generateUrlWithFilter(selectedLayer.url, filter);
    super.applyLayers(layers);
  }
}
