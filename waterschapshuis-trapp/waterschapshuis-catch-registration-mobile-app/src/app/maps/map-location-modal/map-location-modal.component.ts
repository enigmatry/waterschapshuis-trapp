import { Component, OnInit } from '@angular/core';
import { NavParams, ModalController } from '@ionic/angular';
import { transform } from 'ol/proj';
import { ProjectionModel } from '../models/projection.model';
import { LaunchNavigatorService } from '../services/launch-navigator.service';
import { AreasService } from 'src/app/shared/services/areas.service';
import { AreaDetails } from 'src/app/shared/models/area-details';
import { Router } from '@angular/router';

@Component({
  selector: 'app-map-location-modal',
  templateUrl: './map-location-modal.component.html',
  styleUrls: ['./map-location-modal.component.scss'],
})
export class MapLocationModalComponent implements OnInit {
  coordinates: Array<number>;
  coords: string;
  areaDetails: AreaDetails;

  constructor(
    private router: Router,
    private navParams: NavParams,
    private modalController: ModalController,
    private launchNavigator: LaunchNavigatorService,
    private areasService: AreasService
  ) { }

  ngOnInit() {
    this.coordinates = this.navParams.get('coordinate');
    this.coords = this.coordinates[0].toFixed(2) + ', ' + this.coordinates[1].toFixed(2);

    this.areasService.getLocationDetails(this.coordinates[0], this.coordinates[1])
      .subscribe(result =>
        this.areaDetails = result
      );
  }

  navigateToSelectedPlace() {
    const coords = transform(
      [this.coordinates[0], this.coordinates[1]],
      ProjectionModel.dutchMatrix,
      ProjectionModel.geodeticMatrix
    );

    this.launchNavigator.openMapAppAndNavigateToSelectedLocation([coords[1], coords[0]]);
  }

  openTimeRegistration() {
    this.modalController.dismiss();
    this.router.navigate(['/time-registration'], { state: { location: this.areaDetails } });
  }

  openLocationData() {
    this.modalController.dismiss();
    this.router.navigate(['/areas'], {
      state: {
        catchArea: this.areaDetails.catchArea,
        subArea: this.areaDetails.subArea
      }
    });
  }
}
