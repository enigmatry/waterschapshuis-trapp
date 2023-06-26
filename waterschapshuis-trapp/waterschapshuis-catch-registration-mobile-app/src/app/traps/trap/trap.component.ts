
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavController } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { noop, Observable } from 'rxjs';
import { TrapStatus } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Logger } from 'src/app/core/logger/logger';
import { ProjectionModel } from 'src/app/maps/models/projection.model';
import { TrapDetails } from 'src/app/maps/models/trap-details.model';
import { MapStateService } from 'src/app/maps/services/map-state.service';
import { NetworkService } from 'src/app/network/network.service';
import { LoaderService } from 'src/app/services/loader.service';
import { ToastService } from 'src/app/services/toast.service';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { AlertService } from 'src/app/shared/services/alert.service';

import { TrapBaseDetails } from '../model/trap-base-details.model';
import { TrapCreateOrUpdateCommandVm } from '../model/trap-create-or-update-command-vm';
import { TrapService } from '../services/trap.service';
import { CatchDetailsComponent } from './catch-details/catch-details.component';
import { ByCatchDetailsComponent } from './by-catch-details/by-catch-details.component';
import { TrapGeneralInfoComponent } from './trap-general-info/trap-general-info.component';
import { CurrentUserProviderService } from 'src/app/shared/services/current-user-provider.service';

const logger = new Logger('TrapComponent');

@Component({
  selector: 'app-trap',
  templateUrl: './trap.component.html',
  styleUrls: ['./trap.component.scss']
})
export class TrapComponent extends OnDestroyMixin implements AfterViewInit {

  @ViewChild('trapDetails') trapDetailsComponent: TrapGeneralInfoComponent;
  @ViewChild('catchDetails') catchDetailsComponent: CatchDetailsComponent;
  @ViewChild('byCatchDetails') byCatchDetailsComponent: ByCatchDetailsComponent;

  trap: TrapDetails;
  trapBaseDetails: TrapBaseDetails;
  loader: any;
  isTrapDetailsFormValid$: Observable<boolean>;
  isRemoved: boolean;
  notCatching: boolean;
  isDeleteAllowed: boolean;

  constructor(
    private route: ActivatedRoute,
    private loaderService: LoaderService,
    private toastService: ToastService,
    private trapService: TrapService,
    private mapService: MapStateService,
    public navController: NavController,
    public networkService: NetworkService,
    private alertService: AlertService,
    private currentUserProvider: CurrentUserProviderService
  ) {
    super();
  }

  ngAfterViewInit() {
    this.isTrapDetailsFormValid$ = this.trapDetailsComponent.isFormValid$;
  }

  ionViewDidEnter() {
    this.initView().then(
      noop,
      () => {
        this.dismisLoader();
        this.toastService.error('Fout opgetreden tijdens inlezen data');
      }
    );
  }

  async deleteTrap(): Promise<void> {

    const cmd = this.createDeleteCommand();

    const confirm = await this.alertService.getConfirmDialog(
      'Status vangmiddel veranderen',
      'Weet u zeker dat u het vangmiddel wilt verwijderen?',
      'Nee',
      'Ja',
      this.delete,
      {
        cmd
      }
    );
    await confirm.present();
  }

  moveTrap(): void {
    this.navController.navigateRoot(`/maps/fine-tunning/${this.trap.id}`);
  }

  async saveTrap(): Promise<void> {
    const cmd = this.createCommand();
    this.save(true, { cmd });
  }

  private async initView(): Promise<void> {
    this.loader = await this.loaderService.createLoader();
    this.presentLoader();

    const trapId = this.route.snapshot.paramMap.get('trapId');

    if (trapId && !this.trap) {
      await this.loadTrap(trapId);
    }

    this.dismisLoader();
  }

  private async loadTrap(trapId: string): Promise<void> {
    this.trap = await this.trapService.get(trapId).toPromise();
    this.trapBaseDetails = TrapBaseDetails.fromTrapDetails(this.trap);
    this.isRemoved = this.trap.status === TrapStatus.Removed;
    this.notCatching = this.trap.status === TrapStatus.NotCatching;
    this.isDeleteAllowed = this.trap.isEditAllowed
      && this.trap.createdById === this.currentUserProvider.currentUser?.id;
  }

  private delete = async (dialogResult: boolean, args: any = null) => {

    if (!dialogResult) { return; }

    const loader = await this.loaderService.createLoader();
    loader.present();

    this.trapService.deleteTrap(args.cmd)
      .pipe(untilComponentDestroyed(this))
      .subscribe(
        () => {
          this.mapService.refreshLayers(OverlayLayerName.TrapDetails);

          loader.dismiss();

          this.toastService.success('Vangmiddel succesvol bewaard');
          this.navController.navigateRoot('/maps');
        },
        err => {
          logger.error(err);
          this.toastService.error('Fout opgetreden');
          loader.dismiss();
        }
      );

  }

  private save = async (dialogResult: boolean, args: any = null) => {

    if (!dialogResult) { return; }

    const loader = await this.loaderService.createLoader();
    loader.present();

    this.trapService.saveTrap(args.cmd)
      .pipe(untilComponentDestroyed(this))
      .subscribe(
        () => {
          this.mapService.refreshLayers(OverlayLayerName.TrapDetails);

          loader.dismiss();

          this.toastService.success('Vangmiddel succesvol bewaard');
          this.navController.navigateRoot('/maps');
        },
        err => {
          logger.error(err);
          this.toastService.validationError(err, false);
          loader.dismiss();
        }
      );
  }

  private presentLoader() {
    this.loader.present();
  }

  private dismisLoader() {
    this.loader.dismiss();
  }

  private createDeleteCommand(): TrapCreateOrUpdateCommandVm {
    const cmd = this.createCommand();

    cmd.previousStatus = undefined;
    cmd.shouldDelete = true;

    return cmd;
  }

  private createCommand(): TrapCreateOrUpdateCommandVm {

    const trapBaseDetails = this.trapDetailsComponent.formValues;

    const catches = !this.trap || this.trap?.status === TrapStatus.Catching ?
      this.catchDetailsComponent.fromFormArray() : [];

    const byCatches = !this.trap || this.trap?.status === TrapStatus.Catching ?
      this.byCatchDetailsComponent.getValues() : [];

    const removedCatches = this.trap
      ? this.trap.catches.filter(x => x.markedForRemoval) : [];

    const coordinates = !!this.trap ?
      [this.trap.longitude, this.trap.latitude] :
      this.mapService.getCenterCoordinatesInSpecifiedProjection(ProjectionModel.dutchMatrix);

    return TrapDetails.createCommand(
      trapBaseDetails.id,
      trapBaseDetails.trapType.id,
      trapBaseDetails.status,
      trapBaseDetails.remarks,
      trapBaseDetails.numberOfTraps,
      trapBaseDetails.recordedOn,
      (catches.concat(byCatches)).concat(removedCatches),
      coordinates[0],
      coordinates[1],
      this.trap?.status !== trapBaseDetails.status ? this.trap?.status : undefined,
      !this.trap,
      catches?.reduce((sum, current) => sum + current.number, 0) + trapBaseDetails.numberOfCatches,
      byCatches?.reduce((sum, current) => sum + current.number, 0) + trapBaseDetails.numberOfCatches,
      this.trap?.createdOn ?? new Date(),
      this.trap?.createdById ?? this.currentUserProvider.currentUser?.id
    );
  }
}
