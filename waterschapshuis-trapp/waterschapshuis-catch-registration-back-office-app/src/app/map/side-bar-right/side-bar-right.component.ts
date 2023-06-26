import { Component } from '@angular/core';
import { SideBarActions } from 'src/app/core/side-bar/side-bar-abstract/side-bar-actions.enum';
import { SideBarPosition } from 'src/app/core/side-bar/side-bar-abstract/side-bar-positions.enum';
import { ISideBar } from 'src/app/core/side-bar/side-bar-abstract/side-bar.interface';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { TrapsService } from '../services/traps.service';
import { ToastService } from 'src/app/shared/toast/toast.service';
import { TrapDetails } from '../models/trap-details.model';
import { forkJoin } from 'rxjs';
import { ObservationDetails } from '../models/observation-details.model';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { ObservationService } from '../services/observation.service';

@Component({
  selector: 'app-side-bar-right',
  templateUrl: './side-bar-right.component.html',
  styleUrls: ['./side-bar-right.component.scss']
})
export class SideBarRightComponent extends OnDestroyMixin implements ISideBar {
  data: { trapIds: Array<string>, observationIds: Array<string> };
  loading: boolean;

  traps: Array<TrapDetails>;
  observations: Array<ObservationDetails>;

  trapListPanelVisible = false;
  observationListPanelVisible = false;
  selectedTrap: TrapDetails;
  selectedObservation: ObservationDetails;

  totalItems: number;

  constructor(
    private sideBarService: SideBarService,
    private trapsService: TrapsService,
    private toastService: ToastService,
    private observationsService: ObservationService
  ) {
    super();
  }

  close(): void {
  }
  toggle(): void {
  }

  open(): void {
    this.loading = true;
    this.trapListPanelVisible = false;
    this.observationListPanelVisible = false;
    forkJoin({
      traps: this.trapsService.getTraps(this.data.trapIds),
      observations: this.observationsService.getObservations(this.data.observationIds)
    })
      .pipe(
        untilComponentDestroyed(this)
      ).subscribe(res => {
        this.traps = [...res.traps];
        this.observations = [...res.observations];
        this.totalItems = this.traps.length + this.observations.length;
        this.loading = false;
      },
        error => {
          this.toastService.showErrorMessage(error.message);
          this.loading = false;
        });
  }

  toggleSideBar(event: Event): void {
    event.stopPropagation();
    this.sideBarService.toggleSideBar({ position: SideBarPosition.end, action: SideBarActions.close });
  }

  toggleTrapPanel(trap: TrapDetails): void {
    this.trapListPanelVisible = !!trap;
    if (!this.trapListPanelVisible) {
      this.open();
    }
    this.selectedTrap = trap;
  }

  toggleObservationPanel(observation: ObservationDetails): void {
    this.observationListPanelVisible = !!observation;
    if (!this.observationListPanelVisible) {
      this.open();
    }
    this.selectedObservation = observation;
  }
}
