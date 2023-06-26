import { Component, Input, OnChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NavController } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { Subject } from 'rxjs';
import { noop } from 'rxjs/internal/util/noop';
import { distinctUntilChanged } from 'rxjs/operators';
import { Logger } from 'src/app/core/logger/logger';
import { TrapStatusNL } from 'src/app/maps/models/trap-status.enum';
import { TrappingType } from 'src/app/maps/models/trapping-type.model';
import { TrackingService } from 'src/app/maps/services/tracking.service';
import { ToastService } from 'src/app/services/toast.service';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { TrapBaseDetails } from '../../model/trap-base-details.model';
import { GetTrapTypesResponseItem, INamedEntityItem, TrapStatus } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

const logger = new Logger('TrapGeneralInfoComponent');

@Component({
  selector: 'app-trap-general-info',
  templateUrl: './trap-general-info.component.html',
  styleUrls: ['./trap-general-info.component.scss'],
})
export class TrapGeneralInfoComponent extends OnDestroyMixin implements OnChanges {

  private isFormValidSubject: Subject<boolean> = new Subject<boolean>();
  isFormValid$ = this.isFormValidSubject.asObservable();

  @Input() trap: TrapBaseDetails;

  form: FormGroup;
  trapTypes: GetTrapTypesResponseItem[];
  trapStatuses: Array<INamedEntityItem>;
  trappingTypes: TrappingType[] = [];
  editMode = false;
  createMode = false;
  isRemoved = false;
  notCatching = false;
  today: string = new Date().toISOString();
  selectedStatus: string;

  get isTrackingEnabled(): boolean {
    return this.trackingService.trackingEnabled;
  }

  get formValues(): TrapBaseDetails {
    const selectedTrapType = this.trapTypes.find(t => t.id === this.form.get('trapTypeId').value);
    return new TrapBaseDetails(
      this.trap.id,
      selectedTrapType,
      this.form.get('trappingTypeId').value,
      Number(this.form.get('statusValue').value),
      this.form.get('remarks').value,
      this.form.get('numberOfTraps').value,
      new Date(),
      new Date(this.form.get('recordedOn').value),
      this.trap.numberOfCatches,
      this.trap.isEditAllowed
    );
  }

  constructor(
    public nav: NavController,
    public router: Router,
    private fb: FormBuilder,
    private toastService: ToastService,
    public lookupsService: LookupsService,
    public trackingService: TrackingService
  ) {
    super();
    this.createForm();
  }

  ngOnChanges(): void {
    this.initView().then(
      noop,
      () => {
        this.toastService.error('Fout opgetreden tijdens inlezen data');
      }
    );
  }

  async initView(): Promise<void> {

    this.trappingTypes = await this.lookupsService.getTrappingTypes().toPromise();

    const trappingTypeId = this.trap ?
      this.trap.trappingTypeId :
      this.isTrackingEnabled
        ? this.trackingService.trappingTypeId
        : this.trappingTypes.find(t => t.name === 'Muskusrat').id;

    this.createMode = !this.trap;

    this.trapTypes =
      await this.lookupsService.getTrapTypes(trappingTypeId).toPromise();

    if (this.createMode) {
      this.trap = this.createDefaultTrap(trappingTypeId);
    } else {
      this.trapStatuses = this.getStatusesList(this.trapTypeSelected.allowedStatuses, true);
    }

    this.editMode = this.trap.isEditAllowed;

    this.isRemoved = this.trap.status === TrapStatus.Removed;
    this.notCatching = this.trap.status === TrapStatus.NotCatching;

    setTimeout(() =>
      this.form.patchValue({
        trappingTypeId: this.trap.trappingTypeId,
        trapTypeId: this.trap.trapType.id,
        numberOfTraps: this.trap.numberOfTraps,
        statusValue: Number(this.trap.status).toString(),
        remarks: this.trap.remarks,
        recordedOn: this.trap.recordedOn.toISOString()
      }));

    // otherwise if control is prefilled with more than 250 characters (remarks from old-imported traps)
    // there will be no error underline
    this.form.controls.remarks.markAsTouched();

    this.disableFromFields();
  }

  get formControls() {
    return this.form.controls;
  }

