import { ISqliteColumnData } from './sqlite-column-data';

export interface ISqliteTableData {
  name: string;
  columns: Array<ISqliteColumnData>;
  generateId: boolean;
}
