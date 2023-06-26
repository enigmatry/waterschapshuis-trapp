import { ConnectionStatus } from './connection-status.enum';

export interface NetworkServiceResult {
    status: ConnectionStatus;
    simulateOffline: boolean;
}
