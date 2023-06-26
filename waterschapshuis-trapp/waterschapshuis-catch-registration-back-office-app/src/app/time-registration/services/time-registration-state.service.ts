import { Injectable } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { map } from 'rxjs/operators';
import { IListItem } from 'src/app/shared/models/list-item';
import { distinct } from 'src/app/shared/models/utils';
import { AreasService } from 'src/app/shared/services/areas.service';
import { CatchRegistration } from '../models/catch-registration';
import { TimeRegistrationsOfDate } from '../models/time-registrations-of-date';
import { TimeRegistrationsOfWeekForm } from '../models/time-registrations-of-week-form';
import { WeekDateRange } from '../models/week-date-range';
import { TimeRegistrationService } from './time-registration.service';
import { DatePipe } from '@angular/common';
import { TimeRegistration, TimeRegistrationResponse } from '../models/time-registration';
import { CatchStatus, TimeRegistrationStatus } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { TimeRegistrationGeneral, TimeRegistrationGeneralForm } from '../models/time-registration-general';
import { Observable } from 'rxjs';


class ListItemsDictionaryItem {
    constructor(public id: string, public items: IListItem[]) { }
}

@Injectable({
    providedIn: 'root'
})
export class TimeRegistrationStateService {
    subAreasDictionary = new Array<ListItemsDictionaryItem>();
    hourSquaresDictionary = new Array<ListItemsDictionaryItem>();
    hasChanges = false;

    constructor(
        private timeRegistrationService: TimeRegistrationService,
        private areasService: AreasService,
        private fb: FormBuilder,
        private datePipe: DatePipe) { }

    loadTimeRegistration(weekDateRange: WeekDateRange, weekDays: Date[]): Observable<TimeRegistrationResponse> {
        return this.timeRegistrationService
            .fetchTimeRegistrations(weekDateRange)
            .pipe(map(response => {
                const daysOfWeek = response.daysOfWeek.map(TimeRegistrationsOfDate.fromResponse);
                const timeRegistrationGeneralItems =
                    response.timeRegistrationGeneralItems.map(i => TimeRegistrationGeneral.fromResponse(i));
                const catches = response.catches.map(CatchRegistration.fromResponse);
                return new TimeRegistrationResponse(
                    this.buildFormFromResult(daysOfWeek, timeRegistrationGeneralItems, weekDateRange, weekDays),
                    catches
                );
            }));
    }

    loadTimeRegistrationForUser(weekDateRange: WeekDateRange, userId: string, rayonId: string, weekDays: Date[]) {
        return this.timeRegistrationService
            .fetchTimeRegistrationsForUser(weekDateRange, userId, rayonId)
            .pipe(map(response => {
                const daysOfWeek = response.daysOfWeek.map(TimeRegistrationsOfDate.fromResponse);
                const timeRegistrationGeneralItems =
                    response.timeRegistrationGeneralItems.map(i => TimeRegistrationGeneral.fromResponse(i));
                const catches = response.catches.map(CatchRegistration.fromResponse);

                return new TimeRegistrationResponse(
                    this.buildFormFromResult(daysOfWeek, timeRegistrationGeneralItems, weekDateRange, weekDays),
                    catches
                );
            }));
    }

    createGeneralItemsFormGroup(generalTimeRegistrations: TimeRegistrationGeneral[]): any {
        return this.fb.group({
            generalItems: this.fb.array(generalTimeRegistrations.map(i =>
                new TimeRegistrationGeneralForm(i).buildFormGroup(i.date, this.fb)))
        });
    }

    fillGeneralTimeRegistrationForMissingDays(generalTimeRegistrations: TimeRegistrationGeneral[], daysOfWeek: Date[]):
        TimeRegistrationGeneral[] {
        daysOfWeek.forEach(d => {
            const hasItemsForDate = generalTimeRegistrations
                .some(x => this.datePipe.transform(x.date, 'yyyy-MM-dd') === this.datePipe.transform(d, 'yyyy-MM-dd'));
            if (!hasItemsForDate) {
                generalTimeRegistrations.push(TimeRegistrationGeneral.createWithDateOnly(d));
            }
        });
        return generalTimeRegistrations;
    }

    buildFormFromResult(
        timeRegistrations: TimeRegistrationsOfDate[],
        timeRegistrationGeneralItems: TimeRegistrationGeneral[],
        weekDateRange: WeekDateRange,
        weekDays: Date[]): FormGroup {
        const timeRegistrationsForm = this.fillTimeRegistrationsForWeek(timeRegistrations, weekDateRange);
        const filledTimeRegistrationGeneral = this.fillGeneralTimeRegistrationForMissingDays(timeRegistrationGeneralItems, weekDays);
        return this.fb.group({
            timeRegistrationsForm: new TimeRegistrationsOfWeekForm().buildFormGroup(timeRegistrationsForm, this.fb),
            timeRegistrationGeneralForm: this.createGeneralItemsFormGroup(filledTimeRegistrationGeneral)
        });
    }

