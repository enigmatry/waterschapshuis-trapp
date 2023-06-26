import { formatDate } from '@angular/common';
import { AfterViewInit, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Camera, CameraOptions } from '@ionic-native/camera/ngx';
import { ActionSheetController, NavController, Platform } from '@ionic/angular';
import { Guid } from 'guid-typescript';
import { noop } from 'rxjs';
import { INamedEntityItem } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Logger } from 'src/app/core/logger/logger';
import { NetworkService } from 'src/app/network/network.service';
import { LoaderService } from 'src/app/services/loader.service';
import { ToastService } from 'src/app/services/toast.service';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { environment } from 'src/environments/environment';

import { untilComponentDestroyed, OnDestroyMixin } from '@w11k/ngx-componentdestroyed';
import { transform } from 'ol/proj';
import { ObservationResponse } from 'src/app/maps/models/observation-response.model';
import { MapStateService } from 'src/app/maps/services/map-state.service';
import { ObservationService } from 'src/app/maps/services/observation.service';
import { ObservationUpdateService } from 'src/app/maps/services/observation-update.service';
import { ProjectionModel } from 'src/app/maps/models/projection.model';
import { finalize } from 'rxjs/operators';

const logger = new Logger('ObservationComponent');

@Component({
  selector: 'app-observation-details',
  templateUrl: './observation-details.component.html',
  styleUrls: ['./observation-details.component.scss'],
})
export class ObservationDetailsComponent extends OnDestroyMixin implements AfterViewInit {
  clickedImage: string;
  form: FormGroup;

  isUpdateMode: boolean;
  observation: ObservationResponse;
  isOfflineMode: boolean;

  options: CameraOptions = {
    quality: 60,
    encodingType: this.camera.EncodingType.JPEG,
    mediaType: this.camera.MediaType.PICTURE,
    destinationType: this.camera.DestinationType.DATA_URL,
    correctOrientation: true
  };

  observationTypes: INamedEntityItem[];

  constructor(
    public nav: NavController,
    private platform: Platform,
    private camera: Camera,
    private fb: FormBuilder,
    private actionsheetCtrl: ActionSheetController,
    private mapStateService: MapStateService,
    private observationService: ObservationService,
    private observationUpdateService: ObservationUpdateService,
    private toastService: ToastService,
    private networkService: NetworkService,
    private route: ActivatedRoute,
    private loaderService: LoaderService
  ) {
    super();
    this.createForm();

    this.observationTypes = [
      { name: 'Schade', id: '1' },
      { name: 'Overig', id: '2' }
    ];
  }

  ngAfterViewInit() {
    this.initView().then(noop);
  }

  async createOrUpdateObservation() {
    if (this.isUpdateMode) {
      const type = this.form.value.type;
      const remarks = this.form.value.remarks;
      await this.updateObservation({ ...this.observation, type, remarks });
    } else {
      await this.createObservation();
    }
  }

  cancel(): void {
    this.nav.navigateRoot('/maps');
  }

  async getPhoto(): Promise<void> {
    const actionSheet = await this.actionsheetCtrl.create({
      header: 'Option',
      buttons: [
        {
          text: 'Maak een foto',
          role: 'destructive',
          icon: this.platform.is('ios') ? 'ios-camera-outline' : null,
          handler: () => {
            this.platform.ready().then(() => { this.takePhoto(); });
          }
        },
        {
          text: 'Kies een foto uit de galerij',
          icon: this.platform.is('ios') ? 'ios-images-outline' : null,
          handler: () => {
            this.platform.ready().then(() => { this.openGallery(); });
          }
        },
      ]
    });
    await actionSheet.present();
  }

  takePhoto(): void {
    const options = {
      ...this.options,
      sourceType: this.camera.PictureSourceType.CAMERA
    };

    this.camera.getPicture(options).then((imageData) => {
      this.handleImage(imageData);
    }, (err) => {
      logger.error(err);
    });
  }

  openGallery(): void {

    const options = {
      ...this.options,
      sourceType: this.camera.PictureSourceType.SAVEDPHOTOALBUM
    };

    this.camera.getPicture(options).then((imageData) => {
      this.handleImage(imageData);
    }, (err) => {
      logger.error(err);
    });
  }

