export interface QueuedRequest {
  id?: number;
  method: string;
  url: string;
  payload: any;
  timestamp: Date;
  retryAttempts?: number;
}
