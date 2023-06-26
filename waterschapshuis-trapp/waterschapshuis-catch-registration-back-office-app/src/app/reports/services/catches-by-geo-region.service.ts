import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { OverlayLayer } from 'src/app/shared/models/overlay-layer.model';
import { MapService } from 'src/app/shared/services/map.service';
import { OverlayLayerCategoryCode } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { CatchesByGeoRegionReportFilter } from '../models/common/catches-by-geo-region-report-template.model';
import { IListItem } from 'src/app/shared/models/list-item';
import { map, take } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class CatchesByGeoRegionService {
    private catchesByGeoRegionReportFilterSubject = new Subject<CatchesByGeoRegionReportFilter>();
    private layers: OverlayLayer[] | null = null;

    overlayLayers: Observable<OverlayLayer[]>;
    catchesByGeoRegionReportFilter = this.catchesByGeoRegionReportFilterSubject.asObservable();

    constructor(private mapService: MapService) { }

    async loadOverlayLayers() {
        this.overlayLayers =  this.mapService
            .getOverlayLayers([
                OverlayLayerCategoryCode.ReportGeoRegionCatches
            ]);
    }
    async getOverlayLayersList(): Promise<IListItem[]> {
        const layers = this.layers ? this.layers
        : await this.mapService.getOverlayLayers([
            OverlayLayerCategoryCode.ReportGeoRegionCatches
        ]).pipe(take(1)).toPromise();

        return layers.map(x => {
            return {
                id: x.fullName,
                name: x.displayName
            };
        });
    }

    updateFilter(value: CatchesByGeoRegionReportFilter): void {
        this.catchesByGeoRegionReportFilterSubject.next(value);
    }
}
