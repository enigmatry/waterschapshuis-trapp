import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormArray } from '@angular/forms';
import { CatchDetails } from 'src/app/maps/models/catch-details.model';
import { ToastService } from 'src/app/services/toast.service';
import { INamedEntityItem, IGetCatchTypesResponseItem } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { CatchService } from 'src/app/traps/services/catch.service';
import { noop } from 'rxjs';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { distinctUntilChanged } from 'rxjs/operators';
import { CatchBaseType } from '../../model/catch-base-type.enum';
import { getDateForWeeksAgoFromToday as getMondayWeeksAgoFromToday } from '../date-helper';

export class CatchFormModel {
  type: CatchBaseType;
  recordedOn: string;
}

@Component({
  selector: 'app-catch-details',
  templateUrl: './catch-details.component.html',
  styleUrls: ['./catch-details.component.scss']
})
export class CatchDetailsComponent extends OnDestroyMixin implements OnInit {
  form: FormGroup;
  catchItems: FormArray;

  editMode = false;
  today: string = new Date().toISOString();
  mondaySixWeeksAgo: string = getMondayWeeksAgoFromToday(6);
  catchTypes: Array<IGetCatchTypesResponseItem>;

  catchTypeGroups: INamedEntityItem[] = [
    { id: CatchBaseType.Muskusrat, name: 'Muskusrat' },
    { id: CatchBaseType.Beverrat, name: 'Beverrat' }
  ];

  constructor(
    private fb: FormBuilder,
    private toastService: ToastService,
    private catchService: CatchService
  ) {
    super();
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

  fromFormArray(): Array<CatchDetails> {
    const newCatches: Array<CatchDetails> = [];
    const date = new Date(this.form.get('recordedOn').value);
    const catchesFormArray = this.form.get('catches') as FormArray;

    catchesFormArray.controls.forEach(catchCtrl => {

      const noOfCatches = Number(catchCtrl.get('noOfCatches').value);

      if (!isNaN(noOfCatches) && noOfCatches > 0) {
        newCatches.push(
          CatchDetails.fromFormValues(
            catchCtrl.get('catchType').value,
            noOfCatches,
            date
          ));
      }
    });

    return newCatches;
  }

  private async initView(): Promise<void> {

    this.catchTypes = await this.catchService.getCatchTypes().toPromise();

    const defaultModel = { type: CatchBaseType.Muskusrat, recordedOn: new Date().toISOString() };

    this.bindForm(defaultModel);

    this.form.patchValue(defaultModel);
  }

  private bindForm(model: CatchFormModel): void {

    if (!model?.type || !model?.recordedOn) { return; }

    const currentCatchTypes = this.catchTypes.filter(ct => ct.name.startsWith(model.type));

    this.catchItems = this.form.get('catches') as FormArray;

    this.catchItems.clear();

    currentCatchTypes.forEach(cct => {
      this.catchItems.push(this.fb.group({
        catchType: cct,
        noOfCatches: 0
      }));
    });
  }

  private createForm(): void {
    this.form = this.fb.group({
      type: [null, Validators.required],
      recordedOn: [null, Validators.required],
      catches: this.fb.array([])
    });

    this.form.get('type').valueChanges
      .pipe(
        distinctUntilChanged(),
        untilComponentDestroyed(this)
      )
      .subscribe(value => this.bindForm({ type: value, recordedOn: this.form.value.recordedOn }));
  }
}
