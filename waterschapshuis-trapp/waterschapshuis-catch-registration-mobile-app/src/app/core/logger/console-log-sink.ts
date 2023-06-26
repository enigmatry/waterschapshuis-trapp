import { LogLevel } from './log-level.enum';
import { Injectable } from '@angular/core';
import { ILogSink } from './log-sink';
import { environment } from 'src/environments/environment';

@Injectable()
export class ConsoleLogSink implements ILogSink {

    readonly minimumLogLevel = environment.logger.consoleLogLevel;

    log(source: string, level: LogLevel, message: any, params: any[]): void {
        console.log.call(console, `[${source}]`, message, ...params);
    }

    error(source: string, error: Error, params: any[]) {
        console.error.call(console, `[${source}]`, ...params, error);
    }
}
