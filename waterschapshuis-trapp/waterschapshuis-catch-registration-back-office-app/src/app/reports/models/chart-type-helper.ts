
import { ChartType } from './chart-type';

export class ChartTypeHelper {
    public static availableChartTypes: ChartType[] = [
        { name: 'Lijngrafiek', value: 'line', number: 1 },
        { name: 'Staafdiagram', value: 'bar', number: 2 },
        { name: 'Spreidingsdiagram', value: 'scatter', number: 3 }
      ];

    public static getChartTypeNumberByChartName = (chartName: string): number => {
        return ChartTypeHelper.availableChartTypes.find(x => x.value === chartName).number;
    }

    public static getChartTypeByChartNumber = (value: number): ChartType => {
        return value === 0 ? ChartTypeHelper.availableChartTypes[0] : ChartTypeHelper.availableChartTypes.find(x => x.number === value);
    }
}
