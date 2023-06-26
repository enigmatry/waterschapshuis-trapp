import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { untilComponentDestroyed, OnDestroyMixin } from '@w11k/ngx-componentdestroyed';
import { take } from 'rxjs/operators';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { AlertAction, AlertContentComponent } from 'src/app/shared/alert/alert/alert-content.component';
import { TimeRegistrationStateService } from 'src/app/time-registration/services/time-registration-state.service';
import { TimeRegistrationDateService } from '../../services/time-registration-date.service';
import { TimeRegistrationWeekNavigationService } from '../../services/time-registration-week-navigation.service';
import { WeekDirection } from '../../models/week-direction.enum';

@Component({
  selector: 'app-time-registrations-header',
  templateUrl: './time-registrations-header.component.html',
  styleUrls: ['./time-registrations-header.component.scss']
})
export class TimeRegistrationsHeaderComponent extends OnDestroyMixin implements OnInit {
  component: any = this;
  policyName = PolicyName;
  protected alertActions: AlertAction[] = [{ text: 'Nee', action: false, isDefault: true }, { text: 'Ja', action: true, isDefault: false }];

  @Input() selectedUserName: string;
  @Input() saveButtonDisabledStatus: boolean;
  @Input() closeWeekButtonDisabledStatus: boolean;
  @Input() completeWeekButtonDisabledStatus: boolean;

  @Input() saveButtonVisibilityStatus: boolean;
  @Input() closeWeekButtonVisibilityStatus: boolean;
  @Input() completeWeekButtonVisibilityStatus: boolean;

  @Output() saveEvent = new EventEmitter();
  @Output() closeWeekEvent = new EventEmitter();
  @Output() completeWeekEvent = new EventEmitter();

  selectedYear: number;
  selectedWeekNumber: number;

  constructor(
    public timeRegistrationWeekNavigationService: TimeRegistrationWeekNavigationService,
    private timeRegistrationDateService: TimeRegistrationDateService,
    private timeRegistrationStateService: TimeRegistrationStateService,
    private activatedRoute: ActivatedRoute,
    public dialog: MatDialog) {
    super();
  }

  async ngOnInit(): Promise<void> {
    this.timeRegistrationWeekNavigationService.selectedYearAndWeekNumber$
      .pipe(untilComponentDestroyed(this.component))
      .subscribe(data => {
        this.selectedYear = data.currentYear;
        this.selectedWeekNumber = data.currentWeekNumber;
      });

    const params = await this.activatedRoute.params.pipe(take(1)).toPromise();

    if (!isNaN(params.year) && !isNaN(params.week)) {
      const dateRange = this.timeRegistrationWeekNavigationService.getStartAndEndDateOfWeek(Number(params.year), Number(params.week));
      this.timeRegistrationDateService.setSelectedWeekDate(dateRange.startDate);
      this.timeRegistrationWeekNavigationService.setWeekNumberAndYear(
        {
          currentYear: Number(params.year),
          currentWeekNumber: Number(params.week)
        });
    }
  }

  navigateToPreviousWeek(): void {
    this.timeRegistrationStateService.hasChanges ?
      this.showConfirmDialog(WeekDirection.previous) :
      this.navigateBetweenWeeks(WeekDirection.previous);
  }

  navigateToNextWeek(): void {
    this.timeRegistrationStateService.hasChanges ?
      this.showConfirmDialog(WeekDirection.next) :
      this.navigateBetweenWeeks(WeekDirection.next);
  }

  navigateBetweenWeeks(direction: WeekDirection) {
    this.timeRegistrationWeekNavigationService.changeWeekNumberAndYear(direction);
  }

  saveRegistrations() {
    this.saveEvent.emit();
  }

  closeWeek() {
    this.closeWeekEvent.emit();
  }

  completeWeek() {
    this.completeWeekEvent.emit();
  }

  private showConfirmDialog(direction: WeekDirection) {
    const dialogRef = this.dialog.open(AlertContentComponent, {
      data: {
        title: 'Navigation',
        header: '',
        message: 'Weet u zeker dat u wilt annuleren? Uw wijzingen zullen niet bewaard worden.',
        actions: this.alertActions
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.navigateBetweenWeeks(direction);
        this.timeRegistrationStateService.hasChanges = false;
      }
    });
  }

  get navigateToNextDisabled() {
    const weekAndYear = this.timeRegistrationDateService.getISOWeekNavigationData();
    return this.selectedYear === weekAndYear.currentYear &&
      this.selectedWeekNumber >= weekAndYear.currentWeekNumber;
  }
}
