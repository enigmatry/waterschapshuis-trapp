import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {
    TimeRegistrationsEditCommandItem,
    TimeRegistrationStatus,
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { IListItem, ListItem } from 'src/app/shared/models/list-item';
import { forbidZeroTimeValueValidator } from '../validators/time-not-zero-validator';
import { CatchRegistration } from './catch-registration';
import { TotalTimeHelper } from './registered-time-helper';

export class TimeRegistration {
    id?: string = null;
    date?: Date = null;
    catchArea?: IListItem = null;
    subArea?: IListItem = null;
    hourSquare?: IListItem = null;
    trappingType?: IListItem = null;
    hours?: number;
    minutes?: number;
    status: TimeRegistrationStatus;

    constructor(date: Date) {
        this.date = date;
    }

    public get time(): string {
        return this.hours !== null && this.hours !== undefined && this.minutes !== null && this.minutes !== undefined ?
            `${String(this.hours).padStart(2, '0')}:${String(this.minutes).padStart(2, '0')}` : null;
    }

    public set time(value: string) {
        this.hours = TotalTimeHelper.parseHours(value);
        this.minutes = TotalTimeHelper.parseMinutes(value);
    }

    static fromResponse(response: any): TimeRegistration {
        const timeRegItem = new TimeRegistration(response.date);
        timeRegItem.id = response.id;
        timeRegItem.date = response.date;
        timeRegItem.catchArea = response.catchArea ? ListItem.fromNameEntityItem(response.catchArea) : null;
        timeRegItem.subArea = response.subArea ? ListItem.fromNameEntityItem(response.subArea) : null;
        timeRegItem.hourSquare = response.hourSquare ? ListItem.fromNameEntityItem(response.hourSquare) : null;
        timeRegItem.trappingType = response.trappingType ? ListItem.fromNameEntityItem(response.trappingType) : null;
        timeRegItem.hours = response.hours;
        timeRegItem.minutes = response.minutes;
        timeRegItem.status = response.status;

        return timeRegItem;
    }

    static fromFormGroup(formGroup: FormGroup): TimeRegistration {
        const timeRegItem = new TimeRegistration(formGroup.get('date').value);
        timeRegItem.id = formGroup.get('id').value;
        timeRegItem.date = formGroup.get('date').value;
        timeRegItem.catchArea = ListItem.fromNameEntityFormControl(formGroup.get('catchArea') as FormControl);
        timeRegItem.subArea = ListItem.fromNameEntityFormControl(formGroup.get('subArea') as FormControl);
        timeRegItem.hourSquare = ListItem.fromNameEntityFormControl(formGroup.get('hourSquare') as FormControl);
        timeRegItem.trappingType = ListItem.fromNameEntityFormControl(formGroup.get('trappingType') as FormControl);
        timeRegItem.hours = TotalTimeHelper.parseHours(formGroup.get('time').value);
        timeRegItem.minutes = TotalTimeHelper.parseMinutes(formGroup.get('time').value);
        timeRegItem.status = formGroup.get('status').value;
        return timeRegItem;
    }

    toForm(): TimeRegistrationForm {
        return new TimeRegistrationForm(this);
    }
}
export class TimeRegistrationResponse {
    formData: FormGroup;
    catches: CatchRegistration[];

    constructor(formData: FormGroup, catches: CatchRegistration[]) {
        this.formData = formData;
        this.catches = catches;
    }
}

export class TimeRegistrationForm {
    id = new FormControl();
    date = new FormControl();
    catchArea = new FormControl();
    subArea = new FormControl();
    hourSquare = new FormControl();
    trappingType = new FormControl();
    time = new FormControl();
    status = new FormControl();

    item: TimeRegistration;
    formGroup: FormGroup;

    static toggleValidators(element: FormGroup) {
        if (element.get('id').value || element.get('catchArea').value ||
            element.get('subArea').value || element.get('hourSquare').value ||
            element.get('trappingType').value || element.get('time').value) {
            element.get('catchArea').setValidators([Validators.required]);
            element.get('subArea').setValidators([Validators.required]);
            element.get('hourSquare').setValidators([Validators.required]);
            element.get('trappingType').setValidators([Validators.required]);
            element.get('time').setValidators([Validators.required, forbidZeroTimeValueValidator()]);
        } else {
            element.get('catchArea').clearValidators();
            element.get('subArea').clearValidators();
            element.get('hourSquare').clearValidators();
            element.get('trappingType').clearValidators();
            element.get('time').clearValidators();
        }
        element.get('catchArea').updateValueAndValidity();
        element.get('subArea').updateValueAndValidity();
        element.get('hourSquare').updateValueAndValidity();
        element.get('trappingType').updateValueAndValidity();
        element.get('time').updateValueAndValidity();
    }

    static markAsTouched(element: FormGroup) {
        element.get('catchArea').markAsTouched();
        element.get('subArea').markAsTouched();
        element.get('hourSquare').markAsTouched();
        element.get('trappingType').markAsTouched();
        element.get('time').markAsTouched();
    }

    constructor(item: TimeRegistration) {
        this.item = item;

        this.id.setValue(this.item.id);

        this.date.setValue(this.item.date);
        this.date.setValidators([Validators.required]);

        this.catchArea.setValue(this.item.catchArea);
        this.catchArea.setValidators([Validators.required]);

        this.subArea.setValue(this.item.subArea);
        this.subArea.setValidators([Validators.required]);

        this.hourSquare.setValue(this.item.hourSquare);
        this.hourSquare.setValidators([Validators.required]);

        this.trappingType.setValue(this.item.trappingType);
        this.trappingType.setValidators([Validators.required]);

        this.time.setValue(this.item.time);
        this.time.setValidators([Validators.required, forbidZeroTimeValueValidator()]);

        this.status.setValue(this.item.status);
    }

    static fromFormGroup(formGroup: FormGroup): TimeRegistrationForm {
        const timeRegItem = TimeRegistration.fromFormGroup(formGroup);
        return new TimeRegistrationForm(timeRegItem);
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

    buildFormGroup(timeRegistrationsDate: Date, fb: FormBuilder): FormGroup {
        this.formGroup = fb.group({
            id: this.id,
            date: timeRegistrationsDate,
            catchArea: this.catchArea.value ? this.catchArea : null,
            subArea: this.subArea.value ? this.subArea : null,
            hourSquare: this.hourSquare.value ? this.hourSquare : null,
            trappingType: this.trappingType.value ? this.trappingType : null,
            time: this.time,
            status: this.status.value ? this.status : null
        });

        return this.formGroup;
    }

    toCommandItem(status?: TimeRegistrationStatus): TimeRegistrationsEditCommandItem {
        const commandItem = new TimeRegistrationsEditCommandItem();
        commandItem.id = this.id.value;
        commandItem.subAreaId = this.subArea.value.id;
        commandItem.hourSquareId = this.hourSquare.value.id;
        commandItem.trappingTypeId = this.trappingType.value.id;
        commandItem.hours = TotalTimeHelper.parseHours(this.time.value);
        commandItem.minutes = TotalTimeHelper.parseMinutes(this.time.value);
        commandItem.status = status ?? this.status.value;

        return commandItem;
    }
}
