import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FindService } from '../services/find.service';
import { Router } from '@angular/router';
import { MapStateService } from '../services/map-state.service';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { switchMap } from 'rxjs/operators';
import { AlertService } from 'src/app/shared/services/alert.service';

import { ToastController } from '@ionic/angular';
import { GeolocationService } from 'src/app/shared/services/geolocation.service';
import { LoaderService } from 'src/app/services/loader.service';
import { LaunchNavigatorService } from '../services/launch-navigator.service';

@Component({
  selector: 'app-map-filter',
  templateUrl: './map-filter.component.html',
  styleUrls: ['./map-filter.component.scss'],
})
export class MapFilterComponent extends OnDestroyMixin implements OnInit {
  @ViewChild('infinite-scroll') infiniteScroll: ElementRef;
  @ViewChild('search') search: any;

  inputValue: string;
  searchedDataNumFound: number;
  searchedDataPreview: any[] = [];

  constructor(
    public findService: FindService,
    private geolocationService: GeolocationService,
    private router: Router,
    private mapStateService: MapStateService,
    private alertService: AlertService,
    private toastController: ToastController,
    private loaderService: LoaderService,
    private launchNavigatorService: LaunchNavigatorService
  ) {
    super();
  }

  ngOnInit() {
    this.setFocus();
    this.findService.onSearchParamsChange()
      .pipe(
        switchMap(params => this.findService.getSearchedDataPreview(params)),
        untilComponentDestroyed(this)
      ).subscribe(res => {
        this.setFocus();

        this.searchedDataNumFound = res.response.numFound;

        if (this.searchedDataPreview) {
          this.searchedDataPreview = this.searchedDataPreview.concat(res.response.docs);
        } else {
          this.searchedDataPreview = res.response.docs;
        }
      });
  }

  async prepareParametersForSearchedValue() {
    const startIdx = this.searchedDataPreview ?
      (this.searchedDataPreview?.length + 15 < this.searchedDataNumFound ? this.searchedDataPreview?.length + 15 : 0) : 0;

    this.geolocationService.getCurrentPosition()
      .pipe(untilComponentDestroyed(this))
      .subscribe(location => {
        const parameters = {
          value: this.inputValue,
          latitude: location.coords.latitude,
          longitude: location.coords.longitude,
          startIndex: startIdx
        };

        this.findService.updateSearchParams(parameters);
      }, () => {
        this.toastController.create({
          message: `Voor deze functie is toegang tot uw locatie nodig!`,
          duration: 4000,
          position: 'bottom',
          color: 'danger'
        }).then(toast => toast.present());
      });
  }

  async onPreviewItemClick(item) {
    const res = await this.findService.getDataForSelectedPreviewItem(item).toPromise();
    const points = res.response.docs[0].centroide_rd.match(/\(([^)]+)\)/)[1].split(' ');
    this.router.navigate(['/maps'], { replaceUrl: true }).then(nav => {
      this.mapStateService.setCenter([points[0] * 1, points[1] * 1]);
      this.mapStateService.zoom = 12;

      this.alertService.getConfirmDialog('Navigeer naar locatie',
        'Wilt u een route plannen naar de geselecteerde locatie?', 'Nee', 'Ja',
        (result: boolean) => {
          if (result) {
            this.launchNavigatorService.openMapAppAndNavigateToSelectedLocation(res.response.docs[0].weergavenaam);
          }
        },
        { res: res.response.docs[0] }
      ).then(x => x.present());
    });
  }

  onInputValueChange(event: CustomEvent) {
    this.clearSearchedData();
    this.inputValue = event.detail.value;

    if (this.inputValue?.length >= 3) {
      this.prepareParametersForSearchedValue();
    }
  }

  clearSearchedData() {
    this.searchedDataPreview = null;
  }

  loadPreviewDataOnScroll(event) {
    setTimeout(() => {
      this.prepareParametersForSearchedValue();
      event.target.complete();

      if (this.searchedDataPreview.length >= this.searchedDataNumFound) {
        event.target.disabled = true;
      }
    }, 1000);
  }

  private setFocus(): void {
    setTimeout(() => {
      this.search.setFocus();
    }, 1);
  }
}
