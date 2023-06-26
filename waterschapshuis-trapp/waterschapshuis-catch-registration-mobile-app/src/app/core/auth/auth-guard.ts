import { CurrentUserProviderService } from './../../shared/services/current-user-provider.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private currentUserProviderService: CurrentUserProviderService, private router: Router) { }

  canActivate = async (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> => {
    // handle situtation where we have access token but user was not loaded
    const canActivate = await this.authService.hasAccessToken() && !!this.currentUserProviderService.currentUser;
    if (!canActivate) {
      await this.router.navigate(['login']);
    }
    return canActivate;
  }
}
