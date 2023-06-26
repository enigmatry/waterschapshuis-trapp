import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ObservationDetails } from '../models/observation-details.model';

@Component({
  selector: 'app-observation-info',
  templateUrl: './observation-info.component.html',
  styleUrls: ['./observation-info.component.scss']
})
export class ObservationInfoComponent {

  @Input() observation: ObservationDetails;
  @Output() showObservationDetails = new EventEmitter();

  details(): void {
    this.showObservationDetails.emit(this.observation);
  }
}
