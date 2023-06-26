import { BaseGeoJsonFeaturesUpdater } from './base-geo-json-features-updater.model';
import { OverlayLayerName } from '../../shared/models/overlay-layer-name.enum';
import { TrapGeoJsonFeaturesUpdater } from './traps/trap-geo-json-features-updater.model.ts';
import { ObservationGeoJsonFeaturesUpdater } from './observations/observation-geo-json-features-updaters.model.ts';
import { GeoJsonUpdateCommand } from './geo-json-update-command.model';
import { Feature } from 'ol';

class NullFeatureUpdater extends BaseGeoJsonFeaturesUpdater {
  execute = (features: Feature[], geoJsonUpdateCommands: GeoJsonUpdateCommand[]): Feature[] => features;

  shouldAdd = (feature: Feature, command: GeoJsonUpdateCommand): boolean => false;
  shouldUpdate = (feature: Feature, command: GeoJsonUpdateCommand): boolean => false;
  shouldRemove = (feature: Feature, command: GeoJsonUpdateCommand): boolean => false;
  tryUpdateFeaturesWithChildCommands = (features: Feature[], geoJsonUpdateChildCommands: GeoJsonUpdateCommand[]): Feature[] => features;
}

export class GeoJsonFeaturesUpdaterFactory {
  public static get = (layerName: string): BaseGeoJsonFeaturesUpdater => {
    if (layerName.includes(OverlayLayerName.TrapDetails)) {
      return new TrapGeoJsonFeaturesUpdater();
    }
    if (layerName.includes(OverlayLayerName.Observations)) {
      return new ObservationGeoJsonFeaturesUpdater();
    }
    return new NullFeatureUpdater();
  }
}
