import { Injectable } from '@angular/core';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { Logger } from '../core/logger/logger';
import { Platform } from '@ionic/angular';

const logger = new Logger('SqliteCacheStorage');

@Injectable({
    providedIn: 'root'
})
export class SqliteCacheStorage {

    private database: SQLiteObject;

    private readonly databaseConfig = {
        name: `waterschapshuis-cache.db`,
        location: 'default'
    };
    private readonly storeName = 'Cache';

    constructor(private sqlite: SQLite, private platform: Platform) {
    }

    async ready(): Promise<void> {
        if (this.database) {
            return Promise.resolve();
        }
        return this.platform.ready().then(() => this.initializeDatabase());
    }

    async get(key: string): Promise<any> {
        const value = await this.getSingleOrDefault(`SELECT * FROM ${this.storeName} WHERE key = ?`, [key]);
        return this.jsonParse(value);
    }

    async set(key: string, value: any): Promise<any> {
        const jsonValue = JSON.stringify(value);
        return this.executeSql(`INSERT OR REPLACE INTO ${this.storeName} (key, value) VALUES (?, ?)`, [key, jsonValue]);
    }

    async setAll(items: any[], getKey: (item: any) => string, getValue: (item: any) => any): Promise<any> {
        return this.database.transaction((tx) => {
            const cmd = `INSERT OR REPLACE INTO ${this.storeName} (key, value) VALUES (?, ?)`;
            for (const item of items) {
                tx.executeSql(cmd, [getKey(item), JSON.stringify(getValue(item))]);
            }
        }).then(() => logger.debug(`Transaction completed. Saved ${items.length} items.`));
    }

    async remove(key: string): Promise<any> {
        return this.executeSql(`DELETE FROM ${this.storeName} WHERE key = ?`, [key]);
    }

    async clear(): Promise<void> {
        return this.executeSql(`DELETE FROM ${this.storeName}`);
    }

    async length(): Promise<number> {
        const result = await this.executeSql(`SELECT COUNT(*) as size FROM ${this.storeName}`);
        return result?.rows?.item(0)?.size;
    }

    async keys(): Promise<string[]> {
        const result = await this.executeSql(`SELECT key FROM ${this.storeName}`);
        const keys = [];
        for (let i = 0; i < result.rows.length; i++) {
            keys.push(result.rows.item(i).key);
        }
        return keys;
    }

    async forEach(iteratorCallback: (value: any, key: string, iterationNumber: number) => any): Promise<void> {
        const result = await this.executeSql(`SELECT * FROM ${this.storeName}`);
        for (let i = 0; i < result.rows.length; i++) {
            const item = result.rows.item(i);
            const key = item.key;
            const value = this.jsonParse(item.value);

            iteratorCallback(value, key, i);
        }
    }

    private async initializeDatabase(): Promise<void> {
        try {
            this.database = await this.sqlite.create(this.databaseConfig);
            await this.createTable();
        } catch (error) {
            logger.error(error, `Error on open or create database ${this.databaseConfig.name}`);
            throw error;
        }
    }

    private async createTable(): Promise<any> {
        return this.executeSql('CREATE TABLE IF NOT EXISTS ' + this.storeName +
            ' (id INTEGER PRIMARY KEY, key unique, value)');
    }

    private async executeSql(statement: string, params?: any[]): Promise<any> {
        return this.database.executeSql(statement, params || []);
    }

    private async getSingleOrDefault(statement: string, params?: any[]): Promise<any> {
        const results = await this.executeSql(statement, params);
        return results?.rows?.length ? results.rows.item(0).value : null;
    }

    private normalizeKey(key: string) {
        if (typeof key !== 'string') {
            logger.debug(`${key} used as a key, but it is not a string.`);
            key = String(key);
        }
        return key;
    }

    private jsonParse(value: any): any {
        return value ? JSON.parse(value) : value;
    }

}
