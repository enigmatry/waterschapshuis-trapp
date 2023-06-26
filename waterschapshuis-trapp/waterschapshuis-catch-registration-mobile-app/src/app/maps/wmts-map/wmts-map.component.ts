import { Component, OnInit, ViewChild } from '@angular/core';
import { ActionSheetController, ModalController, NavController } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { noop } from 'rxjs';
import { BackgroundJobService } from 'src/app/background-job/background-job.service';
import { LoaderService } from 'src/app/services/loader.service';
import { ToastService } from 'src/app/services/toast.service';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AppSettingsService } from 'src/app/shared/services/app-settings.service';
import { MapTrackingModalComponent } from '../map-tracking-modal/map-tracking-modal.component';
import { MapComponent } from '../map/map.component';
import { TrackingService } from '../services/tracking.service';


@Component({
  selector: 'app-wmts-map',
  templateUrl: './wmts-map.component.html',
  styleUrls: ['./wmts-map.component.scss'],
})
export class WmtsMapComponent extends OnDestroyMixin implements OnInit {
  @ViewChild('mapComponent') mapComponent: MapComponent;

  trackingEnabled: boolean;
  hasUnsyncedItems: boolean;
  loader: any;

  constructor(
    public trackingService: TrackingService,
    public backgroundJobService: BackgroundJobService,
    private modal: ModalController,
    private toastService: ToastService,
    private loaderService: LoaderService,
    private alertService: AlertService,
    private actionsheetCtrl: ActionSheetController,
    private appSettingsService: AppSettingsService,
    private navController: NavController
  ) {
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

  ionViewDidEnter(): void {
    if (this.mapComponent && this.mapComponent.map) {
      this.mapComponent.map.updateSize();
    }

    // this.backgroundJobService.hasUnsyncedItems$
    //   .pipe(untilComponentDestroyed(this))
    //   .subscribe(value => {
    //     this.hasUnsyncedItems = value;
    //   });
    // this.hasUnsyncedItems = await this.backgroundJobService.hasUnsyncedItems$.pipe(take(1)).toPromise();
  }

  async showMenu(options: 'add' | 'more'): Promise<void> {
    const actionSheet = await this.actionsheetCtrl.create({
      header: 'Kies een optie...',
      buttons: this.getManuButtons(options)
    });
    await actionSheet.present();
  }

  private getManuButtons(options: 'add' | 'more'): Array<any> {
    let buttons = [];
    if (options === 'add') {
      buttons = [
        this.getMenuOption('Vangmiddel', '/maps/fine-tunning', { title: 'Vangmiddel', route: '/traps/vangmiddel' }),
        this.getMenuOption('Melding', '/maps/fine-tunning', { title: 'Melding', route: '/melding' })
      ];
    } else {
      const layers = this.appSettingsService.getOverlayLayerIds();
      const showTrackingLineFilterInMenu = !!layers?.find(l => l.startsWith(OverlayLayerName.TrackingLines));

      buttons.push(this.getMenuOption('Kies kaartlagen', '/maps/map-menu-layers'));

      if (showTrackingLineFilterInMenu) {
        buttons.push(this.getMenuOption('Filter getoonde speurkaarten', '/maps/map-tracking-filter'));
      }

      buttons.push(this.getMenuOption('Bekijk legenda', '/maps/legend'));
    }

    buttons.push(this.getMenuOption('Annuleer'));

    return buttons;
  }

  private getMenuOption(text: string, redirectUrl?: string, state?: { title: string, route: string }): any {
    return {
      text,
      handler: () => redirectUrl ? this.navController.navigateForward(redirectUrl, { state }) : undefined
    };
  }

  async openTrappingPopup(): Promise<void> {
    if (!this.trackingEnabled) {
      this.loader.present();

      const trappingTypes = await this.trackingService.getAllTrappingTypes().toPromise();

      const trackingModal = await this.modal.create({
        component: MapTrackingModalComponent,
        id: ' MapTrackingModal',
        componentProps: {
          trappingTypes
        },
        cssClass: 'map-tracking-modal'
      });
      this.loader.dismiss();
      trackingModal.present();
    } else {
      this.alertService.getConfirmDialog('Tracking',
        'U zet de tracking nu uit. Weet u het zeker?', 'Nee', 'Ja', (res) => {
          if (res) {
            this.trackingService.stopBackgroundGeolocation();
          }
        }, null).then(dialog => dialog.present());
    }
  }

  private async initView(): Promise<void> {
    this.loader = await this.loaderService.createLoader();

    this.trackingService.trackingEnabled$
      .pipe(untilComponentDestroyed(this))
      .subscribe(enabled => {
        this.trackingEnabled = enabled;
      });

    this.trackingService.getTrackingStatus();

  }
}
