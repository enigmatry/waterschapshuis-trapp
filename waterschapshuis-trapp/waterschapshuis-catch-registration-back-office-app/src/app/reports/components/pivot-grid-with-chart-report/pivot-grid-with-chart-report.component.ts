import { Component, OnInit, OnDestroy, ViewChild, AfterViewInit } from '@angular/core';
import { DxPivotGridComponent, DxChartComponent } from 'devextreme-angular';
import { ActivatedRoute } from '@angular/router';
import PivotGridDataSource from 'devextreme/ui/pivot_grid/data_source';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { DevExtremeReportTemplate } from '../../models/common/dev-extreme-report-template.model';
import { ReportBaseComponent } from '../report-base.component';
import { AuthService } from 'src/app/core/auth/auth.service';
import { SideBarFilterService } from '../../services/side-bar-filter.service';
import { Subscription } from 'rxjs';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { ChartTypeHelper } from '../../models/chart-type-helper';


@Component({
  selector: 'app-pivot-grid-with-chart-report',
  templateUrl: './pivot-grid-with-chart-report.component.html',
  styleUrls: ['./pivot-grid-with-chart-report.component.scss']
})
export class PivotGridWithChartReportComponent extends ReportBaseComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild(DxPivotGridComponent) pivotGrid: DxPivotGridComponent;
  @ViewChild(DxChartComponent) chart: DxChartComponent;

  reportTemplate: DevExtremeReportTemplate;
  dataSource: PivotGridDataSource;
  chartIsReady = false;

  private chartTypeSubscription: Subscription;

  get updatedTemplateContent(): any {
    return this.dataSource ? this.dataSource.fields() : [];
  }

  constructor(
    sideBarService: SideBarService,
    private route: ActivatedRoute,
    private sideBarFilter: SideBarFilterService,
    private authService: AuthService,
    private lookupsService: LookupsService) {
    super(sideBarService);
    this.reportTemplate = this.route.snapshot.data.template as DevExtremeReportTemplate;
    this.reportTemplate
      .createDataSource(this.authService, this.lookupsService)
      .subscribe(response => this.dataSource = response);
  }

  ngOnInit() {
    super.ngOnInit();
    const chartType = ChartTypeHelper.getChartTypeByChartNumber(this.reportTemplate.chartType);
    this.sideBarFilter.changeChartType(chartType);
  }

  ngAfterViewInit(): void {
    this.pivotGrid.instance
    .bindChart(this.chart.instance, {
      dataFieldsDisplayMode: 'splitPanes',
      alternateDataFields: false
    });

    this.chartTypeSubscription = this.sideBarFilter.chartType
      .subscribe(t => {
        this.chart.commonSeriesSettings = { type: t.value };
        this.reportTemplate.chartType = ChartTypeHelper.getChartTypeNumberByChartName(t.value);
    });
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
    this.chartTypeSubscription?.unsubscribe();
  }

  onContentReady(e): void {
    this.chartIsReady = true;
  }

}
