import { Fill, RegularShape, Stroke, Style, Text, Icon } from 'ol/style';

import {
  IMapStyleLookup,
  IMapStyleLookupKey,
  MapStyleLookupKeyCode,
  IGetOverlayLayersResponseItem
} from '../waterschapshuis-catch-registration-backoffice-api';
import { getColorForPropertyValue } from './color-style-helper';
import Point from 'ol/geom/Point';

export { getStyle };

const pointStyle = {};
const strokeStyles = {};
const fillStyles = {};
let patternFillStyle: Style;

const catchLabelFillColor = '#FFFFFF';
const labelVisibilityMaxResolution = 100;

const mapAreaLabelSettings = { strokeColor: '#000000', width: 2, font: '17px Verdana' };

const polygonStrokeColor = '#000000';

const iconsFolder = '../../assets/icons/';

const image = new Image();
image.onload = () => {
const ctx = document.createElement('canvas').getContext('2d');
const pattern = ctx.createPattern(image, 'repeat');
patternFillStyle  = new Style({
    fill: new Fill({
      color: pattern
    }),
    stroke: new Stroke({
      color: polygonStrokeColor,
      width: 0.1
    })
  });
};
image.src = '../../assets/img/Patern.PNG';

const getStyle = (properties: any, styles: IMapStyleLookup[], response: IGetOverlayLayersResponseItem, resolution: any) => {
  if (properties.geometry instanceof Point) {
    return getPointStyle(createKey(response, properties), styles, resolution, properties.NumberOfCatches);
  } else {
    return getPolygonStyle(response.color, response.width, properties);
  }
};

const getPointStyle = (key: IMapStyleLookupKey, styles: IMapStyleLookup[], resolution: any, numberOfCatches: number) => {
  const style = pointStyle[generateKey(key)] || createPointStyle(key, styles);
  if (style) {
    style.setText(createPointTextStyle(numberOfCatches, key, resolution));
  }
  return style;
};

const getPolygonStyle = (strokeColor: string, width: number, properties: any) => {
  if (!!strokeColor) {

    if (properties.hasOwnProperty('DaysOffset')) {
      [width, strokeColor] = getStyleForTrackingLine(properties.DaysOffset as number);
    }

    const style = strokeStyles[strokeColor + width] || createStrokeStyle(strokeColor, width);
    if (style) {
      style.setText(createPolygonTextStyle(strokeColor, properties.Name));
    }
    return style;
  } else {
    const fillcolor = getColorForPropertyValue(properties);
    return fillcolor ?  fillStyles[fillcolor] || createFillStyle(fillcolor)
    :  patternFillStyle;
  }
};

const getStyleForTrackingLine = (daysOffset: number): [number, string] => {
  const redColor = '#cc0000';
  const greenColor = '#3B9C14';
  const purpleColor = '#a64d79';
  const orangeColor = '#ffa600';

  return daysOffset <= 1 ? [5, redColor]
    : daysOffset > 1 && daysOffset <= 7 ? [4, purpleColor]
    : daysOffset > 7 && daysOffset <= 7 * 3 ? [3, greenColor]
    : [2, orangeColor];
};

const createPointStyle = (key: IMapStyleLookupKey, styles: IMapStyleLookup[]) => {
  const config = findConfig(key, styles);

  if (!config) { return null; }

  const result = new Style({
      image: new Icon({ src: iconsFolder + config.iconName })
  });

  pointStyle[generateKey(key)] = result;

  return result;
};

const createPolygonTextStyle = (color: string, text: string) => {
  if (text) {
    return new Text({
      text,
      font: mapAreaLabelSettings.font,
      fill: new Fill({
        color
      }),
      stroke: new Stroke({
        color: mapAreaLabelSettings.strokeColor,
        width: mapAreaLabelSettings.width
      })
    });
  }
};

const createPointTextStyle = (value: number, key: IMapStyleLookupKey, resolution: any) => {
  if (value && resolution < labelVisibilityMaxResolution) {
    return new Text({
      text: value.toString(),
      padding: [0, 0, 0, 1],
      fill: new Fill({
        color: '#FFFFFF'
      }),
      offsetY: key.trapTypeId === '5fa2dc7f-8c1a-1255-a41e-6bf28b183def' ? 5 : 0,
      stroke: new Stroke({
        color: '#000000',
        width: 3
      })
    });
  }
};

const findConfig = (key: IMapStyleLookupKey, styles: IMapStyleLookup[]): IMapStyleLookup => {
  return styles.find(s => s.key.lookupKeyCode.code === key.lookupKeyCode.code
    && (!key.trapTypeId || s.key.trapTypeId === key.trapTypeId)
    && (!key.trapStatus || s.key.trapStatus === key.trapStatus));
};

const generateKey = (key: IMapStyleLookupKey) => `${key.lookupKeyCode.code}_${key.trapTypeId}_${key.trapStatus}`;

const createStrokeStyle = (strokeColor: string, width: number) => {
  strokeStyles[strokeColor + width] = new Style({
    stroke: new Stroke({
      color: strokeColor,
      width
    })
  });
  return strokeStyles[strokeColor + width];
};

const createFillStyle = (fillColor: string) => {
  fillStyles[fillColor] = new Style({
    fill: new Fill({
      color: fillColor
    }),
    stroke: new Stroke({
      color: polygonStrokeColor,
      width: 0.1
    })
  });
  return fillStyles[fillColor];
};


// null is not treated the same as undefined when rendering shapes
const toUndefinedIfNull = (value: any): any => {
  return value !== null ? value : undefined;
};

const createKey = (response: IGetOverlayLayersResponseItem, properties: any): IMapStyleLookupKey => {
  let lookupKeyCode: MapStyleLookupKeyCode;
  if (properties.StyleCode) {
    // take style from the feature properties
    lookupKeyCode = new MapStyleLookupKeyCode();
    lookupKeyCode.code = properties.StyleCode;
  } else {
    // if nothing is set use style from the layer
    lookupKeyCode = response.defaultMapStyle;
  }

  return {
    lookupKeyCode,
    trapStatus: properties.TrapTypeId ? properties.Status : null,
    trapTypeId: properties.TrapTypeId && lookupKeyCode.code === MapStyleLookupKeyCodeEnum.TrapType
      ? properties.TrapTypeId
      : null
  };
};

const enum MapStyleLookupKeyCodeEnum {
  TrapType = 'TT',
  ObservationLocation = 'OL',
  ArchivedObservationLocation = 'AOL',
}

