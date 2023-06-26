import { Component, OnInit } from '@angular/core';
import { TrappingType } from '../models/trapping-type.model';
import { TrackingService } from '../services/tracking.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-map-tracking-modal',
  templateUrl: './map-tracking-modal.component.html',
  styleUrls: ['./map-tracking-modal.component.scss'],
})
export class MapTrackingModalComponent implements OnInit {
  trappingTypes: Array<TrappingType>;
  selectedTrappingType: TrappingType;
  trackingForm: FormGroup;
  isTimewriting = true;
  isTrackingMap = true;
  isTrackingPrivate: boolean;

  constructor(public trackingService: TrackingService, private fb: FormBuilder, private modal: ModalController) { }

  ngOnInit() {
    this.selectedTrappingType = this.trappingTypes.find(t => t.name === 'Muskusrat');
    this.isTrackingPrivate = true;
    this.createForm();
  }

  private createForm(): void {
    this.trackingForm = this.fb.group({
      trappingType: [this.selectedTrappingType, Validators.required],
      timewriting: [true, Validators.requiredTrue],
      trackingMap: [true, Validators.requiredTrue],
      isTrackingPrivate: [this.isTrackingPrivate, Validators.required]
    });
  }

  startBackgroundGeolocation(event: any): void {
    this.isTimewriting = this.trackingForm.get('timewriting').value;
    this.isTrackingMap = this.trackingForm.get('trackingMap').value;
    this.isTrackingPrivate = this.trackingForm.get('isTrackingPrivate').value;
    this.trackingService.startBackgroundGeolocation(
      this.selectedTrappingType.id, this.isTimewriting, this.isTrackingMap, this.isTrackingPrivate);
    this.modal.dismiss();
  }

  async changeTrappingTypeForm(): Promise<void> {
    this.selectedTrappingType = this.trackingForm.get('trappingType').value;
  }

  async changeTrackingPurposeForm(): Promise<void> {
    this.isTimewriting = this.trackingForm.get('timewriting').value;
    this.isTrackingMap = this.trackingForm.get('trackingMap').value;
    if (!this.isTrackingMap) {
      this.isTrackingPrivate = true;
      this.trackingForm.patchValue({
        isTrackingPrivate: this.isTrackingPrivate
      });
    }
  }

  get isFormValid(): boolean {
    return this.trackingForm.get('trappingType').valid &&
      (this.trackingForm.get('timewriting').valid ||
        this.trackingForm.get('trackingMap').valid) &&
        this.trackingForm.get('isTrackingPrivate').valid;
  }
}
