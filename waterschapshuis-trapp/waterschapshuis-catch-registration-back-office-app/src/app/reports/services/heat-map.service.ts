import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { OverlayLayer } from 'src/app/shared/models/overlay-layer.model';
import { MapService } from 'src/app/shared/services/map.service';
import { OverlayLayerCategoryCode } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { HeatMapReportFilter } from '../models/common/heat-map-report-template.model';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class HeatMapService {
    private heatMapReportFilterSubject = new Subject<HeatMapReportFilter>();
    heatMapReportFilter = this.heatMapReportFilterSubject.asObservable();


    constructor(private mapService: MapService) { }

    async getOverlayLayer() {
        return await this.mapService.getOverlayLayers([OverlayLayerCategoryCode.HeatMapOfCatches])
          .pipe(map(l => l[0]))
          .toPromise();
      }

    updateFilter(value: HeatMapReportFilter): void {
        this.heatMapReportFilterSubject.next(value);
    }
}
