import {
    TimeRegistrationsEditCommand,
    TimeRegistrationsEditCommandTimeRegistrationsOfDate,
    TimeRegistrationStatus
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { TimeRegistrationsOfDate, TimeRegistrationsOfDateForm } from './time-registrations-of-date';
import { WeekDateRange } from './week-date-range';

export class TimeRegistrationsOfWeekForm {
    daysOfWeek = new FormArray([]);

    static fromFormGroup(formGroup: FormGroup): TimeRegistrationsOfWeekForm {
        const timeRegForm = new TimeRegistrationsOfWeekForm();
        timeRegForm.daysOfWeek = formGroup.get('daysOfWeek') as FormArray;
        return timeRegForm;
    }

    buildFormGroup(timeRegistrationsOfWeek: TimeRegistrationsOfDate[], fb: FormBuilder): FormGroup {
        timeRegistrationsOfWeek.forEach(timeRegistrationsOfDate => {
            this.daysOfWeek.push(new TimeRegistrationsOfDateForm().buildFormGroup(timeRegistrationsOfDate, fb));
        });
        return fb.group({ daysOfWeek: this.daysOfWeek });
    }

    toCommand(
        dateRange: WeekDateRange,
        status?: TimeRegistrationStatus,
        statusNotToChange?: TimeRegistrationStatus): TimeRegistrationsEditCommand {
        const commandItems: TimeRegistrationsEditCommandTimeRegistrationsOfDate[] = [];
        this.daysOfWeek.controls.forEach(itemForm => {
            const formGroup = itemForm as FormGroup;
            if (formGroup) {
                const timeRegistrationsOfDateForm = TimeRegistrationsOfDateForm.fromFormGroup(formGroup, status);
                if (timeRegistrationsOfDateForm.items.length > 0) {
                    commandItems.push(timeRegistrationsOfDateForm.toCommand(status, statusNotToChange));
                }
            }
        });

        const command = new TimeRegistrationsEditCommand();
        command.daysOfWeek = commandItems;

        command.startDate = dateRange.startDate;
        command.endDate = dateRange.endDate;

        return command;
    }
}
