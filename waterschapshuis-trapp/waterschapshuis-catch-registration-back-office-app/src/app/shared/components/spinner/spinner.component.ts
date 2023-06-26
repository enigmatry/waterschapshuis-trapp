import { Component, OnInit, Input, OnDestroy } from '@angular/core';

import { SpinnerService } from '../../services/spinner.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html'
})
export class SpinnerComponent implements OnInit {
  loading: boolean;
  isLoading: Subject<boolean> = this.spinnerService.isLoading;

  constructor(private spinnerService: SpinnerService) {
   }

  ngOnInit(): void {
  }

}
