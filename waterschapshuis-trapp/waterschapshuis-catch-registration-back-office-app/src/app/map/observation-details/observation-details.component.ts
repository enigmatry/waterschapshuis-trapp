import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ObservationDetails } from '../models/observation-details.model';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { SideBarPosition } from 'src/app/core/side-bar/side-bar-abstract/side-bar-positions.enum';
import { SideBarActions } from 'src/app/core/side-bar/side-bar-abstract/side-bar-actions.enum';
import { ObservationService } from 'src/app/map/services/observation.service';
import { MapStateService } from '../services/map-state.service';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';

@Component({
  selector: 'app-observation-details',
  templateUrl: './observation-details.component.html',
  styleUrls: ['./observation-details.component.scss']
})
export class ObservationDetailsComponent implements OnInit {

  @Input() observation: ObservationDetails;
  @Output() closeObservationDetails = new EventEmitter();

  observationType: string;

  policyName = PolicyName;

  constructor(
    private sideBarService: SideBarService,
    private observationService: ObservationService,
    private mapStateService: MapStateService
  ) { }

  ngOnInit(): void {
    this.observationType = this.observation?.type === 1 ? 'Schade' : 'Overig';
  }

  toggleSideBar(event: Event): void {
    event.stopPropagation();
    this.sideBarService.toggleSideBar({ position: SideBarPosition.end, action: SideBarActions.close });
  }

  navigateBack(): void {
    this.closeObservationDetails.emit(undefined);
  }

  archiveObservation(): void {
    this.observationService.archiveActiveObservation(this.observation)
      .subscribe((data: ObservationDetails) => {
        this.observation = { ...data };
        this.mapStateService.refreshLayers(OverlayLayerName.Observations);
      });
  }

}
