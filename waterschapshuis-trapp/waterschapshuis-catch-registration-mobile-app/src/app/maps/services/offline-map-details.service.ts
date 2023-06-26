import { Injectable } from '@angular/core';
import { SqliteProviderService } from 'src/app/services/sqlite-provider.service';
import { SqliteTableConfig } from 'src/app/shared/models/sqlite-table-config';

@Injectable({
  providedIn: 'root'
})
export class OfflineMapDetailsService {

  constructor(private sqlLiteProvider: SqliteProviderService) { }

  getOfflineMapDownloadDate = async (): Promise<any> => {
    const lastDownloadDate = await this.sqlLiteProvider
      .getAll(SqliteTableConfig.offlineMapDetails);

    return lastDownloadDate.length > 0 ? lastDownloadDate[0].lastDownloadDate : '';
  }

  getStoredOfflineMapLastModifiedDate = async (): Promise<any> => {
    const storedOfflineMapName = await this.sqlLiteProvider
      .getAll(SqliteTableConfig.offlineMapDetails);

    return storedOfflineMapName.length > 0 ? new Date(storedOfflineMapName[0].lastMapVersionFileModifiedDate) : new Date(-8640000000000000);
  }

  updateOfflineMapDownloadDate = (zipFileLastModifiedDate): Promise<void> => {
    return this.sqlLiteProvider.deleteAll(SqliteTableConfig.offlineMapDetails).then(_ =>
      this.sqlLiteProvider.insertData(SqliteTableConfig.offlineMapDetails, [new Date(), zipFileLastModifiedDate]));
  }
}

export interface OfflineMapDetails {
  available: boolean;
  downloadDate: string;
}
