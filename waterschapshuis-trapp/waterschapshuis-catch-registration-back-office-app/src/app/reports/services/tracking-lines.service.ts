import { Injectable } from '@angular/core';
import { MapService } from 'src/app/shared/services/map.service';
import { OverlayLayerCategoryCode } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { concatMap, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TrackingLinesService {

  constructor(private mapService: MapService) { }

  async getOverlayLayer() {
    return await this.mapService.getOverlayLayers([OverlayLayerCategoryCode.ReportTracking])
      .pipe(map(l => l[0]))
      .toPromise();
  }
}
