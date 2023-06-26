import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { PredictionTableCell, PredictionTableModel } from './prediction-table.model';
import { Observable, Subscription } from 'rxjs';
import { PredictionReportSeason } from '../../../models/prediction-report-season.model';

const nameOf = <T>(name: keyof T) => name;

@Component({
  selector: 'app-prediction-table',
  templateUrl: './prediction-table.component.html',
  styleUrls: ['./prediction-table.component.scss']
})
export class PredictionTableComponent implements OnInit, OnDestroy {

  @Input() tableDataSource: Observable<PredictionTableModel>;
  cells: PredictionTableCell[] = [];
  accuracy: number;

  private dataSourceSubscription: Subscription;
  private seasons: PredictionReportSeason[] = [];
  constructor() { }

  ngOnDestroy(): void {
    this.dataSourceSubscription?.unsubscribe();
  }

  ngOnInit() {
    this.seasons = PredictionReportSeason.getSeasonsOrderedStartingWithCurrent();
    this.dataSourceSubscription = this.tableDataSource
      .subscribe(data => {
        this.cells = data.cells;
        this.accuracy = data.accuracy;
      });
  }

  getDisplayedTableColumns() {
    const result = ['title'];
    this.seasons.forEach(s => {
      result.push(s.seasonEnName);
    });
    return result;
  }
}
