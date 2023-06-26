import { Component, OnInit, Input, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { IListItem } from 'src/app/shared/models/list-item';
import { FormGroup } from '@angular/forms';
import { AreasService } from 'src/app/shared/services/areas.service';
import { TimeRegistrationStatus } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { TimeRegistrationFormState } from '../models/time-registration-form-state.enum';
import { TimeRegistrationItemForm } from '../models/time-registration-item';
import { TimeRegistrationGeneralItemForm } from '../models/time-registration-general-item';

@Component({
  selector: 'app-time-registration-item',
  templateUrl: './time-registration-item.component.html',
  styleUrls: ['./time-registration-item.component.scss'],
})
export class TimeRegistrationItemComponent implements OnInit, AfterViewInit {
  @Input() itemFormGroup: FormGroup;
  @Input() index: number;
  @Input() titleNumber: number;
  @Input() catchAreas: IListItem[] = [];
  @Input() subAreas: IListItem[] = [];
  @Input() hourSquares: IListItem[] = [];
  @Input() trappingTypes: IListItem[] = [];
  @Input() categories: IListItem[] = [];
  @Input() isForLocation: boolean;
  @Input() state: TimeRegistrationFormState;

  timeRegistrationFormState = TimeRegistrationFormState;

  @Output() deleteEvent = new EventEmitter();
  @Output() changeEvent = new EventEmitter();

  title: string;
  tabType = TimeRegistrationFormState.Bestrijding;
  dataLoaded = false;

  constructor(
    private areasService: AreasService
  ) { }

  async ngOnInit(): Promise<void> {
    this.title = 'Dagdeel ' + this.titleNumber;

    if (this.state !== TimeRegistrationFormState.Creation) {
      this.tabType = this.state;
    }

    if (this.isForLocation) {
      this.setLocationDefaults();
    } else {
      await this.loadSubAreas();
      await this.loadHourSquares();
    }

    this.setDefaultTrappingType();
    this.updateItemDisabledStatus();
  }

  ngAfterViewInit() {
    this.dataLoaded = true;
  }

  updateItemDisabledStatus(): void {
    if (this.itemFormGroup.get('status').value === TimeRegistrationStatus.Written) {
      this.itemFormGroup.enable();
    } else {
      this.itemFormGroup.disable();
    }
  }

  setLocationDefaults() {
    if (!this.itemFormGroup.get('catchArea')?.value) {
      this.itemFormGroup.get('catchArea')?.setValue(this.catchAreas[0]);
    }
    if (!this.itemFormGroup.get('subArea')?.value) {
      this.itemFormGroup.get('subArea')?.setValue(this.subAreas[0]);
    }
    if (!this.itemFormGroup.get('hourSquare')?.value) {
      this.itemFormGroup.get('hourSquare')?.setValue(this.hourSquares[0]);
    }
  }

  deleteTimeRegistration(): void {
    const status: TimeRegistrationStatus = this.itemFormGroup.get('status').value;
    if (status !== TimeRegistrationStatus.Closed && status !== TimeRegistrationStatus.Completed) {
      this.deleteEvent.emit({ index: this.index, state: this.state });
    }
  }

  resetSubArea(): void {
    this.itemFormGroup.get('subArea').setValue(null);
    this.loadSubAreas();
  }

  resetHourSquare(): void {
    this.itemFormGroup.get('hourSquare').setValue(null);
    this.loadHourSquares();
  }

  async loadSubAreas(): Promise<void> {
    const catchArea = this.itemFormGroup.get('catchArea')?.value;
    if (catchArea && catchArea.id) {
      this.subAreas = await this.areasService.getSubAreas(catchArea.id).toPromise();
    } else {
      this.subAreas = [];
    }
  }

  async loadHourSquares(): Promise<void> {
    const subArea = this.itemFormGroup.get('subArea')?.value;
    if (subArea && subArea.id) {
      this.hourSquares = await this.areasService.getHourSquares(subArea.id).toPromise();
    } else {
      this.hourSquares = [];
    }
  }

  onChangeCatchArea(event: CustomEvent) {
    this.itemFormGroup.get('catchArea').setValue(event.detail.value);
    if (this.dataLoaded) {
      this.resetSubArea();
      this.resetHourSquare();
      this.changeEvent.emit();
    }
  }

  onChangeSubArea(event: CustomEvent) {
    this.itemFormGroup.get('subArea').setValue(event.detail.value);
    if (this.dataLoaded) {
      this.resetHourSquare();
      this.changeEvent.emit();
    }
  }

  onChangeItem(event: CustomEvent) {
    if (this.dataLoaded) {
      this.changeEvent.emit();
    }
  }

  private setDefaultTrappingType() {
    if (!this.itemFormGroup.get('trappingType')?.value) {
      this.itemFormGroup.get('trappingType')?.setValue(this.trappingTypes[0]);
    }
  }

  typeChanged(event: any): void {
    this.tabType = event.detail.value;

    this.tabType === TimeRegistrationFormState.Bestrijding ?
      TimeRegistrationItemForm.resetTimeRegistrationFormToInitialState(this.itemFormGroup) :
      TimeRegistrationGeneralItemForm.resetTimeRegistrationGeneralFormToInitialState(this.itemFormGroup);
  }

  compareWith = (o1, o2) => {
    return o1 && o2 ? o1.id === o2.id : o1 === o2;
  }
}
