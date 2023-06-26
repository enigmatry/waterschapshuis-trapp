import { Injectable } from '@angular/core';
import { AppInsightsInitService } from 'src/app/core/application-insights/application-insights';
import { AuthService } from 'src/app/core/auth/auth.service';
import { CurrentUserService } from '../current-user.service';

export function initFactory(config: AppInitService) {
    return () => config.initializeApp();
}

@Injectable({
    providedIn: 'root'
})
export class AppInitService {
    constructor(
        private appInsightsService: AppInsightsInitService,
        private auth: AuthService,
        private currentUserService: CurrentUserService
    ) { }

    async initializeApp(): Promise<boolean> {
        await this.auth.handleAuthRedirect();
        await this.currentUserService.createUserFromIdentity();
        this.appInsightsService.init();

        return true;
    }
}
