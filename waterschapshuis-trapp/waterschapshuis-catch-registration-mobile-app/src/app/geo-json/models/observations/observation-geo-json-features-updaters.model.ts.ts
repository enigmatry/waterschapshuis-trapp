import { GeoJsonUpdateCommand, CommandActionType } from '../geo-json-update-command.model';
import { BaseGeoJsonFeaturesUpdater } from '../base-geo-json-features-updater.model';
import { Feature } from 'ol';

export class ObservationGeoJsonFeaturesUpdater extends BaseGeoJsonFeaturesUpdater {

  shouldAdd = (feature: Feature, command: GeoJsonUpdateCommand): boolean =>
    command.actionType === CommandActionType.Add || !feature

  shouldUpdate = (feature: Feature, command: GeoJsonUpdateCommand): boolean =>
    feature.getProperties().Archived === command.properties.Archived

  shouldRemove = (feature: Feature, command: GeoJsonUpdateCommand): boolean =>
    feature.getProperties().Archived !== command.properties.Archived

  tryUpdateFeaturesWithChildCommands = (features: Feature[], geoJsonUpdateChildCommands: GeoJsonUpdateCommand[]): Feature[] => features;
}
