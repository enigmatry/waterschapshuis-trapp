import { Injectable } from '@angular/core';
import { QueuedRequest } from '../models/queued-request.model';
import { SqliteTableConfig } from '../../shared/models/sqlite-table-config';
import { SqliteProviderService } from '../../services/sqlite-provider.service';
import { ISqliteTableData } from '../../shared/models/sqlite-table-data';

@Injectable({
  providedIn: 'root'
})
export class QueuedRequestStoreClient {

  private readonly config: ISqliteTableData;

  constructor(private sqlite: SqliteProviderService) {
    this.config = SqliteTableConfig.backgroundJobQueuedRequests;
  }

  async getAll(): Promise<QueuedRequest[]> {
    const items = await this.sqlite.getAll(this.config);
    return items.map(item => this.map(item));
  }

  async add(req: QueuedRequest): Promise<void> {
    const payload = this.isJSON(req.payload)
      ? req.payload
      : JSON.stringify(req.payload);

    const data = [
      req.method,
      req.url,
      payload,
      req.timestamp || new Date(),
      req.retryAttempts || 0
    ];
    return this.sqlite.insertData(this.config, data);
  }

  async remove(id: number): Promise<void> {
    return this.sqlite.deleteByIds(this.config, [id]);
  }

  async removeAll(): Promise<void> {
    return this.sqlite.deleteAll(this.config);
  }

  async updateRetryAttempts(id: number, retryAttempts: number): Promise<void> {
    return this.sqlite.updateById(this.config, { retryAttempts }, id);
  }

  private map(row: any): QueuedRequest {
    return {
      id: row.id,
      method: row.method,
      url: row.url,
      payload: JSON.parse(row.payload),
      timestamp: row.timestamp,
      retryAttempts: row.retryAttempts
    };
  }

  // src: https://github.com/prototypejs/prototype/blob/5fddd3e/src/prototype/lang/string.js#L702
  private isJSON(str: string) {
    if (typeof str !== 'string') { return false; }
    if (!str) { return false; }
    str = str.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@');
    str = str.replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']');
    str = str.replace(/(?:^|:|,)(?:\s*\[)+/g, '');
    return (/^[\],:{}\s]*$/).test(str);
  }
}
