import { Component, OnInit, AfterViewInit, HostListener, ViewChild, ChangeDetectorRef } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { TimeRegistrationStatus, TimeRegistrationsEditCommand, CatchStatus } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { TimeRegistrationsOfWeekForm } from '../../models/time-registrations-of-week-form';
import { WeekDateRange } from '../../models/week-date-range';
import { TimeRegistrationStateService } from '../../services/time-registration-state.service';
import { TimeRegistrationWeekNavigationService } from '../../services/time-registration-week-navigation.service';
import { TimeRegistrationService } from '../../services/time-registration.service';
import { concatMap } from 'rxjs/operators';
import { MatSidenav } from '@angular/material/sidenav';
import { TimeRegistrationGeneralForm } from '../../models/time-registration-general';
import { ToastService } from 'src/app/shared/toast/toast.service';
import { CatchRegistration } from '../../models/catch-registration';
import { TimeRegistrationResponse } from '../../models/time-registration';
import { TotalTimeHelper } from '../../models/registered-time-helper';
import { TotalTimeCalculatorService } from '../../services/total-time-calculator.service';

@Component({
  selector: 'app-time-registrations-personal',
  templateUrl: './time-registrations-personal.component.html',
  styleUrls: ['./time-registrations-personal.component.scss']
})
export class TimeRegistrationsPersonalComponent extends OnDestroyMixin
  implements OnInit, AfterViewInit {
  component: any = this;
  policyName = PolicyName;
  formGroup: FormGroup;
  timeRegistrationData: FormArray;
  catchesData: CatchRegistration[];
  timeRegistrationGeneralData: FormArray;
  weekDateRange: WeekDateRange;
  daysOfWeek: Date[] = [];
  haveAnyRecordWithWrittenStatus = false;
  timeRegistrationFormEnabledStatus = TimeRegistrationStatus.Written;
  disableAddingNewTimeRegistrationItems: boolean;
  hasAnyLoadedWrittenTimeRegistrionItems: boolean;
  hasAnyLoadedWrittenTimeRegistrionGeneralItems: boolean;
  isSidebarVisible = true;
  generalTimeRegistrationsFormGroup: FormGroup;

  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(
    public timeRegistrationStateService: TimeRegistrationStateService,
    private timeRegistrationService: TimeRegistrationService,
    private timeRegistrationWeekNavigationService: TimeRegistrationWeekNavigationService,
    private totalTimeCalculatorService: TotalTimeCalculatorService,
    private toastService: ToastService,
    private changeDetector: ChangeDetectorRef) {
    super();
  }
  ngAfterViewInit(): void {
    if (window.innerWidth < 1199) {
      this.sidenav.close();
    }
  }

  async ngOnInit(): Promise<void> {
    this.timeRegistrationWeekNavigationService.selectedYearAndWeekNumber$
      .pipe(untilComponentDestroyed(this.component),
        concatMap(data => {
          this.weekDateRange = this.timeRegistrationWeekNavigationService.getStartAndEndDateOfWeek(
            data.currentYear, data.currentWeekNumber);

          this.daysOfWeek = this.timeRegistrationWeekNavigationService.getDaysOfWeek(
            this.weekDateRange.startDate, this.weekDateRange.endDate);

          return this.timeRegistrationStateService.loadTimeRegistration(this.weekDateRange, this.daysOfWeek);
        }))
      .subscribe(form => {
        this.initFormData(form);
      });
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    if (event.target.innerWidth < 1199) {
      this.sidenav.close();
    } else {
      this.sidenav.open();
    }
  }

  async initFormData(form: TimeRegistrationResponse): Promise<void> {
    this.formGroup = new FormGroup(form.formData.controls);
    this.timeRegistrationData = this.formGroup?.get('timeRegistrationsForm').get('daysOfWeek') as FormArray;
    this.timeRegistrationGeneralData = this.formGroup?.get('timeRegistrationGeneralForm').get('generalItems') as FormArray;
    this.catchesData = form.catches;
    const formControlsEnabled = this.anyItemWithStatusWritten() ||
      this.allOnBothTimeRegistrationFormsWithStatusNull() && !this.catchesData.length;
    this.disableAddingNewTimeRegistrationItems = !formControlsEnabled;

    this.hasAnyLoadedWrittenTimeRegistrionItems =
      this.timeRegistrationStateService.anyTimeRegistrationWithStatusWritten(this.timeRegistrationData);

    this.hasAnyLoadedWrittenTimeRegistrionGeneralItems =
      this.timeRegistrationStateService.anyTimeRegistrationGeneralsWithStatusWritten(this.timeRegistrationGeneralData);

    this.formGroup.updateValueAndValidity();
    this.changeDetector.detectChanges();
  }

  saveTimeRegistrations(): void {
    const formGroup = TimeRegistrationsOfWeekForm.fromFormGroup(this.formGroup.get('timeRegistrationsForm') as FormGroup);
    const command = formGroup.toCommand(this.weekDateRange, TimeRegistrationStatus.Written);

    command.timeRegistrationGeneralItems =
      TimeRegistrationGeneralForm.toTimeRegistrationGeneralCommand(this.timeRegistrationGeneralData, TimeRegistrationStatus.Written);

    this.save(command);
  }

  closeThisWeek(): void {
    const timeRegistrationsFormGroup = TimeRegistrationsOfWeekForm
      .fromFormGroup(this.formGroup.get('timeRegistrationsForm') as FormGroup);
    const command = timeRegistrationsFormGroup.toCommand(this.weekDateRange, TimeRegistrationStatus.Closed);

    command.timeRegistrationGeneralItems =
      TimeRegistrationGeneralForm.toTimeRegistrationGeneralCommand(this.timeRegistrationGeneralData, TimeRegistrationStatus.Closed);

    command.catches = CatchRegistration.toCommand(this.catchesData, CatchStatus.Closed);

    this.save(command);
  }

  save(command: TimeRegistrationsEditCommand): void {
    this.timeRegistrationService.saveTimeRegistration(command)
      .subscribe(
        (response) => {
          this.timeRegistrationStateService.hasChanges = false;

          const result = new TimeRegistrationResponse(
            this.timeRegistrationStateService.buildFormFromResult(
              response.timeRegistrations, response.timeRegistrationGeneralItems, this.weekDateRange, this.daysOfWeek),
            response.catches
          );
          this.initFormData(result);
        },
        err => this.toastService.validationError(err, false)
      );
  }

  saveButtonDisabled(): boolean {
    return !this.saveButtonEnabled();
  }

  private saveButtonEnabled(): boolean {
    return this.formGroup?.valid &&
      (this.timeRegistrationStateService.anyNewEnteredOnEitherForm(this.timeRegistrationData, this.timeRegistrationGeneralData) ||
        this.hasChangesAndAnyFormHasLoadedWrittenItems());
  }

  private hasChangesAndAnyFormHasLoadedWrittenItems(): boolean {
    return (this.timeRegistrationStateService.hasChanges && this.hasAnyLoadedWrittenTimeRegistrionItems) ||
      (this.timeRegistrationStateService.hasChanges && this.hasAnyLoadedWrittenTimeRegistrionGeneralItems);
  }

  closeWeekButtonDisabled(): boolean {
    return !this.closeWeekButtonEnabled();
  }

  private closeWeekButtonEnabled(): boolean {
    return (this.formGroup?.valid && this.allOnBothTimeRegistrationFormsWithStatusNull() &&
      this.closeWeekButtonEnabledForCatchRegistrationForm()) ||
      (this.saveButtonEnabled() &&
        (!this.allOnBothTimeRegistrationFormsWithStatusNull() ||
          this.timeRegistrationStateService.anyNewEnteredOnEitherForm(this.timeRegistrationData, this.timeRegistrationGeneralData))) ||
      (this.formGroup?.valid &&
        ((!this.timeRegistrationStateService.hasChanges && this.hasAnyLoadedWrittenTimeRegistrionItems) ||
          (!this.timeRegistrationStateService.hasChanges && this.hasAnyLoadedWrittenTimeRegistrionGeneralItems)));
  }
  private closeWeekButtonEnabledForCatchRegistrationForm(): boolean {
    return !!this.catchesData &&
      this.timeRegistrationStateService.anyCatchRegistrationWithStatusWritten(this.catchesData);
  }

  private allOnBothTimeRegistrationFormsWithStatusNull() {
    return this.timeRegistrationStateService.allTimeRegistrationsWithStatusNull(this.timeRegistrationData) &&
      this.timeRegistrationStateService.allTimeRegistrationGeneralsWithStatusNull(this.timeRegistrationGeneralData);
  }

  private anyItemWithStatusWritten() {
    return this.timeRegistrationStateService.anyTimeRegistrationWithStatusWritten(this.timeRegistrationData) ||
      this.timeRegistrationStateService.anyCatchRegistrationWithStatusWritten(this.catchesData) ||
      this.timeRegistrationStateService.anyTimeRegistrationGeneralsWithStatusWritten(this.timeRegistrationGeneralData);
  }

  public timeRegistrationItemsContainsTime(): boolean {
    return this.totalTimeCalculatorService.timeRegistrationContainsTime(this.timeRegistrationData);
  }

  public timeRegistrationGeneralContainsTime(): boolean {
    return this.totalTimeCalculatorService.timeRegistrationGeneralContainsTime(this.timeRegistrationGeneralData, this.timeRegistrationData);
  }

  public displayTotalTimeRegistrationTime(): string {
    const minutesSum = this.totalTimeCalculatorService.calculateTimeRegistrationTotalTime(this.timeRegistrationData);

    return TotalTimeHelper.toDisplayString(minutesSum);
  }

  public displayTotalTimeRegistrationGeneralTime(): string {
    const minutesSum = this.totalTimeCalculatorService.calculateTimeRegistrationGeneralTotalTime(
      this.timeRegistrationGeneralData,
      this.timeRegistrationData
    );
    return TotalTimeHelper.toDisplayString(minutesSum);
  }

  public displayTotalCatchRegistrations(): number {
    return this.totalTimeCalculatorService.calculateTotalCatchRegistrations(this.catchesData);
  }

}
