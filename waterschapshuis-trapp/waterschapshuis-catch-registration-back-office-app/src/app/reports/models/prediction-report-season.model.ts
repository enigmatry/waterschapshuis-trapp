import { cloneDeep } from 'lodash';
import { SeasonPeriod } from 'src/app/reports/models/season-period-model';
import { YearAndPeriodHelper } from 'src/app/reports/models/year-and-period-helper.model';

export class PredictionReportSeason {
    constructor(public season: number,
                public seasonAbbreviation: string,
                public expectedHoursFormControlName: string,
                public expectedCatchesFormControlName: string,
                public seasonEnName: string) {
    }
    private static predictionFilterSeasonList = [
        new PredictionReportSeason(1, 'W:', 'expectedWinterHours', 'expectedWinterCatches', 'winter'),
        new PredictionReportSeason(2, 'L:', 'expectedSpringHours', 'expectedSpringCatches', 'spring'),
        new PredictionReportSeason(3, 'Z:', 'expectedSummerHours', 'expectedSummerCatches', 'summer'),
        new PredictionReportSeason(4, 'H:', 'expectedAutumnHours', 'expectedAutumnCatches', 'autumn')];



    public static getSeasonsOrderedStartingWithCurrent() {
        const seasons = cloneDeep(PredictionReportSeason.predictionFilterSeasonList);
        const currentSeason = SeasonPeriod.getSeasonForPeriod(YearAndPeriodHelper.getPeriod());
        for (let i = 0; i < currentSeason - 1; i++) {
            seasons.push(seasons.shift());
        }
        return seasons;
    }
}
