import { Season } from 'src/app/reports/models/season.enum';
import { PredictionData } from 'src/app/reports/models/prediction-data.model';

export class PredictionChartModel {
    static readonly currentYear = (new Date()).getFullYear();
    constructor(
        public season: Season,
        public expected: number,
        public predicted: number,
        public currentYear: number,
        public oneYearAgo: number,
        public twoYearsAgo: number,
        public threeYearsAgo: number) {
        }

    public static CreateCatchesPredictionChartModel(season: Season, dataSource: PredictionData): PredictionChartModel {
        const propertyName = this.translateSeason(season) + 'Catches';
        return new PredictionChartModel(season,
            dataSource?.request[propertyName],
            dataSource?.response?.prediction ? dataSource?.response?.prediction[propertyName] : 0,
            dataSource.response.items.find(x => x.year === this.currentYear)[propertyName],
            dataSource.response.items.find(x => x.year === this.currentYear - 1)[propertyName],
            dataSource.response.items.find(x => x.year === this.currentYear - 2)[propertyName],
            dataSource.response.items.find(x => x.year === this.currentYear - 3)[propertyName]
        );
    }

    public static CreateHoursPredictionChartModel(season: Season, dataSource: PredictionData): PredictionChartModel {
        const propertyName = this.translateSeason(season) + 'Hours';
        return new PredictionChartModel(season,
            dataSource?.response?.prediction ? dataSource?.response?.prediction[propertyName] : 0,
            dataSource?.request[propertyName],
            dataSource.response.items.find(x => x.year === this.currentYear)[propertyName],
            dataSource.response.items.find(x => x.year === this.currentYear - 1)[propertyName],
            dataSource.response.items.find(x => x.year === this.currentYear - 2)[propertyName],
            dataSource.response.items.find(x => x.year === this.currentYear - 3)[propertyName]
        );
    }

    private static translateSeason(season: Season): string {
        switch (season) {
            case Season.Winter:
                return 'winter';
            case Season.Autumn:
                return 'autumn';
            case Season.Summer:
                return 'summer';
            default:
                return 'spring';

        }
    }
}

