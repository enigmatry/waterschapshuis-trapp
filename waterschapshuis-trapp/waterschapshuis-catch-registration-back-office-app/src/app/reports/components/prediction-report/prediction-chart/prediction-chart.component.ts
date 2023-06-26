import { Component, OnInit, Input, AfterViewInit, OnDestroy } from '@angular/core';
import dxChart, { dxChartOptions } from 'devextreme/viz/chart';
import { PredictionChartModel } from './prediction-chart.model';
import { Observable, Subscription } from 'rxjs';

const nameOf = <T>(name: keyof T) => name;


@Component({
  selector: 'app-prediction-chart',
  templateUrl: './prediction-chart.component.html',
  styleUrls: ['./prediction-chart.component.scss']
})
export class PredictionChartComponent implements OnInit, AfterViewInit, OnDestroy {

  @Input() chartId: string;
  @Input() chartDataSource: Observable<PredictionChartModel[]>;
  @Input() chartTitle: string;
  @Input() chartExpectedTitle: string;
  @Input() chartPredictedTitle: string;

  private dataSourceSubscription: Subscription;

  private chart: dxChart;

  private currentYear = (new Date()).getFullYear();

  constructor() { }
  ngOnDestroy(): void {
    this.dataSourceSubscription?.unsubscribe();
  }

  ngAfterViewInit(): void {
    this.init();
  }

  ngOnInit() {
  }

  private init() {
    this.chart = new dxChart(document.getElementById(this.chartId), this.getChartOptions());
    this.dataSourceSubscription = this.chartDataSource
      .subscribe(data => this.chart.option('dataSource', data));
  }

  private getChartOptions = (): dxChartOptions => ({
    dataSource: new Array<PredictionChartModel[]>(),
    legend: { horizontalAlignment: 'center', verticalAlignment: 'bottom' },
    argumentAxis: {
        valueMarginsEnabled: false,
        discreteAxisDivisionMode: 'crossLabels',
        grid: { visible: true }
    },
    tooltip: { enabled: true },
    export: { enabled: true, fileName: 'prognose' },
    series: [
      {
        valueField: nameOf<PredictionChartModel>('expected'),
        name: this.chartExpectedTitle,
        color: '#FFC733'
      },
      {
        valueField: nameOf<PredictionChartModel>('predicted'),
        name: this.chartPredictedTitle,
        color: '#f50c0c'
      },
      {
        valueField: nameOf<PredictionChartModel>('currentYear'),
        name: this.currentYear.toString(),
        color: '#a0522d'
      },
      {
        valueField: nameOf<PredictionChartModel>('oneYearAgo'),
        name: (this.currentYear - 1).toString(),
        color: '#0a3df5'
      },
      {
        valueField: nameOf<PredictionChartModel>('twoYearsAgo'),
        name: (this.currentYear - 2).toString(),
        color: '#4ef542'
      },
      {
        valueField: nameOf<PredictionChartModel>('threeYearsAgo'),
        name: (this.currentYear - 3).toString(),
        color: '#c542f5'
      }
    ],
    commonSeriesSettings: {
        type: 'line',
        argumentField: nameOf<PredictionChartModel>('season')
    },
    title: {
      text: this.chartTitle
    }
  })
}
