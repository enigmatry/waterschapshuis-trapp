import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavController } from '@ionic/angular';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { AppSettingsService } from 'src/app/shared/services/app-settings.service';

import { OverlayLayer } from '../../models/overlay-layer.model';
import { MapStateService } from '../../services/map-state.service';
import { LayerFilter } from '../../models/layer-filter.model';

@Component({
  selector: 'app-map-tracking-filter',
  templateUrl: './map-tracking-filter.component.html',
  styleUrls: ['./map-tracking-filter.component.scss']
})
export class MapTrackingFilterComponent implements OnInit {

  form: FormGroup;
  today: string = new Date().toISOString();

  currentFilter: LayerFilter;

  constructor(
    private fb: FormBuilder,
    private navController: NavController,
    private appSettingsService: AppSettingsService,
    private mapStateService: MapStateService
  ) {
    this.createForm();
  }

  ngOnInit(): void {
    this.currentFilter = this.appSettingsService.getMapLayerFilter();

    const dateToday = new Date();

    const initailFormValue = {
      start: this.currentFilter ?
        this.currentFilter.start?.toISOString() :
        new Date(dateToday.setMonth(dateToday.getMonth() - 3)).toISOString(),
      end: this.currentFilter ?
        this.currentFilter.end?.toISOString() :
        new Date().toISOString()
    };

    this.form.patchValue(initailFormValue);
  }

  applyFilter(): void {
    const startDate = new Date(this.form.get('start').value);
    const endDate = new Date(this.form.get('end').value);
    const layerFullName = this.getCurrentLayer().fullName;

    const filter = LayerFilter.create(
      layerFullName,
      startDate,
      endDate,
      null
    );

    this.filterTrackingLines(filter);

    this.navController.back();
  }

  showAll(): void {
    this.filterTrackingLines();
    this.appSettingsService.setMapLayerFilter(undefined);
    this.navController.back();
  }

  private filterTrackingLines(filter?: LayerFilter): void {

    this.applyFilterOnTrackingLayer(filter, this.mapStateService.availableOverlayLayersData);

    this.mapStateService.refreshLayers(OverlayLayerName.TrackingLines);
  }

  private createForm(): void {
    this.form = this.fb.group({
      start: [null, Validators.required],
      end: [null, Validators.required]
    });
  }

  private getCurrentLayer(currentLayers?: Array<OverlayLayer>): OverlayLayer {
    currentLayers = currentLayers || this.mapStateService.availableOverlayLayersData;
    return currentLayers.find(l => l.fullName.startsWith(OverlayLayerName.TrackingLines));
  }

  private applyFilterOnTrackingLayer(filter: LayerFilter, currentLayers: Array<OverlayLayer>): void {

    const trackingLayer = this.getCurrentLayer(currentLayers);

    if (!filter) {
      filter = LayerFilter.create(
        trackingLayer.fullName,
        undefined,
        undefined,
        this.currentFilter?.predefinedFilter
      );
    }
    trackingLayer.url = filter.apply(trackingLayer.url);

    this.appSettingsService.setMapLayerFilter(filter);
  }
}
