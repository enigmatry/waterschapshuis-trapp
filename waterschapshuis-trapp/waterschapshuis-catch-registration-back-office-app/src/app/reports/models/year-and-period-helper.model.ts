export class YearAndPeriodHelper {

    public static getFilterValuesForYearAndPeriod(): any[] {
        const result = [];
        result.push(null);
        const currentDate = new Date();
        const period = this.getPeriod();

        for (let index = 1; index < 14; index++) {
            if (index < period) {
                result.push(currentDate.getFullYear() * 100 + index);
            } else {
                result.push((currentDate.getFullYear() - 1) * 100 + index);
            }
        }
        return result;
    }

    private static getWeekNumber(date: Date): number {
        const firstDayOfYear = new Date(date.getFullYear(), 0, 1);
        const pastDaysOfYear = (date.valueOf() - firstDayOfYear.valueOf()) / 86400000;
        return Math.ceil((pastDaysOfYear + firstDayOfYear.getDay() + 1) / 7);
    }

    public static getPeriod(date: Date = new Date()) {
        return Math.round(YearAndPeriodHelper.getWeekNumber(date) / 4) > 13 ?
            13 : Math.round(YearAndPeriodHelper.getWeekNumber(date) / 4);
    }

}
