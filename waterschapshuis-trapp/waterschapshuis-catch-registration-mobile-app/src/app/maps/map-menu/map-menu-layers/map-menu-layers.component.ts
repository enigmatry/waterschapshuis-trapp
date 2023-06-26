import { Component, OnInit } from '@angular/core';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import {
  MapNetworkType,
  OverlayLayerCategoryCode,
  OverlayLayerPlatformType,
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { NetworkService } from 'src/app/network/network.service';

import { BackgroundLayer } from '../../models/background-layer.model';
import { OverlayLayer } from '../../models/overlay-layer.model';
import { MapStateService } from '../../services/map-state.service';
import { OfflineMapService } from '../../services/offline-map.service';

@Component({
  selector: 'app-map-menu-layers',
  templateUrl: './map-menu-layers.component.html',
  styleUrls: ['./map-menu-layers.component.scss'],
})
export class MapMenuLayersComponent extends OnDestroyMixin implements OnInit {
  data: any;
  backgroundLayers: BackgroundLayer[] = [];
  component: any = this;
  selectedBackgroundLayer: string;
  areaOverlayLayers: OverlayLayer[] = [];
  locationOverlayLayers: OverlayLayer[] = [];
  private overlayLayers: OverlayLayer[] = [];

  constructor(
    public mapStateService: MapStateService,
    private offlineService: OfflineMapService,
    private networkService: NetworkService
  ) { super(); }

  ngOnInit() {
    this.getAvailableBackgroundLayers();
    this.overlayLayers = this.mapStateService.availableOverlayLayersData
      .filter(x => x.platformType !== OverlayLayerPlatformType.BackofficeOnly);
    this.areaOverlayLayers = this.overlayLayers
      .filter(x => x.categoryCode === OverlayLayerCategoryCode.MapAreas);
    this.locationOverlayLayers = this.overlayLayers
      .filter(x => x.categoryCode === OverlayLayerCategoryCode.MapLocations);
    this.selectOverlayLayer();
  }

  private getAvailableBackgroundLayers() {
    this.offlineService.offlineMapAvailabilityStatus().pipe(untilComponentDestroyed(this.component)).subscribe(offlineMapStatus => {
      if (offlineMapStatus.available) {
        this.backgroundLayers = this.mapStateService.availableBackgroundLayersData;
      } else {
        this.backgroundLayers = this.mapStateService.availableBackgroundLayersData.filter(layers => layers.name !== 'osm');
      }
    });
  }

  selectLayer(clickedLayer: any): void {
    this.mapStateService.currentlyActiveBackgroundLayer = clickedLayer.key;

    const selectedLayer = this.backgroundLayers.find(
      layer => layer.key === clickedLayer.key);

    if (selectedLayer.networkType === MapNetworkType.Offline) {
      this.networkService.forceOfflineMode();
    } else {
      this.networkService.forceOnlineMode();
    }

    this.mapStateService.updateBackgroundLayers([selectedLayer]);
    this.selectDefaultOverlayLayer(selectedLayer.defaultOverlayLayer);
  }

  selectOverlayLayer(): void {
    this.mapStateService.updateOverlayLayers(this.overlayLayers);
  }

  selectDefaultOverlayLayer(defaultOverlayLayer: string) {
    const hiddenLayers = this.overlayLayers.filter(x => x.categoryCode === OverlayLayerCategoryCode.DefaultLayers);
    hiddenLayers.forEach(x => {
      x.selected = false;
    });
    if (defaultOverlayLayer) {
      this.overlayLayers.find(x => x.fullName.indexOf(defaultOverlayLayer) > -1).selected = true;
    }
    this.selectOverlayLayer();
  }
}
