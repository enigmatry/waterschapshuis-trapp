import { Component, OnInit, Input } from '@angular/core';
import { TrapsCachedClient } from 'src/app/cache/clients/traps-cached.client';
import { GetTrapHistoriesHistoryItem } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { AppSettings } from 'src/app/app-configuration/app-settings';

@Component({
    selector: 'app-trap-history',
    templateUrl: './trap-history.component.html',
    styleUrls: ['./trap-history.component.scss'],
})
export class TrapHistoryComponent implements OnInit {

    @Input() trapId: string;

    loading = false;
    histories: GetTrapHistoriesHistoryItem[];

    constructor(private trapCachedClient: TrapsCachedClient) { }

    ngOnInit() {
        if (this.trapId) {
            this.loading = true;
            this.trapCachedClient
                .getHistories(this.trapId, 'recordedOn', 'desc', AppSettings.historySettings.maxHistoryItemsReturned, 1)
                .subscribe(
                    response => {
                        this.histories = response.items;
                        this.loading = false;
                    },
                    err => this.loading = false
                );
        }
    }
}
