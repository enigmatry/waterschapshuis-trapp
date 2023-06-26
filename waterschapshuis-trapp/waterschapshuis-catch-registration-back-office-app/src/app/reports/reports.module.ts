import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgxPermissionsModule } from 'ngx-permissions';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { DevExtremeModule } from 'devextreme-angular';

import { SharedModule } from 'src/app/shared/shared.module';
import { MapModule } from '../map/map.module';
import { PivotGridWithChartReportComponent } from './components/pivot-grid-with-chart-report/pivot-grid-with-chart-report.component';
import { ReportBaseComponent } from './components/report-base.component';
import { SideBarReportsComponent } from './components/side-bar-reports/side-bar-reports.component';
import { ReportsRoutingModule } from './reports-routing.module';
import { TemplateExportComponent } from './components/template-export/template-export.component';
import { TemplateExportDialogComponent } from './components/template-export/template-export-dialog/template-export-dialog.component';
import { CatchesReportFilterComponent } from './components/side-bar-reports/catches-report-filter/catches-report-filter.component';
import { PredictionReportComponent } from './components/prediction-report/prediction-report.component';
import { PredictionFilterComponent } from './components/side-bar-reports/prediction-filter/prediction-filter.component';
import { DevExtremeReportFilterComponent } from './components/side-bar-reports/dev-extreme-report-filter/dev-extreme-report-filter.component';
import { PredictionTableComponent } from './components/prediction-report/prediction-table/prediction-table.component';
import { PredictionChartComponent } from './components/prediction-report/prediction-chart/prediction-chart.component';
import { TrackingLinesFilterComponent } from './components/side-bar-reports/tracking-lines-filter/tracking-lines-filter.component';
import { HeatMapReportFilterComponent } from './components/side-bar-reports/heat-map-report-filter/heat-map-report-filter.component';
import { TrackingLinesReportComponent } from './components/map-report/tracking-lines-report/tracking-lines-report.component';
import { CatchesByGeoRegionReportComponent } from './components/map-report/catches-by-geo-region-report/catches-by-geo-region-report.component';
import { HeatMapReportComponent } from './components/map-report/heat-map-report/heat-map-report.component';
import { MapReportBaseComponent } from './components/map-report/map-report-base.component';
import { ChartExportComponent } from './components/chart-export/chart-export.component';

@NgModule({
  declarations: [
    ReportBaseComponent,
    MapReportBaseComponent,
    PivotGridWithChartReportComponent,
    HeatMapReportComponent,
    TrackingLinesReportComponent,
    CatchesByGeoRegionReportComponent,
    SideBarReportsComponent,
    TemplateExportDialogComponent,
    TemplateExportComponent,
    CatchesReportFilterComponent,
    PredictionReportComponent,
    PredictionFilterComponent,
    DevExtremeReportFilterComponent,
    PredictionTableComponent,
    PredictionChartComponent,
    TrackingLinesFilterComponent,
    HeatMapReportFilterComponent,
    ChartExportComponent
  ],
  imports: [
    ReportsRoutingModule,
    DevExtremeModule,
    SharedModule,
    CommonModule,
    MapModule,
    NgxPermissionsModule.forChild(),
    ReactiveFormsModule,
    FormsModule,
    ClipboardModule
  ],
  entryComponents: [
    SideBarReportsComponent,
  ]
})
export class ReportsModule { }
