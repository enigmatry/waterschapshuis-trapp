import { Season } from './season.enum';
import { YearAndPeriodHelper } from './year-and-period-helper.model';
import { cloneDeep } from 'lodash';
export class SeasonPeriod {
    static seasonsAndPeriods =
        [{ season: 1, periods: [13, 1, 2, 3], name: Season.Winter },
        { season: 2, periods: [4, 5, 6], name: Season.Spring },
        { season: 3, periods: [7, 8, 9], name: Season.Summer },
        { season: 4, periods: [10, 11, 12], name: Season.Autumn }

        ];

    public static getSeasonByNumber(seasonNumber: number) {
        return this.seasonsAndPeriods.find(x => x.season === seasonNumber);
    }

    public static getSeasonForPeriod(period: number): number {
        return this.seasonsAndPeriods.find(x => x.periods.indexOf(period) > -1).season;
    }

    public static getSeasonsOrderedStartingWithCurrent() {
        const seasons = cloneDeep(SeasonPeriod.seasonsAndPeriods);
        const currentSeason = SeasonPeriod.getSeasonForPeriod(YearAndPeriodHelper.getPeriod());
        for (let i = 0; i < currentSeason - 1; i++) {
            seasons.push(seasons.shift());
        }
        return seasons;
    }
}
