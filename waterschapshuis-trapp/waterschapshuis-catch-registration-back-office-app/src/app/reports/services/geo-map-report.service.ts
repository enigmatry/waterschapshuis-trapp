import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { BackgroundLayer } from 'src/app/shared/models/background-layer.model';
import { IMapStyleLookup } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { GeoDataResponse } from 'src/app/shared/models/geodata-response.model';
import { GeoDataOptions } from 'src/app/shared/models/geodata-options.model';
import { MapService } from 'src/app/shared/services/map.service';

@Injectable({
    providedIn: 'root'
})
export class GeoMapReportService {
    private backgroundLayerSubject = new Subject<GeoDataOptions>();
    private mapStylesSubject = new Subject<IMapStyleLookup[]>();

    backgroundLayer = this.backgroundLayerSubject.asObservable();
    mapStyles = this.mapStylesSubject.asObservable();

    constructor(private mapService: MapService) { }

    reload = () => {
        this.loadBackgroundLayer();
        this.loadMapStyles();
    }

    private loadBackgroundLayer = () =>
        this.mapService
            .getBackgroundLayers()
            .subscribe(response => {
                if (response.length > 0) {
                    this.loadGeoData(response[0]);
                }
            })

    private loadMapStyles = () =>
        this.mapService
            .getStyles()
            .subscribe(styles => this.mapStylesSubject.next(styles))

    private loadGeoData = (backgroundLayer: BackgroundLayer) =>
        this.mapService
            .getGeoData(backgroundLayer)
            .subscribe((response: GeoDataResponse) => {
                const options = GeoDataOptions.fromObject(response);
                this.backgroundLayerSubject.next(options);
            })

}
