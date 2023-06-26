import { Component, OnDestroy, OnInit } from '@angular/core';
import { flatMap, map, take } from 'rxjs/operators';
import { SideBarActions } from 'src/app/core/side-bar/side-bar-abstract/side-bar-actions.enum';
import { ISideBarHeaderConfig } from 'src/app/core/side-bar/side-bar-abstract/side-bar-header-config.interface';
import { SideBarMode } from 'src/app/core/side-bar/side-bar-abstract/side-bar-mode.enum';
import { SideBarPosition } from 'src/app/core/side-bar/side-bar-abstract/side-bar-positions.enum';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { ToastService } from 'src/app/shared/toast/toast.service';
import { OverlayLayer } from '../../shared/models/overlay-layer.model';
import { MapStateService } from '../services/map-state.service';
import { SideBarRightComponent } from '../side-bar-right/side-bar-right.component';
import { SideBarComponent } from '../side-bar/side-bar.component';
import { AppMap } from 'src/app/shared/models/app-map.model';
import { MapBrowserEvent } from 'ol';
import { NgxPermissionsService } from 'ngx-permissions';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import VectorLayer from 'ol/layer/Vector';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { TrapsService } from '../services/traps.service';
import { generateUrlWithFilter, MapFilter } from 'src/app/shared/models/map-filter.model';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';

@Component({
  selector: 'app-map-overview',
  templateUrl: './map-overview.component.html',
  styleUrls: ['./map-overview.component.scss']
})
export class MapOverviewComponent extends OnDestroyMixin implements OnInit, OnDestroy {
  private map: AppMap;
  layerLookupKey = 'name';
  private overlayLayers: OverlayLayer[] = [];
  currentFilterApplied: MapFilter;

  sideBarLeftHeaderConfig: ISideBarHeaderConfig = { showBackButton: true, showExpansionButton: true, title: 'Kaart', expanded: false };

  constructor(
    private mapStateService: MapStateService,
    public trapService: TrapsService,
    private sideBarService: SideBarService,
    private toastService: ToastService,
    private permissionsService: NgxPermissionsService
  ) {
    super();
  }

  ngOnInit(): void {
    this.sideBarService.registerSideBar({
      type: SideBarComponent, position: SideBarPosition.start, header: this.sideBarLeftHeaderConfig
    });
    this.sideBarService.registerSideBar({ type: SideBarRightComponent, position: SideBarPosition.end });
    this.sideBarService.toggleSideBar({
      type: SideBarComponent, position: SideBarPosition.start, action: SideBarActions.open, mode: SideBarMode.over
    });

    this.map = new AppMap(
      'map',
      this.permissionsService.getPermission(PolicyName.MapContentRead) ? this.mapSingleClickCallback : null,
      this.permissionsService.getPermission(PolicyName.MapContentWrite) ? this.mapMoveEndCallback : null);
    this.mapStateService.init();

    this.mapStateService.geoDataOptions
      .pipe(untilComponentDestroyed(this))
      .subscribe(this.map.addBackgroundLayer);

    this.mapStateService.contentLayersToRefresh
      .pipe(
        map((query: string) => this.handleContentLayersRefresh(query)))
      .subscribe();

    this.mapStateService.mapStyles.pipe(
      flatMap(styles => {
        this.map.addStyles(styles);
        // fetch overlay layers after styles are loaded
        return this.mapStateService.overlayLayers;
      }),
      untilComponentDestroyed(this))
      .subscribe(layers => {
        this.overlayLayers = layers;
        this.handleOverlayLayers(layers); });

    this.mapStateService.mapFilter
      .pipe(untilComponentDestroyed(this))
      .subscribe(filterData => {
        this.currentFilterApplied = filterData;
        this.handleFilterChange(filterData);
      });
  }

  private handleOverlayLayers = (layers: OverlayLayer[]) =>
    layers.forEach(x => {
      if (!x.selected) {
        this.map.disposeLayerById(x.fullName);
        return;
      }
      if (!x.fullName.startsWith(OverlayLayerName.TrapDetails)) {
        this.map.tryAddOverlayLayer(x);
      } else {
        x.url = generateUrlWithFilter(x.url, this.currentFilterApplied);
        this.map.addOrReplaceExistingOverlayLayer(x);
      }
    })

  private mapSingleClickCallback = (event: MapBrowserEvent) => {
    const pointFeatures = this.map
      .getFeaturesAtPixel(event.pixel)
      .filter(x => x.getGeometry().getType() === 'Point');

    const trapIds = [];
    const observationIds = [];

    pointFeatures.forEach(tl => {
      if (tl.getProperties().TrapId) {
        trapIds.push(tl.getProperties().TrapId);
      } else if (tl.getProperties().Id) {
        observationIds.push(tl.getProperties().Id);
      }
    });

    const totalItems = trapIds.length + observationIds.length;

    if (totalItems === 0) { return; }

    if (totalItems <= 20) {
      // remove previous trap info
      this.sideBarService
        .toggleSideBar({ position: SideBarPosition.end, data: { trapIds: [], observationIds: [] }, action: SideBarActions.open });

      this.sideBarService
        .toggleSideBar({ position: SideBarPosition.end, data: { trapIds, observationIds }, action: SideBarActions.open });
    } else {
      this.toastService
        .showErrorMessage('U heeft meer dan 20 vangmiddelen geselecteerd. Zoom verder in en selecteer minder vangmiddelen.');
    }
  }

  private mapMoveEndCallback = (event: MapBrowserEvent) => {
    this.mapStateService.setCenter(event.frameState.viewState.center);
  }

  private handleContentLayersRefresh(query: string): any {
    this.map.getLayers()
      .getArray()
      .filter(mapLayer => mapLayer.get(this.layerLookupKey).includes(query))
      .forEach(mapLayer => {
        (mapLayer as VectorLayer).getSource().refresh();
      });
  }

  private async handleFilterChange(filter: MapFilter): Promise<void> {
    this.overlayLayers.forEach(x => {
      if (!x.selected) {
        this.map.disposeLayerById(x.fullName);
        return;
      }
      x.url = generateUrlWithFilter(x.url, filter);
      this.map.addOrReplaceExistingOverlayLayer(x);
    });
  }

  ngOnDestroy(): void {
    this.sideBarService.toggleSideBar({ position: SideBarPosition.start, action: SideBarActions.close });
    this.sideBarService.toggleSideBar({ position: SideBarPosition.end, action: SideBarActions.close });
  }
}
