import { Component, OnInit } from '@angular/core';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';

import { TrackingLocation } from '../models/tracking-location.model';

@Component({
  selector: 'app-background-location',
  templateUrl: './background-location.component.html'
})
export class BackgroundLocationComponent {

  locations: TrackingLocation[] = [];

  constructor(
    private sqliteProvider: SqliteProviderService
  ) { }

  async getGeolocation(): Promise<void> {
    this.locations = [];
    this.locations = await this.sqliteProvider.getAll(SqliteTableConfig.tracking);
  }

  getDate(timestamp: number): Date {
    return new Date(timestamp);
  }
}
