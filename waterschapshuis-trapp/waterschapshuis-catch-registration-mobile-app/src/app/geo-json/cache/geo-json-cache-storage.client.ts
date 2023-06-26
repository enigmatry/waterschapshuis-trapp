import { Injectable } from '@angular/core';
import { File } from '@ionic-native/file/ngx';
import { Logger } from 'src/app/core/logger/logger';

const logger = new Logger('GeoJsonCacheStorageClient');

@Injectable({
  providedIn: 'root'
})
export class GeoJsonCacheStorageClient {

  constructor(private file: File) { }

  get(cacheKey: string): Promise<string> {
    return this.loadFromFile(this.getFileName(cacheKey));
  }

  save(cacheKey: string, geoJson: string): Promise<void> {
    return this.writeToFile(this.getFileName(cacheKey), geoJson);
  }

  delete(cacheKey: string): Promise<boolean> {
    return this.removeFile(this.getFileName(cacheKey));
  }

  exists(cacheKey: string): Promise<boolean> {
    return this.fileExists(cacheKey);
  }

  private getFileName(cacheKey: string): string {
    return `${cacheKey}.geojson`;
  }

  private writeToFile(fileName: string, geoJson: string): Promise<void> {
    return this.file.writeFile(this.file.dataDirectory, fileName, geoJson, { replace: true })
      .then(_ => logger.debug(`GeoJson file saved ${fileName}`));
  }

  private loadFromFile(fileName: string): Promise<string> {
    return this.file.readAsText(this.file.dataDirectory, fileName)
      .catch(error => {
        logger.debug(`Error reading geoJson file ${fileName}`, error);
        return null;
      });
  }

  private removeFile(fileName: string): Promise<boolean> {
    return this.file.removeFile(this.file.dataDirectory, fileName)
      .then(result => {
        if (result.success) { logger.debug(`File removed ${fileName}`); }
        return result.success;
      });
  }

  private fileExists(fileName: string): Promise<boolean> {
    return this.file.checkFile(this.file.dataDirectory, fileName);
  }

}

