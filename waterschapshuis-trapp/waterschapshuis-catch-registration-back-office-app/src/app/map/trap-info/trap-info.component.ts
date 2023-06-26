import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';

import { TrapDetails } from '../models/trap-details.model';

@Component({
  selector: 'app-trap-info',
  templateUrl: './trap-info.component.html',
  styleUrls: ['./trap-info.component.scss']
})
export class TrapInfoComponent {
  @Input() trap: TrapDetails;
  @Input() showRouteButton = false;
  @Output() trapPanelVisible = new EventEmitter();
  policyName = PolicyName;

  constructor() { }

  details(): void {
    this.trapPanelVisible.emit(this.trap);
  }
}
