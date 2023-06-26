import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import {
    TimeRegistrationStatus,
    IGetTimeRegistrationsResponseGeneralItem,
    TimeRegistrationsEditCommandItem,
    TimeRegistrationsEditTimeRegistrationGeneral
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { IListItem, ListItem } from 'src/app/shared/models/list-item';
import {
    parseHours,
    parseMinutes,
    resetFormAndClearValidators,
    toDisplayString,
    updateFormValueAndValidity
} from 'src/app/shared/models/utils';

export interface ITimeRegistrationGeneralItem {
    id?: string;
    category: IListItem;
    hours: number;
    minutes: number;
    generalTime: string;
    status: TimeRegistrationStatus;

    toForm(): TimeRegistrationGeneralItemForm;
}

export class TimeRegistrationGeneralItem implements ITimeRegistrationGeneralItem {
    id?: string = null;
    category: IListItem = null;
    hours = 0;
    minutes = 0;
    status = TimeRegistrationStatus.Written;

    constructor() { }

    public get generalTime(): string {
        return toDisplayString(this.minutes, this.hours);
    }

    public set generalTime(value: string) {
        this.hours = parseHours(value);
        this.minutes = parseMinutes(value);
    }

    static fromJS(data: any): ITimeRegistrationGeneralItem {


        const timeRegGenItem = new TimeRegistrationGeneralItem();
        timeRegGenItem.category = ListItem.fromNameEntityItem(data.category);
        timeRegGenItem.hours = parseHours(data.generalTime);
        timeRegGenItem.minutes = parseMinutes(data.generalTime);
        timeRegGenItem.status = data.status;

        return timeRegGenItem;
    }

    static fromResponse(response: IGetTimeRegistrationsResponseGeneralItem): ITimeRegistrationGeneralItem {
        const timeRegGenItem = new TimeRegistrationGeneralItem();
        timeRegGenItem.id = response.id;
        timeRegGenItem.category = ListItem.fromNameEntityItem(response.category);
        timeRegGenItem.hours = response.hours;
        timeRegGenItem.minutes = response.minutes;
        timeRegGenItem.status = response.status;

        return timeRegGenItem;
    }

    static fromFormGroup(formGroup: FormGroup): ITimeRegistrationGeneralItem {
        const timeRegItem = new TimeRegistrationGeneralItem();
        timeRegItem.id = formGroup.get('id').value;
        timeRegItem.category = ListItem.fromNameEntityFormControl(formGroup.get('category') as FormControl);
        timeRegItem.hours = parseHours(formGroup.get('generalTime').value);
        timeRegItem.minutes = parseMinutes(formGroup.get('generalTime').value);
        timeRegItem.status = formGroup.get('status').value;
        return timeRegItem;
    }

    toForm(): TimeRegistrationGeneralItemForm {
        return new TimeRegistrationGeneralItemForm(this);
    }
}

export class TimeRegistrationGeneralItemForm {
    id = new FormControl();
    category = new FormControl();
    generalTime = new FormControl();
    status = new FormControl();

    item: ITimeRegistrationGeneralItem;
    formGroup: FormGroup;

    constructor(item: ITimeRegistrationGeneralItem) {
        this.item = item;

        this.id.setValue(this.item.id);

        this.category.setValue(this.item.category);
        this.category.setValidators([Validators.required]);

        this.generalTime.setValue(this.item.generalTime);
        this.generalTime.setValidators([Validators.required]);

        this.status.setValue(this.item.status);
    }

    static fromFormGroup(formGroup: FormGroup): TimeRegistrationGeneralItemForm {
        const timeRegItem = TimeRegistrationGeneralItem.fromFormGroup(formGroup);
        return new TimeRegistrationGeneralItemForm(timeRegItem);
    }

    static resetTimeRegistrationGeneralFormToInitialState(itemFormGroup: FormGroup) {
        resetFormAndClearValidators(itemFormGroup, ['id', 'status']);
        itemFormGroup.get('category').setValidators([Validators.required]);
        itemFormGroup.get('generalTime').setValidators([Validators.required]);

        itemFormGroup.get('generalTime').patchValue('00:00');
        itemFormGroup.get('time').patchValue('00:00');

        updateFormValueAndValidity(itemFormGroup);
    }

    buildFormGroup(fb: FormBuilder): FormGroup {
        this.formGroup = fb.group({
            id: this.id,
            category: this.category,
            generalTime: this.generalTime,
            status: this.status
        });

        return this.formGroup;
    }


    toCommandItem(date: Date): TimeRegistrationsEditCommandItem {
        const commandItem = new TimeRegistrationsEditTimeRegistrationGeneral();
        commandItem.id = this.id.value;
        commandItem.categoryId = this.category.value.id;
        commandItem.date = new Date(date);
        commandItem.date.setHours(12, 0, 0, 0);
        commandItem.hours = parseHours(this.generalTime.value);
        commandItem.minutes = parseMinutes(this.generalTime.value);
        commandItem.status = this.status.value;

        return commandItem;
    }
}
