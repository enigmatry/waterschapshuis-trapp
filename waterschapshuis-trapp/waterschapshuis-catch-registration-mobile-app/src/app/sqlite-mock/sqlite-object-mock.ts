export class SQLiteObject {
  objectInstance: any;

  constructor(objectInstance: any) {
    this.objectInstance = objectInstance;
  }

  executeSql(statement: string, params: any): Promise<any> {

    return new Promise((resolve, reject) => {
      try {
        const st = this.objectInstance.prepare(statement, params);
        const rows: Array<any> = [];
        while (st.step()) {
          const row = st.getAsObject();
          rows.push(row);
        }
        const payload = {
          rows: {
            item: (i: number) => {
              return rows[i];
            },
            length: rows.length
          },
          rowsAffected: this.objectInstance.getRowsModified() || 0,
          insertId: this.objectInstance.insertId || void 0
        };

        const arr: ArrayBuffer = this.objectInstance.export();
        localStorage.setItem('waterschapshuis-dev-db', String(arr));

        resolve(payload);
      } catch (e) {
        reject(e);
      }
    });
  }
}
