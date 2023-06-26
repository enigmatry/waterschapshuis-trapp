import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AreasService } from 'src/app/shared/services/areas.service';
import { GetLocationAreaDataResponse, GetLocationAreaDataResponseArea } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { NavController } from '@ionic/angular';
import { LoaderService } from 'src/app/services/loader.service';
import { finalize } from 'rxjs/operators';
import { Logger } from 'src/app/core/logger/logger';
import { ToastService } from 'src/app/services/toast.service';

enum AreaSelection {
  CatchArea = 'catch-area',
  SubArea = 'sub-area'
}

const logger = new Logger('AreaDataComponent');

@Component({
  selector: 'app-area-data',
  templateUrl: './area-data.component.html',
  styleUrls: ['./area-data.component.scss'],
})
export class AreaDataComponent implements OnInit {

  areaSelection = AreaSelection;

  data: GetLocationAreaDataResponse;
  areaData: GetLocationAreaDataResponseArea;

  catchAreaName: string;
  subAreaName: string;

  constructor(
    private router: Router,
    private service: AreasService,
    private nav: NavController,
    private loaderService: LoaderService,
    private toastService: ToastService) { }

  ngOnInit() {
    const state = this.router.getCurrentNavigation()?.extras?.state;
    const catchArea = state?.catchArea;
    const subarea = state?.subArea;
    this.catchAreaName = catchArea?.name;
    this.subAreaName = subarea?.name;
    this.loadData(catchArea.id, subarea.id).then()
      .catch(error => {
        logger.error(error);
        this.toastService.error('Fout opgetreden tijdens inlezen data');
      });
  }

  onAreaChanged(selection: AreaSelection) {
    switch (selection) {
      case AreaSelection.CatchArea:
        this.areaData = this.data?.catchArea;
        break;
      case AreaSelection.SubArea:
        this.areaData = this.data?.subArea;
        break;
    }
  }

  goBack() {
    this.nav.back();
  }

  private async loadData(catchAreaId: string, subAreaId: string) {
    const loader = await this.presentLoader();
    this.data = await this.service.getLocationData(catchAreaId, subAreaId)
    .pipe(finalize(() => loader.dismiss())).toPromise();

    this.areaData = this.data?.catchArea;
  }

  private async presentLoader() {
    const loader = await this.loaderService.createLoader();
    loader.present();

    return loader;
  }
}
