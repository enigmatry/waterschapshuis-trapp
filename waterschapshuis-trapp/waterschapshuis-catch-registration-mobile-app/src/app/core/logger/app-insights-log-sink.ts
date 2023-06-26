import { LogLevel } from './log-level.enum';
import { Injectable } from '@angular/core';
import { ApplicationInsights, SeverityLevel } from '@microsoft/applicationinsights-web';
import { ILogSink } from './log-sink';
import { environment } from 'src/environments/environment';

@Injectable()
export class AppInsightsLogSink implements ILogSink {

    readonly minimumLogLevel = environment.logger.appInsightsLogLevel;

    constructor(private appInsights: ApplicationInsights) { }

    log(source: string, level: LogLevel, message: any, params: any[]): void {
        if (this.appInsights) {
            this.trackTrace(source, level, message, params);
        }
    }

    error(source: string, error: Error, params: any[]) {
        if (this.appInsights) {
            this.trackException(source, error, params);
        }
    }

    private trackTrace(source: string, level: LogLevel, message: any, params: any[]) {
        this.appInsights.trackTrace({
            message: `[${source}] ${message} ${this.paramsToString(params)}`,
            severityLevel: this.getSeverity(level)
        });
    }

    private trackException(source: string, error: Error, params: any[]) {
        this.appInsights.trackException({
            exception: error,
            properties: { source, message: this.paramsToString(params) }
        });
    }

    private paramsToString(params: any[]): string {
        if (!params) { return null; }
        return params.join(' '); // or maybe JSON.stringify()?
    }

    private getSeverity(logLevel: LogLevel): SeverityLevel {
        switch (logLevel) {
            case LogLevel.Debug: return SeverityLevel.Verbose;
            case LogLevel.Info: return SeverityLevel.Information;
            case LogLevel.Warning: return SeverityLevel.Warning;
            case LogLevel.Error: return SeverityLevel.Error;
        }
    }
}
