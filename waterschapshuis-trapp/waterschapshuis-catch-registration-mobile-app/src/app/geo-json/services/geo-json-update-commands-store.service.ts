import { Injectable } from '@angular/core';
import { GeoJsonUpdateCommand } from '../models/geo-json-update-command.model';
import { Observable, from } from 'rxjs';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';
import { map, filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GeoJsonUpdateCommandsStoreService {
  private readonly config = SqliteTableConfig.geoJsonUpdateCommand;

  constructor(private sqLiteProvider: SqliteProviderService) { }

  add = (commands: Array<GeoJsonUpdateCommand>): Observable<any> =>
    from(commands.map(command => from(this.sqLiteProvider.insertData(
      SqliteTableConfig.geoJsonUpdateCommand, [
      command.actionType,
      command.longitude,
      command.latitude,
      JSON.stringify(command.properties),
      command.propertiesIdFieldName,
      command.layerName,
      command.layerNameSuffix,
      command.timestamp,
      command.sourceMapProjection,
      command.destinationMapProjection
    ])
    )))

  get = (layerFullName: string): Observable<GeoJsonUpdateCommand[]> =>
    from(this.sqLiteProvider.getAll(this.config))
      .pipe(
        map(items => items
          .map(item =>
            new GeoJsonUpdateCommand(
              item.actionType,
              item.longitude,
              item.latitude,
              JSON.parse(item.properties),
              item.propertiesIdFieldName,
              item.layerName,
              item.layerNameSuffix,
              item.timestamp,
              item.sourceMapProjection,
              item.destinationMapProjection))
          .filter(command => command.relatesToLayerWithName(layerFullName))))

  clear = (): Observable<void> => from(this.sqLiteProvider.deleteAll(this.config));
}

