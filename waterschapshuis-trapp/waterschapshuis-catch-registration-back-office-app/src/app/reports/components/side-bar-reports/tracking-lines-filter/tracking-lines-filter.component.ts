import { Component, OnInit, Input } from '@angular/core';
import { TrackingLinesReportTemplate } from 'src/app/reports/models/common/tracking-lines-report-template.model';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { ReportsRouteUri } from 'src/app/reports/models/reports-route-uri.enum';
import { SideBarFilterService } from 'src/app/reports/services/side-bar-filter.service';
import { TrackingLineFilter } from 'src/app/reports/models/tracking-line-filter.model';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-tracking-lines-filter',
  templateUrl: './tracking-lines-filter.component.html',
  styleUrls: ['./tracking-lines-filter.component.scss']
})
export class TrackingLinesFilterComponent extends OnDestroyMixin implements OnInit {
  @Input() trackingLinesTemplate: TrackingLinesReportTemplate;

  routerLink = `reports/${ReportsRouteUri.TrackingLines}`;
  form: FormGroup;

  buttonDisabled = false;

  constructor(private router: Router, private sideBarFilter: SideBarFilterService) {
    super();
  }

  ngOnInit() {
    const filter = this.sideBarFilter.trackingLineFilterStart;
    this.form = new FormGroup({
      start: new FormControl(filter.start, [Validators.required]),
      end: new FormControl(filter.end, [Validators.required])
    });

    this.sideBarFilter.mapRendered
      .pipe(untilComponentDestroyed(this))
      .subscribe(x => this.buttonDisabled = !x);
  }

  isExpanded() {
    return this.router.url.includes(this.trackingLinesTemplate.routeUri);
  }

  onFilter() {
    this.buttonDisabled = true;
    this.sideBarFilter.filterTrackingLines(
      new TrackingLineFilter(this.form.value.start, this.form.value.end));
  }

  private maxDateValidator(max: Date): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      return (control.value as Date) > max ? { maxDate: max } : null;
    };
  }
}
