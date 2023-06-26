import { GeoJsonUpdateCommand, CommandActionType } from '../geo-json-update-command.model';
import { BaseGeoJsonFeaturesUpdater } from '../base-geo-json-features-updater.model';
import { Feature } from 'ol';

export class TrapGeoJsonFeaturesUpdater extends BaseGeoJsonFeaturesUpdater {

  shouldAdd = (feature: Feature, command: GeoJsonUpdateCommand): boolean =>
    (command.actionType === CommandActionType.Add || !feature) && command.actionType !== CommandActionType.Delete

  shouldUpdate = (feature: Feature, command: GeoJsonUpdateCommand): boolean =>
    feature.getProperties().Status === command.properties.Status && command.actionType !== CommandActionType.Delete

  shouldRemove = (feature: Feature, command: GeoJsonUpdateCommand): boolean =>
    feature.getProperties().Status !== command.properties.Status || command.actionType === CommandActionType.Delete
}
