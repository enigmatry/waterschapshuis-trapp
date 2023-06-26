import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AreasService } from 'src/app/shared/services/areas.service';
import { IListItem } from 'src/app/shared/models/list-item';
import { SideBarFilterService } from 'src/app/reports/services/side-bar-filter.service';
import { PredictionRequest } from 'src/app/reports/models/prediction-request.model';
import { Router } from '@angular/router';
import { ReportsRouteUri } from 'src/app/reports/models/reports-route-uri.enum';
import { PredictionReportSeason } from '../../../models/prediction-report-season.model';

@Component({
  selector: 'app-prediction-filter',
  templateUrl: './prediction-filter.component.html',
  styleUrls: ['./prediction-filter.component.scss']
})
export class PredictionFilterComponent implements OnInit {

  form: FormGroup;
  hourSquares: IListItem[];
  routerLink = `reports/${ReportsRouteUri.Prediction}`;
  seasons: PredictionReportSeason[] = [];
  constructor(
    private areasService: AreasService,
    private sideBarFilterService: SideBarFilterService,
    private router: Router) { }

  ngOnInit() {
    this.createForm();
    this.loadHourSquares();
    this.seasons = PredictionReportSeason.getSeasonsOrderedStartingWithCurrent();
  }

  createForm() {
    this.form = new FormGroup({
      hourSquareId: new FormControl(null, Validators.required),

      // Expected catches controls.
      expectedWinterCatches: new FormControl(0, Validators.min(0)),
      expectedSpringCatches: new FormControl(0, Validators.min(0)),
      expectedSummerCatches: new FormControl(0, Validators.min(0)),
      expectedAutumnCatches: new FormControl(0, Validators.min(0)),

      // Expected hours controls.
      expectedWinterHours: new FormControl(0, Validators.min(0)),
      expectedSpringHours: new FormControl(0, Validators.min(0)),
      expectedSummerHours: new FormControl(0, Validators.min(0)),
      expectedAutumnHours: new FormControl(0, Validators.min(0))
    });
  }

  isExpanded() {
    return this.router.url.includes(this.routerLink);
  }

  onCalculate() {
    const request = new PredictionRequest();

    request.hourSquareId = this.form.value.hourSquareId;
    // Expected Catches.
    request.winterCatches = this.form.value.expectedWinterCatches;
    request.springCatches = this.form.value.expectedSpringCatches;
    request.summerCatches = this.form.value.expectedSummerCatches;
    request.autumnCatches = this.form.value.expectedAutumnCatches;
    // Expected hours.
    request.winterHours = this.form.value.expectedWinterHours;
    request.springHours = this.form.value.expectedSpringHours;
    request.summerHours = this.form.value.expectedSummerHours;
    request.autumnHours = this.form.value.expectedAutumnHours;

    this.sideBarFilterService.calculateHourSquarePredictions(request);
  }

  loadHourSquares(): void {
    this.areasService.getHourSquares(undefined)
      .subscribe(response => {
        this.hourSquares = response;
      });
  }


}
