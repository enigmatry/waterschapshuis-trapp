import { Feature } from 'ol';
import GeoJSON from 'ol/format/GeoJSON';
import VectorLayer from 'ol/layer/Vector';
import { all } from 'ol/loadingstrategy';
import { transform } from 'ol/proj';
import VectorSource, { LoadingStrategy } from 'ol/source/Vector';
import { forkJoin } from 'rxjs';
import { AppSettings } from 'src/app/app-configuration/app-settings';
import { GeoJsonFeaturesUpdaterFactory } from 'src/app/geo-json/models/geo-json-features-updater-factory.model';
import { GeoJsonUpdateCommandsStoreService } from 'src/app/geo-json/services/geo-json-update-commands-store.service';
import { LocalGeoJsonService } from 'src/app/geo-json/services/local-geo-json.service';
import { NetworkService } from 'src/app/network/network.service';
import { GeolocationService } from 'src/app/shared/services/geolocation.service';
import WMTSTileGrid from 'ol/tilegrid/WMTS';
import TileLayer from 'ol/layer/Tile';
import WMTS from 'ol/source/WMTS';

import {
    IGetOverlayLayersResponseItem,
    IMapStyleLookup,
    IOverlayLayerCacheSettings,
    OverlayLayerCategoryCode,
    OverlayLayerLookupStrategy,
    OverlayLayerPlatformType,
    OverlayLayerType,
} from '../../api/waterschapshuis-catch-registration-mobile-api';
import { getStyle } from '../helpers/map-styles-helper';
import { ProjectionModel } from './projection.model';
import { bboxFromGeoposition, bboxExtended } from './bbox-loading-strategy';

export const MAP_OVERLAY_LAYERS = [
    OverlayLayerCategoryCode.MapAreas,
    OverlayLayerCategoryCode.MapLocations,
    OverlayLayerCategoryCode.DefaultLayers
];

export class OverlayLayer {
    readonly layerLookupKey: string = 'name';
    readonly maxLookupResolution: number = AppSettings.mapSettings.maxLookupResolution;

    displayName: string;
    url: string;
    name: string;
    fullName: string;
    categoryCode: OverlayLayerCategoryCode;
    categoryDisplayName: string;
    geometryFieldName: string;
    lookupStrategy: OverlayLayerLookupStrategy;
    selected: boolean;
    cacheSettings: IOverlayLayerCacheSettings;
    platformType: OverlayLayerPlatformType;
    type: OverlayLayerType;

    constructor(private response: IGetOverlayLayersResponseItem) {
        this.name = response.name;
        this.fullName = response.fullName;
        this.displayName = response.displayName;
        this.url = response.url;
        this.categoryCode = response.categoryCode;
        this.categoryDisplayName = response.categoryDisplayName;
        this.geometryFieldName = response.geometryFieldName;
        this.lookupStrategy = response.lookupStrategy;
        this.selected = false;
        this.cacheSettings = response.cacheSettings;
        this.platformType = response.platformType;
        this.type = response.type;
    }

    static fromResponse(response: IGetOverlayLayersResponseItem): OverlayLayer {
        return new OverlayLayer(response);
    }

    get isBBoxLookupStrategy(): boolean {
        return this.lookupStrategy !== OverlayLayerLookupStrategy.All;
    }

    getLoadingStrategy(geolocationService: GeolocationService): LoadingStrategy {
        switch (this.lookupStrategy) {
            case OverlayLayerLookupStrategy.All:
                return all;
            case OverlayLayerLookupStrategy.BBox:
                const scaleFactor = AppSettings.mapSettings.bboxExtendFactor;
                return bboxExtended(scaleFactor);
            case OverlayLayerLookupStrategy.Tracking:
                const radius = AppSettings.trackingSettings.trackingPointRadius * 1000;
                return bboxFromGeoposition(() => geolocationService.lastKnownPosition, radius);
            default:
                return all;
        }
    }

    createVectorLayer(
        styles: IMapStyleLookup[],
        featureProjection: string,
        localGeoJsonService: LocalGeoJsonService,
        geoJsonUpdateCommandsStore: GeoJsonUpdateCommandsStoreService,
        networkService: NetworkService,
        geolocationService: GeolocationService
    ) {
        const vectorSource = new VectorSource({
            format: new GeoJSON({
                dataProjection: ProjectionModel.dutchMatrix,
                featureProjection
            }),
            loader: (extent, resolution, projection) => {
                let extentInDutchProjection;
                if (this.isBBoxLookupStrategy) {
                    extentInDutchProjection = transform(extent, projection, ProjectionModel.dutchMatrix);
                }

                forkJoin({
                    localGeoJson: localGeoJsonService.getGeoJson(this, extentInDutchProjection),
                    geoJsonUpdateCommands: geoJsonUpdateCommandsStore.get(this.fullName)
                })
                    .subscribe(result => {
                        let features = (vectorSource.getFormat() as GeoJSON).readFeatures(result.localGeoJson);
                        if (networkService.isOffline()) {
                            vectorSource.clear();
                            features = GeoJsonFeaturesUpdaterFactory
                                .get(this.fullName)
                                .execute(features, result.geoJsonUpdateCommands);
                        }
                        vectorSource.addFeatures(features);
                    });
            },
            strategy: this.getLoadingStrategy(geolocationService)
        });

        const vectorLayer = new VectorLayer({
            minResolution: 0,
            maxResolution: this.isBBoxLookupStrategy ? this.maxLookupResolution : Infinity,
            source: vectorSource,
            zIndex: this.response.displayZIndex,
            style: (feature: Feature, resolution: any) => getStyle(feature.getProperties(), styles, this.response, resolution)
        });
        vectorLayer.set(this.layerLookupKey, this.response.fullName);

        return vectorLayer;
    }

    createWMTSTileLayer(layerLookupKey: string, grid: WMTSTileGrid) {
        const wmts = new WMTS({
            url: this.url,
            layer: this.name,
            matrixSet: ProjectionModel.dutchMatrix,
            tileGrid: grid,
            style: 'default'
        });

        const tileLayer = new TileLayer({
            zIndex: this.response.displayZIndex,
            source: wmts
        });

        tileLayer.set(layerLookupKey, this.fullName);
        return tileLayer;
    }
}
