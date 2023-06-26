import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NavController } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { Logger } from 'src/app/core/logger/logger';
import { LoaderService } from 'src/app/services/loader.service';
import { ToastService } from 'src/app/services/toast.service';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { TrapService } from 'src/app/traps/services/trap.service';

import { MapComponent } from '../map/map.component';
import { ProjectionModel } from '../models/projection.model';
import { TrapDetails } from '../models/trap-details.model';
import { MapStateService } from '../services/map-state.service';
import { Coordinate } from 'ol/coordinate';
import { finalize } from 'rxjs/operators';

const logger = new Logger('MapFineTunningComponent');

@Component({
  selector: 'app-map-fine-tunning',
  templateUrl: './map-fine-tunning.component.html',
  styleUrls: ['./map-fine-tunning.component.scss'],
})
export class MapFineTunningComponent extends OnDestroyMixin {
  @ViewChild('mapComponent') mapComponent: MapComponent;

  title: string;

  private navigateTo: string;

  constructor(
    router: Router,
    private route: ActivatedRoute,
    private navController: NavController,
    private mapService: MapStateService,
    private loaderService: LoaderService,
    private toastService: ToastService,
    private trapService: TrapService) {
    super();
    this.readRouterState(router);
  }

  async setLocation(): Promise<void> {
    const trapId = this.route.snapshot.params.trapId;

    if (trapId) {
      this.updateTrapLocation(trapId);
    } else {
      this.navController.navigateForward([this.navigateTo]);
    }
  }

  private readRouterState(router: Router) {
    const state = router.getCurrentNavigation().extras?.state;
    this.title = state?.title;
    this.navigateTo = state?.route;
  }

  private async updateTrapLocation(trapId: string) {
    const loader = await this.presentLoader();
    const trap = await this.trapService.get(trapId).toPromise();
    const coordinates = this.mapService.getCenterCoordinatesInSpecifiedProjection(ProjectionModel.dutchMatrix);
    const command = this.generateTrapUpdateCommand(trap, coordinates);

    this.trapService.saveTrap(command)
      .pipe(untilComponentDestroyed(this), finalize(() => loader.dismiss()))
      .subscribe(
        () => {
          const trapsLayerFullName = `${OverlayLayerName.TrapDetails}:TrapCreatedYear${(new Date()).getFullYear()}Active`;
          this.mapService.refreshLayers(trapsLayerFullName);
          this.navController.navigateRoot('/maps');
        },
        err => {
          logger.error(err);
          this.toastService.error('Fout opgetreden');
        }
      );
  }

  private async presentLoader() {
    const loader = await this.loaderService.createLoader();
    loader.present();

    return loader;
  }

  private generateTrapUpdateCommand(trap: TrapDetails, coordinates: Coordinate) {
    return TrapDetails.createCommand(
      trap.id,
      trap.trapTypeId,
      trap.status,
      trap.remarks,
      trap.numberOfTraps,
      trap.recordedOn,
      [],
      coordinates[0],
      coordinates[1],
      undefined,
      false,
      trap.numberOfCatches,
      trap.numberOfByCatches,
      trap.createdOn,
      trap.createdById);
  }
}
