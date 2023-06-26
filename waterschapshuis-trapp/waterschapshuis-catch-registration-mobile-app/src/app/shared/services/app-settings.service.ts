import { Injectable } from '@angular/core';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { SqliteTableConfig } from '../models/sqlite-table-config';
import { BoundingBox } from 'src/app/cache/clients/cached.client';
import { AppSettings as AppSettings } from './app-settings.model';
import { LayerFilter } from 'src/app/maps/models/layer-filter.model';

@Injectable({
  providedIn: 'root'
})
export class AppSettingsService {
  private appSettings: AppSettings;

  constructor(private sqlLiteProvider: SqliteProviderService) { }

  async init(): Promise<any> {
    this.appSettings = await this.findAppSettings();

    if (!this.appSettings) {
      this.appSettings = new AppSettings();
      await this.insertAppSettings();
    }
  }

  getOverlayLayerIds(): Array<string> {
    return this.appSettings.overlayLayerIds || [];
  }

  async setOverlayLayerIds(value: Array<string>): Promise<void> {
    this.appSettings.overlayLayerIds = value;
    await this.updateAppSettings();
  }

  getBackgroundLayerId(): string {
    return this.appSettings.backgroundLayerId;
  }

  getMapLayerFilter(): LayerFilter {
    return this.appSettings.layerFilter ?
      LayerFilter.create(
        this.appSettings.layerFilter.layerFullName,
        new Date(this.appSettings.layerFilter.start),
        new Date(this.appSettings.layerFilter.end),
        this.appSettings.layerFilter.predefinedFilter
      ) : null;
  }

  async setBackgroundLayerId(value: string): Promise<void> {
    this.appSettings.backgroundLayerId = value;
    await this.updateAppSettings();
  }

  async setOfflineBoundingBox(value: BoundingBox): Promise<void> {
    this.appSettings.offlineBoundingBox = value;
    await this.updateAppSettings();
  }

  async setMapLayerFilter(value: LayerFilter): Promise<void> {
    this.appSettings.layerFilter = value;
    await this.updateAppSettings();
  }

  private updateAppSettings = async () => await this.sqlLiteProvider
    .updateById(SqliteTableConfig.appSettings, this.appSettings.getObject(), this.appSettings.id)

  private insertAppSettings = async (): Promise<void> =>
    await this.sqlLiteProvider.insertData(SqliteTableConfig.appSettings, this.appSettings.getValues())

  private findAppSettings = async (): Promise<AppSettings> => {
    const fromDb = await this.sqlLiteProvider.getById(SqliteTableConfig.appSettings, AppSettings.Id);
    if (fromDb) {
      return AppSettings.fromObject(fromDb);
    }
    return null;
  }
}
