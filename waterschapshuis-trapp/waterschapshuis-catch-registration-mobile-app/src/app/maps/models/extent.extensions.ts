import Circle from 'ol/geom/Circle';
import { Coordinate } from 'ol/coordinate';
import { Extent, scaleFromCenter } from 'ol/extent';
import { Geoposition } from '@ionic-native/geolocation/ngx';
import { ProjectionLike, transform } from 'ol/proj';
import { ProjectionModel } from './projection.model';

export function extentFromCoordinate(position: Coordinate, radius: number): Extent {
  return new Circle(position, radius).getExtent();
}

export function extentFromGeoposition(position: Geoposition, projection: ProjectionLike, radius: number): Extent {
  const coordinate = transform([position.coords.longitude, position.coords.latitude], ProjectionModel.geodeticMatrix, projection);
  return extentFromCoordinate(coordinate, radius);
}

export function scaleExtent(extent: Extent, value: number): Extent {
  const newExtent = extent.slice() as Extent;
  scaleFromCenter(newExtent, value);
  return newExtent;
}
