import { Extent, containsExtent } from 'ol/extent';
import { LoadingStrategy } from 'ol/source/Vector';
import { scaleExtent } from './extent.extensions';

const emptyExtent: Extent = [0, 0, 0, 0];

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

