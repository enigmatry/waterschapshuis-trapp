import { Injectable } from '@angular/core';
import { NetworkService } from 'src/app/network/network.service';
import { ConnectionStatus } from 'src/app/shared/offline/models/connection-status.enum';
import { GeoJsonCacheStorageClient } from './geo-json-cache-storage.client';
import { GeoJsonCacheMetadataClient } from './geo-json-cache-metadata.client';
import { OverlayLayer } from 'src/app/maps/models/overlay-layer.model';
import { ICacheMetadata } from './cache-metadata.model';
import { dateAdd } from './date-helper';
import {
  OverlayLayerCacheStrategy as CacheStrategy, IOverlayLayerCacheSettings as CacheSettings, OverlayLayerLookupStrategy
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { Extent, containsExtent } from 'ol/extent';
import { Logger } from 'src/app/core/logger/logger';

const logger = new Logger('GeoJsonCacheManager');

@Injectable({
  providedIn: 'root'
})
export class GeoJsonCacheManager {

  constructor(
    private cacheStorage: GeoJsonCacheStorageClient,
    private cacheMetadata: GeoJsonCacheMetadataClient,
    private networkService: NetworkService) { }

  getCachedGeoJson(layer: OverlayLayer, bbox: number[]): Promise<string> {
    return this.getCachedInternal(layer, bbox);
  }

  updateCachedGeoJson(layer: OverlayLayer, bbox: number[], geoJson: string): Promise<void> {
    return this.updateCacheInternal(layer, bbox, geoJson);
  }

  private getCacheKey(layer: OverlayLayer) {
    return layer.fullName.replace(/:/g, '-');
  }

  private async getCachedInternal(layer: OverlayLayer, bbox: number[]): Promise<string> {
    const cacheKey = this.getCacheKey(layer);

    if (this.networkService.isOffline()) {
      logger.debug(`CACHE HIT: ${cacheKey}, offline.`);
      return this.cacheStorage.get(cacheKey);
    }

    const cacheSettings = layer.cacheSettings;
    const cacheStrategy = this.getCacheStrategy(cacheSettings);

    if (cacheStrategy === CacheStrategy.NoCache) {
      logger.debug(`CACHE MISS: ${cacheKey}, no-cache strategy.`);
      return Promise.resolve(null);
    }

    const cacheMetadata = await this.cacheMetadata.get(cacheKey);
    if (this.isCacheExpired(cacheSettings, cacheMetadata)) {
      logger.debug(`CACHE MISS: ${cacheKey}, expired ${this.getExpiryDate(cacheSettings, cacheMetadata)}.`);
      return Promise.resolve(null);
    }

    const isBboxLookupStrategy = layer.isBBoxLookupStrategy;
    if (isBboxLookupStrategy && !this.isWithinCachedExtent(bbox, cacheMetadata)) {
      logger.debug(`CACHE MISS: ${cacheKey}, requested extent is outside of cached extent.`);
      return Promise.resolve(null);
    }

    logger.debug(`CACHE HIT: ${cacheKey}.`);
    return this.cacheStorage.get(cacheKey);
  }

  private async updateCacheInternal(layer: OverlayLayer, bbox: number[], geoJson: string) {
    const cacheKey = this.getCacheKey(layer);

    await this.cacheStorage.save(cacheKey, geoJson);
    await this.cacheMetadata.createOrUpdate(cacheKey, bbox);
  }

  private isCacheExpired(cacheSettings: CacheSettings, cacheMetadata: ICacheMetadata): boolean {
    if (!cacheSettings || !cacheMetadata) {
      return true;
    }
    const expiryDate = this.getExpiryDate(cacheSettings, cacheMetadata);
    const now = new Date();
    return now > expiryDate;
  }

  isWithinCachedExtent(bbox: number[], cacheMetadata: ICacheMetadata): boolean {
    if (!cacheMetadata) {
      return true;
    }
    const requestedExtent = bbox as Extent;
    const cachedExtent = cacheMetadata.bbox as Extent;
    return containsExtent(cachedExtent, requestedExtent);
  }

  private getCacheStrategy(cacheSettings: CacheSettings): CacheStrategy {
    return cacheSettings ? cacheSettings.cacheStrategy : CacheStrategy.NoCache;
  }

  private getExpiryDate(cacheSettings: CacheSettings, cacheMetadata: ICacheMetadata): Date {
    return dateAdd(new Date(cacheMetadata?.updatedOn), 'second', cacheSettings?.durationSeconds);
  }
}
