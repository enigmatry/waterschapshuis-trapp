import { ApplicationInsights, DistributedTracingModes, ITelemetryItem } from '@microsoft/applicationinsights-web';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { CurrentUserProviderService } from 'src/app/shared/services/current-user-provider.service';

@Injectable({
  providedIn: 'root'
})
export class AppInsightsInitService {

  constructor(
    private appInsights: ApplicationInsights,
    private currentUserProviderService: CurrentUserProviderService) {
  }

  init() {
    if (this.appInsights) {
      this.appInsights.loadAppInsights();
      this.setAuthenticatedUserId();
      const context = this.appInsights.context;
      context.application.ver = environment.appVersion;

      this.appInsights.addTelemetryInitializer(this.currentRouteAsPageNameTelemetryInitializer());
      this.appInsights.trackPageView();
    }
  }

  private setAuthenticatedUserId() {
    this.currentUserProviderService.currentUser$.subscribe(currentUser => {
      if (currentUser) {
        this.appInsights.setAuthenticatedUserContext(currentUser.email, currentUser.id, true);
      }
    });
  }

  private currentRouteAsPageNameTelemetryInitializer() {
    return (envelope: ITelemetryItem) => {
      const currentRoute = envelope.ext?.trace?.name;

      if (envelope.baseType === 'PageviewData' || envelope.baseType === 'PageviewPerformanceData') {
        envelope.baseData.name = currentRoute;
      } else if (envelope.baseType === 'MetricData' && envelope.baseData.name === 'PageVisitTime') {
        envelope.data.PageName = currentRoute;
      }
    };
  }
}

export function AppInsightsFactory(): ApplicationInsights {
  if (environment.production) {
    return new ApplicationInsights({
      config: {
        instrumentationKey: environment.appInsights.instrumentationKey,
        enableAutoRouteTracking: true,
        autoTrackPageVisitTime: true,
        enableAjaxPerfTracking: true,
        maxAjaxCallsPerView: -1,
        distributedTracingMode: DistributedTracingModes.W3C
      }
    });
  } else {
    return null;
  }
}