  sendObservationEmail(): void {
    const coords = transform(
      [this.observation.longitude, this.observation.latitude],
      ProjectionModel.dutchMatrix,
      ProjectionModel.geodeticMatrix
    );

    const emailBody =
      `Observatie van type: ${this.observationTypes.find(t => t.id === this.observation.type).name}
      Wordt gemeld door: ${this.observation?.createdBy}
      Op datum: ${formatDate(this.observation.createdOn, 'dd-MM-yyyy', 'en-US').toString()}
      Met opmerkingen: ${this.observation?.remarks}
      Ga direct naar locatie in Google Maps:
      https://www.google.com/maps/search/?api=1&query=${coords[1].toFixed(6)},${coords[0].toFixed(6)}&zoom=15`;

    const email = {
      attachments: [],
      subject: 'Melding',
      body: emailBody,
      isHtml: false
    };

    if (this.clickedImage) {
      const imageName = this.clickedImage.split('observations/').pop();
      const containerName = environment.azureStorage.baseObservationBlobContainer;
      this.observationService.downloadImage(decodeURIComponent(imageName))
        .pipe(untilComponentDestroyed(this))
        .subscribe(res => {
          this.observationService.sendEmail(containerName, this.observation, email, res);
        });
    } else {
      this.observationService.openEmailComposer(email);
    }
  }

  async archiveActiveObservation() {
    await this.updateObservation({ ...this.observation, archived: true });
  }

  private async updateObservation(value: any) {
    const loader = await this.startLoading();
    this.observationUpdateService.updateObservation(value)
      .pipe(finalize(async () => await loader.dismiss()))
      .subscribe(() => {
        this.mapStateService.refreshLayers(OverlayLayerName.Observations);
        this.nav.navigateBack('/maps');
      }, error => {
        logger.error(error, 'Error occurred while updating observation.');
        this.toastService.error('Fout opgetreden');
      });
  }

  private async createObservation() {
    const loader = await this.startLoading();
    const coordinates = this.mapStateService.getCenterCoordinatesInSpecifiedProjection(ProjectionModel.dutchMatrix);

    const cmd = {
      id: Guid.create().toString(),
      type: this.form.value.type,
      image: this.form.value.image,
      remarks: this.form.value.remarks,
      longitude: coordinates[0],
      latitude: coordinates[1],
      recordedOn: new Date(),
      syncedToApi: 0
    };

    this.observationService.addObservation(cmd)
      .pipe(untilComponentDestroyed(this), finalize(async () => await loader.dismiss()))
      .subscribe(
        () => {
          this.toastService.success('Melding succesvol bewaard');
          this.nav.navigateRoot('/maps');
        },
        err => {
          logger.error(err, 'Error saving observation');
          this.toastService.error('Fout opgetreden');
        }
      );
  }

  private async initView(): Promise<void> {
    const loader = await this.startLoading();

    try {
      await this.initData();
    } catch (error) {
      logger.error(error, 'Error occurred while initializing observation data.');
      this.toastService.error('Fout opgetreden tijdens inlezen data');
    }

    await loader.dismiss();
  }

  private async initData() {
    this.isUpdateMode = false;
    const id = this.route.snapshot.params?.observationId;
    if (id) {
      this.observation = await this.observationService.get(id).toPromise();
      this.isUpdateMode = true;
    }

    this.isOfflineMode = this.networkService.isOffline();

    if (this.isUpdateMode) {
      if (this.observation?.photoUrl && !this.isOfflineMode) {
        this.clickedImage = this.observation.photoUrl;
      } else if (this.observation?.image && this.isOfflineMode) {
        this.clickedImage = this.observation?.image;
      }
      this.form.patchValue(this.observation);
    }
  }

  private handleImage(imageData: any): void {
    this.clickedImage = 'data:image/jpeg;base64,' + imageData;
  }

  private createForm(): void {
    this.form = this.fb.group({
      type: [null, Validators.required],
      image: [null],
      remarks: [null, [Validators.required, Validators.maxLength(250)]]
    });
  }

  private async startLoading() {
    const loader = await this.loaderService.createLoader();
    await loader.present();
    return loader;
  }
}
