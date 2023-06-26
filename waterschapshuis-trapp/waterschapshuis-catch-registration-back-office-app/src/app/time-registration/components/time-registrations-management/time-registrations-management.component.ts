import { Component, HostListener, OnInit, AfterViewInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { of } from 'rxjs';
import { concatMap } from 'rxjs/operators';
import { TimeRegistrationsEditCommand, TimeRegistrationStatus, CatchStatus } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { TimeRegistrationsOfWeekForm } from '../../models/time-registrations-of-week-form';
import { WeekDateRange } from '../../models/week-date-range';
import { TimeRegistrationStateService } from '../../services/time-registration-state.service';
import { TimeRegistrationWeekNavigationService } from '../../services/time-registration-week-navigation.service';
import { TimeRegistrationService } from '../../services/time-registration.service';
import { UsersWithTimeRegistrationsPerRayonService } from '../../services/users-with-time-registrations-per-rayon.service';
import { SidebarUserType, TimeRegistrationUser } from '../../models/time-registrations-user';
import { UsersWithTimeRegistrationsPerRayonComponent } from '../side-bar/users-with-time-registrations-per-rayon/users-with-time-registrations-per-rayon.component';
import { MatSidenav } from '@angular/material/sidenav';
import { TimeRegistrationGeneralForm } from '../../models/time-registration-general';
import { ToastService } from 'src/app/shared/toast/toast.service';
import { CatchRegistration } from '../../models/catch-registration';
import { TimeRegistrationResponse } from '../../models/time-registration';
import { TotalTimeHelper } from '../../models/registered-time-helper';
import { TotalTimeCalculatorService } from '../../services/total-time-calculator.service';

@Component({
  selector: 'app-time-registrations-management',
  templateUrl: './time-registrations-management.component.html',
  styleUrls: ['./time-registrations-management.component.scss']
})
export class TimeRegistrationsManagementComponent extends OnDestroyMixin
  implements OnInit, AfterViewInit {
  @ViewChild(UsersWithTimeRegistrationsPerRayonComponent) childComponent: UsersWithTimeRegistrationsPerRayonComponent;

  component: any = this;

  rayonId: string;
  timeRegistrationData: FormArray;
  timeRegistrationDataItems: AbstractControl[];
  catchesData: CatchRegistration[];
  timeRegistrationGeneralData: FormArray;
  timeRegistrationFormEnabledStatus = TimeRegistrationStatus.Closed;
  timeRegistrationGeneralFormEnabledStatus = TimeRegistrationStatus.Closed;
  disableAddingNewTimeRegistrationItems: boolean;
  disableAddingNewTimeRegistrationGeneralItems: boolean;
  formGroup: FormGroup;
  daysOfWeek: Date[] = [];
  hasAnyRecordWithClosedStatus: boolean;
  selectedUser: TimeRegistrationUser;
  weekDateRange: WeekDateRange;
  hasAnyLoadedClosedTimeRegistrationItems: boolean;
  hasAnyLoadedClosedTimeRegistrionGeneralItems: boolean;
  isSidebarVisible = true;

  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(
    public timeRegistrationStateService: TimeRegistrationStateService,
    private usersWithTimeRegistrationsPerRayonService: UsersWithTimeRegistrationsPerRayonService,
    private timeRegistrationWeekNavigationService: TimeRegistrationWeekNavigationService,
    private timeRegistrationService: TimeRegistrationService,
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
      .pipe(untilComponentDestroyed(this.component))
      .subscribe(data => {
        this.weekDateRange = this.timeRegistrationWeekNavigationService.getStartAndEndDateOfWeek(
          data.currentYear, data.currentWeekNumber);

        this.daysOfWeek = this.timeRegistrationWeekNavigationService.getDaysOfWeek(
          this.weekDateRange.startDate, this.weekDateRange.endDate);

        this.usersWithTimeRegistrationsPerRayonService.changeSelectedUser(this.selectedUser);
      });

    this.usersWithTimeRegistrationsPerRayonService.selectedUser$
      .pipe(untilComponentDestroyed(this.component),
        concatMap(user => {
          this.selectedUser = user;
          this.rayonId = user?.rayonId;

          if (user?.sideBarUserType === SidebarUserType.usersInOrganization) {
            this.timeRegistrationFormEnabledStatus = -1;
            this.timeRegistrationGeneralFormEnabledStatus = -1;
          } else {
            this.timeRegistrationFormEnabledStatus = TimeRegistrationStatus.Closed;
            this.timeRegistrationGeneralFormEnabledStatus = TimeRegistrationStatus.Closed;
          }

          if (!user) {
            return of(null);
          }

          return this.timeRegistrationStateService.loadTimeRegistrationForUser(this.weekDateRange, user.id, this.rayonId, this.daysOfWeek);
        }))
      .subscribe(form => {
        this.initFormData(form);
      },
        err => console.log(err));
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
    this.formGroup = form?.formData;
    this.timeRegistrationData = this.formGroup?.get('timeRegistrationsForm').get('daysOfWeek') as FormArray;
    this.timeRegistrationDataItems = this.timeRegistrationData?.controls;
    this.timeRegistrationGeneralData = this.formGroup?.get('timeRegistrationGeneralForm').get('generalItems') as FormArray;
    this.catchesData = form?.catches;

    const formControlsEnabled = this.anyItemWithStatusClosed() &&
      this.selectedUser?.sideBarUserType !== SidebarUserType.usersInOrganization;
    const generalFormControlsEnabled = this.anyGeneralItemWithStatusClosed() &&
      this.selectedUser?.sideBarUserType !== SidebarUserType.usersInOrganization;
    this.disableAddingNewTimeRegistrationItems =
      !formControlsEnabled || this.timeRegistrationFormEnabledStatus === -1;

    this.disableAddingNewTimeRegistrationGeneralItems =
      !generalFormControlsEnabled || this.timeRegistrationGeneralFormEnabledStatus === -1;

    this.hasAnyLoadedClosedTimeRegistrationItems =
      this.timeRegistrationStateService.anyWithStatusClosed(this.timeRegistrationData);

    this.hasAnyLoadedClosedTimeRegistrionGeneralItems = this.timeRegistrationGeneralData?.controls
      .some(item => item.value.status === TimeRegistrationStatus.Closed);

    this.formGroup?.updateValueAndValidity();
    this.changeDetector.detectChanges();
  }

  private anyItemWithStatusClosed() {
    return this.timeRegistrationStateService.anyTimeRegistrationWithStatusClosed(this.timeRegistrationData) ||
      this.timeRegistrationStateService.anyCatchRegistrationWithStatusClosed(this.catchesData);
  }

  private anyGeneralItemWithStatusClosed() {
      return this.timeRegistrationStateService.anyTimeRegistrationGeneralsWithStatusClosed(this.timeRegistrationGeneralData);
  }

  saveTimeRegistrations(): void {
    const formGroup = TimeRegistrationsOfWeekForm.fromFormGroup(this.formGroup.get('timeRegistrationsForm') as FormGroup);
    const command = formGroup.toCommand(this.weekDateRange, TimeRegistrationStatus.Closed);

    command.timeRegistrationGeneralItems =
      TimeRegistrationGeneralForm.toTimeRegistrationGeneralCommand(
        this.timeRegistrationGeneralData,
        TimeRegistrationStatus.Closed,
        TimeRegistrationStatus.Completed);

    this.save(command);
  }

  completeThisWeek(): void {
    const formGroup = TimeRegistrationsOfWeekForm.fromFormGroup(this.formGroup.get('timeRegistrationsForm') as FormGroup);
    const command = formGroup.toCommand(this.weekDateRange, TimeRegistrationStatus.Completed, TimeRegistrationStatus.Written);

    command.timeRegistrationGeneralItems =
      TimeRegistrationGeneralForm.toTimeRegistrationGeneralCommand(this.timeRegistrationGeneralData, TimeRegistrationStatus.Completed);

    command.catches = CatchRegistration.toCommand(this.catchesData, CatchStatus.Completed);

    this.save(command);
    this.childComponent.updateUsersWeekCompletedStatus(this.selectedUser.id, this.selectedUser.rayonId);
  }

  save(command: TimeRegistrationsEditCommand): void {
    this.timeRegistrationService.saveTimeRegistrationForUser(this.selectedUser.id, this.selectedUser.rayonId, command)
      .subscribe(
        (response) => {
          this.timeRegistrationStateService.hasChanges = false;
          this.hasAnyLoadedClosedTimeRegistrationItems = response.timeRegistrations.some(item =>
            item.status === TimeRegistrationStatus.Closed
          );

          const result = new TimeRegistrationResponse(
            this.timeRegistrationStateService.buildFormFromResult(
              response.timeRegistrations, response.timeRegistrationGeneralItems, this.weekDateRange,
              this.daysOfWeek),
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
        this.hasChangesAndAnyFormHasLoadedClosedItems());
  }
  private hasChangesAndAnyFormHasLoadedClosedItems(): boolean {
    return (this.timeRegistrationStateService.hasChanges && this.hasAnyLoadedClosedTimeRegistrationItems) ||
      (this.timeRegistrationStateService.hasChanges && this.hasAnyLoadedClosedTimeRegistrionGeneralItems);
  }

  completeWeekButtonDisabled(): boolean {
    return !this.completeWeekButtonEnabled();
  }

  actionButtonsVisible(): boolean {
    return this.selectedUser?.sideBarUserType !== SidebarUserType.usersInOrganization &&
      (!!this.timeRegistrationData || !!this.timeRegistrationGeneralData || !!this.catchesData);
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

  private completeWeekButtonEnabled(): boolean {
    return (this.formGroup?.valid && this.allOnBothTimeRegistrationFormsWithStatusNull() &&
      this.completeButtonEnabledForCatchRegistrationForm()) ||
      (this.saveButtonEnabled() &&
        (!this.allOnBothTimeRegistrationFormsWithStatusNull() ||
          this.timeRegistrationStateService.anyNewEnteredOnEitherForm(this.timeRegistrationData, this.timeRegistrationGeneralData))) ||
      (this.formGroup?.valid && (!this.timeRegistrationStateService.hasChanges && this.hasAnyLoadedClosedTimeRegistrationItems) ||
        (!this.timeRegistrationStateService.hasChanges && this.hasAnyLoadedClosedTimeRegistrionGeneralItems));
  }
  private allOnBothTimeRegistrationFormsWithStatusNull() {
    return this.timeRegistrationStateService.allTimeRegistrationsWithStatusNull(this.timeRegistrationData) &&
      this.timeRegistrationStateService.allTimeRegistrationGeneralsWithStatusNull(this.timeRegistrationGeneralData);
  }
  private completeButtonEnabledForCatchRegistrationForm(): boolean {
    return !!this.catchesData &&
      this.timeRegistrationStateService.anyCatchRegistrationWithStatusClosed(this.catchesData);
  }
}
