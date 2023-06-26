export interface ISyncable {
  syncObjectName: string;
  getObjectsToSync(): Promise<Array<any>>;
  syncData(items: Array<any>): Promise<void>;
  postSync?(items: Array<any>): Promise<void>;
  countDataToSync(): Promise<number>;
}
