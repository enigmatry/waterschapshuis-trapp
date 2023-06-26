import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { NgxPermissionsService } from 'ngx-permissions';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';

@Injectable({
  providedIn: 'root'
})
export class TimeRegistrationsManagementPermissionGuard implements CanActivate {
  constructor(private permissionsService: NgxPermissionsService, private router: Router) {}

  canActivate = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree =>
      this.permissionsService
        .hasPermission(PolicyName.TimeRegistrationManagementReadWrite)
        .then(hasPermission => {
          if (hasPermission) {
            return true;
          } else {
            return this.router.parseUrl('/error/403');
          }
        })
}
