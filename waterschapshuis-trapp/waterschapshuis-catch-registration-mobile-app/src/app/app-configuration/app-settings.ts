import { RequestRetryPolicy } from '../background-job/models/request-retry-policy.enum';

export class AppSettings {
  public static currentLocationSettings = {
    defaultZoomLevel: 11,
    ZIndex: 50,
    refreshPeriod: 3000
  };
  public static synchronizationSettings = {
    syncBatchSize: 20
  };
  public static trackingSettings = {
    desiredAccuracy: 0, // HIGH = 0,  MEDIUM = 10, LOW = 100, PASSIVE = 1000
    stationaryRadius: 10, // in meters
    distanceFilter: 10, // in meters
    trackingPointRadius: 10, // in kilometers
  };
  public static blobStorageObservationSettings = {
    containerName: 'observations'
  };
  public static offlineMapSettings = {
    maxZoomLevelForSelectedMap: 14,
    blobStorage: {
      containerName: 'backgroundmaptiles',
      zipFileName: 'openMapPbfTiles.zip'
    },
    fileSystemLocation: {
      pbfDirectoryName: 'offline-map-pbfs',
      tempDirectoryName: 'tempPbfZip',
      tempZipFilename: 'openMapPbfTiles.zip'
    }
  };
  public static backgroundJobSettings = {
    initialDelay: 60 * 1000, // 1 minute
    interval: 2 * 60 * 1000, // 2 minutes for testing (was 15 minutes)
    retryPolicy: RequestRetryPolicy.RetryWithFixedLimit,
    maxRetryAttempts: 5
  };
  public static prefetchDataSettings = {
    widthKilometers: 15, // bare in mind that this is radius, diameter will be 50km
    pageSize: 10000
  };
  public static mapSettings = {
    hitTolerance: 5, // in pixels
    maxLookupResolution: 50, // for feature layers. 0 is de lowest layer (you will only see couple of meters),
                             // the higher the number the more data will be loaded. 17 is about Rayon level
    bboxExtendFactor: 1.2, //
    longpressTimeoutSec: 2.5 // duration of the longpress on the map to show location dialog
  };
  public static historySettings = {
    maxHistoryItemsReturned: 1000
  };
}
