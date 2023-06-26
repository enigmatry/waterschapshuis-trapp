import { ISqliteTableData } from './sqlite-table-data';

export class SqliteTableConfig {
  public static tracking: ISqliteTableData = {
    name: 'Tracking',
    columns: [
      { name: 'id', type: 'TEXT', isPrimaryKey: true },
      { name: 'timestamp', type: 'NUMERIC' },
      { name: 'longitude', type: 'DOUBLE' },
      { name: 'latitude', type: 'DOUBLE' },
      { name: 'trappingTypeId', type: 'TEXT' },
      { name: 'sessionId', type: 'TEXT' },
      { name: 'isTimewriting', type: 'NUMERIC' },
      { name: 'isTrackingMap', type: 'NUMERIC' },
      { name: 'isTrackingPrivate', type: 'NUMERIC' }
    ],
    generateId: false
  };

  public static observations: ISqliteTableData = {
    name: 'Observations',
    columns: [
      { name: 'id', type: 'TEXT', isPrimaryKey: true },
      { name: 'type', type: 'INTEGER' },
      { name: 'image', type: 'TEXT' },
      { name: 'remarks', type: 'TEXT' },
      { name: 'longitude', type: 'DOUBLE' },
      { name: 'latitude', type: 'DOUBLE' },
      { name: 'recordedOn', type: 'DATETIME' },
      { name: 'syncedToApi', type: 'INTEGER' }
    ],
    generateId: false
  };

  public static wvrToken: ISqliteTableData = {
    name: 'WvrAuthToken',
    columns: [
      { name: 'id', type: 'INTEGER', isPrimaryKey: true },
      { name: 'accessToken', type: 'TEXT' },
      { name: 'idToken', type: 'TEXT' },
      { name: 'refreshToken', type: 'TEXT' },
      { name: 'expiresIn', type: 'DATETIME' }
    ],
    generateId: true
  };

  public static appSettings: ISqliteTableData = {
    name: 'AppSettings',
    columns: [
      { name: 'id', type: 'TEXT', isPrimaryKey: true },
      { name: 'overlayLayerIds', type: 'TEXT' },
      { name: 'backgroundLayerId', type: 'TEXT' },
      { name: 'offlineBboxJson', type: 'TEXT' },
      { name: 'trackingLayerFilterJson', type: 'TEXT' },
      { name: 'lastMigrationId', type: 'INTEGER' }
    ],
    generateId: false
  };

  public static offlineMapDetails: ISqliteTableData = {
    name: 'offlineMapDetails',
    columns: [
      { name: 'lastDownloadDate', type: 'TEXT' },
      { name: 'lastMapVersionFileModifiedDate', type: 'TEXT' }
    ],
    generateId: false
  };

  public static geoJsonCacheMetadata: ISqliteTableData = {
    name: 'GeoJsonCacheMetadata',
    columns: [
      { name: 'id', type: 'INTEGER', isPrimaryKey: true },
      { name: 'cacheKey', type: 'TEXT' },
      { name: 'bbox', type: 'TEXT' },
      { name: 'updatedOn', type: 'DATETIME' }
    ],
    generateId: true
  };

  public static geoJsonUpdateCommand: ISqliteTableData = {
    name: 'GeoJsonUpdateCommand',
    columns: [
      { name: 'id', type: 'INTEGER', isPrimaryKey: true },
      { name: 'actionType', type: 'INTEGER' },
      { name: 'longitude', type: 'DOUBLE' },
      { name: 'latitude', type: 'DOUBLE' },
      { name: 'properties', type: 'TEXT' },
      { name: 'propertiesIdFieldName', type: 'TEXT' },
      { name: 'layerName', type: 'TEXT' },
      { name: 'layerNameSuffix', type: 'TEXT' },
      { name: 'timestamp', type: 'DATETIME' },
      { name: 'sourceMapProjection', type: 'TEXT' },
      { name: 'destinationMapProjection', type: 'TEXT' }
    ],
    generateId: true
  };

  public static backgroundJobQueuedRequests: ISqliteTableData = {
    name: 'BackgroundJobQueuedRequests',
    columns: [
      { name: 'id', type: 'INTEGER', isPrimaryKey: true },
      { name: 'method', type: 'TEXT' },
      { name: 'url', type: 'TEXT' },
      { name: 'payload', type: 'TEXT' },
      { name: 'timestamp', type: 'DATETIME' },
      { name: 'retryAttempts', type: 'INTEGER' }
    ],
    generateId: true
  };
}
