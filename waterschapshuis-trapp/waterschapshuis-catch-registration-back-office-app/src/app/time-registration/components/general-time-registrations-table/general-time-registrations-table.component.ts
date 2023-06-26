import { DatePipe } from '@angular/common';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { TimeRegistrationStatus } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { IListItem } from 'src/app/shared/models/list-item';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { TotalTimeHelper } from '../../models/registered-time-helper';
import { TimeRegistrationGeneral, TimeRegistrationGeneralForm } from '../../models/time-registration-general';
import { TimeRegistrationStateService } from '../../services/time-registration-state.service';

@Component({
  selector: 'app-general-time-registrations-table',
  templateUrl: './general-time-registrations-table.component.html',
  styleUrls: ['./general-time-registrations-table.component.scss']
})
export class GeneralTimeRegistrationsTableComponent implements OnInit, OnChanges {
  @Input() daysOfWeek: Date[] = [];
  @Input() timeRegistrationGeneralData: FormArray;
  @Input() timeRegistrationData: FormArray;
  @Input() enabledStatus: TimeRegistrationStatus;
  @Input() disableAddingNewItems: boolean;
  myForm: FormGroup;

  categories: IListItem[] = [];
  constructor(
    private timeRegistrationStateService: TimeRegistrationStateService,
    private lookupsService: LookupsService,
    private fb: FormBuilder,
    private datePipe: DatePipe) { }

  async ngOnInit() {
    this.categories = await this.lookupsService.getTimeRegistrationActiveCategories().toPromise();
  }

  ngOnChanges() {
    this.updateItemDisabledStatus();
  }

  updateItemDisabledStatus(): void {
    this.timeRegistrationGeneralData.controls.forEach((item: FormGroup) => {
      if (this.getItemDisabledStatus(item)) {
        item.disable();
      } else {
        item.enable();
      }
    });
  }

  getItemDisabledStatus(item: FormGroup): boolean {
    return this.enabledStatus !== item.get('status').value && (this.disableAddingNewItems || !item.get('status').value === null);
  }


  addRow(item: FormGroup): void {
    if (item.invalid && item.touched) { return; }

    const newTimeRegistration = TimeRegistrationGeneral.createWithDateOnly(item.get('date').value as Date);
    const timeRegistrationGeneralFormGroup =
      new TimeRegistrationGeneralForm(newTimeRegistration).buildFormGroup(item.get('date').value, this.fb);

    this.timeRegistrationGeneralData.push(timeRegistrationGeneralFormGroup);
  }

  deleteRow(item: FormGroup): void {
    const index = this.timeRegistrationGeneralData.controls.indexOf(item);

    const isLastItemForDate = this.filterTimeRegistrationGeneralByDate(item.get('date').value).length === 1;
    if (isLastItemForDate) {
      const group = this.timeRegistrationGeneralData.controls[index] as FormGroup;
      for (const name in group.controls) {
        if (name !== 'date') {
          group.controls[name].reset();
        }
      }
      TimeRegistrationGeneralForm.toggleValidators(item);
    } else {
      this.timeRegistrationGeneralData.removeAt(index);
    }
    this.setStatusChanged();
  }

  onChangeItem(item: FormGroup) {
    TimeRegistrationGeneralForm.toggleValidators(item);
    TimeRegistrationGeneralForm.markAsTouched(item);
    this.setStatusChanged();
  }

  getTimeRegistrationGeneralItemsForDate(date: Date) {
    return this.filterTimeRegistrationGeneralByDate(date);
  }

  getActionButtonVisibility(item: FormGroup, dayOfWeek: Date, index: number): boolean {
    return this.isAllowedDeleteItem(item) && this.isLastItem(this.getTimeRegistrationGeneralItemsForDate(dayOfWeek).length, index);
  }

  isAllowedDeleteItem(item: FormGroup): boolean {
    return (item.get('status').value === this.enabledStatus ||
      item.get('status').value === null) && !this.disableAddingNewItems;
  }

  isLastItem(numberOfItems: number, index: number): boolean {
    return numberOfItems === index + 1;
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleDateString('nl-NL', { weekday: 'short', year: 'numeric', month: 'numeric', day: 'numeric' });
  }

  getTimeRegistrationTotalTime(date: Date) {
    const timeRegistrationItems = this.filterTimeRegistrationByDate(date);
    const timeRegistrationMinutes = TotalTimeHelper.calculateTotalTimeForRow(timeRegistrationItems);
    return TotalTimeHelper.toDisplayString(timeRegistrationMinutes);
  }

  getTotalTime(date: Date) {
    const generalTimeRegistrationItems = this.filterTimeRegistrationGeneralByDate(date)
      .map(x => ({ time: x.get('time').value }));
    const timeRegistrationItems = this.filterTimeRegistrationByDate(date);

    const timeRegistrationMinutes = TotalTimeHelper.calculateTotalTimeForRow(timeRegistrationItems);
    const generaltimeRegistrationMinutes = TotalTimeHelper.calculateTotalTimeForRow(generalTimeRegistrationItems);
    return TotalTimeHelper.toDisplayString(timeRegistrationMinutes + generaltimeRegistrationMinutes);
  }

  rowContainsTime(date: Date) {
    const generalTimeRegistrationItems = this.filterTimeRegistrationGeneralByDate(date)
      .map(x => ({ time: x.get('time').value }));
    const timeRegistrationItems = this.filterTimeRegistrationByDate(date);

    return TotalTimeHelper.itemsContainTime(timeRegistrationItems) || TotalTimeHelper.itemsContainTime(generalTimeRegistrationItems);
  }

  timeRegistrationHasRegisteredTime(date: Date): boolean {
    return this.filterTimeRegistrationByDate(date).length > 0;
  }

  timeRegistrationGeneralHasRegisteredTime(date: Date): boolean {
    const generalTimeRegistrationItems = this.filterTimeRegistrationGeneralByDate(date)
      .map(x => ({ time: x.get('time').value }));
    return TotalTimeHelper.itemsContainTime(generalTimeRegistrationItems);
  }

  private filterTimeRegistrationByDate(date: Date): AbstractControl[] {
    return this.timeRegistrationData.controls.flatMap(x => x.get('items').value).filter(x =>
        x.time !== null && (this.datePipe.transform(x?.date, 'yyyy-MM-dd') === this.datePipe.transform(date, 'yyyy-MM-dd')));
  }

  private filterTimeRegistrationGeneralByDate(date: Date): AbstractControl[] {
    return this.timeRegistrationGeneralData?.controls?.filter(x =>
      this.datePipe.transform(x?.get('date')?.value, 'yyyy-MM-dd') === this.datePipe.transform(date, 'yyyy-MM-dd'));
  }

  private setStatusChanged(): void {
    this.timeRegistrationStateService.hasChanges = true;
  }
}
