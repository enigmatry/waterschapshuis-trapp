import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ReportTemplateBase } from '../models/common/report-template-base.model';
import { ReportService } from './report.service';

@Injectable({
  providedIn: 'root'
})
export class ReportsRouteResolverService implements Resolve<ReportTemplateBase> {

  constructor(private reportService: ReportService) { }

  resolve = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<ReportTemplateBase> | Promise<ReportTemplateBase> | ReportTemplateBase =>
      route.params.id
        ? this.reportService.getReportTemplateById(route.params.id)
        : this.reportService.getReportTemplateByUri(route.routeConfig.path)
}
