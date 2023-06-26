import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { parseUrl, stringifyUrl } from 'query-string';
import { Logger } from 'src/app/core/logger/logger';

const logger = new Logger('GeoJsonClient');

@Injectable({
    providedIn: 'root'
})
export class GeoJsonClient {

    constructor(private httpClient: HttpClient) { }

    getGeoJson(url: string): Observable<string> {
        // logger.debug(url);
        return this.httpClient.get(url, { responseType: 'text' });
    }

    getGeoJsonInBbox(url: string, bbox: number[], geometryFieldName: string): Observable<string> {
        return this.getGeoJson(this.extendUrlWithBboxQuery(url, bbox, geometryFieldName));
    }

    private extendUrlWithBboxQuery(url: string, bbox: number[], geometryFieldName: string) {
        if (bbox) {
            const params = parseUrl(url);
            params.query.CQL_FILTER = (params.query.CQL_FILTER ? params.query.CQL_FILTER + `and` : '')
            + `(BBOX(${geometryFieldName},${bbox.toString()}))`;
            url = stringifyUrl(params, { encode: true });
        }
        return url;
    }
}
