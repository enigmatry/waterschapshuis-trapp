import { Injectable } from '@angular/core';
import { ApplicationInsights, ITelemetryItem } from '@microsoft/applicationinsights-web';
import { CurrentUserService } from 'src/app/shared/current-user.service';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AppInsightsInitService {

  constructor(
    private appInsights: ApplicationInsights,
    private currentUserService: CurrentUserService) {
  }

  init(): void {
    if (this.appInsights) {
      this.appInsights.loadAppInsights();
      this.setAuthenticatedUserId();
      const context = this.appInsights.context;
      context.application.ver = environment.appVersion;

      this.appInsights.addTelemetryInitializer(this.currentRouteAsPageNameTelemetryInitializer());
      this.appInsights.trackPageView();
    }
  }

  private setAuthenticatedUserId(): void {
    this.currentUserService.currentUser$.subscribe(currentUser => {
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
        instrumentationKey: environment.applicationInsights.instrumentationKey,
        enableAutoRouteTracking: true,
        autoTrackPageVisitTime: true,
        enableAjaxPerfTracking: true,
        maxAjaxCallsPerView: -1
      }
    });
  } else {
    return null;
  }
}
