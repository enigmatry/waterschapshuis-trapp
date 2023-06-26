import { catchNumberColorScale, catchPerKmColorScale, hoursPerKmColorScale, Fill } from './map-report-color-scales';

export { getColorForPropertyValue };

const getCalculatedColor = (value: number, colorsPalette: Fill[]): string => {
    for (const fillColor of colorsPalette) {
        if (between(value, fillColor.min, fillColor.max)) {
            return fillColor.color;
        }
    }
};

const getCatchNumberColor = (value: number): string => {
    return getCalculatedColor(value, catchNumberColorScale);
};

const getCatchPerKmColor = (value: number): string  => {
    return getCalculatedColor(value, catchPerKmColorScale);
};

const getHourPerKmColor = (value: number): string => {
    return getCalculatedColor(value, hoursPerKmColorScale);
};

function between(x: number, min: number, max: number): boolean {
    return (x > min && x <= max) || (x > min && max === undefined) || (min === undefined && x <= max);
}

const getColorForPropertyValue = (properties: { [key: string]: any }): string => {
    if (properties.CatchNumber != null && properties.CatchNumber >= 0) {
        return getCatchNumberColor(properties.CatchNumber);
    } else if (properties.CatchesPerKM != null && properties.CatchesPerKM >= 0) {
        return getCatchPerKmColor(properties.CatchesPerKM);
    } else {
        return getHourPerKmColor(properties.HoursPerKM);
    }
};

