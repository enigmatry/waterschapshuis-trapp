import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { ProgressService } from 'src/app/shared/services/progress.service';
import { ProgressEmitter } from 'src/app/shared/models/progressEmitter';

@Component({
    selector: 'app-offline-map-download-modal',
    templateUrl: './offline-map-download-modal.component.html',
    styleUrls: ['./offline-map-download-modal.component.scss'],
})
export class OfflineMapDownloadModalComponent extends OnDestroyMixin implements OnInit {

    public progressEmitters = ProgressEmitter;
    progressEmitter;
    percentage: number;
    component: any = this;


    constructor(
        public modal: ModalController,
        public progressService: ProgressService
    ) { super(); }

    ngOnInit() {
        this.progressService.progress$.pipe(untilComponentDestroyed(this.component))
            .subscribe(data => {
                this.progressEmitter = {
                    name: data.progressEmitter.toString(),
                    value: data.progressEmitter
                };
                this.percentage = data.progressPercentage;
            });
    }
}
