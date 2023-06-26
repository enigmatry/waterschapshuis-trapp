import { OnInit, OnDestroy, Component } from '@angular/core';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { AppMap } from 'src/app/shared/models/app-map.model';
import { OverlayLayer } from 'src/app/shared/models/overlay-layer.model';
import { SpinnerService } from 'src/app/shared/services/spinner.service';
import { ReportBaseComponent } from '../report-base.component';
import { GeoMapReportService } from '../../services/geo-map-report.service';
import { SideBarFilterService } from '../../services/side-bar-filter.service';

@Component({
    templateUrl: 'map-report-base.component.html'
})
export class MapReportBaseComponent extends ReportBaseComponent implements OnInit, OnDestroy {
    private map: AppMap;

    constructor(
        sideBarService: SideBarService,
        private geoMapReportService: GeoMapReportService,
        private spinnerService: SpinnerService,
        private sideBarFilterService: SideBarFilterService) {
        super(sideBarService);
    }

    ngOnInit() {
        super.ngOnInit();

        this.geoMapReportService.reload();

        this.map = new AppMap('map');

        this.geoMapReportService.backgroundLayer
            .pipe(untilComponentDestroyed(this))
            .subscribe(this.map.addBackgroundLayer);

        this.geoMapReportService.mapStyles
            .pipe(untilComponentDestroyed(this))
            .subscribe(this.map.addStyles);

        this.map.mapRendered
            .pipe(untilComponentDestroyed(this))
            .subscribe(x => this.mapRenderComplete(x));
    }

    ngOnDestroy(): void {
        super.ngOnDestroy();
        this.map.getTargetElement().remove();
    }

    applyLayers(layers: OverlayLayer[]) {
        this.spinnerService.show();
        layers.forEach(x => {
            if (!x.selected) {
                this.map.disposeLayerById(x.fullName);
                return;
            }
            this.map.addOrReplaceExistingOverlayLayer(x);
        });
    }

    private mapRenderComplete(renderComplete: boolean) {
        this.sideBarFilterService.updateMapRendered(renderComplete);
        this.spinnerService.hide();
    }

    applyLayer(layer: OverlayLayer) {
        this.spinnerService.show();
        this.map.disposeLayerById(layer.fullName);
        this.map.tryAddOverlayLayer(layer);
    }
}
