import { Component, OnInit } from '@angular/core';
import { ModalController, NavController, NavParams } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { forkJoin, noop } from 'rxjs';
import { Logger } from 'src/app/core/logger/logger';
import { ObservationResponse } from 'src/app/maps/models/observation-response.model';
import { TrapDetails } from 'src/app/maps/models/trap-details.model';
import { ToastService } from 'src/app/services/toast.service';
import { TrapService } from 'src/app/traps/services/trap.service';

import { MapItemSelectionModel, MapItemType } from '../models/map-item-selection.model';
import { ObservationService } from '../services/observation.service';
import { LoaderService } from 'src/app/services/loader.service';

const logger = new Logger('MapMultiSelectModalComponent');

@Component({
  selector: 'app-map-multi-select-modal-modal',
  templateUrl: './map-multi-select-modal.component.html',
  styleUrls: ['./map-multi-select-modal.component.scss'],
})
export class MapMultiSelectModalComponent extends OnDestroyMixin implements OnInit {
  traps: TrapDetails[];
  dismissSelectionModal: any;
  observations: ObservationResponse[];

  loader: any;

  title: string;

  constructor(
    private navParams: NavParams,
    public nav: NavController,
    public ionModal: ModalController,
    private toastService: ToastService,
    private trapService: TrapService,
    private observationService: ObservationService,
    private loaderService: LoaderService) {
    super();
  }

  ngOnInit(): void {
    this.initView().then(
      noop,
      () => {
        this.loader.dismiss();
        this.toastService.error('Fout opgetreden tijdens inlezen data');
      }
    );
  }
  private async initView(): Promise<void> {
    this.loader = await this.loaderService.createLoader();

    const selectedItems: MapItemSelectionModel[] = this.navParams.get('mapItemsToDisplay');

    this.dismissSelectionModal = this.navParams.get('dismissSelectionModal');

    forkJoin({
      traps: this.trapService.getTrapsForGivenTrapIds(
        selectedItems.filter(t => t.type === MapItemType.Trap).map(t => t.id)
      ),
      observations: this.observationService.getObservationsForGivenIds(
        selectedItems.filter(t => t.type === MapItemType.Observation).map(t => t.id)
      )
    }).pipe(untilComponentDestroyed(this))
      .subscribe(res => {
        this.traps = res.traps;
        this.observations = res.observations;

        this.setModalTitle();

        this.loader.dismiss();
      },
        err => {
          this.loader.dismiss();
          this.toastService.error('Fout opgetreden');
          logger.error(err, 'Error showing trap location details');
        });
  }

  showObservationPage(observation: ObservationResponse): void {
    this.dismissSelectionModal();
    this.nav.navigateForward(`/melding/${observation?.id}`);
  }

  showTrapDetailsPage(trap: TrapDetails) {
    this.nav.navigateForward(`traps/vangmiddel/${trap?.id}`);
    this.closeModal();
  }

  closeModal() {
    this.ionModal.dismiss(null, null, 'MapMultiSelectModal');
  }

  private setModalTitle(): void {
    this.title = this.traps.length > 0 && this.observations.length > 0 ?
      `${this.traps.length + this.observations.length} Objecten` :
      this.observations.length > 0 ? `${this.observations.length} Meldingen` :
        `${this.traps.length} Vangmiddelen`;
  }
}
