import { Component, Input, OnInit } from '@angular/core';
import { transform } from 'ol/proj';
import { CatchDetails } from 'src/app/maps/models/catch-details.model';
import { ProjectionModel } from 'src/app/maps/models/projection.model';
import { TrapDetails } from 'src/app/maps/models/trap-details.model';
import { LaunchNavigatorService } from 'src/app/maps/services/launch-navigator.service';
import { CurrentUserProviderService } from 'src/app/shared/services/current-user-provider.service';

@Component({
  selector: 'app-trap-details',
  templateUrl: './trap-details.component.html',
  styleUrls: ['./trap-details.component.scss'],
})
export class TrapDetailsComponent implements OnInit {
  @Input() title: string;
  @Input() showRouteButton: boolean;
  @Input() trap: TrapDetails;

  constructor(
    private launchNavigator: LaunchNavigatorService,
    private currentUserProvider: CurrentUserProviderService
  ) { }

  ngOnInit() { }

  navigateToSelectedTrap(trap: TrapDetails) {
    const coords = transform(
      [trap.longitude, trap.latitude],
      ProjectionModel.dutchMatrix,
      ProjectionModel.geodeticMatrix
    );

    this.launchNavigator.openMapAppAndNavigateToSelectedLocation([coords[1], coords[0]]);
  }

  canRemoveCatch = (item: CatchDetails): boolean => {
    return !this.showRouteButton &&
      item.createdOn?.toDateString() === (new Date()).toDateString() &&
      item.createdById === this.currentUserProvider.currentUser?.id;
  }

  catches = (): CatchDetails[] => this.trap.catchesOnly.filter(x => !x.markedForRemoval);

  byCatches = (): CatchDetails[] => this.trap.byCatchesOnly.filter(x => !x.markedForRemoval);

  catchMarkForRemoval = (catchForRemoval: CatchDetails) => catchForRemoval.markedForRemoval = true;
}
