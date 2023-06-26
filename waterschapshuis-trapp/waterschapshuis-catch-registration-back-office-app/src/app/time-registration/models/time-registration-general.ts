import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {
    GetTimeRegistrationsOfWeekResponseTimeRegistrationGeneral,
    TimeRegistrationsEditTimeRegistrationGeneral,
    TimeRegistrationStatus
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { forbidZeroTimeValueValidator } from '../validators/time-not-zero-validator';
import { TotalTimeHelper } from './registered-time-helper';

export class TimeRegistrationGeneral {
    constructor(
        id: string,
        date: Date,
        categoryId: string,
        hours: number,
        minutes: number,
        status: TimeRegistrationStatus) {
        this.id = id;
        this.date = date;
        this.categoryId = categoryId;
        this.hours = hours;
        this.minutes = minutes;
        this.status = status;
    }
    id: string;
    date: Date;
    categoryId: string;
    hours?: number;
    minutes?: number;
    status: TimeRegistrationStatus;

    public get time(): string {
        return this.hours !== null && this.hours !== undefined && this.minutes !== null && this.minutes !== undefined ?
            `${String(this.hours).padStart(2, '0')}:${String(this.minutes).padStart(2, '0')}` : null;
    }

    public set time(value: string) {
        this.hours = TotalTimeHelper.parseHours(value);
        this.minutes = TotalTimeHelper.parseMinutes(value);
    }


    static createWithDateOnly(date: Date): TimeRegistrationGeneral {
        return new TimeRegistrationGeneral(null, date, null, null, null, null);
    }

    static fromResponse(response: GetTimeRegistrationsOfWeekResponseTimeRegistrationGeneral): TimeRegistrationGeneral {
        return new TimeRegistrationGeneral(
            response.id,
            response.date,
            response.category.id,
            response.hours,
            response.minutes,
            response.status
        );
    }

    static toDisplayString(minutes: number, hours?: number): string {
        if (hours !== undefined) {
            return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}`;
        } else {
            hours = Math.floor(minutes / 60);
            minutes = hours === 0 ? minutes : minutes % (hours * 60);
            return this.toDisplayString(minutes, hours);
        }
    }
}

export class TimeRegistrationGeneralForm {

    constructor(item: TimeRegistrationGeneral) {
        this.id.setValue(item.id);
        this.date.setValue(item.date);
        this.categoryId.setValue(item.categoryId);
        this.time.setValue(item.time);
        this.status.setValue(item.status);

        if (item.id !== null) {
            this.time.setValidators([Validators.required, forbidZeroTimeValueValidator()]);
            this.date.setValidators([Validators.required]);
            this.categoryId.setValidators([Validators.required]);
        }
    }
    id = new FormControl();
    date = new FormControl();
    categoryId = new FormControl();
    time = new FormControl();
    status = new FormControl();

    formGroup: FormGroup;

    static toggleValidators(element: FormGroup) {
        if (element.get('id').value || element.get('categoryId').value || element.get('time').value) {
            element.get('categoryId').setValidators([Validators.required]);
            element.get('time').setValidators([Validators.required, forbidZeroTimeValueValidator()]);
        } else {
            element.get('categoryId').clearValidators();
            element.get('time').clearValidators();
        }
        element.get('categoryId').updateValueAndValidity();
        element.get('time').updateValueAndValidity();
    }

    static markAsTouched(element: FormGroup) {
        element.get('categoryId').markAsTouched();
        element.get('time').markAsTouched();
    }

    static toCommand(control: any, status?: TimeRegistrationStatus): TimeRegistrationsEditTimeRegistrationGeneral {
        const commandItem = new TimeRegistrationsEditTimeRegistrationGeneral();
        commandItem.id = control.id;
        commandItem.categoryId = control.categoryId;
        commandItem.date = new Date(Date.parse(control.date));
        commandItem.date.setHours(12, 0, 0, 0);
        commandItem.hours = TotalTimeHelper.parseHours(control.time);
        commandItem.minutes = TotalTimeHelper.parseMinutes(control.time);
        commandItem.status = status ?? control.status;
        return commandItem;
    }

    static toTimeRegistrationGeneralCommand(
        timeRegistrationGeneral: FormArray,
        status: TimeRegistrationStatus,
        statusNotToChange?: TimeRegistrationStatus)
        : TimeRegistrationsEditTimeRegistrationGeneral[] {
        return timeRegistrationGeneral.controls.filter(item => this.timeRegistrationItemFilled(item)).map(x => {
            const statusToSet = x.value.status === statusNotToChange ? null : status;
            return this.toCommand(x.value, statusToSet);
        });
    }

    private static timeRegistrationItemFilled(item: AbstractControl): boolean {
        return item.get('categoryId').value || item.get('time').value || item.get('status').value;
    }

    buildFormGroup(timeRegistrationsDate: Date, fb: FormBuilder): FormGroup {
        this.formGroup = fb.group({
            id: this.id,
            date: timeRegistrationsDate,
            categoryId: this.categoryId.value ? this.categoryId : null,
            time: this.time,
            status: this.status.value ? this.status : null
        });

        return this.formGroup;
    }

}

