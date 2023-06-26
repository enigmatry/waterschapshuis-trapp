import { Injectable } from '@angular/core';
import { ReportService } from './report.service';
import { PredictionRequest } from '../models/prediction-request.model';
import { PredictionData } from '../models/prediction-data.model';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { ChartType } from '../models/chart-type';
import { TrackingLineFilter } from '../models/tracking-line-filter.model';
import { startWith } from 'rxjs/operators';
import { ChartTypeHelper } from '../models/chart-type-helper';

@Injectable({
  providedIn: 'root'
})
export class SideBarFilterService {
  private hourSquarePrediction$ = new Subject<PredictionData>();
  private chartType$ = new BehaviorSubject<ChartType>(ChartTypeHelper.availableChartTypes[0]);
  private trackingLineFilter$ = new Subject<TrackingLineFilter>();

  private mapRenderedSubject = new Subject<boolean>();
  mapRendered = this.mapRenderedSubject.asObservable();

  constructor(private reportsService: ReportService) { }

  calculateHourSquarePredictions(request: PredictionRequest) {
    this.reportsService.getPrediction(request)
      .subscribe(response => this.hourSquarePrediction$.next(
        new PredictionData(request, response)));
  }

  get hourSquarePredictions(): Observable<PredictionData> {
    return this.hourSquarePrediction$.asObservable();
  }

  changeChartType(type: ChartType) {
    this.chartType$.next(type);
  }

  get chartType() {
    return this.chartType$.asObservable();
  }

  updateMapRendered(value: boolean): void {
    this.mapRenderedSubject.next(value);
  }

  filterTrackingLines(filter: TrackingLineFilter) {
    this.trackingLineFilter$.next(filter);
  }

  get trackingLineFilter() {
    return this.trackingLineFilter$.asObservable()
      .pipe(startWith(this.trackingLineFilterStart));
  }

  get trackingLineFilterStart() {
    const end = new Date();
    const start = new Date();
    start.setMonth(end.getMonth() - 6);
    return new TrackingLineFilter(start, end);
  }
}

