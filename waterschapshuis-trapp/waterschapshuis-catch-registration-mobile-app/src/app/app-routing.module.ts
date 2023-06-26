import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { BackgroundLocationComponent } from './maps/background-location/background-location.component';
import { ErrorPageComponent } from './common/error-page/error-page.component';
import { LoginComponent } from './core/login/login.component';
import { AuthGuard } from './core/auth/auth-guard';
import { UserSummaryComponent } from './traps/user-summary/user-summary.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent},
  { path: 'home', canActivate: [AuthGuard], component: HomeComponent },
  { path: 'error/:type', component: ErrorPageComponent },
  { path: 'maps', canActivate: [AuthGuard], loadChildren: () => import('./maps/maps.module').then(m => m.MapsModule) },
  { path: 'tracking', canActivate: [AuthGuard], component: BackgroundLocationComponent },
  { path: 'traps', canActivate: [AuthGuard], loadChildren: () => import('./traps/traps.module').then(m => m.TrapsModule) },
  {
    path: 'time-registration', canActivate: [AuthGuard],
    loadChildren: () => import('./time-registration/time-registration.module').then(m => m.TimeRegistrationModule)
  },
  { path: 'user-summary', canActivate: [AuthGuard], component: UserSummaryComponent },
  { path: 'areas', loadChildren: () => import('./area/area.module').then(m => m.AreaModule)},
  { path: 'melding', loadChildren: () => import('./observations/observations.module').then(m => m.ObservationsModule) }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
