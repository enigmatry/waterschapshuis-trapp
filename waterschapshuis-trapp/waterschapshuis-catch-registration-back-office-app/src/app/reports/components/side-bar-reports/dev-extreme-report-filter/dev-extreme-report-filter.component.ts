import { Component, OnDestroy, OnInit } from '@angular/core';
import { SideBarFilterService } from 'src/app/reports/services/side-bar-filter.service';
import { ChartType } from 'src/app/reports/models/chart-type';
import { Router } from '@angular/router';
import { ReportsRouteUri, standardReportUris } from 'src/app/reports/models/reports-route-uri.enum';
import { Subscription } from 'rxjs';
import { ChartTypeHelper } from 'src/app/reports/models/chart-type-helper';

@Component({
  selector: 'app-dev-extreme-report-filter',
  templateUrl: './dev-extreme-report-filter.component.html',
  styleUrls: ['./dev-extreme-report-filter.component.scss']
})
export class DevExtremeReportFilterComponent implements OnInit, OnDestroy {

  routerLinks = [ ...standardReportUris, ReportsRouteUri.OwnReport];
  selectedChartType: ChartType;
  availableChartTypes: ChartType[] = ChartTypeHelper.availableChartTypes;
  private chartTypeSubscription: Subscription;
  constructor(private sideBarFilter: SideBarFilterService, private router: Router) { }

  ngOnInit() {
    this.sideBarFilter.chartType.subscribe(t => {
      this.selectedChartType = t;
    });
  }

  onChartTypeChanged() {
    this.sideBarFilter.changeChartType(this.selectedChartType);
  }

  isVisible() {
    return this.routerLinks.some(substring => this.router.url.includes(substring));
  }

  ngOnDestroy(): void {
    this.chartTypeSubscription?.unsubscribe();
  }
}
