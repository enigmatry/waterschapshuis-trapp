import { GeoJsonUpdateCommand, CommandActionType } from './geo-json-update-command.model';
import { Feature } from 'ol';
import Point from 'ol/geom/Point';

export abstract class BaseGeoJsonFeaturesUpdater {

  execute(features: Feature[], geoJsonUpdateCommands: GeoJsonUpdateCommand[]): Feature[] {
    geoJsonUpdateCommands
      .filter(x =>
        x.actionType === CommandActionType.Add ||
        x.actionType === CommandActionType.Update ||
        x.actionType === CommandActionType.Delete)
      .sort((x, y) => new Date(x.timestamp).getTime() - new Date(y.timestamp).getTime())
      .forEach(command => {
        const feature = this.selectFeature(features, command);
        if (this.shouldAdd(feature, command)) {
          features.push(command.asGeoJsonFeature());
        } else if (this.shouldUpdate(feature, command)) {
          feature.setGeometry(new Point(command.coordinates));
          feature.setProperties(Object.assign(feature.getProperties(), command.properties));
        } else if (this.shouldRemove(feature, command)) {
          features = features.filter(x => x.getProperties()[command.propertiesIdFieldName] !== command.propertiesId);
        }
      });

    return features;
  }

  protected selectFeature = (features: Feature[], command: GeoJsonUpdateCommand)
    : Feature => features.find(x => x.getProperties()[command.propertiesIdFieldName] === command.propertiesId)

  abstract shouldAdd(feature: Feature, command: GeoJsonUpdateCommand): boolean;
  abstract shouldUpdate(feature: Feature, command: GeoJsonUpdateCommand): boolean;
  abstract shouldRemove(feature: Feature, command: GeoJsonUpdateCommand): boolean;
}
