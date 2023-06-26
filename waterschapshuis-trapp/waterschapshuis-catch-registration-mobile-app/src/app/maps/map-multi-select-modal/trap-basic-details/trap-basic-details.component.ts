import { Component, Input, Output, EventEmitter } from '@angular/core';
import { TrapDetails } from 'src/app/maps/models/trap-details.model';

@Component({
  selector: 'app-trap-basic-details',
  templateUrl: './trap-basic-details.component.html'
})
export class TrapBasicDetailsComponent {

  @Input() trap: TrapDetails;
  @Output() trapSelected = new EventEmitter<TrapDetails>();

  onSelect() {
    this.trapSelected.emit(this.trap);
  }

}
