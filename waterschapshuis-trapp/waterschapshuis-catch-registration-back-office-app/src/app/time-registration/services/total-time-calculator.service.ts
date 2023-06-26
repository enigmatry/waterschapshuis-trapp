import { Injectable } from '@angular/core';
import { FormArray } from '@angular/forms';
import { CatchRegistration } from '../models/catch-registration';
import { TotalTimeHelper } from '../models/registered-time-helper';

@Injectable({
    providedIn: 'root'
})
export class TotalTimeCalculatorService {

    public calculateTimeRegistrationTotalTime(timeRegistrationData: FormArray): number {
        return this.calculateTotalTime(timeRegistrationData.getRawValue().flatMap(tr => tr.items));
    }
    public calculateTimeRegistrationGeneralTotalTime(timeRegistrationGeneralData: FormArray, timeRegistrationData: FormArray): number {
        return this.calculateTotalTime(timeRegistrationGeneralData.getRawValue()) +
            this.calculateTimeRegistrationTotalTime(timeRegistrationData);
    }

    public timeRegistrationContainsTime(timeRegistrationData: FormArray): boolean {
        return this.containsTime(timeRegistrationData.getRawValue().flatMap(tr => tr.items));
    }

    public timeRegistrationGeneralContainsTime(timeRegistrationGeneralData: FormArray, timeRegistrationData: FormArray): boolean {
        return this.containsTime(timeRegistrationGeneralData.getRawValue()) ||
            this.timeRegistrationContainsTime(timeRegistrationData);
    }

    public calculateTotalCatchRegistrations(catchesData: CatchRegistration[]): number {
        return catchesData.filter(x => !x.isByCatch)
            .map(x => x.number)
            .reduce((a, b) => a + b, 0);
    }

    private containsTime(items: any[]): boolean {
        return !!this.filterByTimeExisting(items).length;
    }

    private calculateTotalTime(items: any[]): number {
        const itemsWithTimeRegistered = this.filterByTimeExisting(items);
        return TotalTimeHelper.calculateTotalTimeForRow(itemsWithTimeRegistered);
    }

    private filterByTimeExisting(items: any[]) {
        return items.filter(tr => tr.time !== null);
    }
}
