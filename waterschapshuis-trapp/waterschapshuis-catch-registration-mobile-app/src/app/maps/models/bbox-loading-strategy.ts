import { Extent, containsExtent } from 'ol/extent';
import { LoadingStrategy } from 'ol/source/Vector';
import { Geoposition } from '@ionic-native/geolocation/ngx';
import { extentFromGeoposition, scaleExtent } from './extent.extensions';
import { ProjectionModel } from './projection.model';

export const emptyExtent: Extent = [0, 0, 0, 0];

/**
 * Default strategy for loading features based on the view's extent.
 */
export function bboxDefault(): LoadingStrategy {
  return (extent, resolution) => [extent];
}

/**
 * Strategy for loading features based on the extended view's extent.
 * Extent is extended to reduce number subsequent calls.
 */
export function bboxExtended(scaleFactor: number): LoadingStrategy {
  let extendedExtent = emptyExtent;

  return (extent: Extent, resolution: number) => {
    if (containsExtent(extendedExtent, extent)) {
      return [extent];
    }
    extendedExtent = scaleExtent(extent, scaleFactor);
    return [extendedExtent];
  };
}

/**
 * Strategy for loading features based on the geoposition.
 */
export function bboxFromGeoposition(getPosition: () => Geoposition, radius: number): LoadingStrategy {
  return (extent: Extent, resolution: number) => {
    const position = getPosition();
    if (!position || !position.coords) { return []; }

    const extentFromLocation = extentFromGeoposition(position, ProjectionModel.dutchMatrix, radius);
    return [extentFromLocation];
  };
}