  get trapTypeSelected() {
    return this.trapTypes.find(x => x.id === this.trap.trapType.id);
  }

  private disableFromFields() {
    const trapTypeCtrl = this.form.get('trapTypeId');
    const statusValueCtrl = this.form.get('statusValue');
    const trappingTypeCtrl = this.form.get('trappingTypeId');
    const numberOfTrapsCtrl = this.form.get('numberOfTraps');
    const recordedOnCtrl = this.form.get('recordedOn');

    const disabled = !this.createMode || this.isRemoved || this.notCatching;

    if (this.isTrackingEnabled || disabled) {
      trappingTypeCtrl.disable();
    }

    if (disabled) {
      trapTypeCtrl.disable();
    }

    if (!this.createMode) {
      recordedOnCtrl.disable();
    }

    if (this.notCatching) {
      numberOfTrapsCtrl.disable();
    }

    if (this.isRemoved) {
      numberOfTrapsCtrl.disable();
      statusValueCtrl.disable();
    }
  }

  private createForm(): void {
    this.form = this.fb.group({
      trapTypeId: [null, Validators.required],
      numberOfTraps: [null, Validators.required],
      statusValue: [null, Validators.required],
      remarks: [null, Validators.maxLength(250)],
      trappingTypeId: [null, Validators.required],
      recordedOn: [null, Validators.required]
    });

    this.form.get('trappingTypeId').valueChanges
      .pipe(
        untilComponentDestroyed(this),
        distinctUntilChanged()
      )
      .subscribe((trappingType: string) => {
        this.changeTrappingTypeForm(trappingType);
      });

    this.form.get('trapTypeId').valueChanges
      .pipe(
        untilComponentDestroyed(this),
        distinctUntilChanged()
      )
      .subscribe((trapTypeId: string) => {
        this.changeTrapTypeForm(trapTypeId);
      });

    this.form.get('statusValue').valueChanges
      .pipe(
        untilComponentDestroyed(this),
        distinctUntilChanged()
      )
      .subscribe((statusId: string) => {
        this.changeStatusValue(statusId);
      });

    this.form.statusChanges
      .pipe(untilComponentDestroyed(this))
      .subscribe(value =>
        this.isFormValidSubject.next(value === 'VALID')
      );
  }

  private async changeTrappingTypeForm(trappingTypeId: string): Promise<void> {
    if (trappingTypeId) {
      this.trapTypes = await this.lookupsService.getTrapTypes(trappingTypeId).toPromise();
      const trapTypeSelected = this.trap && this.trap.trappingTypeId === trappingTypeId ? this.trap.trapType : this.trapTypes[0];
      setTimeout(() => {
        this.form.get('trapTypeId').patchValue(trapTypeSelected.id);
      }, 1);
    }
  }

  private async changeTrapTypeForm(trapTypeId: string): Promise<void> {
    if (trapTypeId) {
      const trapTypeSelected = this.trapTypes.find(x => x.id === trapTypeId);
      this.trapStatuses = this.getStatusesList(trapTypeSelected.allowedStatuses, !this.createMode);
      setTimeout(() => {
        this.selectedStatus = TrapStatusNL[Number(this.trap.status).toString()];
        this.form.get('statusValue').patchValue(Number(this.trap.status).toString());
      }, 1);
    }
  }

  private changeStatusValue(trapStatus: string): void {
    this.selectedStatus = TrapStatusNL[Number(trapStatus).toString()];
  }

  private getStatusesList(allowedStatuses: TrapStatus[], includeRemoved: boolean): Array<INamedEntityItem> {
    if (!includeRemoved && allowedStatuses.indexOf(Number(TrapStatus.Removed)) > 0) {
      allowedStatuses.splice(allowedStatuses.indexOf(Number(TrapStatus.Removed)));
    }
    return allowedStatuses.map(statusId => {
      return {
        id: statusId.toString(),
        name: TrapStatusNL[statusId]
      };
    });
  }

  private createDefaultTrap(trappingTypeId: string): TrapBaseDetails {
    const conibearTrapType =
      this.trapTypes.find(tt => tt.name === 'Conibear');

    return TrapBaseDetails.getDefault(trappingTypeId, conibearTrapType);
  }
}
