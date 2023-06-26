import { Injectable } from '@angular/core';
import { Logger } from 'src/app/core/logger/logger';
import { emptyExtent } from 'src/app/maps/models/bbox-loading-strategy';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';
import { ICacheMetadata } from './cache-metadata.model';

const logger: Logger = new Logger('GeoJsonCacheMetadataClient');
@Injectable({
  providedIn: 'root'
})
export class GeoJsonCacheMetadataClient {

  private readonly config = SqliteTableConfig.geoJsonCacheMetadata;

  constructor(private sqlite: SqliteProviderService) { }

  async getAll(): Promise<ICacheMetadata[]> {
    const rows = await this.sqlite.getAll(this.config);
    return rows.map(row => this.readData(row));
  }

  async get(cacheKey: string): Promise<ICacheMetadata> {
    const results = await this.getAll();
    return results.find(item => item.cacheKey === cacheKey);
  }

  async createOrUpdate(cacheKey: string, bbox: number[]): Promise<void> {
    const item = await this.get(cacheKey);
    return item ? this.update(item, bbox)
      : this.create(cacheKey, bbox);
  }

  async deleteByKey(cacheKey: string): Promise<void> {
    const item = await this.get(cacheKey);
    if (item) {
      return this.sqlite.deleteByIds(this.config, [item.id]);
    }
  }

  deleteAll(): Promise<void> {
    return this.sqlite.deleteAll(this.config);
  }

  private create(cacheKey: string, bbox: number[]) {
    const data = [cacheKey, JSON.stringify(bbox), new Date()];
    return this.sqlite.insertData(this.config, data);
  }

  private update(item: ICacheMetadata, bbox: number[]) {
    const data = { bbox: JSON.stringify(bbox), updatedOn: new Date() };
    return this.sqlite.updateById(this.config, data, item.id);
  }

  private readData(row: any): ICacheMetadata {
    return {
      id: row.id,
      cacheKey: row.cacheKey,
      bbox: this.tryParse(row.bbox),
      updatedOn: row.updatedOn
    };
  }

  private tryParse(bbox: string) {
    try {
      return bbox ? JSON.parse(bbox) : emptyExtent;
    } catch (error) {
      logger.error(error, 'Error parsing bbox', bbox);
      return emptyExtent;
    }
  }
}
