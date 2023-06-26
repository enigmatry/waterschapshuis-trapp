import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, NavigationExtras } from '@angular/router';
import { NavController } from '@ionic/angular';
import { noop } from 'rxjs';
import { take } from 'rxjs/operators';
import { IGetCatchTypesResponseItem } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { CatchService } from 'src/app/traps/services/catch.service';
import { CatchDetails } from 'src/app/maps/models/catch-details.model';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-by-catch-details-edit',
  templateUrl: './by-catch-details-edit.component.html',
  styleUrls: ['./by-catch-details-edit.component.scss']
})
export class ByCatchDetailsEditComponent implements OnInit {

  form: FormGroup;
  byCatchItems: FormArray;
  byCatchTypes: Array<IGetCatchTypesResponseItem>;

  trapId: string;

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    public navController: NavController,
    private toastService: ToastService,
    private catchService: CatchService
  ) {
    this.createForm();
  }

  ngOnInit(): void {
    this.initView().then(
      noop,
      () => {
        this.toastService.error('Fout opgetreden tijdens inlezen data');
      }
    );
  }

  saveByCatches(): void {

    const addedCatchTypes = [];

    const byCatches = this.form.get('byCatches') as FormArray;

    byCatches.controls.forEach((ctrl: FormGroup) => {
      const newByCatch = this.getSelectedByCatch(ctrl);
      if (newByCatch) {
        addedCatchTypes.push(newByCatch);
      }
    });

    // using NavigationExtras state to send data between components according to: https://ionicacademy.com/pass-data-angular-router-ionic-4/
    const navigationExtras: NavigationExtras = {
      state: {
        newByCatches: addedCatchTypes
      }
    };
    const route = this.trapId ? `traps/vangmiddel/${this.trapId}` : `traps/vangmiddel`;
    this.navController.navigateBack([route], navigationExtras);
  }

  private getSelectedByCatch(ctrl: FormGroup): CatchDetails {

    const no = ctrl.get('noOfCatches').value;

    if (!isNaN(no) && Number(no) > 0) {
      const id = ctrl.get('id').value;
      const catchType = this.byCatchTypes.find(ct => ct.id === id);
      return CatchDetails.fromCatchTypeAndNumber(catchType, Number(no));
    }

    return undefined;
  }

  private async initView(): Promise<void> {

    const params = await this.activatedRoute.params.pipe(take(1)).toPromise();
    if (!params.animalType || isNaN(params.animalType)) {
      return;
    }
    this.trapId = params.trapId;

    this.byCatchTypes = await this.catchService.getByCatchTypesByAnimalType(Number(params.animalType)).toPromise();

    const defaultModel = {};

    this.bindForm();

    this.form.patchValue(defaultModel);
  }

  private bindForm(): void {

    this.byCatchItems = this.form.get('byCatches') as FormArray;

    this.byCatchItems.clear();

    this.byCatchTypes = this.byCatchTypes.sort((x, y) => x.order - y.order);

    const top10byCatchTypes = this.byCatchTypes.slice(0, 10);
    const otherbyCatchTypes = this.byCatchTypes.slice(10).sort((x, y) => {
      if (x.name < y.name) { return -1; }
      if (x.name > y.name) { return 1; }
      return 0;
    });

    top10byCatchTypes.concat(otherbyCatchTypes).forEach(cct => {
      this.byCatchItems.push(this.fb.group({
        id: cct.id,
        byCatchTypeName: cct.name,
        noOfCatches: 0
      }));
    });
  }


  private createForm(): void {
    this.form = this.fb.group({
      byCatches: this.fb.array([])
    });
  }

}
