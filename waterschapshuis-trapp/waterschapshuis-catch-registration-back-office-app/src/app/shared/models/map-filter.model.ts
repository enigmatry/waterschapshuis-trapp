import { parseUrl, stringifyUrl } from 'query-string';

export { generateUrlWithFilter };

export class MapFilter {
    trapStartDate: string;
    trapEndDate: string;
    trapTypeId: string;
    catchStartDate: string;
    catchEndDate: string;
    catchType: number;
    showTrapsWithCatches: number;
}

const generateUrlWithFilter = (url: string, filter: any): string => {
    const params = parseUrl(url);
    params.query.viewparams = getLayerUrlViewParamsValue(filter, params.query.viewparams);
    return stringifyUrl(params, { encode: false });
};

const getLayerUrlViewParamsValue = (filter: any, existingViewParams: any): string => {
    let layerViewParams = existingViewParams ? existingViewParams : '';
    const layerViewParamsArray = layerViewParams.split(';');
    // tslint:disable-next-line: forin
    for (const key in filter) {
        if (layerViewParamsArray.find( x => x.startsWith(key))) {
            layerViewParams = layerViewParams.replace(`${layerViewParamsArray.find(x => x.startsWith(key))};`, '');
        }
        if ((!!filter[key]) || (typeof filter[key] === 'number')) {
            layerViewParams += `${key}:${filter[key]};`;
        }
    }
    return layerViewParams;
};