    fillTimeRegistrationsForWeek(result: TimeRegistrationsOfDate[], weekDateRange: WeekDateRange): TimeRegistrationsOfDate[] {
        for (const dayOfWeek = new Date(weekDateRange.startDate); dayOfWeek < new Date(weekDateRange.endDate);
            dayOfWeek.setDate(dayOfWeek.getDate() + 1)) {
            const newDate = new Date(dayOfWeek);
            const anyRecordOnDate = result.some(x => this.datePipe.transform(x.date, 'yyyy-MM-dd') === this.datePipe.transform(newDate, 'yyyy-MM-dd'));
            if (!anyRecordOnDate) {
                result.push(new TimeRegistrationsOfDate(new Date(dayOfWeek)));
            }
        }

        return result.sort((a, b) => b.date.valueOf() - a.date.valueOf()).reverse();
    }

    initDictionaries = (catchAreaIds: string[], subAreaIds: string[]) => {
        catchAreaIds.filter(distinct).filter(x => x).forEach(id => this.loadSubAreas(id));
        subAreaIds.filter(distinct).filter(x => x).forEach(id => this.loadHourSquares(id));
    }

    loadSubAreas = (catchAreaId: string) => {
        if (this.getSubAreas(catchAreaId)) { return; }

        return this.areasService
            .getSubAreas(catchAreaId)
            .subscribe(subAreas => {
                this.subAreasDictionary.push(new ListItemsDictionaryItem(catchAreaId, subAreas));
            });
    }

    loadHourSquares = (subAreaId: string) => {
        if (this.getHourSquares(subAreaId)) { return; }

        this.areasService
            .getHourSquares(subAreaId)
            .subscribe(hourSquares => {
                this.hourSquaresDictionary.push(new ListItemsDictionaryItem(subAreaId, hourSquares));
            });
    }

    getSubAreas = (catchAreaId: string): IListItem[] =>
        catchAreaId ? this.subAreasDictionary.find(x => x.id === catchAreaId)?.items : []

    getHourSquares = (subAreaId: string): IListItem[] =>
        subAreaId ? this.hourSquaresDictionary.find(x => x.id === subAreaId)?.items : []

    getTimeRegistrations = (timeRegistrationItems: FormArray): Array<TimeRegistration> => {
        const items: TimeRegistration[] = [];

        if (!!timeRegistrationItems) {
            timeRegistrationItems.value.forEach(element => {
                if (element.items) {
                    element.items.forEach((item: TimeRegistration) => {
                        items.push(item);
                    });
                }
            });
        }

        return items;
    }

    anyNewEnteredOnEitherForm = (timeRegistrationItems: FormArray, timeRegistrationGeneralItems: FormArray): boolean => {
        return this.getTimeRegistrations(timeRegistrationItems)
            .some((item: TimeRegistration) =>
                item.id === null &&
                item.catchArea !== null &&
                item.subArea !== null &&
                item.hourSquare !== null &&
                item.trappingType !== null &&
                item.time !== null) ||
            timeRegistrationGeneralItems?.value
                .some((item: TimeRegistrationGeneral) =>
                    item.id === null &&
                    item.categoryId !== null &&
                    item.time !== null);
    }

    anyWithStatus = (timeRegistrationItems: FormArray, status: TimeRegistrationStatus) => {
        return this.getTimeRegistrations(timeRegistrationItems)
            .some((item: TimeRegistration) =>
                item.status === status
            );
    }

    allWithStatus = (timeRegistrationItems: FormArray, status: TimeRegistrationStatus) => {
        return this.getTimeRegistrations(timeRegistrationItems)
            .every((item: TimeRegistration) =>
                item.status === status
            );
    }
    allTimeRegistrationsWithStatusNull = (timeRegistrationItems: FormArray) => {
        return this.allWithStatus(timeRegistrationItems, null);
    }
    allTimeRegistrationGeneralsWithStatusNull = (timeRegistrationGeneralItems: FormArray) => {
        return timeRegistrationGeneralItems?.controls
            .every(item => item.value.status === null);
    }
    allWithStatusClosed = (timeRegistrationItems: FormArray) => {
        return this.allWithStatus(timeRegistrationItems, TimeRegistrationStatus.Closed);
    }

    anyWithStatusClosed = (timeRegistrationItems: FormArray) => {
        return this.anyWithStatus(timeRegistrationItems, TimeRegistrationStatus.Closed);
    }
    anyTimeRegistrationWithStatusWritten = (timeRegistrationItems: FormArray) => {
        return this.anyWithStatus(timeRegistrationItems, TimeRegistrationStatus.Written);
    }
    anyTimeRegistrationWithStatusClosed = (timeRegistrationItems: FormArray) => {
        return this.anyWithStatus(timeRegistrationItems, TimeRegistrationStatus.Closed);
    }
    anyTimeRegistrationGeneralsWithStatusWritten = (timeRegistrationGeneralItems: FormArray) => {
        return timeRegistrationGeneralItems?.controls
            .some(item => item.value.status === TimeRegistrationStatus.Written);
    }
    anyTimeRegistrationGeneralsWithStatusClosed = (timeRegistrationGeneralItems: FormArray) => {
        return timeRegistrationGeneralItems?.controls
            .some(item => item.value.status === TimeRegistrationStatus.Closed);
    }
    anyCatchRegistrationWithStatusWritten = (catchRegistrationItems: CatchRegistration[]) => {
        return catchRegistrationItems?.some(item => item.status === CatchStatus.Written);
    }
    anyCatchRegistrationWithStatusClosed = (catchRegistrationItems: CatchRegistration[]) => {
        return catchRegistrationItems?.some(item => item.status === CatchStatus.Closed);
    }
}
