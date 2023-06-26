import { Component, OnInit } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import {
  OverlayLayerCategoryCode,
  OverlayLayerPlatformType,
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { AppSettings } from 'src/app/app-configuration/app-settings';
import { SideBarMode } from 'src/app/core/side-bar/side-bar-abstract/side-bar-mode.enum';
import { SideBarPosition } from 'src/app/core/side-bar/side-bar-abstract/side-bar-positions.enum';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { MapService } from 'src/app/shared/services/map.service';

import { ISideBar } from '../../core/side-bar/side-bar-abstract/side-bar.interface';
import { BackgroundLayer } from '../../shared/models/background-layer.model';
import { OverlayLayer } from '../../shared/models/overlay-layer.model';
import { MapStateService } from '../services/map-state.service';

class LayerCategory {
  constructor(public displayName: string, public layers: OverlayLayer[]) { }
}

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent extends OnDestroyMixin implements ISideBar, OnInit {
  data: any;
  backgroundLayers: BackgroundLayer[] = [];
  overlayLayerCategories: LayerCategory[] = [];
  private overlayLayers: OverlayLayer[] = [];
  defaultLayerCategory = 'NationalGeoRegister';

  constructor(
    private mapStateService: MapStateService,
    private sideBarService: SideBarService,
    private mapService: MapService
  ) {
    super();
  }

  open(): void { }

  close(): void { }

  toggle(): void { }

  ngOnInit(): void {
    this.mapService
      .getBackgroundLayers()
      .pipe(untilComponentDestroyed(this))
      .subscribe(backgroundLayers => {
        this.backgroundLayers = backgroundLayers;
        const selectedLayer = this.backgroundLayers.find(l => l.key === AppSettings.defaultSelectedLayers.background);
        this.selectLayer(selectedLayer.key);
      });

    this.mapService
      .getOverlayLayers([
        OverlayLayerCategoryCode.MapAreas,
        OverlayLayerCategoryCode.MapLocations,
        OverlayLayerCategoryCode.DefaultLayers
      ])
      .pipe(untilComponentDestroyed(this))
      .subscribe(response => {
        this.overlayLayers = response.filter(x => x.platformType !== OverlayLayerPlatformType.MobileOnly);
        this.overlayLayerCategories = this.createOverlayLayerCategories(this.overlayLayers);
        this.selectOverlayLayers();
      });
  }

  scrollToTop(...params: string[]) {
    params.forEach(id => {
      const el = document.getElementById(id);
      el.scrollTo(0, 0);
    });
  }

  public selectLayer(layerKey: string) {
    const selectedLayer = this.backgroundLayers.find(
      layer => layer.key === layerKey);
    this.mapStateService.updateBackgroundLayers([selectedLayer]);
    this.selectDefaultOverlayLayer(selectedLayer.defaultOverlayLayer);
  }

  selectOverlayLayers(): void {
    this.mapStateService.updateOverlayLayers(this.overlayLayers);
  }

  selectDefaultOverlayLayer(defaultOverLayLayer: string) {
    const hiddenLayers = this.overlayLayers.filter(x => x.categoryCode === OverlayLayerCategoryCode.DefaultLayers);
    hiddenLayers.forEach(x => {
      x.selected = false;
    });
    if (defaultOverLayLayer) {
      this.overlayLayers.find(x => x.fullName.indexOf(defaultOverLayLayer) > -1).selected = true;
    }
    this.selectOverlayLayers();
  }

  toggleSideBar(): void {
    this.sideBarService.toggleSideBar({ position: SideBarPosition.start, mode: SideBarMode.over, action: 'toggle' });
  }

  private createOverlayLayerCategories = (overlayLayers: OverlayLayer[]): LayerCategory[] => {
    const result = new Array<LayerCategory>();
    const categoryNames = overlayLayers
      .map(x => x.categoryDisplayName)
      .filter((val, index, self) => self.indexOf(val) === index);

    categoryNames.forEach(categoryName =>
      result.push(new LayerCategory(categoryName, overlayLayers.filter(x => x.categoryDisplayName === categoryName)))
    );

    return result;
  }
}
