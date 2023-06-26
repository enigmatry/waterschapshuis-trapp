import { LogLevel } from './log-level.enum';
import { ILogSink } from './log-sink';

/**
 * Simple logger with the possibility of registering custom outputs.
 *
 * Example usage:
 *
 * import { Logger } from 'app/core/logger/logger';
 *
 * const logger = new Logger('myFile');
 *
 * logger.debug('message');
 */
export class Logger {

  private static sinks: ILogSink[] = [];

  public static registerLogSinks(sinks: ILogSink[] = []) {
    Logger.sinks = sinks;
  }

  constructor(private source: string) { }

  debug(message: any, ...params: any[]) {
    this.logMessage(LogLevel.Debug, message, params);
  }

  info(message: any, ...params: any[]) {
    this.logMessage(LogLevel.Info, message, params);
  }

  warn(message: any, ...params: any[]) {
    this.logMessage(LogLevel.Warning, message, params);
  }

  error(error: Error, ...params: any[]) {
    this.logError(LogLevel.Error, error, params);
  }

  private logMessage(level: LogLevel, message: any, params: any[]) {
    Logger.sinks.forEach(logSink => {
      if (level >= logSink.minimumLogLevel) {
        logSink.log(this.source, level, message, params);
      }
    });
  }

  private logError(level: LogLevel, error: Error, params: any[]) {
    Logger.sinks.forEach(logSink => {
      if (level >= logSink.minimumLogLevel) {
        logSink.error(this.source, error, params);
      }
    });
  }
}
