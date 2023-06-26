import { Injectable } from '@angular/core';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { ISqliteTableData } from '../shared/models/sqlite-table-data';
import { SqliteTableConfig } from '../shared/models/sqlite-table-config';
import { Logger } from '../core/logger/logger';
import { SqliteMigrations } from './sqlite-migrations';

const logger = new Logger('SqliteProviderService');

@Injectable({
  providedIn: 'root'
})
export class SqliteProviderService {

  private database: SQLiteObject;

  constructor(
    private sqlite: SQLite
  ) { }

  async init(): Promise<boolean> {
    return await this.createDatabase()
      .then(() => this.createTables())
      .then(() => this.getLastMigrationId())
      .then(lastMigration => this.runNewMigrations(lastMigration));
  }

  private async runNewMigrations(lastMigrationId: number): Promise<boolean> {
    const newMigrations = SqliteMigrations.getNewMigrations(lastMigrationId);

    for (const migration of newMigrations) {
      await this.executeCommandsInTransaction(migration.queries);
      await this.updateById(SqliteTableConfig.appSettings, { lastMigrationId: migration.id }, 'AppSettingId');
    }

    return true;
  }

  private async getLastMigrationId(): Promise<number> {
    const isColumnExist = await this.isColumnExist(SqliteTableConfig.appSettings, 'lastMigrationId');
    if (!isColumnExist) {
      const cmd = `ALTER TABLE ${SqliteTableConfig.appSettings.name} ADD COLUMN lastMigrationId NUMBER DEFAULT 0`;
      await this.executeCommand(cmd);
      return 0;
    }

    const query = `SELECT lastMigrationId from ${SqliteTableConfig.appSettings.name}`;
    const result = await this.executeCommand(query);

    const value = result.rows.length > 0 ? Number(result.rows.item(0).lastMigrationId) : undefined;

    return isNaN(value) ? 0 : value;
  }

  private createTables(): void {
    this.createTable(SqliteTableConfig.wvrToken);
    this.createTable(SqliteTableConfig.tracking);
    this.createTable(SqliteTableConfig.observations);
    this.createTable(SqliteTableConfig.appSettings);
    this.createTable(SqliteTableConfig.offlineMapDetails);
    this.createTable(SqliteTableConfig.geoJsonCacheMetadata);
    this.createTable(SqliteTableConfig.geoJsonUpdateCommand);
    this.createTable(SqliteTableConfig.backgroundJobQueuedRequests);
  }

  private async createTable(config: ISqliteTableData): Promise<void> {

    let cmd = 'CREATE TABLE IF NOT EXISTS ';
    cmd = cmd.concat(config.name);

    cmd = cmd.concat(' (');

    config.columns.map(col => {
      cmd = cmd.concat(` ${col.name}`);
      cmd = cmd.concat(` ${col.type}`);
      if (col.isPrimaryKey) {
        cmd = cmd.concat(' PRIMARY KEY');
      }
      cmd = cmd.concat(',');
    });

    cmd = cmd.substring(0, cmd.length - 2);

    cmd = cmd.concat(')');

    return this.executeCommand(cmd);
  }

  async insertData(config: ISqliteTableData, values: Array<any>): Promise<void> {

    let cmd = 'INSERT INTO ';
    cmd = cmd.concat(config.name);
    cmd = config.generateId ? cmd.concat(' VALUES(NULL, ') : cmd.concat(' VALUES(');

    values.map(col => {
      cmd = cmd.concat(`?`);
      cmd = cmd.concat(',');
    });

    cmd = cmd.substring(0, cmd.length - 1);

    cmd = cmd.concat(')');

    await this.executeCommand(cmd, values);
  }

  async getAll(config: ISqliteTableData, orderBy?: string, order?: string): Promise<any[]> {
    let cmd = 'SELECT * FROM ';
    cmd = cmd.concat(config.name);

    const columnExists = config.columns.find(col => col.name === orderBy);
    if (orderBy && columnExists) {
      cmd = cmd.concat(` ORDER BY ${orderBy}`);
      if (order) {
        cmd = cmd.concat(` ${order}`);
      }
    }

    const result = await this.executeCommand(cmd);

    const resultList = [];

    for (let i = 0; i < result.rows.length; i++) {
      resultList.push(result.rows.item(i));
    }

    return resultList;
  }

  async getById(config: ISqliteTableData, id: string | number): Promise<any | null> {

    const cmd = `SELECT * FROM ${config.name} WHERE id = ${this.toSqlParamValue(id)}`;
    const result = await this.executeCommand(cmd);

    return result.rows.length === 1 ? result.rows.item(0) : null;
  }

  async deleteByIds(config: ISqliteTableData, ids: Array<number | string>, deleteByColumn: string = 'id'): Promise<void> {

    if (!ids?.length) {
      return;
    }

    let cmd = 'DELETE FROM ';
    cmd = cmd.concat(config.name);
    cmd = cmd.concat(` WHERE ${deleteByColumn} IN (`);

    ids.map(() => {
      cmd = cmd.concat(`?`);
      cmd = cmd.concat(',');
    });

    cmd = cmd.substring(0, cmd.length - 1);

    cmd = cmd.concat(')');

    return this.executeCommand(cmd, ids);
  }

  async deleteAll(config: ISqliteTableData): Promise<void> {
    let cmd = 'DELETE FROM ';
    cmd = cmd.concat(config.name);

    return this.executeCommand(cmd);
  }

  async updateById(config: ISqliteTableData, values: { [index: string]: any; }, id?: number | string): Promise<void> {

    let cmd = 'UPDATE ';
    cmd = cmd.concat(config.name);
    cmd = cmd.concat(' SET ');

    for (const key of Object.keys(values)) {
      cmd = cmd.concat(`${key} = '${values[key]}',`);
    }

    cmd = cmd.substring(0, cmd.length - 1);

    if (id) {
      cmd = cmd.concat(` WHERE id = ${this.toSqlParamValue(id)}`);
    }

    return this.executeCommand(cmd);
  }

  async count(config: ISqliteTableData): Promise<number> {
    let cmd = 'SELECT COUNT(*) AS size FROM ';
    cmd = cmd.concat(config.name);

    const result = await this.executeCommand(cmd);

    return result.rows.item(0).size;
  }

  private async createDatabase(): Promise<void> {
    // create/open database
    this.database = await this.sqlite.create({
      name: `waterschapshuis-local.db`,
      location: 'default'
    }).catch((error: Error) => {
      logger.error(error, 'Error on open or create database');
      return Promise.reject(error.message || error);
    });

    logger.debug('database created', this.database);
  }

  private async executeCommand(cmd: string, value?: Array<any>): Promise<any> {
    return this.database.executeSql(cmd, value ? value : []);
  }

  private async executeCommandsInTransaction(cmds: Array<string>): Promise<any> {
    return this.database.transaction(() => {
      this.database.sqlBatch(cmds);
    })
      .catch(e => {
        logger.error(e);
      });
  }

  private toSqlParamValue = (param: string | number) => typeof param === 'string' ? `'${param}'` : `${param}`;

  private async isColumnExist(config: ISqliteTableData, columnName: string): Promise<boolean> {
    const cmd = `SELECT name FROM PRAGMA_TABLE_INFO('${config.name}') WHERE name = '${columnName}'`;

    const result = await this.executeCommand(cmd);

    return result.rows.length > 0;
  }
}
