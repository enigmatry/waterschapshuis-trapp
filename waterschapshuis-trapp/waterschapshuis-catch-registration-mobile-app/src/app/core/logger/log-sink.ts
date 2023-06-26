import { LogLevel } from './log-level.enum';

export interface ILogSink {
    readonly minimumLogLevel: LogLevel;
    log(source: string, level: LogLevel, message: any, params: any[]): void;
    error(source: string, error: Error, params: any[]): void;
}
