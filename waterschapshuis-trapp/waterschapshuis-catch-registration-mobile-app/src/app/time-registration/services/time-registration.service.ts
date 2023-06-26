import { Injectable } from '@angular/core';
import {
  TimeRegistrationsClient,
  TimeRegistrationsEditCommand
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Observable, BehaviorSubject } from 'rxjs';
import { TimeRegistrationItem, TimeRegistrationItemForm } from '../models/time-registration-item';
import { TimeRegistration, TimeRegistrationForm } from '../models/time-registration';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { map } from 'rxjs/operators';
import { TimeRegistrationGeneralItem, TimeRegistrationGeneralItemForm } from '../models/time-registration-general-item';

@Injectable({
  providedIn: 'root'
})
export class TimeRegistrationService {
  private timeRegistrationForm: BehaviorSubject<FormGroup | undefined> =
    new BehaviorSubject(
      new TimeRegistrationForm().buildFormGroup(new TimeRegistration(new Date(this.getTodayUtcDateShortString())), this.fb)
    );

  timeRegistration$: Observable<FormGroup> = this.timeRegistrationForm.asObservable();

  constructor(
    private fb: FormBuilder,
    private timeRegistrationsClient: TimeRegistrationsClient
  ) { }

  loadTimeRegistration(dateString: string, subAreaHourSquareId: string): Observable<void> {
    const date = new Date(dateString);
    return this.timeRegistrationsClient.get(date, subAreaHourSquareId)
      .pipe(
        map(response => {
          const result = TimeRegistration.fromResponse(response);
          this.timeRegistrationForm.next(
            new TimeRegistrationForm().buildFormGroup(result, this.fb)
          );
        })
      );
  }

  saveTimeRegistration(command: TimeRegistrationsEditCommand): Observable<void> {
    return this.timeRegistrationsClient.post(command)
      .pipe(
        map(response => {
          const result = TimeRegistration.fromResponse(response);
          this.timeRegistrationForm.next(
            new TimeRegistrationForm().buildFormGroup(result, this.fb)
          );
        })
      );
  }

  addTimeRegistrationItem(): void {
    const currentTimeReg = this.timeRegistrationForm.getValue();
    const currentItems = currentTimeReg.get('inCreation') as FormArray;
    const timeRegItem = new TimeRegistrationItem();
    const timeRegItemForm = new TimeRegistrationItemForm(timeRegItem);

    currentItems.push(this.fb.group(timeRegItemForm));

    this.timeRegistrationForm.next(currentTimeReg);
  }

  addItemToTimeRegistrationItemList(item): void {
    const currentTimeReg = this.timeRegistrationForm.getValue();
    const currentItems = currentTimeReg.get('items') as FormArray;
    const timeRegItem = TimeRegistrationItem.fromJS(item);
    const timeRegItemForm = new TimeRegistrationItemForm(timeRegItem);

    currentItems.push(this.fb.group(timeRegItemForm));
  }

  addItemToTimeRegistrationGeneralItemList(item): void {
    const currentTimeReg = this.timeRegistrationForm.getValue();
    const currentGeneralItems = currentTimeReg.get('generalItems') as FormArray;
    const timeRegGenItem = TimeRegistrationGeneralItem.fromJS(item);
    const timeRegItemForm = new TimeRegistrationGeneralItemForm(timeRegGenItem);

    currentGeneralItems.push(this.fb.group(timeRegItemForm));
  }


  deleteTimeRegistrationItem(i: number, formArrayName: string): void {
    const currentTimeReg = this.timeRegistrationForm.getValue();
    const currentItems = currentTimeReg.get(formArrayName) as FormArray;

    currentItems.removeAt(i);

    this.timeRegistrationForm.next(currentTimeReg);
  }

  getTodayUtcDateShortString(): string {
    return new Date().toISOString().substring(0, 10);
  }
}
