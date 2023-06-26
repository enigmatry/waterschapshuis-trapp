import { AfterViewInit, Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormArray, AbstractControl, FormBuilder } from '@angular/forms';
import { IListItem } from 'src/app/shared/models/list-item';
import { BehaviorSubject } from 'rxjs';
import { TimeRegistrationStateService } from 'src/app/time-registration/services/time-registration-state.service';
import { TimeRegistrationForm } from 'src/app/time-registration/models/time-registration';
import { TimeRegistrationStatus } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

@Component({
  selector: 'app-time-registrations-of-date',
  templateUrl: './time-registrations-of-date.component.html',
  styleUrls: ['./time-registrations-of-date.component.scss']
})
export class TimeRegistrationsOfDateComponent implements OnInit, AfterViewInit {
  @Input() formGroup: FormGroup;
  @Input() catchAreas: IListItem[] = [];
  @Input() trappingTypes: IListItem[] = [];
  @Input() rowClass: string;
  @Input() enabledStatus: TimeRegistrationStatus;
  @Input() disableAddingNewItems: boolean;

  get dateAsString(): string {
    const dateOptions = { weekday: 'short', year: 'numeric', month: 'numeric', day: 'numeric' };
    return this.formGroup ? new Date(this.formGroup.get('date').value).toLocaleDateString('nl-NL', dateOptions) : '';
  }

  columns: Array<string> = ['catchArea', 'subArea', 'hourSquare', 'time', 'trappingType', 'actionColumn'];
  dataSource = new BehaviorSubject<AbstractControl[]>([]);

  dataLoaded = false;

  constructor(
    private timeRegistrationStateService: TimeRegistrationStateService,
    private fb: FormBuilder) { }

  ngOnInit() {
    const timeRegistrationsFormControls = this.getFormGroupItems().controls;

    this.timeRegistrationStateService
      .initDictionaries(
        timeRegistrationsFormControls.map(x => x.get('catchArea').value?.id),
        timeRegistrationsFormControls.map(x => x.get('subArea').value?.id));

    this.updateItemDisabledStatus();

    this.dataSource.next(timeRegistrationsFormControls);
  }

  ngAfterViewInit() {
    this.dataLoaded = true;

    this.getFormGroupItems().controls.forEach(x => TimeRegistrationForm.toggleValidators(x as FormGroup));
    this.formGroup.updateValueAndValidity();
  }

  updateItemDisabledStatus(): void {
    this.getFormGroupItems().controls.forEach((element: FormGroup) => {

      if (this.getItemDisabledStatus(element)) {
        element.disable();
      } else {
        element.enable();
      }
    });
  }

  addRow(element: FormGroup): void {
    if (element.invalid && element.touched) { return; }

    const timeRegistrationsFormControls = this.getFormGroupItems();
    timeRegistrationsFormControls.push(this.fb.group({
      id: null,
      date: this.formGroup.get('date').value,
      catchArea: null,
      subArea: null,
      hourSquare: null,
      trappingType: null,
      time: null,
      status: null
    }));
    this.dataSource.next(timeRegistrationsFormControls.controls);
  }

  deleteRow(element: FormGroup): void {
    const timeRegistrationsItems = this.getFormGroupItems();
    const index = timeRegistrationsItems.controls.indexOf(element);

    // if its a last item just reset all controls in it
    if (timeRegistrationsItems.controls.length === 1) {
      const group = timeRegistrationsItems.controls[0] as FormGroup;
      for (const name in group.controls) {
        if (name !== 'date') {
          group.controls[name].reset();
        }
      }
      TimeRegistrationForm.toggleValidators(element);
    } else {
      timeRegistrationsItems.removeAt(index);
      this.dataSource.next(timeRegistrationsItems.controls);
    }
    this.setStatusChanged();
  }

  resetSubArea(catchAreaId: string, element: FormGroup): void {
    element.get('subArea').setValue(null);
    this.timeRegistrationStateService.loadSubAreas(catchAreaId);
  }

  resetHourSquare(subAreaId: string, element: FormGroup): void {
    element.get('hourSquare').setValue(null);
    if (subAreaId) {
      this.timeRegistrationStateService.loadHourSquares(subAreaId);
    }
  }

  onChangeCatchArea(event: any, element: FormGroup) {
    element.get('catchArea').setValue(event.value);
    const selectedCatchArea = element.get('catchArea').value;
    this.resetSubArea(selectedCatchArea.id, element);

    const selectedSubArea = element.get('subArea').value;
    this.resetHourSquare(selectedSubArea?.id, element);
    TimeRegistrationForm.toggleValidators(element);
    TimeRegistrationForm.markAsTouched(element);
    this.setStatusChanged();
  }

  onChangeSubArea(event: any, element: FormGroup) {
    element.get('subArea').setValue(event.value);
    const selectedSubArea = element.get('subArea').value;
    this.resetHourSquare(selectedSubArea?.id, element);
    TimeRegistrationForm.toggleValidators(element);
    this.setStatusChanged();
  }

  getItemDisabledStatus(item: FormGroup): boolean {
    return this.enabledStatus !== item.get('status').value && (this.disableAddingNewItems || !item.get('status').value === null);
  }

  onChangeItem(element: FormGroup) {
    if (this.dataLoaded) {
      TimeRegistrationForm.toggleValidators(element);
      TimeRegistrationForm.markAsTouched(element);
      this.setStatusChanged();
    }
  }

  getActionButtonVisibility(element: FormGroup): boolean {
    return this.isAllowedActionStatus(element) && this.isLastElementForDate(element);
  }

  isAllowedActionStatus(element: FormGroup): boolean {
    return (element.get('status').value === this.enabledStatus ||
      element.get('status').value === null) && !this.disableAddingNewItems;
  }

  isLastElementForDate(element: FormGroup): boolean {
    const timeRegistrationsItems = this.getFormGroupItems();
    return (timeRegistrationsItems.controls.length - 1) === timeRegistrationsItems.controls.indexOf(element);
  }

  getFormGroupItems() {
    return this.formGroup.get('items') as FormArray;
  }

  getSubAreas = (element: any): IListItem[] =>
    this.timeRegistrationStateService.getSubAreas(element.get('catchArea').value?.id)

  getHourSquares = (element: any): IListItem[] =>
    this.timeRegistrationStateService.getHourSquares(element.get('subArea').value?.id)

  compareObjects(o1: any, o2: any): boolean {
    return o1?.name === o2?.name && o1?.id === o2?.id;
  }

  private setStatusChanged(): void {
    this.timeRegistrationStateService.hasChanges = true;
  }
}
