import {
    IGetTimeRegistrationsResponseItem,
    TimeRegistrationsEditCommandItem,
    TimeRegistrationStatus
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { IListItem, ListItem } from 'src/app/shared/models/list-item';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import {
    parseHours, parseMinutes,
    resetFormAndClearValidators,
    toDisplayString,
    updateFormValueAndValidity
} from 'src/app/shared/models/utils';

export interface ITimeRegistrationItem {
    id?: string;
    catchArea: IListItem;
    subArea: IListItem;
    hourSquare: IListItem;
    trappingType: IListItem;
    hours: number;
    minutes: number;
    time: string;
    status: TimeRegistrationStatus;

    toForm(): TimeRegistrationItemForm;
}

export class TimeRegistrationItem implements ITimeRegistrationItem {
    id?: string = null;
    catchArea: IListItem = null;
    subArea: IListItem = null;
    hourSquare: IListItem = null;
    trappingType: IListItem = null;
    hours = 0;
    minutes = 0;
    status = TimeRegistrationStatus.Written;

    constructor() { }

    public get time(): string {
        return toDisplayString(this.minutes, this.hours);
    }

    public set time(value: string) {
        this.hours = parseHours(value);
        this.minutes = parseMinutes(value);
    }

    static fromJS(data: any): ITimeRegistrationItem {
        const timeRegItem = new TimeRegistrationItem();
        timeRegItem.catchArea = ListItem.fromNameEntityItem(data.catchArea);
        timeRegItem.subArea = ListItem.fromNameEntityItem(data.subArea);
        timeRegItem.hourSquare = ListItem.fromNameEntityItem(data.hourSquare);
        timeRegItem.trappingType = ListItem.fromNameEntityItem(data.trappingType);
        timeRegItem.hours = parseHours(data.time);
        timeRegItem.minutes = parseMinutes(data.time);
        timeRegItem.status = data.status;

        return timeRegItem;
    }

    static fromResponse(response: IGetTimeRegistrationsResponseItem): ITimeRegistrationItem {
        const timeRegItem = new TimeRegistrationItem();
        timeRegItem.id = response.id;
        timeRegItem.catchArea = ListItem.fromNameEntityItem(response.catchArea);
        timeRegItem.subArea = ListItem.fromNameEntityItem(response.subArea);
        timeRegItem.hourSquare = ListItem.fromNameEntityItem(response.hourSquare);
        timeRegItem.trappingType = ListItem.fromNameEntityItem(response.trappingType);
        timeRegItem.hours = response.hours;
        timeRegItem.minutes = response.minutes;
        timeRegItem.status = response.status;

        return timeRegItem;
    }

    static fromFormGroup(formGroup: FormGroup): ITimeRegistrationItem {
        const timeRegItem = new TimeRegistrationItem();
        timeRegItem.id = formGroup.get('id').value;
        timeRegItem.catchArea = ListItem.fromNameEntityFormControl(formGroup.get('catchArea') as FormControl);
        timeRegItem.subArea = ListItem.fromNameEntityFormControl(formGroup.get('subArea') as FormControl);
        timeRegItem.hourSquare = ListItem.fromNameEntityFormControl(formGroup.get('hourSquare') as FormControl);
        timeRegItem.trappingType = ListItem.fromNameEntityFormControl(formGroup.get('trappingType') as FormControl);
        timeRegItem.hours = parseHours(formGroup.get('time').value);
        timeRegItem.minutes = parseMinutes(formGroup.get('time').value);
        timeRegItem.status = formGroup.get('status').value;
        return timeRegItem;
    }

    toForm(): TimeRegistrationItemForm {
        return new TimeRegistrationItemForm(this);
    }
}

export class TimeRegistrationItemForm {
    id = new FormControl();
    catchArea = new FormControl();
    subArea = new FormControl();
    hourSquare = new FormControl();
    trappingType = new FormControl();
    time = new FormControl();
    status = new FormControl();

    category = new FormControl();
    generalTime = new FormControl();
    generalStatus = new FormControl();

    item: ITimeRegistrationItem;
    formGroup: FormGroup;

    constructor(item: ITimeRegistrationItem) {
        this.item = item;

        this.id.setValue(this.item.id);

        this.catchArea.setValue(this.item.catchArea);
        this.catchArea.setValidators([Validators.required]);

        this.subArea.setValue(this.item.subArea);
        this.subArea.setValidators([Validators.required]);

        this.hourSquare.setValue(this.item.hourSquare);
        this.hourSquare.setValidators([Validators.required]);

        this.trappingType.setValue(this.item.trappingType);
        this.trappingType.setValidators([Validators.required]);

        this.time.setValue(this.item.time);
        this.time.setValidators([Validators.required]);

        this.generalTime.setValue('00:00');
        this.status.setValue(this.item.status);
    }

    static fromFormGroup(formGroup: FormGroup): TimeRegistrationItemForm {
        const timeRegItem = TimeRegistrationItem.fromFormGroup(formGroup);
        return new TimeRegistrationItemForm(timeRegItem);
    }

    static resetTimeRegistrationFormToInitialState(itemFormGroup: FormGroup) {
        resetFormAndClearValidators(itemFormGroup, ['id', 'status']);

        itemFormGroup.get('catchArea').setValidators([Validators.required]);
        itemFormGroup.get('subArea').setValidators([Validators.required]);
        itemFormGroup.get('hourSquare').setValidators([Validators.required]);
        itemFormGroup.get('trappingType').setValidators([Validators.required]);
        itemFormGroup.get('time').setValidators([Validators.required]);

        itemFormGroup.get('time').patchValue('00:00');
        itemFormGroup.get('generalTime').patchValue('00:00');

        updateFormValueAndValidity(itemFormGroup);
    }

    buildFormGroup(fb: FormBuilder): FormGroup {
        this.formGroup = fb.group({
            id: this.id,
            catchArea: this.catchArea,
            subArea: this.subArea,
            hourSquare: this.hourSquare,
            trappingType: this.trappingType,
            time: this.time,
            status: this.status,

            category: this.category,
            generalTime: this.generalTime
        });

        return this.formGroup;
    }

    toCommandItem(): TimeRegistrationsEditCommandItem {
        const commandItem = new TimeRegistrationsEditCommandItem();
        commandItem.id = this.id.value;
        commandItem.subAreaId = this.subArea.value.id;
        commandItem.hourSquareId = this.hourSquare.value.id;
        commandItem.trappingTypeId = this.trappingType.value.id;
        commandItem.hours = parseHours(this.time.value);
        commandItem.minutes = parseMinutes(this.time.value);
        commandItem.status = this.status.value;

        return commandItem;
    }
}
