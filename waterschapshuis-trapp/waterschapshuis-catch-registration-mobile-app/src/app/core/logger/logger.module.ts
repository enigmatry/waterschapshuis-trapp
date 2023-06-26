import { NgModule, InjectionToken, ModuleWithProviders, Inject } from '@angular/core';
import { ILogSink } from './log-sink';
import { ConsoleLogSink } from './console-log-sink';
import { AppInsightsLogSink } from './app-insights-log-sink';
import { Logger } from './logger';

export const LOG_SINKS = new InjectionToken<ILogSink>('LogSinks');

@NgModule({
  declarations: [],
  imports: []
})
export class LoggerModule {

  static forRoot(): ModuleWithProviders<LoggerModule> {
    return {
      ngModule: LoggerModule,
      providers: [
        { provide: LOG_SINKS, useClass: ConsoleLogSink, multi: true },
        { provide: LOG_SINKS, useClass: AppInsightsLogSink, multi: true }
      ]
    };
  }

  constructor(@Inject(LOG_SINKS) sinks: ILogSink[]) {
    Logger.registerLogSinks(sinks);
  }
}
