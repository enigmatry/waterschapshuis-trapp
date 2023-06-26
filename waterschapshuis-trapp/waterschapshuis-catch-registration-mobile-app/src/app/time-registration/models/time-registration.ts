import {
    IGetTimeRegistrationsResponse,
    TimeRegistrationsEditCommand,
    TimeRegistrationsEditCommandItem,
    TimeRegistrationsEditCommandTimeRegistrationsOfDate,
    TimeRegistrationsEditTimeRegistrationGeneral,
    TimeRegistrationStatus
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { TimeRegistrationItem, ITimeRegistrationItem, TimeRegistrationItemForm } from './time-registration-item';
import { FormControl, FormArray, FormBuilder, FormGroup } from '@angular/forms';
import {
    ITimeRegistrationGeneralItem,
    TimeRegistrationGeneralItem,
    TimeRegistrationGeneralItemForm
} from './time-registration-general-item';

export interface ITimeRegistration {
    date: Date;
    items: ITimeRegistrationItem[];
    generalItems: ITimeRegistrationGeneralItem[];
}

export class TimeRegistration implements ITimeRegistration {
    date: Date;
    totalTimeOfFilteredOutItems: number;
    canAddNew: boolean;
    items: ITimeRegistrationItem[] = [];
    generalItems: ITimeRegistrationGeneralItem[] = [];

    constructor(date: Date) {
        this.date = date;
        this.items = [];
        this.generalItems = [];
    }

    static fromResponse(response: IGetTimeRegistrationsResponse): TimeRegistration {
        const timeRegistration = new TimeRegistration(response.date);
        timeRegistration.canAddNew = response.canAddNew;
        timeRegistration.totalTimeOfFilteredOutItems = response.totalTimeOfFilteredOutItems;
        timeRegistration.items =
            response.items
                .map(item =>
                    TimeRegistrationItem.fromResponse(item)
                );
        timeRegistration.generalItems =
            response.generalItems
                .map(generalItem =>
                    TimeRegistrationGeneralItem.fromResponse(generalItem)
                );
        return timeRegistration;
    }
}

export class TimeRegistrationForm {
    date = new FormControl();
    totalTimeOfFilteredOutItems = new FormControl();
    canAddNew = new FormControl();
    items = new FormArray([]);
    generalItems = new FormArray([]);
    inCreation = new FormArray([]);

    timeRegistration: TimeRegistration;
    formGroup: FormGroup;

    constructor() { }

    static fromFormGroup(formGroup: FormGroup): TimeRegistrationForm {
        const timeRegForm = new TimeRegistrationForm();
        timeRegForm.date = formGroup.get('date') as FormControl;
        timeRegForm.items = formGroup.get('items') as FormArray;
        timeRegForm.generalItems = formGroup.get('generalItems') as FormArray;
        timeRegForm.inCreation = formGroup.get('inCreation') as FormArray;
        return timeRegForm;
    }

    buildFormGroup(item: TimeRegistration, fb: FormBuilder): FormGroup {
        this.timeRegistration = item;
        this.date.setValue(this.timeRegistration.date);
        this.canAddNew.setValue(this.timeRegistration.canAddNew);
        this.totalTimeOfFilteredOutItems.setValue(this.timeRegistration.totalTimeOfFilteredOutItems);

        this.timeRegistration.items.forEach(timeRegItem => {
            const itemFormGroup = timeRegItem.toForm().buildFormGroup(fb);
            if (timeRegItem.status === TimeRegistrationStatus.Closed || timeRegItem.status === TimeRegistrationStatus.Completed) {
                itemFormGroup.disable();
            }
            this.items.push(itemFormGroup);
        });

        this.timeRegistration.generalItems.forEach(timeRegGeneralItem => {
            const itemFormGroup = timeRegGeneralItem.toForm().buildFormGroup(fb);
            if (timeRegGeneralItem.status === TimeRegistrationStatus.Closed ||
                timeRegGeneralItem.status === TimeRegistrationStatus.Completed) {
                itemFormGroup.disable();
            }
            this.generalItems.push(itemFormGroup);
        });

        const hasAnyLoadedItem = !!this.timeRegistration.items || !!this.timeRegistration.generalItems;

        this.formGroup = fb.group({
            date: this.date,
            totalTimeOfFilteredOutItems: this.totalTimeOfFilteredOutItems,
            canAddNew: this.canAddNew,
            items: this.items,
            generalItems: this.generalItems,
            inCreation: this.inCreation,
            hasAnyLoadedItem
        });

        return this.formGroup;
    }

    toCommand(): TimeRegistrationsEditCommand {
        const commandOfDate = new TimeRegistrationsEditCommandTimeRegistrationsOfDate();
        commandOfDate.date = new Date(this.date.value);

        const commandItems: TimeRegistrationsEditCommandItem[] = [];
        const commandGeneralItems: TimeRegistrationsEditTimeRegistrationGeneral[] = [];

        this.items.controls.forEach(itemForm => {
            const formGroup = itemForm as FormGroup;
            if (formGroup) {
                commandItems.push(
                    TimeRegistrationItemForm.fromFormGroup(formGroup).toCommandItem()
                );
            }
        });

        this.generalItems.controls.forEach(itemForm => {
            const formGroup = itemForm as FormGroup;
            if (formGroup) {
                commandGeneralItems.push(
                    TimeRegistrationGeneralItemForm.fromFormGroup(formGroup).toCommandItem(commandOfDate.date)
                );
            }
        });

        commandOfDate.items = commandItems;

        const commandItemsOfDate: TimeRegistrationsEditCommandTimeRegistrationsOfDate[] = [];
        commandItemsOfDate.push(commandOfDate);

        const command = new TimeRegistrationsEditCommand();
        command.daysOfWeek = commandItemsOfDate;
        command.timeRegistrationGeneralItems = commandGeneralItems;
        command.startDate = this.date.value;
        command.endDate = new Date(new Date(command.startDate).setDate(command.startDate.getDate() + 1));

        return command;
    }
}
