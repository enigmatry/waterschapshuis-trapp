import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { MapMainComponent } from '../map/map-main/map-main.component';
import { LoginRedirectComponent } from '../shared/login-redirect/login-redirect.component';
import { HomeScreenComponent } from '../home/home-screen/home-screen.component';
import { ErrorPageComponent } from '../common/error-page/error-page.component';
import { AuthGuard } from '../core/auth/auth.guard';

export const routes: Routes = [
  {
    path: '', canActivate: [AuthGuard], redirectTo: '/home', pathMatch: 'full',
  },
  {
    path: 'login', component: LoginRedirectComponent, data: { title: 'Login' },
  },
  {
    path: 'error/:type', component: ErrorPageComponent, data: { title: 'Error' },
  },
  {
    path: 'map', canActivate: [AuthGuard], component: MapMainComponent, data: { title: 'Map' },
  },
  {
    path: 'home', canActivate: [AuthGuard], component: HomeScreenComponent, data: { title: 'Home' },
  },
  {
    path: 'settings', canActivate: [AuthGuard], loadChildren: () => import('../settings/settings.module').then(m => m.SettingsModule)
  },
  {
    path: 'reports',
    canActivate: [AuthGuard],
    data: { title: 'Rapportage' },
    loadChildren: () => import('../reports/reports.module').then(m => m.ReportsModule),
  },
  {
    path: 'time-registration',
    canActivate: [AuthGuard],
    loadChildren: () => import('../time-registration/time-registration.module').then(m => m.TimeRegistrationModule)
  },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules, scrollPositionRestoration: 'enabled'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
