import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ObservationResponse } from '../../models/observation-response.model';

@Component({
  selector: 'app-observation-basic-details',
  templateUrl: './observation-basic-details.component.html'
})
export class ObservationBasicComponent {

  @Input() observation: ObservationResponse;
  @Output() observationSelected = new EventEmitter<ObservationResponse>();

  onSelect() {
    this.observationSelected.emit(this.observation);
  }

}
