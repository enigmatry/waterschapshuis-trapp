import { Injectable } from '@angular/core';
import { WeekDirection } from '../models/week-direction.enum';
import { WeekNavigationData } from '../models/week-navigation-data';

@Injectable({
    providedIn: 'root'
})
export class TimeRegistrationDateService {

    selectedWeekDate: Date = new Date(new Date().setDate(new Date().getDate() - 7));
    readonly millisecondsOneDay = 24 * 60 * 60 * 1000;

    getISOWeekNavigationData(inputDate: Date = new Date()): WeekNavigationData {
        const date = new Date(inputDate.valueOf());
        date.setHours(0, 0, 0, 0);
        // Thursday in current week decides the year.
        date.setDate(date.getDate() + 3 - (date.getDay() + 6) % 7);
        // January 4 is always in week 1.
        const week1 = new Date(date.getFullYear(), 0, 4);
        // Adjust to Thursday in week 1 and count number of weeks from date to week1.
        return {
            currentWeekNumber: 1 + Math.round(((date.getTime() - week1.getTime()) / 86400000
                - 3 + (week1.getDay() + 6) % 7) / 7),
            currentYear: date.getFullYear()
        };
    }

    setSelectedWeekDate(selectedDate: Date): void {
        this.selectedWeekDate = selectedDate;
    }

    getWeekYearNavigationData(direction: WeekDirection): any {
        this.selectedWeekDate.setDate(this.selectedWeekDate.getDate() + (direction * 7));
        return this.getISOWeekNavigationData(this.selectedWeekDate);
    }

    getISODateStringOfWeek(weekNumber: number, year: number): string {
        const date = new Date(year, 0, 1 + (weekNumber - 1) * 7, 12, 12, 12);
        if (date.getDay() <= 4) {
            date.setDate(date.getDate() - date.getDay() + 1);
        } else {
            date.setDate(date.getDate() + 8 - date.getDay());
        }
        return date.toISOString().substring(0, 10);
    }

    getStartDateOfWeek(isoDate: Date): Date {
        return new Date(isoDate.getTime() - (isoDate.getDay() - 1) * this.millisecondsOneDay);
    }

    getEndDateOfWeek(monday: Date): Date {
        return new Date(monday.getTime() + 7 * this.millisecondsOneDay);
    }
}
