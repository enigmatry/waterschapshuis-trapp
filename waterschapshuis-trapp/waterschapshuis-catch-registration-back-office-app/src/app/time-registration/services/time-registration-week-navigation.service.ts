import { Injectable } from '@angular/core';
import { TimeRegistrationDateService } from './time-registration-date.service';
import { BehaviorSubject } from 'rxjs';
import { WeekNavigationData } from '../models/week-navigation-data';
import { WeekDateRange } from '../models/week-date-range';
import { WeekDirection } from '../models/week-direction.enum';

@Injectable({
    providedIn: 'root'
})
export class TimeRegistrationWeekNavigationService {

    private selectedYearAndWeekNumberSubject: BehaviorSubject<WeekNavigationData> = new BehaviorSubject<WeekNavigationData>(
        this.timeRegistrationDateService.getWeekYearNavigationData(WeekDirection.none));

    selectedYearAndWeekNumber$ = this.selectedYearAndWeekNumberSubject.asObservable();

    constructor(private timeRegistrationDateService: TimeRegistrationDateService) { }

    changeWeekNumberAndYear(direction: WeekDirection) {
        const result = this.timeRegistrationDateService.getWeekYearNavigationData(direction);
        this.setWeekNumberAndYear(result);
    }

    setWeekNumberAndYear(weekDateRange: WeekNavigationData) {
        this.selectedYearAndWeekNumberSubject.next(weekDateRange);
    }

    getStartAndEndDateOfWeek(selectedYear: number, selectedWeekNumber: number): WeekDateRange {
        const isoDate = new Date(this.timeRegistrationDateService.getISODateStringOfWeek(selectedWeekNumber, selectedYear));

        const startDate = this.timeRegistrationDateService.getStartDateOfWeek(isoDate);
        const endDate = this.timeRegistrationDateService.getEndDateOfWeek(startDate);
        return new WeekDateRange(startDate, endDate);
    }

    getDaysOfWeek(startDate: Date, endDate: Date): any[] {
        const daysOfWeek = [];
        for (const dayOfWeek = new Date(startDate); dayOfWeek < endDate; dayOfWeek.setDate(dayOfWeek.getDate() + 1)) {
            daysOfWeek.push(new Date(dayOfWeek));
        }
        return daysOfWeek;
    }
}
