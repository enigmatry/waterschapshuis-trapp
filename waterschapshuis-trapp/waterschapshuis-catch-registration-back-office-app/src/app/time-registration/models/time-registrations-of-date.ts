import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import {
    TimeRegistrationsEditCommandItem,
    TimeRegistrationsEditCommandTimeRegistrationsOfDate,
    TimeRegistrationStatus,
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

import { TimeRegistration, TimeRegistrationForm } from './time-registration';

export class TimeRegistrationsOfDate {
    date: Date;
    items: TimeRegistration[] = [];

    constructor(date: Date) {
        this.date = date;
        this.items = [];
    }

    static fromResponse(response: any): TimeRegistrationsOfDate {
        const timeRegistrationGroup = new TimeRegistrationsOfDate(new Date(response.date));
        timeRegistrationGroup.items = response.timeRegistrations.map(TimeRegistration.fromResponse);
        return timeRegistrationGroup;
    }
}

export class TimeRegistrationsOfDateForm {
    date = new FormControl();
    items = new FormArray([]);

    static fromFormGroup(formGroup: FormGroup, status?: TimeRegistrationStatus): TimeRegistrationsOfDateForm {
        const timeRegForm = new TimeRegistrationsOfDateForm();
        timeRegForm.date = formGroup.get('date') as FormControl;
        (formGroup.get('items') as FormArray).controls.forEach(item => {
            const hasEmptyValue = !item.get('catchArea').value || !item.get('subArea').value ||
                !item.get('hourSquare').value || !item.get('trappingType').value ||
                !item.get('time').value;
            if (!hasEmptyValue) {
                if (!item.get('status').value) {
                    item.get('status').setValue(status);
                }
                timeRegForm.items.push(item);
            }
        });
        return timeRegForm;
    }

    buildFormGroup(timeRegistrationsOfDate: TimeRegistrationsOfDate, fb: FormBuilder): FormGroup {
        this.date.setValue(timeRegistrationsOfDate.date);
        if (timeRegistrationsOfDate.items.length > 0) {
            timeRegistrationsOfDate.items
                .forEach(timeRegistration => {
                    this.items.push(timeRegistration.toForm().buildFormGroup(timeRegistrationsOfDate.date, fb));
                });
        } else {
            this.items.push(new TimeRegistrationForm(
                new TimeRegistration(timeRegistrationsOfDate.date)).buildFormGroup(timeRegistrationsOfDate.date, fb));
        }

        return fb.group({
            date: this.date,
            items: this.items
        });
    }

    toCommand(status: TimeRegistrationStatus, statusNotToChange: TimeRegistrationStatus): TimeRegistrationsEditCommandItem {
        const command = new TimeRegistrationsEditCommandTimeRegistrationsOfDate();
        command.date = new Date(Date.parse(this.date.value));
        command.date.setHours(12, 0, 0, 0);
        const commandItems: TimeRegistrationsEditCommandItem[] = [];
        this.items.controls.forEach(itemForm => {
            const formGroup = itemForm as FormGroup;
            if (formGroup) {
                const timeRegistrationForm = TimeRegistrationForm.fromFormGroup(formGroup);
                const statusToSet = timeRegistrationForm.status.value === statusNotToChange ? null : status;
                commandItems.push(timeRegistrationForm.toCommandItem(statusToSet));
            }
        });

        command.items = commandItems;
        return command;
    }
}
