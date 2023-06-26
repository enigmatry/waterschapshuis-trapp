import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PivotGridWithChartReportComponent } from './components/pivot-grid-with-chart-report/pivot-grid-with-chart-report.component';
import { ReportsRouteResolverService } from './services/reports-route-resolver.service';
import { ReportsRouteUri } from './models/reports-route-uri.enum';
import { PredictionReportComponent } from './components/prediction-report/prediction-report.component';
import { AuthGuard } from '../core/auth/auth.guard';
import { TrackingLinesReportComponent } from './components/map-report/tracking-lines-report/tracking-lines-report.component';
import { CatchesByGeoRegionReportComponent } from './components/map-report/catches-by-geo-region-report/catches-by-geo-region-report.component';
import { HeatMapReportComponent } from './components/map-report/heat-map-report/heat-map-report.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: `/${ReportsRouteUri.Prediction}`
  },
  {
    path: ReportsRouteUri.BycatchesReport,
    component: PivotGridWithChartReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.SubAreaTrackerReport,
    component: PivotGridWithChartReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.HourSquareReport,
    component: PivotGridWithChartReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.CatchesOrganisationReport,
    component: PivotGridWithChartReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.HourOrganisationReport,
    component: PivotGridWithChartReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.OrganisationHistogramReport,
    component: PivotGridWithChartReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.OwnReport,
    component: PivotGridWithChartReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.TrackingLines,
    component: TrackingLinesReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.CatchesByGeoRegion,
    component: CatchesByGeoRegionReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ReportsRouteUri.Prediction,
    component: PredictionReportComponent,
    canActivate: [ AuthGuard ]
  },
  {
    path: ReportsRouteUri.HeatMap,
    component: HeatMapReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: ':report/:id',
    component: PivotGridWithChartReportComponent,
    canActivate: [ AuthGuard ],
    resolve: { template: ReportsRouteResolverService }
  },
  {
    path: '**',
    pathMatch: 'full',
    redirectTo: `/${ReportsRouteUri.Prediction}`
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ReportsRoutingModule { }
