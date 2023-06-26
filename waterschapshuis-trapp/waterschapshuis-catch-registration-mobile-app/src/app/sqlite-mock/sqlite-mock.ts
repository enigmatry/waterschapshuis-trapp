import { SQLiteDatabaseConfig } from '@ionic-native/sqlite';
import { SQLiteObject } from './sqlite-object-mock';

import * as initSqlJs from '../../../node_modules/sql.js/dist/sql-asm.js';

export class SQLiteMock {
  public async create(config: SQLiteDatabaseConfig): Promise<SQLiteObject> {

    // since this is an in memory database we can ignore the config parameters

    const SQL = await initSqlJs();

    let db: any;
    const storeddb = localStorage.getItem('waterschapshuis-dev-db');

    if (storeddb) {
      const arr = storeddb.split(',');
      db = new SQL.Database(arr);
    } else {
      db = new SQL.Database();
    }

    return new Promise((resolve, reject) => {
      resolve(new SQLiteObject(db));
    });
  }
}
