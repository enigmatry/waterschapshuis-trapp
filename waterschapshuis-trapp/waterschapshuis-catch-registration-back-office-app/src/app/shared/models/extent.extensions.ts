import Circle from 'ol/geom/Circle';
import { Coordinate } from 'ol/coordinate';
import { Extent, scaleFromCenter } from 'ol/extent';

export function extentFromCoordinate(position: Coordinate, radius: number): Extent {
  return new Circle(position, radius).getExtent();
}

export function scaleExtent(extent: Extent, value: number): Extent {
  const newExtent = extent.slice() as Extent;
  scaleFromCenter(newExtent, value);
  return newExtent;
}
