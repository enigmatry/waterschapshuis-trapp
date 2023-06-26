import { Component, OnInit } from '@angular/core';
import { HeatMapReportTemplate, HeatMapReportFilter } from 'src/app/reports/models/common/heat-map-report-template.model';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ReportService } from 'src/app/reports/services/report.service';
import { Router } from '@angular/router';
import { ReportsRouteUri } from 'src/app/reports/models/reports-route-uri.enum';
import { OrganizationService } from 'src/app/shared/services/organizations.service';
import { HeatMapService } from 'src/app/reports/services/heat-map.service';
import { IListItem, ListItem } from 'src/app/shared/models/list-item';
import { SideBarFilterService } from 'src/app/reports/services/side-bar-filter.service';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { dateAsYearMonthDay } from 'src/app/shared/models/utils';

@Component({
  selector: 'app-heat-map-report-filter',
  templateUrl: './heat-map-report-filter.component.html',
  styleUrls: ['./heat-map-report-filter.component.scss']
})
export class HeatMapReportFilterComponent extends OnDestroyMixin implements OnInit {
  reportTemplate: HeatMapReportTemplate;
  form: FormGroup;
  buttonDisabled = false;
  allOrganizations: IListItem[];
  catchTypes = [
    { id: 1, name: 'Beverrat' },
    { id: 2, name: 'Muskusrat' }];

  end = new Date();
  start = new Date(new Date().getFullYear(), 0, 1);

  constructor(
    private reportService: ReportService,
    private fb: FormBuilder,
    private router: Router,
    private organizationService: OrganizationService,
    private sideBarFilterService: SideBarFilterService,
    private heatMapservice: HeatMapService) {
    super();
    this.createForm();
  }

  isExpanded = (): boolean => this.router.url.includes(this.reportTemplate.routeUri);

  ngOnInit() {
    this.organizationService.getOrganizations()
      .subscribe(response =>
        this.allOrganizations = ListItem.mapToListItems(response.items));

    this.loadReportTemplate()
      .then(() => this.applyFilter());

    this.sideBarFilterService.mapRendered
      .pipe(untilComponentDestroyed(this))
      .subscribe(x => this.buttonDisabled = !x);
  }

  private createForm(): void {
    this.form = this.fb.group({
      startDate: [this.start],
      endDate: [this.end],
      catchType: [null],
      organization: [null]
    });
  }

  private async loadReportTemplate(): Promise<void> {
    await this.reportService
      .getReportTemplateByUri(ReportsRouteUri.HeatMap)
      .then(report => {
        this.reportTemplate = report as HeatMapReportTemplate;
      });
  }

  applyFilter(): void {
    this.buttonDisabled = true;
    const filter = this.toReportFilterTemplate();
    this.heatMapservice.updateFilter(filter);
  }

  private toReportFilterTemplate(): HeatMapReportFilter {
    return {
      ...this.reportTemplate.filter,
      isBeverrat: this.form.value.catchType,
      organizationId: this.form.value.organization,
      startDate: this.form.value.startDate ? dateAsYearMonthDay(this.form.value.startDate) : null,
      endDate: this.form.value.endDate ? dateAsYearMonthDay(this.form.value.endDate) : null
    };
  }

}
