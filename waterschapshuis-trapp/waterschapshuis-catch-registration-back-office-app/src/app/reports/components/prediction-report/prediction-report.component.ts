import { Component, OnInit, OnDestroy } from '@angular/core';
import { ReportBaseComponent } from '../report-base.component';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { PredictionData } from '../../models/prediction-data.model';
import { SideBarFilterService } from '../../services/side-bar-filter.service';
import { PredictionChartModel } from './prediction-chart/prediction-chart.model';
import { PredictionTableCell, PredictionTableModel } from './prediction-table/prediction-table.model';
import { map } from 'rxjs/operators';
import { SeasonPeriod } from '../../models/season-period-model';


@Component({
    selector: 'app-prediction-report',
    templateUrl: './prediction-report.component.html'
})
export class PredictionReportComponent extends ReportBaseComponent
    implements OnInit, OnDestroy {

    constructor(
        sideBarService: SideBarService,
        private sideBarFilterService: SideBarFilterService) {
        super(sideBarService);
    }

    ngOnInit() {
        super.ngOnInit();
    }

    get catchesChartDataSource() {
        return this.sideBarFilterService.hourSquarePredictions.pipe(
            map(this.mapCatchChartDataSource));
    }

    get hoursChartDataSource() {
        return this.sideBarFilterService.hourSquarePredictions.pipe(
            map(this.mapHourChartDataSource));
    }

    get tableDataSource() {
        return this.sideBarFilterService.hourSquarePredictions.pipe(
            map(data => {
                const model = new PredictionTableModel();
                model.accuracy = data.response.modelQuality;
                model.cells = this.mapTableDataCells(data);
                return model;
            }));
    }

    ngOnDestroy() {
        super.ngOnDestroy();
    }

    mapCatchChartDataSource(dataSource: PredictionData): PredictionChartModel[] {
        const catchChartData = [];
        SeasonPeriod.getSeasonsOrderedStartingWithCurrent().forEach(season => {
            catchChartData.push(PredictionChartModel.CreateCatchesPredictionChartModel(season.name, dataSource));
        });
        return catchChartData;
    }

    mapHourChartDataSource(dataSource: PredictionData): PredictionChartModel[] {
        const hoursChartData = [];
        SeasonPeriod.getSeasonsOrderedStartingWithCurrent().forEach(season => {
            hoursChartData.push(PredictionChartModel.CreateHoursPredictionChartModel(season.name, dataSource));
        });
        return hoursChartData;
    }

    mapTableDataCells = (dataSource: PredictionData): PredictionTableCell[] => [
        new PredictionTableCell('Geplande uren',
            dataSource?.request?.winterHours,
            dataSource?.request?.springHours,
            dataSource?.request?.summerHours,
            dataSource?.request?.autumnHours),
        new PredictionTableCell('Voorspelde vangst',
            dataSource?.response?.prediction ? dataSource?.response?.prediction.winterCatches : 0,
            dataSource?.response?.prediction ? dataSource?.response?.prediction.springCatches : 0,
            dataSource?.response?.prediction ? dataSource?.response?.prediction.summerCatches : 0,
            dataSource?.response?.prediction ? dataSource?.response?.prediction.autumnCatches : 0),
        new PredictionTableCell('Beoogde vangst',
            dataSource?.request?.winterCatches,
            dataSource?.request?.springCatches,
            dataSource?.request?.summerCatches,
            dataSource?.request?.autumnCatches),
        new PredictionTableCell('Benodigde uren',
            dataSource?.response?.prediction ? dataSource?.response?.prediction.winterHours : 0,
            dataSource?.response?.prediction ? dataSource?.response?.prediction.springHours : 0,
            dataSource?.response?.prediction ? dataSource?.response?.prediction.summerHours : 0,
            dataSource?.response?.prediction ? dataSource?.response?.prediction.autumnHours : 0)
    ]

}
