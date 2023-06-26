import { Feature } from 'ol';
import Point from 'ol/geom/Point';
import { transform } from 'ol/proj';

export enum CommandActionType {
  Add = 0,
  Update = 1,
  Delete = 2
}

export class GeoJsonUpdateCommand {
  coordinates: number[];

  get propertiesId(): string {
    return this.properties[this.propertiesIdFieldName];
  }

  constructor(
    public actionType: CommandActionType,
    public longitude: number | undefined,
    public latitude: number | undefined,
    public properties: any,
    public propertiesIdFieldName: string,
    public layerName: string,
    public layerNameSuffix: string = '',
    public timestamp: Date,
    public sourceMapProjection: string | undefined,
    public destinationMapProjection: string | undefined
  ) {
    this.setCoordinates(longitude, latitude);
  }

  asGeoJsonFeature = (): Feature => {
    const result = new Feature();
    result.setGeometry(new Point(this.coordinates));
    result.setProperties(this.properties);
    return result;
  }

  setCoordinates = (longitude: number | undefined, latitude: number | undefined) => {
    if (longitude && latitude) {
      this.coordinates = transform([longitude, latitude], this.sourceMapProjection, this.destinationMapProjection);
    }
  }

  relatesToLayerWithName = (requestedLayerFullName: string): boolean =>
    requestedLayerFullName.includes(this.layerName) &&
    (this.layerNameSuffix ? requestedLayerFullName.includes(this.layerNameSuffix) : true)
}
